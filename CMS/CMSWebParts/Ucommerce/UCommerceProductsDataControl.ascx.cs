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
    public partial class UCommerceProductsDataControl : CMSBaseDataSource
    {

        ICollection<Product> _products = new List<Product>();

        private string dataFilteringControl;
        public string DataFilteringControl
        {
            get
            {
                return dataFilteringControl;
            }
            set
            {
                this.dataFilteringControl = value;

            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var something = dataFilteringControl;
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

            if (DataFilteringControl == "2")
            {
                GetAllProductsRecursive(CurrentCategory);
                var facetsForQuerying = GetFacets();
                var filterProducts = CurrentCategory != null ? SearchLibrary.GetProductsFor(CurrentCategory, facetsForQuerying) : new List<UCommerce.Documents.Product>();
                var listOfProducts = new List<Product>();
              
                if (filterProducts.Count == 0)
                {
                    data = convertToUcommerceProduct(_products);
                }
                else
                {
                    foreach (var product in filterProducts)
                    {
                        listOfProducts.Add(
                            _products.First(x => x.Sku == product.Sku && x.VariantSku == product.VariantSku));
                    }
                   
                    data = convertToUcommerceProduct(listOfProducts);
                }
            }

            else if (DataFilteringControl == "1")
            {               
                _products = Product.All()
                    .Where(x => x.ProductProperties.Any(
                        pp => pp.ProductDefinitionField.Name == "ShowOnHomepage" && pp.Value == "true")).ToList();               

                data = convertToUcommerceProduct(_products);
            }

            return data;
        }       

        public Category CurrentCategory
        {
            get { return SiteContext.Current.CatalogContext.CurrentCategory; }
            set { }
        }

        public List<UCommerceProduct> convertToUcommerceProduct(ICollection<Product> _products)
        {
            string url;
            PriceCalculation price;
            string imageUrl;

            var data = new List<UCommerceProduct>();

            foreach (Product product in _products)
            {
                url = CatalogLibrary.GetNiceUrlForProduct(product);
                price = CatalogLibrary.CalculatePrice(product);

                if (!string.IsNullOrWhiteSpace(product.ThumbnailImageMediaId))
                {
                    var imageService = new ImageService();
                    var image = imageService.GetImage(product.ThumbnailImageMediaId);

                    imageUrl = image.Url;
                    data.Add(new UCommerceProduct { ProductName = product.Name, ProductSKU = product.Sku, Price = price.YourPrice.Amount.ToString(), ProductUrl = url, ImageUrl = imageUrl });
                }
                else
                {
                    data.Add(new UCommerceProduct { ProductName = product.Name, ProductSKU = product.Sku, Price = price.YourPrice.Amount.ToString(), ProductUrl = url });
                }
            }

            return data;
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