using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;


[assembly: PreApplicationStartMethod(typeof(AvenueClothing.Installer.App_Start.PreApplicationStartCode), "Install")]
namespace AvenueClothing.Installer.App_Start
{
    public class PreApplicationStartCode
    {
        public static void Install()
        {
            DynamicModuleUtility.RegisterModule(typeof(Installer));
        }
    }
}
