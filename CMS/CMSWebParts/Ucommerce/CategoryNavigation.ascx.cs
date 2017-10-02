using CMS.PortalEngine.Web.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;

public partial class CMSWebParts_Ucommerce_CategoryNavigation : CMSAbstractWebPart
{
    #region "Properties"
    private Category _currentCategory = SiteContext.Current.CatalogContext.CurrentCategory;


    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {

            CategoryNavigationList.InnerHtml = "";
            foreach (Category currentCategory in CatalogLibrary.GetRootCategories())
            {
                RecursiveMenu(currentCategory);
            }

        }
    }

    private void RecursiveMenu(Category currentCategory)
    {
        CategoryNavigationList.InnerHtml += "<ul class=\"nav nav-list\">";

        if (_currentCategory != null && _currentCategory.Id == currentCategory.Id)
        {
            CategoryNavigationList.InnerHtml += "<li class=\"on\">";
        }
        else
        {
            CategoryNavigationList.InnerHtml += "<li class=\"nav-item\">";
        }

        CategoryNavigationList.InnerHtml += "<a class=\"catnav\" href=\"" + CatalogLibrary.GetNiceUrlForCategory(currentCategory) +
                                        "\">" + currentCategory.Name + "</a>";
        foreach (Category category in currentCategory.Categories)
        {
            RecursiveMenu(category);
        }

        CategoryNavigationList.InnerHtml += "</li>";
        CategoryNavigationList.InnerHtml += "</ul>";

    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}



