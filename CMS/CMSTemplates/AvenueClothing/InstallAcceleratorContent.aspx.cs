using System;
using System.Linq;
using AvenueClothing.Installer.uCommerce.Install.Helpers;
using CMS.UIControls;
using UCommerce.EntitiesV2;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class InstallAcceleratorContent : TemplatePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InstallerLog.InnerHtml = "<p> Installer will now attempt to insert data. </p> ";
            InstallInternal();
        }

        private void InstallInternal()
        {
            if (ProductCatalogGroup.Exists(x => x.Name == "avenue-clothing.com"))
            {
                InstallerLog.InnerHtml += "<p> A CatalogGroup called <b> avenue-clothing.com </b> already exists! </p>";
                InstallerLog.InnerHtml += "<p> Installer will not run! </p>";
                return;
            }

            InstallerLog.InnerHtml += "<p>Installing Currencies, Price Groups, Order Number series, Countries, Payment methods, Shipping methods, Data types, Product Definitons.</p>";
            InstallerLog.InnerHtml += "<p> Configuring e-mail profile and e-mail";

            var installer = new ConfigurationInstaller();
            installer.Configure();

            // Install Demo store Catalog
            InstallerLog.InnerHtml += "<p> Installing catalog structure: Catalog Group, Catalog, Categories and products. </p>";
            var installer2 = new CatalogueInstaller("avenue-clothing.com", "Demo Store");
            installer2.Configure();

            InstallerLog.InnerHtml += "<p> Installing media library and product/category media files. </p>";
            //CreateMediaContent();
            var mediaInstaller = new MediaInstaller();
            mediaInstaller.Configure();

            InstallerLog.InnerHtml += "<p> Deleting old Ucommerce data that is by default installed with Ucommerce. </p>";
            DeleteOldUCommerceData();

            InstallerLog.InnerHtml += "<p> <b> Installation successfully finished! Enjoy Avenue Clothing! </b></p>";
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
    }
}