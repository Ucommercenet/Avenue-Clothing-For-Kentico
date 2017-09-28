using System;
using CMS.UIControls;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Email : TemplateMasterPage
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            PageManager = portalManager;
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}