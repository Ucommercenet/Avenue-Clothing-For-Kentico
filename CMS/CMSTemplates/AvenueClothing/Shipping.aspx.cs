using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UIControls;
using UCommerce;
using UCommerce.Api;
using UCommerce.EntitiesV2;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Shipping : TemplatePage
    {
        private void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

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

        public void btnUpdateShipment_Click(object sender, EventArgs e)
        {
            var selectedRadioButton = rblShippingMethods.Items.OfType<ListItem>().FirstOrDefault(r => r.Selected);
            int shippingMethodId = 0;

            if (selectedRadioButton != null && !Int32.TryParse(selectedRadioButton.Value, out shippingMethodId))
            {
                return;
            }

            TransactionLibrary.CreateShipment(shippingMethodId, overwriteExisting: true);
            TransactionLibrary.ExecuteBasketPipeline();
            HttpContext.Current.Response.Redirect("/basket/payment");
        }
    }
}