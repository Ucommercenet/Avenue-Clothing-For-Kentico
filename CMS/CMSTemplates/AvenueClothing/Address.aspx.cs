using System;
using System.Linq;
using System.Web.UI;
using CMS.UIControls;
using UCommerce.Api;

namespace CMSApp.CMSTemplates.AvenueClothing
{
	public partial class Address : TemplatePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

        }

        public void btnBillingAndShippingUpdate_Click(object sender, EventArgs e)
        {
            var addressPicker = Page.FindWebPart<CMSWebParts_Ucommerce_AddressPicker>();
            addressPicker.SetData();
            EditBillingInformation();

            TransactionLibrary.EditShippingInformation(
                addressPicker.ShippingAddress.FirstName,
                addressPicker.ShippingAddress.LastName,
                addressPicker.ShippingAddress.EmailAddress,
                addressPicker.ShippingAddress.PhoneNumber,
                addressPicker.ShippingAddress.MobilePhoneNumber,
                addressPicker.ShippingAddress.CompanyName,
                addressPicker.ShippingAddress.Line1,
                addressPicker.ShippingAddress.Line2,
                addressPicker.ShippingAddress.PostalCode,
                addressPicker.ShippingAddress.City,
                "",//this is the state, which cant be chosen in the frontend
                addressPicker.ShippingAddress.Attention,
                addressPicker.ShippingAddress.Country.CountryId
            );

            Response.Redirect("~/Basket/Shipping");
        }

        public void EditBillingInformation()
        {
            var addressPicker = Page.FindWebPart<CMSWebParts_Ucommerce_AddressPicker>();
            addressPicker.SetData();

            TransactionLibrary.EditBillingInformation(
                addressPicker.BillingAddress.FirstName,
                addressPicker.BillingAddress.LastName,
                addressPicker.BillingAddress.EmailAddress,
                addressPicker.BillingAddress.PhoneNumber,
                addressPicker.BillingAddress.MobilePhoneNumber,
                addressPicker.BillingAddress.CompanyName,
                addressPicker.BillingAddress.Line1,
                addressPicker.BillingAddress.Line2,
                addressPicker.BillingAddress.PostalCode,
                addressPicker.BillingAddress.City,
                "",//this is the state, which cant be chosen in the frontend
                addressPicker.BillingAddress.Attention,
                addressPicker.BillingAddress.Country.CountryId
            );
        }

        public void btnBillingUpdate_Click(object sender, EventArgs e)
        {
            var addressPicker = Page.FindWebPart<CMSWebParts_Ucommerce_AddressPicker>();
            EditBillingInformation();

            //if the shipping is the same as billing.
            TransactionLibrary.EditShippingInformation(
                addressPicker.BillingAddress.FirstName,
                addressPicker.BillingAddress.LastName,
                addressPicker.BillingAddress.EmailAddress,
                addressPicker.BillingAddress.PhoneNumber,
                addressPicker.BillingAddress.MobilePhoneNumber,
                addressPicker.BillingAddress.CompanyName,
                addressPicker.BillingAddress.Line1,
                addressPicker.BillingAddress.Line2,
                addressPicker.BillingAddress.PostalCode,
                addressPicker.BillingAddress.City,
                "",//this is the state, which cant be chosen in the frontend
                addressPicker.BillingAddress.Attention,
                addressPicker.BillingAddress.Country.CountryId
            );
            Response.Redirect("~/Basket/Shipping");
        }
    }

}