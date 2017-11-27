using System;
using System.Web;
using CMS.UIControls;
using UCommerce.Api;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Payment : TemplatePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void btnContinue_Click(object sender, EventArgs e)
        {
            var paymentPicker = Page.FindWebPart<CMSWebParts_Ucommerce_PaymentPicker>();
            if (paymentPicker == null)
            {
                return;
            }

            int methodPaymentId;
            if (!Int32.TryParse(paymentPicker.SelectedValue, out methodPaymentId))
            {
                return;
            }

            TransactionLibrary.CreatePayment(methodPaymentId, requestPayment: false);
            HttpContext.Current.Response.Redirect("~/Basket/Preview");
        }
    }
}