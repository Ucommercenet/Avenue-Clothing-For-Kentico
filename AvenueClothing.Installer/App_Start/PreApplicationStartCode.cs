using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Hosting;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(AvenueClothing.Installer.App_Start.PreApplicationStartCode), "Install")]
namespace AvenueClothing.Installer.App_Start
{
    public class PreApplicationStartCode
    {
         static string readPath = HostingEnvironment.MapPath("~/bin/uCommerce/installWasRun.txt");
        
        public static bool _installationWasRun;
        public static void Install()
        {
            _installationWasRun = File.Exists(readPath);
            DynamicModuleUtility.RegisterModule(typeof(Installer));
        }
    }
}
