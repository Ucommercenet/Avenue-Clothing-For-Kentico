using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UIControls;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class CMSTemplates_AvenueClothing_Main : TemplateMasterPage
    {

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            PageManager = portalManager;
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}