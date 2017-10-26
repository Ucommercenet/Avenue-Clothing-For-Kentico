using System;
using System.Web;
using CMS.PortalEngine;
using CMS.UIControls;
using UCommerce.Api;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Preview : TemplatePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SetupIsNeeded())
            {
                return;
            }

            PopulateBillingAddress();
            PopulateShippingAddress();
        }

        private void PopulateShippingAddress()
        {
            var shipmentAddress = TransactionLibrary.GetShippingInformation();

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

        private void PopulateBillingAddress()
        {
            var billingAddress = TransactionLibrary.GetBillingInformation();

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
        }

        private bool SetupIsNeeded()
        {
            if (IsPostBack || PortalContext.ViewMode.IsDesign() || PortalContext.ViewMode.IsEdit())
            {
                return false;
            }
            if (!TransactionLibrary.HasBasket())
            {
                return false;
            }

            return true;
        }

        public void btnContinue_Click(object sender, EventArgs e)
        {
            TransactionLibrary.RequestPayments();
            HttpContext.Current.Response.Redirect("~/Basket/Confirmation");
        }
    }
}