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
            var data = base.GetDataSource(offset, maxRecords) as List<UCommerceProduct>;

            UCommerceContext.SetProducts(data);

            return data.ToDataSet();
        }


        protected override object GetDataSourceFromDB()
        {
            CurrentCategory = SiteContext.Current.CatalogContext.CurrentCategory;
            var data = new List<UCommerceProduct>();

            _products = Product.All()
            .Where(x => x.ProductProperties.Any(
                pp => pp.ProductDefinitionField.Name == "ShowOnHomepage" && pp.Value == "true")).ToList();

            Product altProduct = null; ;
            try
            {
                altProduct = CatalogLibrary.GetProduct(AltProductSKUControl);
            }
            catch
            {
                // Checking whether the given SKU value is correct
            }

            data = convertToUcommerceProduct(_products, altProduct);     
            
            return data;
        }    

        public Category CurrentCategory
        {
            get { return SiteContext.Current.CatalogContext.CurrentCategory; }
            set { }
        }

        private List<UCommerceProduct> convertToUcommerceProduct(ICollection<Product> _products, Product altProduct)
        {
            var data = new List<UCommerceProduct>();

            bool firstProduct = true;
            foreach (Product product in _products)
            {
                if (firstProduct && altProduct != null)
                {
                    addProductToList(data, altProduct);
                }
                else
                {
                    addProductToList(data, product);
                }
                firstProduct = false;
            }
            return data;
        }

        private void addProductToList(List<UCommerceProduct> data, Product product)
        {
            string url;
            PriceCalculation price;
            string imageUrl;

            url = CatalogLibrary.GetNiceUrlForProduct(product);
            price = CatalogLibrary.CalculatePrice(product);

            if (!string.IsNullOrWhiteSpace(product.ThumbnailImageMediaId))
            {
                var imageService = new ImageService();
                var image = imageService.GetImage(product.ThumbnailImageMediaId);

                imageUrl = image.Url;
                data.Add(new UCommerceProduct { ProductName = product.Name, ProductSKU = product.Sku, Price = price.YourPrice.Amount.ToString(), ProductUrl = url, ImageUrl = imageUrl, Tax = price.YourTax.ToString() });
            }
            else
            {
                data.Add(new UCommerceProduct { ProductName = product.Name, ProductSKU = product.Sku, Price = price.YourPrice.Amount.ToString(), ProductUrl = url, Tax = price.YourTax.ToString() });
            }
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