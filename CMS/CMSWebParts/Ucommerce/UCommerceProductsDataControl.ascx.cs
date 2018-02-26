using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.DocumentEngine.Web.UI;
using CMS.Helpers;
using CMSApp.Old_App_Code.Custom;
using DotNetOpenAuth.Messaging;
using ServiceStack.Common.Extensions;
using UCommerce.Api;
using UCommerce.Content;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Infrastructure;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;
using UCommerce.Search.Facets;

namespace CMSApp.CMSWebParts.Ucommerce
{
    public partial class UcommerceProductsDataControl : CMSBaseDataSource
    {
        //Query string parameters that shouldn't be used in facets. 
        IList<string> queryStringBlackList = new List<string>() { "product", "category", "catalog", "aliaspath", "viewmode" };
        ICollection<Product> _products = new List<Product>();

        protected override object GetDataSource(int offset, int maxRecords)
        {
            var data = GetDataSourceFromDB() as List<UCommerceProduct>;

            UCommerceContext.SetProducts(data);

            return data.ToDataSet();
        }

        protected override object GetDataSourceFromDB()
        {
            var currentCategory = SiteContext.Current.CatalogContext.CurrentCategory;
            var ucommerceProducts = new List<UCommerceProduct>();

            if (currentCategory == null)
            {
                NoProductsLabel.Text = "There are no products to display. Try selecting a category or adding some products to it.";
            }
            else
            {
                ucommerceProducts = CacheHelper.Cache(cs =>
                    LoadProducts(cs, currentCategory),
                    new CacheSettings(0, "ucommerceProducts|categoryId=" + currentCategory.Id)
               );
            }

            return ucommerceProducts;
        }

        // Method that loads the required data
        // Called only if the data doesn't already exist in the cache
        private List<UCommerceProduct> LoadProducts(CacheSettings cs, Category category)
        {
            // Loads all products from this category from the database
            if (category == null)
                return ConvertToUcommerceProduct(_products);

            GetAllProductsRecursive(category);

            var facetsForQuerying = GetFacets();
            var filterProducts = SearchLibrary.GetProductsFor(category, facetsForQuerying);
            var listOfProducts = new List<Product>();

            SetCacheDependency(cs, category);

            if (!filterProducts.Any())
            {
                return ConvertToUcommerceProduct(_products);
            }

            foreach (var product in filterProducts)
            {
                Product filterProduct = _products.FirstOrDefault(x => x.Sku == product.Sku && x.VariantSku == product.VariantSku);
                if (filterProduct != null)
                {
                    listOfProducts.Add(filterProduct);
                }
            }

            return ConvertToUcommerceProduct(listOfProducts);
        }

        public Category CurrentCategory
        {
            get { return SiteContext.Current.CatalogContext.CurrentCategory; }
            set { }
        }

        public List<UCommerceProduct> ConvertToUcommerceProduct(ICollection<Product> _products)
        {
            var data = new List<UCommerceProduct>();
            var imageService = ObjectFactory.Instance.Resolve<IImageService>();
            foreach (Product product in _products)
            {
                var url = CatalogLibrary.GetNiceUrlForProduct(product, SiteContext.Current.CatalogContext.CurrentCategory, SiteContext.Current.CatalogContext.CurrentCatalog);
                var price = CatalogLibrary.CalculatePrice(product);

                var ucommerceProduct = new UCommerceProduct
                {
                    ProductName = product.DisplayName(),
                    ProductSKU = product.Sku,
                    ProductUrl = url,
                    Price = "-",
                    Tax = "-"
                };

                if (price.YourPrice != null) ucommerceProduct.Price = price.YourPrice.Amount.ToString();
                if (price.YourTax != null) ucommerceProduct.Tax = price.YourTax.ToString();

                data.Add(ucommerceProduct);

                if (!string.IsNullOrWhiteSpace(product.PrimaryImageMediaId))
                {
                    ucommerceProduct.ImageUrl = imageService.GetImage(product.PrimaryImageMediaId).Url;
                }
            }

            return data;
        }

        public IList<Facet> GetFacets()
        {
            var facetsForQuerying = new List<Facet>();
            var queryStringParameter = GetQueryStringParametersForFacets();

            foreach (var queryString in queryStringParameter)
            {
                var facet = new Facet()
                {
                    Name = queryString.Key,
                    FacetValues = new List<FacetValue>()
                };

                foreach (var value in queryString.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    facet.FacetValues.Add(new FacetValue() { Value = value });
                }

                facetsForQuerying.Add(facet);
            }

            return facetsForQuerying;
        }

        public void GetAllProductsRecursive(Category currentCategory)
        {
            _products.AddRange(CatalogLibrary.GetProducts(currentCategory));

            foreach (var childCategory in currentCategory.Categories)
            {
                GetAllProductsRecursive(childCategory);
            }
        }

        private static void SetCacheDependency(CacheSettings cs, Category category)
        {
            // Checks whether the data should be cached (based on the CacheSettings)
            if (cs.Cached)
            {
                // Sets a cache dependency for the data
                // The data is removed from the cache if the objects represented by the dummy key are modified (all objects in this case)
                cs.CacheDependency = CacheHelper.GetCacheDependency("ucommerceProducts|categoryId=" + category.Id);
            }
        }

        private Dictionary<string, string> GetQueryStringParametersForFacets()
        {
            var parameters = new Dictionary<string, string>();
            foreach (var queryString in HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (queryString == null || queryStringBlackList.Contains(queryString)) continue;

                parameters[queryString] = HttpContext.Current.Request.QueryString[queryString];
            }
            return parameters;
        }
    }
}