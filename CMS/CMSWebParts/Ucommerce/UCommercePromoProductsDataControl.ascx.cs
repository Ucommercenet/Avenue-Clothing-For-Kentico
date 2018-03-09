using CMS.DataEngine;
using CMS.DocumentEngine.Web.UI;
using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Data;
using CMSApp.Old_App_Code.Custom;
using System.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using UCommerce.Api;
using UCommerce.Search.Facets;
using System.Web;
using UCommerce.Kentico.Content;
using CMS.Helpers;
using UCommerce.Content;
using UCommerce.Infrastructure;

namespace CMSApp.CMSWebParts.Custom
{
    public partial class UCommercePromoProductsDataControl : CMSBaseDataSource
    {

        ICollection<Product> _products = new List<Product>();
        private string altProductSKUControl;
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
            var data = GetDataSourceFromDB() as List<UCommerceProduct>;

			UCommerceContext.SetProducts(data);

            return data.ToDataSet();
        }


        protected override object GetDataSourceFromDB()
        {
            CurrentCategory = SiteContext.Current.CatalogContext.CurrentCategory;

            _products = SiteContext.Current.CatalogContext.CurrentCatalog.Categories
                .SelectMany(c => c.Products.Where(p => p.ProductProperties.Any(pp => pp.ProductDefinitionField.Name == "ShowOnHomepage" && Convert.ToBoolean(pp.Value)))).ToList();

            if (string.IsNullOrEmpty(AltProductSKUControl) == false) {
                var productRepository = ObjectFactory.Instance.Resolve<IRepository<Product>>();
                var altProduct = productRepository.SingleOrDefault(p => p.Sku == AltProductSKUControl);
                _products = ReplaceAltProduct(_products, altProduct);
            }

            return _products
                .Select(p => ConvertProductToUcommerceProduct(p))
                .ToList();
        }    

        public Category CurrentCategory
        {
            get { return SiteContext.Current.CatalogContext.CurrentCategory; }
            set { }
        }

        private ICollection<Product> ReplaceAltProduct(ICollection<Product> products, Product altProduct)
        {
            var productList = new List<Product>(products);
            if (altProduct != null && productList.Any())
            {
                productList[0] = altProduct;
            }

            return productList;
        }

        private UCommerceProduct ConvertProductToUcommerceProduct(Product product)
        {
            var imageService = ObjectFactory.Instance.Resolve<IImageService>();

            var url = CatalogLibrary.GetNiceUrlForProduct(product);
            var price = CatalogLibrary.CalculatePrice(product);

            var ucommerceProduct = new UCommerceProduct
            {
                ProductName = product.Name,
                ProductSKU = product.Sku,
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
                if (queryString == null) continue;

                parameters[queryString] = HttpContext.Current.Request.QueryString[queryString];
            }
            if (parameters.ContainsKey("product"))
            {
                parameters.Remove("product");
            }
            if (parameters.ContainsKey("category"))
            {
                parameters.Remove("category");
            }
            if (parameters.ContainsKey("catalog"))
            {
                parameters.Remove("catalog");
            }
             if (parameters.ContainsKey("aliaspath"))
            {
                parameters.Remove("aliaspath");
            }
            if (parameters.ContainsKey("viewmode"))
            {
                parameters.Remove("viewmode");
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
            foreach (var product in CatalogLibrary.GetProducts(currentCategory))
            {
                _products.Add(product);
            }

            foreach (var childCategory in currentCategory.Categories)
            {
                GetAllProductsRecursive(childCategory);
            }
        }
    }
}