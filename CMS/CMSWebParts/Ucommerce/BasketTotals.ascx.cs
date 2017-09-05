using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce;

public partial class CMSWebParts_Ucommerce_BasketTotals : CMSAbstractWebPart
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
            bool orderSubTotal = ValidationHelper.GetBoolean(GetValue("OrderSubTotal"), true);
            bool orderVat = ValidationHelper.GetBoolean(GetValue("OrderVat"), true);
            bool orderDiscount = ValidationHelper.GetBoolean(GetValue("OrderDiscount"), true);
            bool orderShipping = ValidationHelper.GetBoolean(GetValue("OrderShipping"), true);
            bool orderPayment = ValidationHelper.GetBoolean(GetValue("OrderPayment"), true);

            var basket = TransactionLibrary.GetBasket(true).PurchaseOrder;
            Currency currency = basket.BillingCurrency;

            var subTotal = new Money(0, currency);
            var tax = new Money(0, currency);
            var discount = new Money(0, currency);
            var shipping = new Money(0, currency);
            var payment = new Money(0, currency);
            var orderTotal = new Money(0, currency);

            if (basket.SubTotal.HasValue)
            {
                subTotal = new Money(basket.SubTotal.Value, currency);
            }
            if (basket.VAT.HasValue)
            {
                tax = new Money(basket.VAT.Value, currency);
            }
            if (basket.DiscountTotal.HasValue)
            {
                discount = new Money(basket.DiscountTotal.Value * -1, currency);
            }
            if (basket.ShippingTotal.HasValue)
            {
                shipping = new Money(basket.ShippingTotal.Value, currency);
            }
            if (basket.PaymentTotal.HasValue)
            {
                payment = new Money(basket.PaymentTotal.Value, currency);
            }
            if (basket.OrderTotal.HasValue)
            {
                orderTotal = new Money(basket.OrderTotal.Value, currency);
            }

            litSubtotal.Text = subTotal.ToString();
            litTax.Text = tax.ToString();
            litDiscount.Text = discount.ToString();
            litShipping.Text = shipping.ToString();
            litPayment.Text = payment.ToString();
            litTotal.Text = orderTotal.ToString();

            subOrder.Visible = orderSubTotal;
            vatOrder.Visible = orderVat;
            discountOrder.Visible = orderDiscount;
            shippingOrder.Visible = orderShipping;
            paymentOrder.Visible = orderPayment;
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



