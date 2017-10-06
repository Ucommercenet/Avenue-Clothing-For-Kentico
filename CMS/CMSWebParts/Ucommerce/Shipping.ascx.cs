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
using System.Collections.Generic;
using System.Linq;
using UCommerce.Infrastructure;

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
            var viewMode = Convert.ToInt32(Request.QueryString["viewmode"]);

            if (viewMode == 6 || viewMode == 3)
            {
                return;
            }
            if (IsPostBack)
            {
                return;
            }

            bool showForCurrentCountry = ValidationHelper.GetBoolean(GetValue("ShowForCurrentCountry"), true);
            var currentShippingMethod = TransactionLibrary.GetShippingMethod();
            var currentBasket = TransactionLibrary.GetBasket().PurchaseOrder;
            var shippingCountry = TransactionLibrary.GetShippingInformation().Country;
            var availableShippingMethods = new List<ShippingMethod>();
           

            if (showForCurrentCountry)
            {
                availableShippingMethods = TransactionLibrary.GetShippingMethods(shippingCountry).ToList();
            }
            else
            {
                var shippingMethods = ObjectFactory.Instance.Resolve<IRepository<ShippingMethod>>();
                availableShippingMethods = shippingMethods.Select(x => !x.Deleted).ToList();
            }

            if (availableShippingMethods.Count != 0)
            {
                pPaymentAlert.Visible = false;
            }
            else
            {
                string warning =
                    "WARNING: No payment methods have been configured for " + shippingCountry.Name + " within <a href=\"http://ucommerce.net\">UCommerce</a> administration area.";
                litAlert.Text = warning;

            }

            foreach (ShippingMethod shippingMethod in availableShippingMethods)
            {
                var price = shippingMethod.GetPriceForCurrency(currentBasket.BillingCurrency);
                var formattedPrice = new Money((price == null ? 0 : price.Price), currentBasket.BillingCurrency);

                ListItem currentListItem = new ListItem($"{shippingMethod.Name} <text>(</text>{formattedPrice}<text>)</text>", shippingMethod.Id.ToString());

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



