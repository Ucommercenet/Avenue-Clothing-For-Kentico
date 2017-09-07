using CMS.DataEngine;
using CMS.DocumentEngine.Web.UI;
using CMS.Membership;
using CMSApp.Old_App_Code.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UCommerce;
using UCommerce.Api;
using UCommerce.EntitiesV2;

namespace CMSApp.CMSWebParts.Custom
{
    public partial class UCommerceOrderlinesDataControl : CMSBaseDataSource
    {

        ICollection<OrderLine> _orderlines = new List<OrderLine>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override object GetDataSource(int offset, int maxRecords)
        {
            var data = base.GetDataSource(offset, maxRecords) as List<UCommerceOrderline>;

            UCommerceContext.SetOrderlines(data);

            return data.ToDataSet();
        }
        protected override object GetDataSourceFromDB()
        {
            var data = new List<UCommerceOrderline>();

            Currency currency = TransactionLibrary.GetBasket(true).PurchaseOrder.BillingCurrency;
            var basket = TransactionLibrary.GetBasket().PurchaseOrder;
            _orderlines = basket.OrderLines;

            int iteration = 0;
            foreach(OrderLine o in _orderlines)
            {
                var product = CatalogLibrary.GetProduct(o.Sku);
                string url = CatalogLibrary.GetNiceUrlForProduct(product, product.GetCategories().FirstOrDefault());
                var price = new Money(o.Price, currency);
                var vat = new Money(o.VAT, currency);
                var total = new Money(o.Total.Value, currency);
                data.Add(new UCommerceOrderline { OrderlineId = o.OrderLineId, OrderlineNumber = iteration.ToString(), ProductName = o.ProductName, ProductSKU = o.Sku, VariantSKU = o.VariantSku, ProductLink = url , Price = price, Vat = vat, Total = total, Quantity = o.Quantity });
                iteration++;
            }

            return data;
        }
    }
}