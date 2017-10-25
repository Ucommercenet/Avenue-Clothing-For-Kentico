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
    public partial class Basket : TemplatePage
    {

        private void Page_Load(object sender, EventArgs e)
        {
            //todo: uncomment this when the javascript stuff is ready
            Page.ClientScript.RegisterStartupScript(
                GetType(), "UCommerce.DemoStore.cart", "<script src=\"~/AvenueClothing/scripts/UCommerce.DemoStore.cart.js\"></script>");            
        }
    }
}