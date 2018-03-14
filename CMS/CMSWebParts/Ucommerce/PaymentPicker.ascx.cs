using System.Web.UI.WebControls;
using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.EntitiesV2;
using UCommerce;
using System.Linq;
using UCommerce.Infrastructure;
using System.Collections.Generic;
using CMS.PortalEngine;
using UCommerce.Api;

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
            var availablePaymentMethods = GetPaymentMethods();

            if (availablePaymentMethods.Count == 0)
            {
                litAlert.Text = "No payments methods available.";
                return;
            }
            pPaymentAlert.Visible = false;

            SetupPaymentMethods(availablePaymentMethods);
        }
    }

    private void SetupPaymentMethods(List<PaymentMethod> billingMethods)
    {
        var basket = TransactionLibrary.GetBasket().PurchaseOrder;
        var existingPayment = basket.Payments.FirstOrDefault();
        var selectedPaymentMethodId = existingPayment != null
            ? existingPayment.PaymentMethod.PaymentMethodId
            : -1;

        foreach (PaymentMethod paymentMethod in billingMethods)
        {
            var feePercent = paymentMethod.FeePercent;
            var fee = paymentMethod.GetFeeForCurrency(basket.BillingCurrency);
            var formattedFee = new Money(fee == null ? 0 : fee.Fee, basket.BillingCurrency);

            string paymentMethodText = $"{paymentMethod.Name} ({formattedFee} + {feePercent:0.00}%)";

            ListItem currentListItem = new ListItem(paymentMethodText, paymentMethod.Id.ToString())
            {
                Selected = paymentMethod.PaymentMethodId == selectedPaymentMethodId
            };

            rblPaymentMethods.Items.Add(currentListItem);
        }

        if (rblPaymentMethods.SelectedIndex == -1)
        {
            rblPaymentMethods.SelectedIndex = 0;
        }
    }

    private List<PaymentMethod> GetPaymentMethods()
    {
        List<PaymentMethod> availablePaymentMethods;
        bool showForCurrentCountry = ValidationHelper.GetBoolean(GetValue("ShowForCurrentCountry"), true);

        if (showForCurrentCountry)
        {
            availablePaymentMethods = TransactionLibrary.GetPaymentMethods().ToList();
        }
        else
        {
            var paymentMethods = ObjectFactory.Instance.Resolve<IRepository<PaymentMethod>>();
            availablePaymentMethods = paymentMethods.Select(x => !x.Deleted).ToList();
        }
        return availablePaymentMethods;
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



