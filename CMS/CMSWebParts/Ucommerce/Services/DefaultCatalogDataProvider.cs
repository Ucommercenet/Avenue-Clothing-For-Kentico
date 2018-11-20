using System.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure.Components.Windsor;
using UCommerce.Runtime;

namespace CMSApp.CMSWebParts.Ucommerce.Services
{
    /// <inheritdoc cref="IDefaultCatalogDataProvider"/>
    public class DefaultCatalogDataProvider : IDefaultCatalogDataProvider
    {
        [Mandatory]
        public ICatalogContext CatalogContext { get; set; }

        /// <summary>
        /// Default implementation. Returns first catalog found on the Catalog Group where DisplayOnWebsite = true.
        /// </summary>
        /// <returns></returns>
        public virtual ProductCatalog GetDefaultProductCatalog()
        {
            var currentCatalogGroup = CatalogContext.CurrentCatalogGroup;

            return currentCatalogGroup.ProductCatalogs.First(x => x.DisplayOnWebSite);
        }

        /// <summary>
        /// Default implementation. Returns first category from the first catalog that contains products and DisplayOnSite = true.
        /// </summary>
        /// <returns></returns>
        public virtual Category GetDefaultCategory()
        {
            var currentCatalogGroup = CatalogContext.CurrentCatalogGroup;
            var defaultCatalog = currentCatalogGroup.ProductCatalogs.First();

            return defaultCatalog.Categories.FirstOrDefault(x => x.DisplayOnSite && !x.Deleted && x.Products.Any());
        }

        /// <summary>
        /// Default implementation. Returns first product from the default category where DisplayOnSite = true;
        /// </summary>
        /// <returns></returns>
        public virtual Product GetDefaultProduct()
        {
            return GetDefaultCategory().Products.FirstOrDefault(x => x.DisplayOnSite);
        }
    }
}