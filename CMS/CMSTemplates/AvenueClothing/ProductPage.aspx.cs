using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.PortalEngine;
using CMS.UIControls;
using UCommerce.Runtime;
using UCommerce.EntitiesV2;
using UCommerce.Api;
using UCommerce.Content;
using UCommerce.Infrastructure;
using UCommerce.Pipelines;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class ProductPage : TemplatePage
    {

        private void Page_Load(object sender, EventArgs e)
        {
            if (!SetupIsNeeded())
            {
                return;
            }

            var currentProduct = SiteContext.Current.CatalogContext.CurrentProduct;

            SetupProduct(currentProduct);
            SetupReviews(currentProduct);

            rptVariant.DataSource = GetUniqueVariants(currentProduct);
            rptVariant.DataBind();
        }

        private IEnumerable<IGrouping<ProductDefinitionField, ProductProperty>> GetUniqueVariants(Product product)
        {
            IEnumerable<IGrouping<ProductDefinitionField, ProductProperty>> uniqueVariants =
                from v in product.Variants.SelectMany(p => p.ProductProperties)
                where v.ProductDefinitionField.DisplayOnSite
                group v by v.ProductDefinitionField
                into g
                select g;

            return uniqueVariants;
        }

        private void SetupReviews(Product product)
        {
            if (product.ProductReviews.Any())
            {
                litReviewHeadline.Text = "<h5>Latest Reviews</h5>";
                rptReviews.DataSource = product.ProductReviews;
                rptReviews.DataBind();
            }
            else
            {
                litReviewHeadline.Text = "<p>No-one has reviewed this product yet.</p>";
            }
        }

        private void SetupProduct(Product product)
        {
            litHeadline.Text = product.Name;
            productSku.Text = product.Sku;
            litDescription.Text = product.GetDescription(CultureInfo.CurrentCulture.ToString()).LongDescription;

            var price = CatalogLibrary.CalculatePrice(product);
            if (price.YourPrice != null)
            {
                litPrice.Text = price.YourPrice.Amount.ToString();
                litTax.Text = price.YourTax.ToString();
            }
            else
            {
                btnAddToBasket.Enabled = false;
                btnAddToBasket.Text = "This product has not been assigned a price.";
                litPrice.Text = "-";
                litTax.Text = "-";
            }
        
            if (string.IsNullOrWhiteSpace(product.PrimaryImageMediaId)) return;

            var imageService = ObjectFactory.Instance.Resolve<IImageService>();
            imgTop.ImageUrl = imageService.GetImage(product.PrimaryImageMediaId).Url;
        }

        private bool SetupIsNeeded()
        {
            if (PortalContext.ViewMode.IsDesign() || PortalContext.ViewMode.IsEdit())
            {
                return false;
            }

            return true;
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
                        returnValue += "<i class=\"fa fa-star\"></i>";
                    }
                    else
                    {
                        returnValue += "<i class=\"fa fa-star-o\"></i>";
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
                        returnValue += "<i class=\"fa fa-star\"></i>";
                    }
                    else
                    {
                        returnValue += "<i class=\"fa fa-star-o\"></i>";
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

            foreach (var value in currentItem.Select(p => p.Value).Distinct())
            {
                options += "<option value=\"" + value + "\">" + value + "</option>";
            }

            litOptions.Text = options;
        }

        public void btnAddToBasket_Click(object sender, EventArgs e)
        {
            var variant = GetVariantFromPostData(SiteContext.Current.CatalogContext.CurrentProduct, "variation-");

            if (variant == null)
            {
                Response.Redirect(Request.RawUrl);
            }

            TransactionLibrary.AddToBasket(1, variant.Sku, variant.VariantSku);
            Response.Redirect(Request.RawUrl);
        }

        public Product GetVariantFromPostData(Product product, string variantPrefix)
        {
            var request = HttpContext.Current.Request;
            var keys = request.Form.AllKeys.Where(k => k.StartsWith(variantPrefix, StringComparison.InvariantCultureIgnoreCase));
            var properties = keys.Select(k => new { Key = k.Replace(variantPrefix, string.Empty), Value = request.Form[k] }).ToList();

            Product variant = null;
            
            if (product.Variants.Any() && properties.Any())
            {
                variant = product.Variants.FirstOrDefault(v => v.ProductProperties
                    .Where(pp => pp.ProductDefinitionField.DisplayOnSite
                                 && pp.ProductDefinitionField.IsVariantProperty
                                 && !pp.ProductDefinitionField.Deleted)
                    .All(p => properties.Any(kv => kv.Key.Equals(p.ProductDefinitionField.Name, StringComparison.InvariantCultureIgnoreCase) && kv.Value.Equals(p.Value, StringComparison.InvariantCultureIgnoreCase))));
            }
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

        public void LeaveReview(Product product, string name, string email, string ratingKey, string reviewHeadline, string reviewText)
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
            Response.Redirect(Request.RawUrl);
        }
    }
}