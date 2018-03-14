using CMS.DocumentEngine.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using UCommerce.Api;
using UCommerce.Search.Facets;
using System.Web;
using CMSApp.CMSWebParts.Ucommerce.DataSourceContext;
using DotNetOpenAuth.Messaging;
using UCommerce.Content;
using UCommerce.Infrastructure;

namespace CMSApp.CMSWebParts.Custom
{
    public partial class UCommercePromoProductsDataControl : CMSBaseDataSource
    {

        ICollection<Product> _products = new List<Product>();
        private string altProductSKUControl;
        //Query string parameters that shouldn't be used in facets. 
        IList<string> queryStringBlackList = new List<string>() { "product", "category", "catalog", "aliaspath", "viewmode" };

        public string AltProductSKUControl
        {
            get
            {
                return altProductSKUControl;
            }
            set
            {
                this.altProductSKUControl = value;

            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        

        protected override object GetDataSource(int offset, int maxRecords)
        {
            //var data = base.GetDataSource(offset, maxRecords) as List<UCommerceProduct>;
            var data = GetDataSourceFromDB() as List<UcommerceProductDto>;

			UcommerceContext.SetProducts(data);

            return data.ToDataSet();
        }


        protected override object GetDataSourceFromDB()
        {
            _products = SiteContext.Current.CatalogContext.CurrentCatalog.Categories
                .SelectMany(c => c.Products.Where(p => p.ProductProperties.Any(pp => pp.ProductDefinitionField.Name == "ShowOnHomepage" && pp.Value.Equals(true.ToString(), StringComparison.CurrentCultureIgnoreCase)))).ToList();

            if (string.IsNullOrEmpty(AltProductSKUControl) == false) {
                var productRepository = ObjectFactory.Instance.Resolve<IRepository<Product>>();
                var altProduct = productRepository.SingleOrDefault(p => p.Sku == AltProductSKUControl);
                _products = ReplaceAltProduct(_products, altProduct);
            }

            return _products
                .Select(p => ConvertProductToUcommerceProductDto(p))
                .ToList();
        }    

        private ICollection<Product> ReplaceAltProduct(ICollection<Product> products, Product altProduct)
        {
            var productList = new List<Product>(products);
            if (altProduct != null)
            {
                productList[0] = altProduct;
            }

            return productList;
        }

        private UcommerceProductDto ConvertProductToUcommerceProductDto(Product product)
        {
            var imageService = ObjectFactory.Instance.Resolve<IImageService>();

            var url = CatalogLibrary.GetNiceUrlForProduct(product);
            var price = CatalogLibrary.CalculatePrice(product);

            var ucommerceProduct = new UcommerceProductDto
            {
                ProductName = product.Name,
                ProductSku = product.Sku,
                ProductUrl = url,
                Price = "-",
                Tax = "-"
            };

            if (price.YourPrice != null)
            {
                ucommerceProduct.Price = price.YourPrice.Amount.ToString();
                ucommerceProduct.Tax = price.YourTax.ToString();
            }

            if (string.IsNullOrWhiteSpace(product.PrimaryImageMediaId) == false)
            {
                ucommerceProduct.ImageUrl = imageService.GetImage(product.PrimaryImageMediaId).Url;
            }

            return ucommerceProduct;
        }

        public IList<Facet> GetFacets()
        {
            var parameters = new Dictionary<string, string>();
            foreach (var queryString in HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (queryString == null || queryStringBlackList.Contains(queryString))
                {
                    continue;
                }

                parameters[queryString] = HttpContext.Current.Request.QueryString[queryString];
            }
           
            var facetsForQuerying = new List<Facet>();

            foreach (var parameter in parameters)
            {
                var facet = new Facet();
                facet.FacetValues = new List<FacetValue>();

                facet.Name = parameter.Key;
                foreach (var value in parameter.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
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
    }
}