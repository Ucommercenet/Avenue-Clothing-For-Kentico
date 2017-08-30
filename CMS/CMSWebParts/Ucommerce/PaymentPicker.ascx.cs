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
using System.Linq;

public partial class CMSWebParts_Ucommerce_PaymentPicker : CMSAbstractWebPart
{
    #region "Properties"

    private bool _selectFirst = true;

    public string SelectedValue
    {
        get
        {
            return this.rblPaymentMethods.SelectedValue;
        }
        set
        {
            this.rblPaymentMethods.SelectedValue = value;
        }
    }
    public RadioButtonList RadioButtonList
    {
        get
        {
            return rblPaymentMethods;
        }
    }

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
            var basket = TransactionLibrary.GetBasket().PurchaseOrder;
            var billingCountry = TransactionLibrary.GetShippingInformation().Country;
            var availableBillingMethods = TransactionLibrary.GetPaymentMethods();
            var payment = basket.Payments.FirstOrDefault();
            bool showForCurrentCountry = ValidationHelper.GetBoolean(GetValue("ShowForCurrentCountry"), true);

            if (showForCurrentCountry)
            {
                availableBillingMethods = TransactionLibrary.GetPaymentMethods(billingCountry);
            }
            foreach (PaymentMethod paymentMethod in availableBillingMethods)
            {
                decimal feePercent = paymentMethod.FeePercent;

                var fee = paymentMethod.GetFeeForCurrency(basket.BillingCurrency);
                var formattedFee = new Money((fee == null ? 0 : fee.Fee), basket.BillingCurrency);

                string paymentMethodText = paymentMethod.Name + "<text> (</text>" + formattedFee + "<text> + </text>"
                                           + feePercent.ToString("0.00") + "<text>%)</text>";

                ListItem currentListItem = new ListItem(paymentMethodText, paymentMethod.Id.ToString());
                rblPaymentMethods.Items.Add(currentListItem);

                if (payment != null && payment.PaymentMethod.Equals(paymentMethod))
                {
                    currentListItem.Selected = true;
                    _selectFirst = false;
                }
            }

            if (_selectFirst)
            {
                rblPaymentMethods.Items[0].Selected = true;
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


