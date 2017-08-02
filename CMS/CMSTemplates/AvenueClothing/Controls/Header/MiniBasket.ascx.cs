using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls.Header
{
    public partial class MiniBasket : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PurchaseOrder basket = null;
            var currency = SiteContext.Current.CatalogContext.CurrentCatalog.PriceGroup.Currency;
            var orderTotal = new Money(0, currency);

            if (TransactionLibrary.HasBasket())
            {
                basket = TransactionLibrary.GetBasket(false).PurchaseOrder;
                if (basket.OrderTotal.HasValue)
                {
                    orderTotal = new Money(basket.OrderTotal.Value, currency);
                }
            }

            var applicationUrl = HttpContext.Current.Request.ApplicationPath;

            var minicartString = "";
            if (basket == null || !basket.OrderLines.Any())
            {
                minicartString += "<a id=\"empty-cart\">Your Shopping Cart is empty</a>";
            }
            else
            {
                minicartString +=
                    "<a href=\""+ applicationUrl +"/basket\" id=\"mini-cart\"><i class=\"icon-shopping-cart icon-white\"></i><span class=\"item-qty\">"
                    + basket.OrderLines.Sum(x => x.Quantity).ToString("#,##")
                    + "</span> items in cart, total: <span class=\"order-total\">" + orderTotal + "</span></a>";
            }

            litMinicart.Text = minicartString;

        }
    }
}