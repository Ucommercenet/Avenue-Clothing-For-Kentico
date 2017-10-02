using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using UCommerce.Search.Facets;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class Catalog : System.Web.UI.UserControl
    {
        public Category CurrentCategory
        {
            get { return SiteContext.Current.CatalogContext.CurrentCategory; }
            set { }
        }

        private ICollection<UCommerce.EntitiesV2.Product> _products = new List<UCommerce.EntitiesV2.Product>();

        private void Page_Load(object sender, EventArgs e)
        {
            CurrentCategory = SiteContext.Current.CatalogContext.CurrentCategory;
            //string imageMediaId = CurrentCategory.ImageMediaId;
            //if (!string.IsNullOrWhiteSpace(imageMediaId))
            //{
                //ID mediaItem = Sitecore.Data.ID.Parse(imageMediaId);
                //imgCategory.MediaItem = Sitecore.Context.Database.GetItem(mediaItem);
            //}

            GetAllProductsRecursive(CurrentCategory);

            var facetsForQuerying = GetFacets();
            var filterProducts = CurrentCategory != null ? SearchLibrary.GetProductsFor(CurrentCategory, facetsForQuerying) : new List<UCommerce.Documents.Product>();

            var listOfProducts = new List<UCommerce.EntitiesV2.Product>();

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
