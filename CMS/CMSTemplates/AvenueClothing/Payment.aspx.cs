using System;
using System.Web;
using CMS.UIControls;
using UCommerce.Api;
using CMS.Helpers;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Payment : TemplatePage
    {
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
        }

        public void btnContinue_Click(object sender, EventArgs e)
        {
            var paymentPicker = Page.FindWebPart<CMSWebParts_Ucommerce_PaymentPicker>();
            int methodPaymentId = -1;

            if (paymentPicker == null)
            {
                return;
            }

            if (!Int32.TryParse(paymentPicker.SelectedValue.ToString(), out methodPaymentId))
            {
                return;
            }

            if(methodPaymentId == -1)
            {
                return;
            }

            TransactionLibrary.CreatePayment(methodPaymentId, requestPayment: false);
            TransactionLibrary.ExecuteBasketPipeline();
            HttpContext.Current.Response.Redirect("~/Basket/Preview");
        }
    }
}