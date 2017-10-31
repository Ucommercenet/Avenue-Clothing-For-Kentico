using System;
using CMS.UIControls;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Address : TemplatePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void UpdateAddresses(object sender, EventArgs e)
        {
            var addressPicker = Page.FindWebPart<CMSWebParts_Ucommerce_AddressPicker>();
            addressPicker.UpdateAddresses();

            Response.Redirect("~/Basket/Shipping");
        }
    }
}