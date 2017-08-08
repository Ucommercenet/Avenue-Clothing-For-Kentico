using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.PortalEngine;
using CMS.UIControls;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;
using UCommerce.Search.Facets;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Catalog : TemplatePage
    {
        public Category CurrentCategory
        {
            get { return SiteContext.Current.CatalogContext.CurrentCategory; }
            set { }
        }

        private ICollection<Product> _products = new List<Product>();

        private void Page_Load(object sender, EventArgs e)
        {
            var viewMode = Convert.ToInt32(Request.QueryString["viewmode"]);

            if (viewMode == 6 || viewMode == 3)
            {
                return;
            }

            CurrentCategory = SiteContext.Current.CatalogContext.CurrentCategory;

                var imageService = new ImageService();

                CategoryImage.ImageUrl = imageService.GetImage(CurrentCategory.ImageMediaId).Url;

                GetAllProductsRecursive(CurrentCategory);

                var facetsForQuerying = GetFacets();
                var filterProducts = CurrentCategory != null
                    ? SearchLibrary.GetProductsFor(CurrentCategory, facetsForQuerying)
                    : new List<UCommerce.Documents.Product>();

                var listOfProducts = new List<Product>();

                if (filterProducts.Count == 0)
                {
                    lvProducts.DataSource = _products;
                }
                else
                {
                    foreach (var product in filterProducts)
                    {
                        listOfProducts.Add(
                            _products.First(x => x.Sku == product.Sku && x.VariantSku == product.VariantSku));
                    }

                    lvProducts.DataSource = listOfProducts;
                }

                lvProducts.DataBind();
            
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