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

            var currentShippingMethod = TransactionLibrary.GetShippingMethod();
            var currentBasket = TransactionLibrary.GetBasket().PurchaseOrder;
            var shippingCountry = TransactionLibrary.GetShippingInformation().Country;
            var availableShippingMethods = TransactionLibrary.GetShippingMethods(shippingCountry);

            if (availableShippingMethods.Count != 0)
            {
                pPaymentAlert.Visible = false;
            }
            else
            {
                string warning =
                    "WARNING: No payment methods have been configured for " + shippingCountry.Name + " within <a href=\"http://ucommerce.net\">UCommerce</a> administration area.";
                litAlert.Text = warning;
                //btnUpdateShipment.Enabled = false;
            }

            foreach (ShippingMethod shippingMethod in availableShippingMethods)
            {
                var price = shippingMethod.GetPriceForCurrency(currentBasket.BillingCurrency);
                var formattedPrice = new Money((price == null ? 0 : price.Price), currentBasket.BillingCurrency);

                ListItem currentListItem = new ListItem(shippingMethod.Name + "<text>(</text>" + formattedPrice + "<text>)</text>, ", shippingMethod.Id.ToString());

                if (currentShippingMethod.Id == shippingMethod.Id)
                {
                    currentListItem.Selected = true;
                }

                rblShippingMethods.Items.Add(currentListItem);
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



