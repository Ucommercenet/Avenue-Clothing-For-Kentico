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
using CMS.PortalEngine.Web.UI;
using CMS.Helpers;

public partial class CMSWebParts_Ucommerce_BasketPreview : CMSAbstractWebPart
{
    #region "Properties"

    private int _currentIteration = 0;



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

            bool orderlineItemNo = ValidationHelper.GetBoolean(GetValue("OrderlineItemNo"), true);
            bool orderlinePrice = ValidationHelper.GetBoolean(GetValue("OrderlinePrice"), true);
            bool orderlineVat = ValidationHelper.GetBoolean(GetValue("OrderlineVAT"), true);
            bool orderlineQuantity = ValidationHelper.GetBoolean(GetValue("OrderlineQuantity"), true);
            bool orderSubTotal = ValidationHelper.GetBoolean(GetValue("OrderSubTotal"), true);
            bool orderVat = ValidationHelper.GetBoolean(GetValue("OrderVat"), true);
            bool orderDiscount = ValidationHelper.GetBoolean(GetValue("OrderDiscount"), true);
            bool orderShipping = ValidationHelper.GetBoolean(GetValue("OrderShipping"), true);
            bool orderPayment = ValidationHelper.GetBoolean(GetValue("OrderPayment"), true);


            if (!orderlinePrice) { price.InnerText = ""; }
            if (!orderlineVat) { vat.InnerText = ""; }
            if (!orderlineQuantity) { quantity.InnerText = ""; }
            if (!orderlineItemNo) { itemno.InnerText = ""; }

            Page.ClientScript.RegisterStartupScript(
                GetType(), "UCommerce.DemoStore.cart", "<script src=\"~/AvenueClothing/scripts/UCommerce.DemoStore.cart.js\"></script>");

            PurchaseOrder basket = null;

            if (TransactionLibrary.HasBasket())
            {
                basket = TransactionLibrary.GetBasket(false).PurchaseOrder;
            }

            if (basket == null || basket.OrderLines.Count == 0)
            {
                test.Visible = true;
                cartPanel.Visible = false;

                return;
            }

            Currency currency = basket.BillingCurrency;

            var subTotal = new Money(0, currency);
            var tax = new Money(0, currency);
            var discount = new Money(0, currency);
            var shipping = new Money(0, currency);
            var payment = new Money(0, currency);
            var orderTotal = new Money(0, currency);

            if (basket.SubTotal.HasValue)
            {
                subTotal = new Money(basket.SubTotal.Value, currency);
            }
            if (basket.VAT.HasValue)
            {
                tax = new Money(basket.VAT.Value, currency);
            }
            if (basket.DiscountTotal.HasValue)
            {
                discount = new Money(basket.DiscountTotal.Value * -1, currency);
            }
            if (basket.ShippingTotal.HasValue)
            {
                shipping = new Money(basket.ShippingTotal.Value, currency);
            }
            if (basket.PaymentTotal.HasValue)
            {
                payment = new Money(basket.PaymentTotal.Value, currency);
            }
            if (basket.OrderTotal.HasValue)
            {
                orderTotal = new Money(basket.OrderTotal.Value, currency);
            }

            litSubtotal.Text = subTotal.ToString();
            litTax.Text = tax.ToString();
            litDiscount.Text = discount.ToString();
            litShipping.Text = shipping.ToString();
            litPayment.Text = payment.ToString();
            litTotal.Text = orderTotal.ToString();

            subOrder.Visible = orderSubTotal;
            vatOrder.Visible = orderVat;
            discountOrder.Visible = orderDiscount;
            shippingOrder.Visible = orderShipping;
            paymentOrder.Visible = orderPayment;


