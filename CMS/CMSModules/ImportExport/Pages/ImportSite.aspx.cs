using System;

using CMS.Base.Web.UI;
using CMS.Core;
using CMS.FormEngine.Web.UI;
using CMS.UIControls;
using CMS.DataEngine;

[UIElement(ModuleName.CMS, "ImportSiteOrObjects")]
public partial class CMSModules_ImportExport_Pages_ImportSite : CMSImportExportPage
{
    private const string CI_SETTINGS_KEY = "CMSEnableCI";
    

    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return pnlMessages;
        }
        set
        {
            pnlMessages = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initializes PageTitle
        titleElem.Breadcrumbs.AddBreadcrumb(new BreadcrumbItem()
        {
            Text = GetString("general.sites"),
            RedirectUrl = UIContextHelper.GetElementUrl(ModuleName.CMS, "Sites", false)
        });

        titleElem.Breadcrumbs.AddBreadcrumb(new BreadcrumbItem()
        {
            Text = GetString("ImportSite.ImportSite")
        });

        titleElem.TitleText = GetString("ImportSite.Title");

        var isCIEnabled = SettingsKeyInfoProvider.GetBoolValue(CI_SETTINGS_KEY);
        if (isCIEnabled)
        {
            ShowWarning(GetString("importsite.cienabled.warning"));
        }
    }
}
