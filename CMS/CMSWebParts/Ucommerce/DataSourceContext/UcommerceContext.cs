using System.Collections.Generic;
using System.Linq;
using CMS.Base;

namespace CMSApp.CMSWebParts.Ucommerce.DataSourceContext
{
    public class UcommerceContext: AbstractContext<UcommerceContext>
        {
            private List<UcommerceProductDto> _products;

            public static void SetProducts(List<UcommerceProductDto> products)
            {
                Current._products = products;
            }

            public static object GetProductValue(string productSku, string propertyName)
            {
                if (Current._products.All(p => p.ProductSku != productSku))
                    return null;

                var product = Current._products.First(p => p.ProductSku == productSku);
                return GetProductValue(product, propertyName);
            }

            public static object GetProductValue(UcommerceProductDto productObj, string propertyName)
            {
                var property = productObj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                return property.GetValue(productObj);
            }
        }
    

}