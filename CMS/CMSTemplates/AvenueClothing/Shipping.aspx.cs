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
            var viewMode = Convert.ToInt32(Request.QueryString["viewmode"]);

            if (viewMode == 6 || viewMode == 3)
            {
                return;
            }
            if (IsPostBack|| !TransactionLibrary.HasBasket())
            {
                return;
            }
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