            rptCart.DataSource = basket.OrderLines;
            rptCart.DataBind();
        }
    }

    public void btnUpdateQuantities_Click(object sender, EventArgs e)
    {
        var senderButton = (Button)sender;
        var orderLineNumber = senderButton.Attributes["orderlinenumber"];
        var repeaterItem = (RepeaterItem)senderButton.NamingContainer;
        TextBox txtQuantity = (TextBox)repeaterItem.FindControl("txtQuantity" + orderLineNumber);

        UpdateCartLine(orderLineNumber, txtQuantity.Text);

        Response.Redirect(Request.RawUrl);
    }

    public void btnRemoveLine_Click(object sender, EventArgs e)
    {
        var senderButton = (Button)sender;
        var orderLineNumber = senderButton.Attributes["orderlinenumber"];

        UpdateCartLine(orderLineNumber, "0");

        Response.Redirect(Request.RawUrl);
    }

    public void btnUpdateAll_Click(object sender, EventArgs e)
    {
        var allButtons = new List<Button>();
        int currentIt = 0;

        //Everytime the repeater runs, it creates a new repeaterItem. I only create 1 button for each time the repeater runs, so the current iteration of the repeater gives the id of the button.
        foreach (RepeaterItem rptItem in rptCart.Items)
        {
            Button currentControl = (Button)rptItem.FindControl("btnUpdateQuantities" + currentIt);

            if (currentControl != null)
            {
                allButtons.Add(currentControl);
            }
            currentIt++;
        }

        foreach (Button senderButton in allButtons)
        {
            var orderLineNumber = senderButton.Attributes["orderlinenumber"];
            var repeaterItem = (RepeaterItem)senderButton.NamingContainer;
            TextBox txtQuantity = (TextBox)repeaterItem.FindControl("txtQuantity" + orderLineNumber);

            UpdateCartLine(orderLineNumber, txtQuantity.Text);
        }

        Response.Redirect(Request.RawUrl);
    }

    public static bool? UpdateCartLine(string lineNumberString, string quantityString)
    {
        var basket = TransactionLibrary.GetBasket().PurchaseOrder;
        int lineNumber = 0;
        int quantity = 0;

        if (!Int32.TryParse(lineNumberString, out lineNumber) || !Int32.TryParse(quantityString, out quantity))
        {
            //if we cant parse the input to ints, we cant go on
            return false;
        }

        var listOfOrderLineIds = basket.OrderLines.Select(x => x.OrderLineId).ToList();
        var currentOrderLineId = listOfOrderLineIds[lineNumber];

        TransactionLibrary.UpdateLineItem(currentOrderLineId, quantity);

        TransactionLibrary.ExecuteBasketPipeline();
        return true;
    }

    public void CartRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        bool orderlinePrice = ValidationHelper.GetBoolean(GetValue("OrderlinePrice"), true);
        bool orderlineVat = ValidationHelper.GetBoolean(GetValue("OrderlineVAT"), true);
        bool orderlineQuantity = ValidationHelper.GetBoolean(GetValue("OrderlineQuantity"), true);
        bool orderlineItemNo = ValidationHelper.GetBoolean(GetValue("OrderlineItemNo"), true);
        bool editable = ValidationHelper.GetBoolean(GetValue("Editable"), true);

        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        {
            return;
        }

        Currency currency = TransactionLibrary.GetBasket(true).PurchaseOrder.BillingCurrency;

        OrderLine currentItem = (OrderLine)e.Item.DataItem;
        var product = CatalogLibrary.GetProduct(currentItem.Sku);
        var itemPrice = new Money(currentItem.Price, currency);
        var itemTax = new Money(currentItem.VAT, currency);
        var lineTotal = new Money(currentItem.Total.Value, currency);

        var lnkItemLink = (HyperLink)e.Item.FindControl("lnkItemLink");
        var litPrice = (Literal)e.Item.FindControl("litPrice");
        var litVat = (Literal)e.Item.FindControl("litVat");
        var litTotal = (Literal)e.Item.FindControl("litTotal");
        var txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
        var btnUpdateQuantities = (Button)e.Item.FindControl("btnUpdateQuantities");
        var btnRemoveLine = (Button)e.Item.FindControl("btnRemoveLine");
        var lblQuantity = (Label)e.Item.FindControl("lblQuantity");
        var litItemNo = (Literal)e.Item.FindControl("litItemNo");


        string url = CatalogLibrary.GetNiceUrlForProduct(product, product.GetCategories().FirstOrDefault());

        lnkItemLink.Text = currentItem.ProductName;
        lnkItemLink.NavigateUrl = url;

        if (currentItem.UnitDiscount.HasValue && currentItem.UnitDiscount > 0)
        {
            var nowPrice = new Money((currentItem.Price - currentItem.UnitDiscount.Value), currency);
            litPrice.Text = "<span style=\"text-decoration: line-through\">" + itemPrice + "</span>" + nowPrice;
        }
        else
        {
            litPrice.Text = itemPrice.ToString();
        }
        litVat.Text = itemTax.ToString();
        litTotal.Text = lineTotal.ToString();
        txtQuantity.Text = currentItem.Quantity.ToString();
        txtQuantity.Attributes.Add("orderlinenumber", _currentIteration.ToString());
        btnUpdateQuantities.Attributes.Add("orderlinenumber", _currentIteration.ToString());
        btnRemoveLine.Attributes.Add("orderlinenumber", _currentIteration.ToString());
        btnUpdateQuantities.ID = "btnUpdateQuantities" + _currentIteration;
        txtQuantity.ID = "txtQuantity" + _currentIteration;
        txtQuantity.Attributes.Add("data-orderlineid", currentItem.OrderLineId.ToString());

        litPrice.Visible = orderlinePrice;
        litVat.Visible = orderlineVat;
        txtQuantity.Visible = orderlineQuantity;
        btnUpdateQuantities.Visible = orderlineQuantity;
        litItemNo.Text = currentItem.Sku;
        litItemNo.Visible = orderlineItemNo;
        if (!editable)
        {
            btnRemoveLine.Visible = false;
            btnUpdateQuantities.Visible = false;
            txtQuantity.Attributes.Add("readonly", "true");
            txtQuantity.CssClass += " textbox-to-label";
        }

        _currentIteration++;
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



