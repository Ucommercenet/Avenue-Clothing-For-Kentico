using System.Web.UI.WebControls;
using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce;
using System.Collections.Generic;
using System.Linq;
using CMS.PortalEngine;
using UCommerce.Infrastructure;
using UCommerce.Transactions;

public partial class CMSWebParts_Ucommerce_Shipping : CMSAbstractWebPart
{
    #region "Properties"

    public string SelectedValue
    {
        get
        {
            return this.rblShippingMethods.SelectedValue;
        }
        set
        {
            this.rblShippingMethods.SelectedValue = value;
        }
    }

    public RadioButtonList RadioButtonList
    {
        get
        {
            return rblShippingMethods;
        }
    }

    private readonly TransactionLibraryInternal _transactionLibraryInternal =
        ObjectFactory.Instance.Resolve<TransactionLibraryInternal>();

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
            var availableShippingMethods = GetShippingMethods();

            if (availableShippingMethods.Count == 0)
            {
                litAlert.Text = "No shipping methods available.";
                return;
            }
            pPaymentAlert.Visible = false;

            SetupShippingMethods(availableShippingMethods);
        }
    }

    private void SetupShippingMethods(List<ShippingMethod> shippingMethods)
    {
        var currentShippingMethod = _transactionLibraryInternal.GetShippingMethod();
        var currentBasket = _transactionLibraryInternal.GetBasket().PurchaseOrder;

        foreach (ShippingMethod shippingMethod in shippingMethods)
        {
            var price = shippingMethod.GetPriceForCurrency(currentBasket.BillingCurrency);
            var formattedPrice = new Money((price == null ? 0 : price.Price), currentBasket.BillingCurrency);

            //ListItem currentListItem = new ListItem($"{shippingMethod.Name} <text>(</text>{formattedPrice}<text>)</text>", shippingMethod.Id.ToString());
            ListItem currentListItem = new ListItem($"{shippingMethod.Name} ({formattedPrice})", shippingMethod.Id.ToString());
            currentListItem.Selected = currentShippingMethod.Id == shippingMethod.Id;

            rblShippingMethods.Items.Add(currentListItem);
        }
    }

    private List<ShippingMethod> GetShippingMethods()
    {
        List<ShippingMethod> availableShippingMethods;
        bool showForCurrentCountry = ValidationHelper.GetBoolean(GetValue("ShowForCurrentCountry"), true);

        if (showForCurrentCountry)
        {
            availableShippingMethods = _transactionLibraryInternal.GetShippingMethods().ToList();
        }
        else
        {
            var shippingMethods = ObjectFactory.Instance.Resolve<IRepository<ShippingMethod>>();
            availableShippingMethods = shippingMethods.Select(x => !x.Deleted).ToList();
        }

        return availableShippingMethods;
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



