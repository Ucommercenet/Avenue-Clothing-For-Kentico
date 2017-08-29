using CMS;
using CMS.DataEngine;
using CMS.MacroEngine;
using CMSApp.Old_App_Code.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: RegisterModule(typeof(UCommerceModule))]
namespace CMSApp.Old_App_Code.Custom
{
    public class UCommerceModule : Module
    {
        public UCommerceModule() : base("UCommerceModule")
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            MacroContext.GlobalResolver.SetNamedSourceData("uCommerceProduct", UCommerceMacroNamespace.Instance);
            MacroContext.GlobalResolver.SetNamedSourceData("uCommerceShippingMethod", UCommerceMacroNamespace.Instance);
        }
    }
}