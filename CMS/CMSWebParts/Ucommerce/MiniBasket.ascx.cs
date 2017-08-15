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

        var minicartString = "";
        string textColor = ValidationHelper.GetString(GetValue("TextColor"), "black");
        if (basket == null || !basket.OrderLines.Any())
        {
            minicartString += "<a id=\"empty-cart\" style=\"color + " + textColor + ";\">Your Shopping Cart is empty</a>";
        }
        else
        {
            bool valuePrice = ValidationHelper.GetBoolean(GetValue("ShowPrice"), false);
            bool valueAmount = ValidationHelper.GetBoolean(GetValue("ShowProductAmount"), false);
            string iconColor = ValidationHelper.GetString(GetValue("IconColor"), "black").ToLower();
            
            if (valuePrice && valueAmount)
            {
                minicartString +=
                "<a href=\"" + applicationUrl + "/basket\" id=\"mini-cart\" style=\"color: " + textColor + "; \"><i class=\"icon-shopping-cart icon-" + iconColor + "\"></i><span class=\"item-qty\">"
                + basket.OrderLines.Sum(x => x.Quantity).ToString("#,##")
                + "</span> items in cart, total: <span class=\"order-total\">" + orderTotal + "</span></a>";
            }
            else if (valuePrice)
            {
                minicartString += "<a href=\"" + applicationUrl + "/basket\" id=\"mini-cart\" style=\"color: " + textColor + "; \"><i class=\"icon-shopping-cart icon-" + iconColor + "\"></i> Total: <span class=\"order-total\">" + orderTotal + "</span></a>";
            }
            else if (valueAmount)
            {
                minicartString +=
                "<a href=\"" + applicationUrl + "/basket\" id=\"mini-cart\" style=\"color: " + textColor + "; \"><i class=\"icon-shopping-cart icon-" + iconColor + "\"></i><span class=\"item-qty\">"
                + basket.OrderLines.Sum(x => x.Quantity).ToString("#,##")
                + "</span> items in cart </a>";
            }
            else
            {
                minicartString +=
                "<a href=\"" + applicationUrl + "/basket\" id=\"mini-cart\"><i class=\"icon-shopping-cart icon-" + iconColor + "\"></i></a>";
            }
        }

        litMinicart.Text = minicartString;
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



