using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.UIControls;
using UCommerce.Runtime;
using UCommerce.EntitiesV2;
using UCommerce.Api;
using UCommerce.Kentico.Content;
using UCommerce.Pipelines;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class ProductPage : TemplatePage
    {
        private Dictionary<string, string> _parameters = new Dictionary<string, string>();
        private Product _currentVariant;

        private void Page_Load(object sender, EventArgs e)
        {
            var currentProduct = SiteContext.Current.CatalogContext.CurrentProduct;

            if(!string.IsNullOrWhiteSpace(currentProduct.ThumbnailImageMediaId))
            {
                var imageService = new ImageService();
                var image = imageService.GetImage(currentProduct.ThumbnailImageMediaId);

                imgTop.ImageUrl = image.Url;
            }

            foreach (var queryString in HttpContext.Current.Request.QueryString.AllKeys)
            {
                _parameters[queryString] = HttpContext.Current.Request.QueryString[queryString].ToLower();
            }

            litHeadline.Text = currentProduct.Name;


            var price = CatalogLibrary.CalculatePrice(currentProduct);

            litPrice.Text = price.YourPrice.Amount.ToString();
            litTax.Text = price.YourTax.ToString();
            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "UCommerce.DemoStore.productpage",
                "<script src=\"/scripts/UCommerce.DemoStore.productpage.js\"></script>");

            IEnumerable<IGrouping<ProductDefinitionField, ProductProperty>> uniqueVariants = from v in currentProduct.Variants.SelectMany(p => p.ProductProperties)
                where v.ProductDefinitionField.DisplayOnSite
                group v by v.ProductDefinitionField into g
                select g;


            var currentProductVariant = "42";

            foreach (var queryString in HttpContext.Current.Request.QueryString.AllKeys)
            {
                _parameters[queryString] = HttpContext.Current.Request.QueryString[queryString];
            }


            if (_parameters.Any())
            {
                Product foundProduct = null;
                foreach (var variant in currentProduct.Variants)
                {
                    foreach (var property in variant.ProductProperties)
                    {
                        if (property.Value != _parameters[property.GetDefinitionField().Name])
                        {
                            foundProduct = null;
                            break;
                        }
                        foundProduct = variant;
                    }

                    if (foundProduct != null)
                    {
                        //var basket = TransactionLibrary.GetBasket(true).PurchaseOrder;
                        //Currency currency = basket.BillingCurrency;
                        //var newPrice = new Money(foundProduct.PriceGroupPrices.First().Price.Value, currency);
                        //var tax = new Money(foundProduct.PriceGroupPrices.First().Price.Value, currency);
                        var newPrice = CatalogLibrary.CalculatePrice(foundProduct);

                        litPrice.Text = newPrice.YourPrice.Amount.ToString();
                        litTax.Text = newPrice.YourTax.ToString();
                        break;
                    }
                }
            }

            productSku.Text = currentProduct.Sku;

            //TODO: We had some problems with this method, so its just hardcoded to use danish.
            //var productDescription = currentProduct.GetDescription(SiteContext.Current.CurrentCulture.ToString());
            var productDescription = currentProduct.GetDescription("da");

            litDescription.Text = productDescription.LongDescription;
            litProductSmallDesc.Text = productDescription.ShortDescription;

            if (currentProduct.ProductReviews.Any())
            {
                litReviewHeadline.Text = "<h5>Latest Reviews</h5>";
                rptReviews.DataSource = currentProduct.ProductReviews;
                rptReviews.DataBind();
            }
            else
            {
                litReviewHeadline.Text = "<p>No-one has reviewed this product yet.</p>";
            }
            litProductReviews.Text = DisplayStars(currentProduct.Rating);

            rptVariant.DataSource = uniqueVariants;
            rptVariant.DataBind();

        }

        public void ReviewRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            ProductReview currentItem = (ProductReview)e.Item.DataItem;

            var litDisplayStars = (Literal)e.Item.FindControl("litDisplayStars");
            var litSpecificReviewHeadline = (Literal)e.Item.FindControl("litSpecificReviewHeadline");
            var litCustomerFirstName = (Literal)e.Item.FindControl("litCustomerFirstName");
            var litAbbr = (Literal)e.Item.FindControl("litAbbr");
            var litReviewText = (Literal)e.Item.FindControl("litReviewText");
            var metaDatePublished = (HtmlGenericControl)e.Item.FindControl("metaDatePublished");
            var metaReviewRating = (HtmlGenericControl)e.Item.FindControl("metaReviewRating");
            var litComments = (Literal)e.Item.FindControl("litComments");

            string abbr = "<abbr title=\"" + currentItem.CreatedOn.ToString("u") + "\">"
                          + currentItem.CreatedOn.ToString("MMM dd, yyyy") + "</abbr>";
            litAbbr.Text = abbr;
            litDisplayStars.Text = DisplayStars(currentItem.Rating);
            litSpecificReviewHeadline.Text = currentItem.ReviewHeadline;
            litCustomerFirstName.Text = currentItem.Customer.FirstName;
            litReviewText.Text = currentItem.ReviewText;
            metaDatePublished.Attributes.Add("content", currentItem.CreatedOn.ToString("yyyy-MM-dd"));
            metaReviewRating.Attributes.Add("content", currentItem.Rating.ToString());
            litComments.Text = currentItem.Comments.ToString();
        }

        public string DisplayStars(int? rating)
        {
            string returnValue = "";
            if (rating.HasValue)
            {
                returnValue += "<span class=\"star-rating\">";
                for (int i = 20; i <= 100; i = i + 20)
                {
                    if (rating >= i)
                    {
                        returnValue += "<i class=\"icon-star\"></i>";
                    }
                    else
                    {
                        returnValue += "<i class=\"icon-start-empty\"></i>";
                    }
                }
            }

            returnValue += "</span>";
            return returnValue;
        }

        public string DisplayStars(double? rating)
        {
            string returnValue = "";
            if (rating.HasValue)
            {
                returnValue += "<span class=\"star-rating\">";
                for (int i = 20; i <= 100; i = i + 20)
                {
                    if (rating >= i)
                    {
                        returnValue += "<i class=\"icon-star\"></i>";
                    }
                    else
                    {
                        returnValue += "<i class=\"icon-start-empty\"></i>";
                    }
                }
            }

            returnValue += "</span>";
            return returnValue;
        }

        public void VariantRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            IGrouping<ProductDefinitionField, ProductProperty> currentItem = (IGrouping<ProductDefinitionField, ProductProperty>)e.Item.DataItem;

            var lbLabel = (Label)e.Item.FindControl("lbLabel");
            var litLabel = (Literal)e.Item.FindControl("litLabel");
            var litOptions = (Literal)e.Item.FindControl("litOptions");
            var litSelect = (Literal)e.Item.FindControl("litSelect");
            var controlName = string.Format("variation-{0}", currentItem.Key.Name.ToLower());

            lbLabel.Attributes.Add("for", controlName);
            litLabel.Text = currentItem.Key.GetDisplayName();

            litSelect.Text = "<select name=\"" + controlName + "\" class=\"variant\" id=\"" + controlName + "\">";
            string options = "";

            KeyValuePair<string, string> currentKVP = new KeyValuePair<string, string>();

            foreach (var query in _parameters)
            {
                if (query.Key.ToLower() == currentItem.Key.GetDisplayName().ToLower())
                {
                    currentKVP = query;
                }
            }
            foreach (var value in currentItem.Select(p => p.Value).Distinct())
            {
                if (currentKVP.Key != null && value.ToLower() == currentKVP.Value.ToLower())
                {
                    options += "<option selected value=\"" + value + "\">" + value + "</option>";
                    continue;
                }
                options += "<option value=\"" + value + "\">" + value + "</option>";
            }

            litOptions.Text = options;
        }

        public void btnAddToBasket_Click(object sender, EventArgs e)
        {
            var variant = GetVariantFromPostData(SiteContext.Current.CatalogContext.CurrentProduct, "variation-");

            if (variant == null)
            {
                return;
            }
            TransactionLibrary.AddToBasket(1, variant.Sku, variant.VariantSku);
            Response.Redirect(Request.RawUrl);
        }

        public static UCommerce.EntitiesV2.Product GetVariantFromPostData(UCommerce.EntitiesV2.Product product, string variantPrefix)
        {
            var request = HttpContext.Current.Request;
            var keys = request.Form.AllKeys.Where(k => k.StartsWith(variantPrefix, StringComparison.InvariantCultureIgnoreCase));
            var properties = keys.Select(k => new { Key = k.Replace(variantPrefix, string.Empty), Value = request.Form[k] }).ToList();

            UCommerce.EntitiesV2.Product variant = null;

            // If there are variant values we'll need to find the selected variant
            if (product.Variants.Any() && properties.Any())
            {
                variant = product.Variants.FirstOrDefault(v => v.ProductProperties
                    .Where(pp => pp.ProductDefinitionField.DisplayOnSite
                                 && pp.ProductDefinitionField.IsVariantProperty
                                 && !pp.ProductDefinitionField.Deleted)
                    .All(p => properties.Any(kv => kv.Key.Equals(p.ProductDefinitionField.Name, StringComparison.InvariantCultureIgnoreCase) && kv.Value.Equals(p.Value, StringComparison.InvariantCultureIgnoreCase))));
            }
            // Only use the current product where there are no variants
            else if (!product.Variants.Any())
            {
                variant = product;
            }

            return variant;
        }

        public void btnSubmitReview_Click(object sender, EventArgs e)
        {
            LeaveReview(SiteContext.Current.CatalogContext.CurrentProduct, reviewName.Value, reviewEmail.Value, "review-rating", reviewHeadline.Value, reviewText.Value);
        }

        public static void LeaveReview(UCommerce.EntitiesV2.Product product, string name, string email, string ratingKey, string reviewHeadline, string reviewText)
        {
            var request = HttpContext.Current.Request;
            var basket = SiteContext.Current.OrderContext.GetBasket();

            if (request.Form.AllKeys.All(x => x != "review-product"))
            {
                return;
            }

            var rating = Convert.ToInt32(request.Form[ratingKey]) * 20;

            if (basket.PurchaseOrder.Customer == null)
            {
                basket.PurchaseOrder.Customer = new Customer() { FirstName = name, LastName = String.Empty, EmailAddress = email };
            }
            else
            {
                basket.PurchaseOrder.Customer.FirstName = name;
                if (basket.PurchaseOrder.Customer.LastName == null)
                {
                    basket.PurchaseOrder.Customer.LastName = String.Empty;
                }
                basket.PurchaseOrder.Customer.EmailAddress = email;
            }

            basket.PurchaseOrder.Customer.Save();

            var review = new ProductReview()
            {
                ProductCatalogGroup = SiteContext.Current.CatalogContext.CurrentCatalogGroup,
                ProductReviewStatus = ProductReviewStatus.SingleOrDefault(s => s.Name == "New"),
                CreatedOn = DateTime.Now,
                CreatedBy = "System",
                Product = product,
                Customer = basket.PurchaseOrder.Customer,
                Rating = rating,
                ReviewHeadline = reviewHeadline,
                ReviewText = reviewText,
                Ip = request.UserHostName
            };

            product.AddProductReview(review);

            PipelineFactory.Create<ProductReview>("ProductReview").Execute(review);
        }
    }
}