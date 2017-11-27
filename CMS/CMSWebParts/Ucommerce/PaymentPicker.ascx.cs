using System.Web.UI.WebControls;
using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce;
using System.Linq;
using UCommerce.Infrastructure;
using System.Collections.Generic;
using CMS.PortalEngine;

public partial class CMSWebParts_Ucommerce_PaymentPicker : CMSAbstractWebPart
{
    #region "Properties"

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
        if (!SetupIsNeeded())
        {
            return;
        }

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
            var availableBillingMethods = GetBillingMethods();

            if (availableBillingMethods.Count == 0)
            {
                litAlert.Text = "No payments methods available for the shipping country.";
                return;
            }
            pPaymentAlert.Visible = false;

            SetupPaymentMethods(availableBillingMethods);
        }
    }

    private void SetupPaymentMethods(List<PaymentMethod> billingMethods)
    {
        var basket = TransactionLibrary.GetBasket().PurchaseOrder;
        var payment = basket.Payments.FirstOrDefault();

        foreach (PaymentMethod paymentMethod in billingMethods)
        {
            decimal feePercent = paymentMethod.FeePercent;
            var fee = paymentMethod.GetFeeForCurrency(basket.BillingCurrency);

            var formattedFee = new Money((fee == null ? 0 : fee.Fee), basket.BillingCurrency);
            string paymentMethodText = $"{paymentMethod.Name} <text>(</text>{formattedFee}<text> + </text>{feePercent:0.00}<text>%)</text>";

            ListItem currentListItem = new ListItem(paymentMethodText, paymentMethod.Id.ToString());
            currentListItem.Selected = payment?.PaymentMethod.Id == paymentMethod.Id;

            rblPaymentMethods.Items.Add(currentListItem);
        }

        if (rblPaymentMethods.SelectedIndex == -1)
        {
            rblPaymentMethods.SelectedIndex = 0;
        }
    }

    private List<PaymentMethod> GetBillingMethods()
    {
        List<PaymentMethod> availableBillingMethods;
        bool showForCurrentCountry = ValidationHelper.GetBoolean(GetValue("ShowForCurrentCountry"), true);

        if (showForCurrentCountry)
        {
            availableBillingMethods = TransactionLibrary.GetPaymentMethods().ToList();
        }
        else
        {
            var paymentMethods = ObjectFactory.Instance.Resolve<IRepository<PaymentMethod>>();
            availableBillingMethods = paymentMethods.Select(x => !x.Deleted).ToList();
        }
        return availableBillingMethods;
    }

    private bool SetupIsNeeded()
    {
        if (IsPostBack || ViewMode.IsDesign() || ViewMode.IsEdit())
        {
            return false;
        }
        if (!TransactionLibrary.HasBasket())
        {
            return false;
        }

        return true;
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



