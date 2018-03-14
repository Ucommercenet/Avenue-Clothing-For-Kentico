using UCommerce;
using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using System.Linq;
using UCommerce.Api;

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
            string textColor = ValidationHelper.GetString(GetValue("TextColor"), "black").ToLower();
            bool valuePrice = ValidationHelper.GetBoolean(GetValue("ShowPrice"), false);
            bool valueAmount = ValidationHelper.GetBoolean(GetValue("ShowProductAmount"), false);
            string iconColor = ValidationHelper.GetString(GetValue("IconColor"), "black").ToLower();

            if (!TransactionLibrary.HasBasket())
            {
                lblMinicartAmount.Text = "Your basket is empty";
                hlMinicart.Attributes.Add("class", "" + textColor);
                return;
            }

            var purchaseOrder = TransactionLibrary.GetBasket(false).PurchaseOrder;
            var numberOfItemsInBasket = purchaseOrder.OrderLines.Sum(x => x.Quantity);
            var basketTotal = purchaseOrder.OrderTotal.HasValue ? new Money(purchaseOrder.OrderTotal.Value, purchaseOrder.BillingCurrency) : new Money(0, purchaseOrder.BillingCurrency);

            hlMinicart.Attributes.Add("href", "~/basket");
            hlMinicart.Attributes.Add("class", "" + textColor);
            imgMinicart.Attributes.Add("class", "icon-shopping-cart icon-" + iconColor);
            if (valuePrice)
            {
                lblMinicartPrice.Text = basketTotal.ToString();
            }
            if (valueAmount)
            {
                lblMinicartAmount.Text = $"{numberOfItemsInBasket} item(s)";
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



