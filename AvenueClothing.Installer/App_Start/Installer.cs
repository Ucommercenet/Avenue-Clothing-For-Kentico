using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AvenueClothing.Installer.uCommerce.Install.Helpers;
using UCommerce.EntitiesV2;

namespace AvenueClothing.Installer.App_Start
{
    public class Installer: IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (new EventHandler(this.PreStart));
        }

        private static object _padLock = new object();
        private static bool _installationWasRun = false;
        public void PreStart(object sender, EventArgs e)
        {
			if (!_installationWasRun)
            {
                lock (_padLock)
                {
                    if (!_installationWasRun)
                    {
                        _installationWasRun = InstallInternal();
                    }
                }
            }
        }

        private static bool InstallInternal()
        {
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
