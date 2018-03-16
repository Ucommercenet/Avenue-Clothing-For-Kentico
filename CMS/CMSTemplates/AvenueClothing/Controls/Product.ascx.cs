using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce.Api;
using UCommerce.Content;
using UCommerce.Extensions;
using UCommerce.Infrastructure;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class Product : System.Web.UI.UserControl
    {
        public UCommerce.EntitiesV2.Product CurrentProduct { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var catalogContext = ObjectFactory.Instance.Resolve<ICatalogContext>();
            var imageService = ObjectFactory.Instance.Resolve<IImageService>();

            if (CurrentProduct == null)
            {
                Visible = false;
                return;
            }

            string url = CatalogLibrary.GetNiceUrlForProduct(CurrentProduct, catalogContext.CurrentCategory);
            PriceCalculation price = CatalogLibrary.CalculatePrice(CurrentProduct);

            if (!string.IsNullOrWhiteSpace(CurrentProduct.ThumbnailImageMediaId))
            {
                productImage.ImageUrl = imageService.GetImage(CurrentProduct.ThumbnailImageMediaId).Url;
                hlImage.Visible = true;
            }

            hlProduct.Text = CurrentProduct.DisplayName();
            hlProduct.NavigateUrl = url;
            hlViewMore.NavigateUrl = url;
            hlImage.NavigateUrl = url;

            litPrice.Text = price?.YourPrice?.Amount.ToString() ?? "-";

            if (price?.YourTax != null)
            {
                litTax.Text = price.YourTax.ToString();
            }
        }
    }
}