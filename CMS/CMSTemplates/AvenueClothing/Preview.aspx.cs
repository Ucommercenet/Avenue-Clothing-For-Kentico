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
            if (IsPostBack)
            {
                return;
            }
            var billingAddress = TransactionLibrary.GetBillingInformation();
            var shipmentAddress = TransactionLibrary.GetShippingInformation();
            var basket = TransactionLibrary.GetBasket(true).PurchaseOrder;

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

            Currency currency = basket.BillingCurrency;

            var subTotal = new Money(basket.SubTotal.Value, currency);
            var tax = new Money(basket.VAT.Value, currency);
            var discount = new Money(basket.DiscountTotal.Value * -1, currency);
            var shippingTotal = new Money(basket.ShippingTotal.Value, currency);
            var paymentTotal = new Money(basket.PaymentTotal.Value, currency);
            var orderTotal = new Money(basket.OrderTotal.Value, currency);
            var shipments = basket.Shipments;


            if (basket.SubTotal.HasValue)
            {
                subTotal = new Money(basket.SubTotal.Value, currency);
            }

            if (basket.VAT.HasValue)
            {
                tax = new Money(basket.VAT.Value, currency);
            }

            if (basket.OrderTotal.HasValue)
            {
                orderTotal = new Money(basket.OrderTotal.Value, currency);
            }

            if (discount.Value > 0)
            {
                litDiscount.Text = discount.ToString();
            }
            else
            {
                trDiscounts.Visible = false;
            }

            var rowspan = 3;
            if (discount.Value > 0)
            {
                rowspan++;
            }
            if (shippingTotal.Value > 0)
            {
                rowspan++;
            }
            if (paymentTotal.Value > 0)
            {
                rowspan++;
            }

            litRowSpan.Text = "<td rowspan=\"" + rowspan + "\" colspan=\"2\"></td>";

            string shipmentString = "";
            if (shippingTotal.Value > 0)
            {
                if (shipments.Count > 1)
                {
                    shipmentString += "<ul>";
                    foreach (var shipment in shipments)
                    {
                        shipmentString += "<li>" + shipment.ShipmentName + "</li>";
                    }
                    shipmentString += "</ul>";
                }
                else
                {
                    shipmentString += "<text> (via" + shipments.First().ShipmentName + ")</text>";
                }
                litShipments.Text = shipmentString;

                litShippingTotal.Text = shippingTotal.ToString();
            }
            else
            {
                trShipping.Visible = false;
            }

            if (paymentTotal.Value > 0)
            {
                string paymentString = "";
                if (basket.Payments.Count > 1)
                {
                    paymentString += "<ul>";
                    foreach (var payment in basket.Payments)
                    {
                        paymentString += "<li>" + payment.PaymentMethodName + "</li>";
                    }
                }
                else
                {
                    paymentString += "<text> (" + basket.Payments.First().PaymentMethodName + ")</text>";
                }
                litPaymentMethods.Text = paymentString;

                litPaymentTotal.Text = paymentTotal.ToString();
            }
            else
            {
                trPaymentTotal.Visible = false;
            }

            litSubTotal.Text = subTotal.ToString();
            litVat.Text = tax.ToString();
            litOrderTotal.Text = orderTotal.ToString();

            rptPreviewItems.DataSource = basket.OrderLines;
            rptPreviewItems.DataBind();
        }

        public void btnContinue_Click(object sender, EventArgs e)
        {
            TransactionLibrary.ExecuteBasketPipeline();
            TransactionLibrary.RequestPayments();
            HttpContext.Current.Response.Redirect("~/Basket/Confirmation");
        }

        public void rptPreviewItems_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            Currency currency = TransactionLibrary.GetBasket(true).PurchaseOrder.BillingCurrency;

            OrderLine currentItem = (OrderLine)e.Item.DataItem;
            var itemPrice = new Money(currentItem.Price, currency);
            var itemTax = new Money(currentItem.VAT, currency);
            var lineTotal = new Money(currentItem.Total.Value, currency);

            var litItemName = (Literal)e.Item.FindControl("litItemName");
            var litItemSku = (Literal)e.Item.FindControl("litItemSku");
            var litPrice = (Literal)e.Item.FindControl("litPrice");
            var litVat = (Literal)e.Item.FindControl("litVat");
            var litQuantity = (Literal)e.Item.FindControl("litQuantity");
            var litTotal = (Literal)e.Item.FindControl("litTotal");

            litItemSku.Text = currentItem.Sku;
            litItemName.Text = currentItem.ProductName;

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
            litQuantity.Text = currentItem.Quantity.ToString();
        }
    }
}