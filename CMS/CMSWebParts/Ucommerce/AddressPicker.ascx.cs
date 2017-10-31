using System.Collections.Generic;
using CMS.PortalEngine.Web.UI;
using UCommerce.Api;
using System.Linq;
using CMS.PortalEngine;
using UCommerce.EntitiesV2;

public partial class CMSWebParts_Ucommerce_AddressPicker : CMSAbstractWebPart
{
    #region "Properties"

    private OrderAddress billingAddress = new OrderAddress();
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

    private OrderAddress shippingAddress = new OrderAddress();
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
        if (!SetupIsNeeded())
        {
            return;
        }

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
            var countries = TransactionLibrary.GetCountries().OrderBy(x => x.Name).ToList();

            PopulateBillingAddress(countries);
            PopulateShippingAddress(countries);
        }
    }

    public void UpdateAddresses()
    {
        EditBillingInformation();
        EditShippingInformation();
    }

    private void EditShippingInformation()
    {
        if (UseDIfferentShippingAddress.Checked)
        {
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
                "",
                shippingAttention.Text,
                int.Parse(shippingCountry.SelectedValue));
        }
        else
        {
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
                "",
                billingAttention.Text,
                int.Parse(billingCountry.SelectedValue));
        }
    }

    private void EditBillingInformation()
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
            "",
            billingAttention.Text,
            int.Parse(billingCountry.SelectedValue));
    }

    private void PopulateShippingAddress(List<Country> countries)
    {
        var existingShipmentAddress = TransactionLibrary.GetShippingInformation();

        shippingFirstName.Text = existingShipmentAddress.FirstName;
        shippingLastName.Text = existingShipmentAddress.LastName;
        shippingEmail.Text = existingShipmentAddress.EmailAddress;
        shippingCompany.Text = existingShipmentAddress.CompanyName;
        shippingAttention.Text = existingShipmentAddress.Attention;
        shippingStreet.Text = existingShipmentAddress.Line1;
        shippingStreetTwo.Text = existingShipmentAddress.Line2;
        shippingCity.Text = existingShipmentAddress.City;
        shippingPostalCode.Text = existingShipmentAddress.PostalCode;
        shippingPhone.Text = existingShipmentAddress.PhoneNumber;
        shippingMobile.Text = existingShipmentAddress.MobilePhoneNumber;

        shippingCountry.DataSource = countries;
        shippingCountry.DataBind();

        if (billingAddress.Country != null)
        {
            shippingCountry.SelectedValue = existingShipmentAddress.Country.Id.ToString();
        }

        ShippingAddress = existingShipmentAddress;
    }

    private void PopulateBillingAddress(IList<Country> countries)
    {
        var existingBillingAddress = TransactionLibrary.GetBillingInformation();

        billingFirstName.Text = existingBillingAddress.FirstName;
        billingLastName.Text = existingBillingAddress.LastName;
        billingEmail.Text = existingBillingAddress.EmailAddress;
        billingCompany.Text = existingBillingAddress.CompanyName;
        billingAttention.Text = existingBillingAddress.Attention;
        billingStreet.Text = existingBillingAddress.Line1;
        billingStreetTwo.Text = existingBillingAddress.Line2;
        billingCity.Text = existingBillingAddress.City;
        billingPostalCode.Text = existingBillingAddress.PostalCode;
        billingPhone.Text = existingBillingAddress.PhoneNumber;
        billingMobile.Text = existingBillingAddress.MobilePhoneNumber;

        billingCountry.DataSource = countries;
        billingCountry.DataBind();
        if (existingBillingAddress.Country != null)
        {
            billingCountry.SelectedValue = existingBillingAddress.Country.Id.ToString();
        }

        BillingAddress = existingBillingAddress;
    }

    private bool SetupIsNeeded()
    {
        if (IsPostBack || ViewMode.IsDesign() || ViewMode.IsEdit())
        {
            return false;
        }
        if (!TransactionLibrary.HasBasket())
        {
            cartIsEmpty.Visible = true;
            Address.Visible = false;

            return false;
        }

        return true;
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



