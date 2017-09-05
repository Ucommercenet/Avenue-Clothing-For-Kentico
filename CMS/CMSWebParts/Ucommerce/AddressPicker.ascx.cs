using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.Api;
using System.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

public partial class CMSWebParts_Ucommerce_AddressPicker : CMSAbstractWebPart
{
    #region "Properties"
    private static OrderAddress billingAddress = new OrderAddress();
    public OrderAddress BillingAddress
    {
        get
        {
            return billingAddress;
        }
        set
        {
            billingAddress = value;
        }
    }

    private static OrderAddress shippingAddress = new OrderAddress();
    public OrderAddress ShippingAddress
    {
        get
        {
            return shippingAddress;
        }
        set
        {
            shippingAddress = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
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
                BillingAddress = billingAddress;
                ShippingAddress = shipmentAddress;
            }
            else
            {

            }
        }
    }

    //Used for setting the data
    public void SetData()
    {
        var billingAddress = TransactionLibrary.GetBillingInformation();
        var shipmentAddress = TransactionLibrary.GetShippingInformation();
        var countries = TransactionLibrary.GetCountries().OrderBy(x => x.Name);

        billingAddress.FirstName = billingFirstName.Text;
        billingAddress.LastName = billingLastName.Text;
        billingAddress.EmailAddress = billingEmail.Text;
        billingAddress.CompanyName = billingCompany.Text;
        billingAddress.Attention = billingAttention.Text;
        billingAddress.Line1 = billingStreet.Text;
        billingAddress.Line2 = billingStreetTwo.Text;
        billingAddress.City = billingCity.Text;
        billingAddress.PostalCode = billingPostalCode.Text;
        billingAddress.PhoneNumber = billingPhone.Text;
        billingAddress.MobilePhoneNumber = billingMobile.Text;

        billingCountry.DataSource = countries;
        billingCountry.DataBind();
        var billingCountryID = Int32.Parse(billingCountry.SelectedValue);

        var billingCountries = ObjectFactory.Instance.Resolve<IRepository<Country>>();
        billingAddress.Country = billingCountries.Select(x => x.CountryId == billingCountryID).ToList().FirstOrDefault();

        shipmentAddress.FirstName = shippingFirstName.Text;
        shipmentAddress.LastName = shippingLastName.Text;
        shipmentAddress.EmailAddress = shippingEmail.Text;
        shipmentAddress.CompanyName = shippingCompany.Text;
        shipmentAddress.Attention = shippingAttention.Text;
        shipmentAddress.Line1 = shippingStreet.Text;
        shipmentAddress.Line2 = shippingStreetTwo.Text;
        shipmentAddress.City = shippingCity.Text;
        shipmentAddress.PostalCode = shippingPostalCode.Text;
        shipmentAddress.PhoneNumber = shippingPhone.Text;
        shipmentAddress.MobilePhoneNumber = shippingMobile.Text;

        shippingCountry.DataSource = countries;
        shippingCountry.DataBind();
        var shippingCountryID = Int32.Parse(shippingCountry.SelectedValue);

        var shippingCountries = ObjectFactory.Instance.Resolve<IRepository<Country>>();
        shipmentAddress.Country = shippingCountries.Select(x => x.CountryId == shippingCountryID).ToList().FirstOrDefault();

        BillingAddress = billingAddress;
        ShippingAddress = shipmentAddress;
    }

    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}



