using System;
using System.Web;
using CMS.UIControls;
using UCommerce;
using UCommerce.Api;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Shipping : TemplatePage
    {
        private void Page_Load(object sender, EventArgs e)
        {

        }

        public void btnUpdateShipment_Click(object sender, EventArgs e)
        {
            var shipping = Page.FindWebPart<CMSWebParts_Ucommerce_Shipping>();
            int shippingMethodId = 0;

            if (shipping == null)
            {
                return;
            }

            if (shipping != null && !Int32.TryParse(shipping.SelectedValue, out shippingMethodId))
            {
                return;
            }

            TransactionLibrary.CreateShipment(shippingMethodId, Constants.DefaultShipmentAddressName, overwriteExisting: true);
            TransactionLibrary.ExecuteBasketPipeline();
            HttpContext.Current.Response.Redirect("~/basket/payment");
        }
    }
}