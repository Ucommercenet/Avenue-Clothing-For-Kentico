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
			//Todo: I need this when the javascript stuff is ready.    
			//Page.ClientScript.RegisterStartupScript(
			//    GetType(),
			//    "UCommerce.DemoStore.checkout",
			//    "<script src=\"/scripts/UCommerce.DemoStore.checkout.js\"></script>");

			if (!IsPostBack)
			{
				var billingAddress = TransactionLibrary.GetBillingInformation();
				var shipmentAddress = TransactionLibrary.GetShippingInformation();
				var countries = TransactionLibrary.GetCountries().OrderBy(x => x.Name);

				billingFirstName.Text = billingAddress.FirstName;
				billingLastName.Text = billingAddress.LastName;
				billingEmail.Text = billingAddress.EmailAddress;
				billingCompany.Text = billingAddress.CompanyName;
				billingAttention.Text = billingAddress.Attention;
				billingStreet.Text = billingAddress.Line1;
				billingStreetTwo.Text = billingAddress.Line2;
				billingCity.Text = billingAddress.City;
				billingPostalCode.Text = billingAddress.PostalCode;
				billingPhone.Text = billingAddress.PhoneNumber;
				billingMobile.Text = billingAddress.MobilePhoneNumber;

				billingCountry.DataSource = countries;
				billingCountry.DataBind();
				if (billingAddress.Country != null)
				{
					billingCountry.SelectedValue = billingAddress.Country.Id.ToString();
				}

				shippingFirstName.Text = shipmentAddress.FirstName;
				shippingLastName.Text = shipmentAddress.LastName;
				shippingEmail.Text = shipmentAddress.EmailAddress;
				shippingCompany.Text = shipmentAddress.CompanyName;
				shippingAttention.Text = shipmentAddress.Attention;
				shippingStreet.Text = shipmentAddress.Line1;
				shippingStreetTwo.Text = shipmentAddress.Line2;
				shippingCity.Text = shipmentAddress.City;
				shippingPostalCode.Text = shipmentAddress.PostalCode;
				shippingPhone.Text = shipmentAddress.PhoneNumber;
				shippingMobile.Text = shipmentAddress.MobilePhoneNumber;

				shippingCountry.DataSource = countries;
				shippingCountry.DataBind();

				if (billingAddress.Country != null)
				{
					shippingCountry.SelectedValue = shipmentAddress.Country.Id.ToString();
				}
			}
		}

		public void btnBillingAndShippingUpdate_Click(object sender, EventArgs e)
		{

			EditBillingInformation();

			TransactionLibrary.EditShippingInformation(
				shippingFirstName.Text,
				shippingLastName.Text,
				shippingEmail.Text,
				shippingPhone.Text,
				shippingMobile.Text,
				shippingCompany.Text,
				shippingStreet.Text,
				shippingStreetTwo.Text,
				shippingPostalCode.Text,
				shippingCity.Text,
				"",//this is the state, which cant be chosen in the frontend
				shippingAttention.Text,
				Int32.Parse(shippingCountry.SelectedValue)
			);

			Response.Redirect("/cart/Shipping");
		}

		public void EditBillingInformation()
		{
			TransactionLibrary.EditBillingInformation(
				billingFirstName.Text,
				billingLastName.Text,
				billingEmail.Text,
				billingPhone.Text,
				billingMobile.Text,
				billingCompany.Text,
				billingStreet.Text,
				billingStreetTwo.Text,
				billingPostalCode.Text,
				billingCity.Text,
				"",//this is the state, which cant be chosen in the frontend
				billingAttention.Text,
				Int32.Parse(billingCountry.SelectedValue)
			);
		}
	
		public void btnBillingUpdate_Click(object sender, EventArgs e)
		{
			EditBillingInformation();

			//if the shipping is the same as billing.
			TransactionLibrary.EditShippingInformation(
				billingFirstName.Text,
				billingLastName.Text,
				billingEmail.Text,
				billingPhone.Text,
				billingMobile.Text,
				billingCompany.Text,
				billingStreet.Text,
				billingStreetTwo.Text,
				billingPostalCode.Text,
				billingCity.Text,
				"",//this is the state, which cant be chosen in the frontend
				billingAttention.Text,
				Int32.Parse(billingCountry.SelectedValue)
			);
			Response.Redirect("~/Basket/Shipping");
		}
	}

}