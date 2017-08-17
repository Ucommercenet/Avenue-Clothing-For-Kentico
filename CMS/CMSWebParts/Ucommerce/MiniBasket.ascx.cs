using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using System.Linq;

public partial class CMSWebParts_Ucommerce_MiniBasket : CMSAbstractWebPart
{
    #region "Properties"

    

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            PurchaseOrder basket = null;
            var currency = SiteContext.Current.CatalogContext.CurrentCatalog.PriceGroup.Currency;
            var orderTotal = new Money(0, currency);

            if (TransactionLibrary.HasBasket())
            {
                basket = TransactionLibrary.GetBasket(false).PurchaseOrder;
                if (basket.OrderTotal.HasValue)
                {
                    orderTotal = new Money(basket.OrderTotal.Value, currency);
                }
            }

            var applicationUrl = HttpContext.Current.Request.ApplicationPath;
            string textColor = ValidationHelper.GetString(GetValue("TextColor"), "black").ToLower();
            bool valuePrice = ValidationHelper.GetBoolean(GetValue("ShowPrice"), false);
            bool valueAmount = ValidationHelper.GetBoolean(GetValue("ShowProductAmount"), false);
            string iconColor = ValidationHelper.GetString(GetValue("IconColor"), "black").ToLower();
            if (basket == null || !basket.OrderLines.Any())
            {
                lblMinicartAmount.Text = "Your basket is empty";
                hlMinicart.Attributes.Add("class", "" + textColor);
            }
            else
            {
                hlMinicart.Attributes.Add("href", applicationUrl + "/basket");
                hlMinicart.Attributes.Add("class", "" + textColor);
                imgMinicart.Attributes.Add("class", "icon-shopping-cart icon-" + iconColor);
                if (valuePrice && valueAmount)
                {
                    lblMinicartAmount.Text = basket.OrderLines.Sum(x => x.Quantity).ToString("#,##") + " item(s):";
                    lblMinicartPrice.Text = "" + orderTotal;
                }
                else if (valuePrice)
                {
                    lblMinicartPrice.Text = "" + orderTotal;
                }
                else if (valueAmount)
                {
                    lblMinicartAmount.Text = basket.OrderLines.Sum(x => x.Quantity).ToString("#,##") + " item(s)";
                }
            }
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}



