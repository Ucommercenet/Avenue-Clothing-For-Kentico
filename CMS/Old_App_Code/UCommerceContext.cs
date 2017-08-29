using CMS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSApp.Old_App_Code.Custom
{
    public class UCommerceContext : AbstractContext<UCommerceContext>
    {
        private List<UCommerceProduct> _products = null;
        private List<UCommerceShippingMethod> _shippingMethods = null;

        // ----------------------------------------------------------------------------------------
        //
        // Functions for products
        public static void SetProducts(List<UCommerceProduct> products)
        {
            Current._products = products;
        }
        public static object GetProductValue(string productSku, string propertyName)
        {
            if (!Current._products.Any(p => p.ProductSKU == productSku))
                return null;

            var product = Current._products.First(p => p.ProductSKU == productSku);
            return GetProductValue(product, propertyName);
        }

        public static object GetProductValue(UCommerceProduct productObj, string propertyName)
        {
            var property = productObj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return property.GetValue(productObj);
        }

        // ----------------------------------------------------------------------------------------
        //
        // Functions for shipping methods
        public static void SetShippingMethods(List<UCommerceShippingMethod> shippingMethods)
        {
            Current._shippingMethods = shippingMethods;
        }
        public static object GetShippingMethodValue(string shippingMethodId, string propertyName)
        {
            if (!Current._shippingMethods.Any(s => s.ShippingMethodId == shippingMethodId))
                return null;

            var shippingMethod = Current._shippingMethods.First(s => s.ShippingMethodId== shippingMethodId);
            return GetShippingMethodValue(shippingMethod, propertyName);
        }

        public static object GetShippingMethodValue(UCommerceShippingMethod shippingMethodObj, string propertyName)
        {
            var property = shippingMethodObj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return property.GetValue(shippingMethodObj);
        }
    }
}