using UCommerce.EntitiesV2;

namespace CMSApp.CMSWebParts.Ucommerce.Services
{
    /// <summary>
    /// Component to be used for testing purposes, for when a test Ucommerce CatalogContext needs to be set up.
    /// </summary>
    public interface IDefaultCatalogDataProvider
    {
        /// <summary>
        /// Method to get the default Catalog. Used for rendering test data, for example when using the "Live Site" button.
        /// </summary>
        /// <returns>The first Catalog from the current Catalog Group (Store) </returns>
        ProductCatalog GetDefaultProductCatalog();

        /// <summary>
        /// Method to get the default Category. Used for rendering test data, for example when using the "Live Site" button.
        /// </summary>
        /// <returns>The first Category from the current Catalog Group (Store) </returns>
        Category GetDefaultCategory();

        /// <summary>
        /// Method to get the default Product. Used for rendering test data, for example when using the "Live Site" button for a product page.
        /// </summary>
        /// <returns>The first Product from the current Catalog Group (Store) </returns>
        Product GetDefaultProduct();
    }
}
