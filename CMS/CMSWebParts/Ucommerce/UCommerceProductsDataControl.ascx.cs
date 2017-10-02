using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.DocumentEngine.Web.UI;
using CMS.Helpers;
using CMSApp.Old_App_Code.Custom;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;
using UCommerce.Search.Facets;

namespace CMSApp.CMSWebParts.Ucommerce
{
	public partial class UcommerceProductsDataControl : CMSBaseDataSource
	{

		ICollection<Product> _products = new List<Product>();
		Category _currentCategory;


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			//var filteringControl = dataFilteringControl;
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
			_currentCategory = SiteContext.Current.CatalogContext.CurrentCategory;
			var ucommerceProducts = new List<UCommerceProduct>();

			if (_currentCategory == null)
			{
				NoProductsLabel.Text = "There are no products to display. Try selecting a category or adding some products to it.";
			}
			else
			{
				ucommerceProducts = CacheHelper.Cache(cs => LoadProducts(cs, _currentCategory),
					new CacheSettings(0, "ucommerceProducts|categoryId=" + _currentCategory.Id));
			}
			return ucommerceProducts;
		}

		// Method that loads the required data
		// Called only if the data doesn't already exist in the cache
		private List<UCommerceProduct> LoadProducts(CacheSettings cs, Category category)
		{
			// Loads all products from this category from the database
			var data = new List<UCommerceProduct>();

			if (category != null)
				GetAllProductsRecursive(category);

			var facetsForQuerying = GetFacets();
			var filterProducts = category != null ? SearchLibrary.GetProductsFor(category, facetsForQuerying) : new List<UCommerce.Documents.Product>();
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

			// Checks whether the data should be cached (based on the CacheSettings)
			if (cs.Cached)
			{
				// Sets a cache dependency for the data
				// The data is removed from the cache if the objects represented by the dummy key are modified (all objects in this case)
				cs.CacheDependency = CacheHelper.GetCacheDependency("ucommerceProducts|categoryId=" + _currentCategory.Id);
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