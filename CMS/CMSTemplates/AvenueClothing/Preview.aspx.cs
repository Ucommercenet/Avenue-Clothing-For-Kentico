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
    public partial class Preview : TemplatePage
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

            var billingAddress = TransactionLibrary.GetBillingInformation();
            var shipmentAddress = TransactionLibrary.GetShippingInformation();

            //billing
            litBillingName.Text = billingAddress.FirstName + " " + billingAddress.LastName;
            litBillingStreet.Text = billingAddress.Line1;
            litBillingPostalCode.Text = billingAddress.PostalCode;
            litBillingCity.Text = billingAddress.City;
            litBillingCountry.Text = billingAddress.Country.Name;

            if (!string.IsNullOrEmpty(billingAddress.Attention))
            {
                litBillingAttention.Text = "<text>att. </text>" + billingAddress.Attention;
            }

            litBillingPhone.Text = billingAddress.PhoneNumber;
            litBillingMobilePhone.Text = billingAddress.MobilePhoneNumber;
            lnkBillingMail.NavigateUrl = "mailto:" + billingAddress.EmailAddress;
            lnkBillingMail.Text = billingAddress.EmailAddress;

            //shipping
            litShippingName.Text = shipmentAddress.FirstName + " " + shipmentAddress.LastName;
            litShippingCompany.Text = shipmentAddress.CompanyName;
            litShippingStreet.Text = shipmentAddress.Line1;
            litShippingPostalCode.Text = shipmentAddress.PostalCode;
            litShippingCity.Text = shipmentAddress.City;
            litShippingCountry.Text = shipmentAddress.Country.Name;

            if (!string.IsNullOrEmpty(shipmentAddress.Attention))
            {
                litShippingAttention.Text = "<text>att. </text>" + shipmentAddress.Attention;
            }

            litShippingPhone.Text = shipmentAddress.PhoneNumber;
            litShippingMobilePhone.Text = shipmentAddress.MobilePhoneNumber;
            lnkShippingMail.NavigateUrl = "mailto:" + shipmentAddress.EmailAddress;
            lnkShippingMail.Text = shipmentAddress.EmailAddress;
        }

        public void btnContinue_Click(object sender, EventArgs e)
        {
            TransactionLibrary.ExecuteBasketPipeline();
            TransactionLibrary.RequestPayments();
            HttpContext.Current.Response.Redirect("~/Basket/Confirmation");
        }
    }
}