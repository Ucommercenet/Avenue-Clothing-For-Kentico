﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class Product : System.Web.UI.UserControl
    {
        public Category CurrentCategory {
            get { return SiteContext.Current.CatalogContext.CurrentCategory ?? null; }
            set {}
        }

        public UCommerce.EntitiesV2.Product CurrentProduct { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentProduct == null)
            {
                Visible = false;
                return;
            }

            string url = CatalogLibrary.GetNiceUrlForProduct(CurrentProduct, CurrentCategory);

            PriceCalculation price = CatalogLibrary.CalculatePrice(CurrentProduct);

            if (!string.IsNullOrWhiteSpace(CurrentProduct.ThumbnailImageMediaId))
            {
                var imageService = new ImageService();
                var image = imageService.GetImage(CurrentProduct.ThumbnailImageMediaId);

                productImage.ImageUrl = image.Url;
                hlIMage.Visible = true;
            }

            hlProduct.Text = CurrentProduct.DisplayName();
            hlProduct.NavigateUrl = url;
            hlViewMore.NavigateUrl = url;
            hlIMage.NavigateUrl = url;

            if (price != null && price.YourPrice != null)
            {
                litPrice.Text = price.YourPrice.Amount.ToString();
            }
            else
            {
                litPrice.Text = "-";
            }

            if (price?.YourTax != null)
            {
                litTax.Text = price.YourTax.ToString();
            } 

         
        }
    }
}