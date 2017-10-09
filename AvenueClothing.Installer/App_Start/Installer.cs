using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using AvenueClothing.Installer.uCommerce.Install.Helpers;
using CMS.Modules;
using CMS.SiteProvider;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Installer.Extensions;
using CMS.DataEngine;
using CMS.MediaLibrary;

namespace AvenueClothing.Installer.App_Start
{
    public class Installer: IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (new EventHandler(this.PreStart));
        }

        private static object _padLock = new object();
        private static bool _hasStarted = false;
        public void PreStart(object sender, EventArgs e)
        {
            if (!_hasStarted)
            {
                lock (_padLock)
                {
                    if (!_hasStarted && isPendingInstallation())
                    {
                        _hasStarted = InstallInternal();
                    }
                    else
                    {
                        _hasStarted = true;
                    }
                }
            }
        }

        private bool isPendingInstallation()
        {
            // * If there are multiple sites, we assmue the installation has already run.
            // * If installation has completed before, and an iisreset is run, the installation will not know if it
            //   ran previous. If there's only ONE site (avenue clothing), and it was turned off on purpose before
            //   the iisreset, the installation is going to start the site again.
            var hasOnlyOneSite = SiteInfoProvider.GetSites().Count() == 1;
            var thereAreNoARunningSite = SiteContext.CurrentSite == null;

            return thereAreNoARunningSite && hasOnlyOneSite;
        }

        private static bool InstallInternal()
        {
            // Get the avenueClothing site by it's GUID.
            var avenueClothingSiteInfoByGuid = SiteInfoProvider.GetSiteInfoByGUID(Guid.Parse("f7e02dbb-5b44-4d3b-ab21-67913faca0b5"));

            StopAllExistingKenticoSites();
            
            if (avenueClothingSiteInfoByGuid != null)
            {
                // Start up the site, which is by default stopped when restoring with Continuous Integration
                SiteInfoProvider.RunSite(avenueClothingSiteInfoByGuid.SiteName);
                
                SettingsKeyInfoProvider.SetValue("CMSFriendlyURLExtension", SiteContext.CurrentSiteName, "");

                // Switch sitecontext to AvenueClothing as current site.
                SiteContext.CurrentSite = avenueClothingSiteInfoByGuid;

                var modules = ResourceInfoProvider.GetResources();
                foreach (ResourceInfo module in modules)
                {
                    ResourceSiteInfoProvider.AddResourceToSite(module.ResourceID, avenueClothingSiteInfoByGuid.SiteID);
                }
            }

            // Modify URL rewrites to match virtual application URL.
            UpdateUrlRewriteRulesWithCurrentVirtualApplicationName();
            var installer = new ConfigurationInstaller();
            installer.Configure();

            // Install Demo store Catalog
            var installer2 = new CatalogueInstaller("avenue-clothing.com", "Demo Store");
            installer2.Configure();

            //CreateMediaContent();
            var mediaInstaller = new MediaInstaller();
            mediaInstaller.Configure();

            DeleteOldUCommerceData();
            
            return true;
        }

        private static void UpdateUrlRewriteRulesWithCurrentVirtualApplicationName()
        {
            XmlDocument webConfig = new XmlDocument();
            var webConfigPath = HttpContext.Current.Request.PhysicalApplicationPath + "../Web.config";

            webConfig.Load(webConfigPath);

            string virtualApplicationName = HttpContext.Current.Request.ApplicationPath.Replace("/", String.Empty);
            
            for (int i = 1; i <= 4; i++)
            {
                XmlNode node =
                    webConfig.SelectSingleNode(String.Format("configuration/system.webServer/rewrite/rules/rule[{0}]/action", i));
                var oldVirtualApplicationName = node.Attributes["url"].Value.Split('/')[1];

                if (node != null)
                {
                    node.Attributes["url"].Value = node.Attributes["url"].Value.Replace("/" + oldVirtualApplicationName + "/", "/" + virtualApplicationName + "/");
                }
            }

            webConfig.Save(webConfigPath);
        }

        private static void StopAllExistingKenticoSites()
        {
            foreach (var site in SiteInfoProvider.GetSites())
            {
                SiteInfoProvider.StopSite(site.SiteName);
            }
        }

        private static void DeleteOldUCommerceData()
        {
            var group = ProductCatalogGroup.SingleOrDefault(g => g.Name == "uCommerce.dk");
            if (group != null)
            {
                // Delete products in group
                foreach (
                    var relation in
                        CategoryProductRelation.All()
                            .Where(x => group.ProductCatalogs.Contains(x.Category.ProductCatalog))
                            .ToList())
                {
                    var category = relation.Category;
                    var product = relation.Product;
                    category.RemoveProduct(product);
                    product.Delete();
                }

                // Delete catalogs
                foreach (var catalog in group.ProductCatalogs)
                {
                    catalog.Deleted = true;
                }

                // Delete group itself
                group.Deleted = true;
                group.Save();
            }
        }

        public void Dispose()
        {
            
        }
    }
}
