using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.Messaging;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UCommerce.Runtime;
using UCommerce.Search;
using UCommerce.Search.Facets;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class Catalog : System.Web.UI.UserControl
    {
        private ICollection<UCommerce.EntitiesV2.Product> _products = new List<UCommerce.EntitiesV2.Product>();
        IList<string> queryStringBlackList = new List<string> { "product", "category", "catalog", "aliaspath", "viewmode" };

        private readonly ICatalogContext _catalogContext = ObjectFactory.Instance.Resolve<ICatalogContext>();
        private readonly SearchLibraryInternal _searchLibraryInternal = ObjectFactory.Instance.Resolve<SearchLibraryInternal>();

        private void Page_Load(object sender, EventArgs e)
        {
            var currentCategory = _catalogContext.CurrentCategory;
            GetAllProductsRecursive(currentCategory);

            var facetsForQuerying = GetFacets();
            var filterProducts = currentCategory != null ? _searchLibraryInternal.GetProductsFor(currentCategory, facetsForQuerying) : new List<UCommerce.Documents.Product>();

            var listOfProducts = new List<UCommerce.EntitiesV2.Product>();

            if (filterProducts.Any())
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
            var facetsForQuerying = new List<Facet>();
            var parameters = GetQueryStringParametersForFacets();
            
            foreach (var parameter in parameters)
            {
                var facet = new Facet()
                {
                    Name = parameter.Key,
                    FacetValues = new List<FacetValue>()
                };
                
                foreach (var value in parameter.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    facet.FacetValues.Add(new FacetValue() { Value = value });
                }
                facetsForQuerying.Add(facet);
            }

            return facetsForQuerying;
        }

        private Dictionary<string,string> GetQueryStringParametersForFacets()
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
            return parameters;
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
