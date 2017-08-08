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
    public partial class Payment : TemplatePage
    {
        private bool _selectFirst = true;

        protected void Page_Load(object sender, EventArgs e)
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

            var basket = TransactionLibrary.GetBasket().PurchaseOrder;
            var billingCountry = TransactionLibrary.GetShippingInformation().Country;
            var availableBillingMethods = TransactionLibrary.GetPaymentMethods(billingCountry);
            var payment = basket.Payments.FirstOrDefault();

            if (availableBillingMethods.Count != 0)
            {
                pPaymentAlert.Visible = false;
            }
            else
            {
                string warning =
                    "WARNING: No payment methods have been configured for " + billingCountry.Name + " within <a href=\"http://ucommerce.net\">UCommerce</a> administration area.";
                litAlert.Text = warning;
                btnContinue.Enabled = false;
                _selectFirst = false;
            }

            foreach (PaymentMethod paymentMethod in availableBillingMethods)
            {
                decimal feePercent = paymentMethod.FeePercent;

                var fee = paymentMethod.GetFeeForCurrency(basket.BillingCurrency);
                var formattedFee = new Money((fee == null ? 0 : fee.Fee), basket.BillingCurrency);

                string paymentMethodText = paymentMethod.Name + "<text>(</text>" + formattedFee + "<text>+</text>"
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

        public void btnContinue_Click(object sender, EventArgs e)
        {
            var selectedRadioButton = rblPaymentMethods.Items.OfType<ListItem>().FirstOrDefault(r => r.Selected);
            int methodPaymentId = 0;

            if (selectedRadioButton == null)
            {
                return;
            }

            if (!Int32.TryParse(selectedRadioButton.Value, out methodPaymentId))
            {
                return;
            }

            TransactionLibrary.CreatePayment(methodPaymentId, requestPayment: false);
            TransactionLibrary.ExecuteBasketPipeline();
            HttpContext.Current.Response.Redirect("~/Basket/Preview");
        }
    }
}