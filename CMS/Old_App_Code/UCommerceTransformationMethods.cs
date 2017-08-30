using CMS.Helpers;
using CMS.MacroEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSApp.Old_App_Code.Custom
{
    public class UCommerceTransformationMethods : MacroMethodContainer
    {

        [MacroMethod(typeof(object), "Returns value of specified property for current uCommerce product item.", 1, SpecialParameters = new[] { "ProductSKU" })]
        [MacroMethodParam(0, "PropertyName", typeof(string), "Name of property which value method should return.")]
        [MacroMethodParam(1, "ProductObject", typeof(string), "Product object from which the value should be taken.")]
        public static object GetValue(EvaluationContext context, params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 2:
                    return UCommerceContext.GetProductValue(
                        // take product object from special parameter (implicitly used)
                        ValidationHelper.GetString(parameters[0], string.Empty),
                        ValidationHelper.GetString(parameters[1], string.Empty));

                case 3:
                    return UCommerceContext.GetProductValue(
                        // take product object from second parameter
                        parameters[2] as UCommerceProduct,
                        ValidationHelper.GetString(parameters[1], string.Empty));

                default:
                    throw new NotSupportedException();
            }
        } 

    }
}