using System;
using System.Web.UI;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class CategoryNavigation : UserControl
    {
        private Category _currentCategory = SiteContext.Current.CatalogContext.CurrentCategory;

        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryNavigationList.InnerHtml = "";
            foreach (Category currentCategory in CatalogLibrary.GetRootCategories())
            {
                RecursiveMenu(currentCategory);
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

            CategoryNavigationList.InnerHtml += "<a class=\"nav-link\" href=\"" + CatalogLibrary.GetNiceUrlForCategory(currentCategory) +
                                            "\">" + currentCategory.Name + "</a>";
            foreach (Category category in currentCategory.Categories)
            {
                RecursiveMenu(category);
            }

            CategoryNavigationList.InnerHtml += "</li>";
            CategoryNavigationList.InnerHtml += "</ul>";
     
        }
        
    }
}