using CMS.DataEngine;
using CMS.DocumentEngine.Web.UI;
using CMS.Membership;
using CMSApp.Old_App_Code.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using UCommerce;
using UCommerce.Api;
using UCommerce.EntitiesV2;

namespace CMSApp.CMSWebParts.Custom
{
    public partial class UCommerceShippingDataControl : CMSBaseDataSource
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override object GetDataSource(int offset, int maxRecords)
        {
            var data = base.GetDataSource(offset, maxRecords) as List<UCommerceShippingMethod>;

            UCommerceContext.SetShippingMethods(data);

            return data.ToDataSet();
        }
        protected override object GetDataSourceFromDB()
        {
            var data = new List<UCommerceShippingMethod>();

            var currentShippingMethod = TransactionLibrary.GetShippingMethod();
            var currentBasket = TransactionLibrary.GetBasket().PurchaseOrder;
            var shippingCountry = TransactionLibrary.GetShippingInformation().Country;
            var availableShippingMethods = TransactionLibrary.GetShippingMethods(shippingCountry);

            // |
            // | IMPLEMENT IN TRANSFORMATION 
            // V

            //if (availableShippingMethods.Count != 0)
            //{
            //    pPaymentAlert.Visible = false;
            //}
            //else
            //{
            //    string warning =
            //        "WARNING: No payment methods have been configured for " + shippingCountry.Name + " within <a href=\"http://ucommerce.net\">UCommerce</a> administration area.";
            //    litAlert.Text = warning;
            //    btnUpdateShipment.Enabled = false;
            //}

            // --------------------------------------------------------

            foreach (ShippingMethod shippingMethod in availableShippingMethods)
            {
                var price = shippingMethod.GetPriceForCurrency(currentBasket.BillingCurrency);
                var formattedPrice = new Money((price == null ? 0 : price.Price), currentBasket.BillingCurrency);

                string shippingMethodName = shippingMethod.Name + "<text>(</text>" + formattedPrice + "<text>)</text>, ";
                string shippingMethodId = shippingMethod.Id.ToString();

                data.Add(new UCommerceShippingMethod {ShippingMethodName = shippingMethodName, ShippingMethodId = shippingMethodId }); 
            }

            return data;
        }
    }
}