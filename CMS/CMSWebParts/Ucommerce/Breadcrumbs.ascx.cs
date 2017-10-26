using System.Collections.Generic;
using System.Linq;
using CMS.Helpers;
using CMS.PortalEngine;
using CMS.PortalEngine.Web.UI;
using Microsoft.Ajax.Utilities;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Runtime;

public partial class CMSWebParts_Ucommerce_Breadcrumbs : CMSAbstractWebPart
{
    #region "Properties"

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
            var delimiter = GetStringValue("Delimiter", ">");
            bool includeKenticoNodes = ValidationHelper.GetBoolean(this.GetValue("IncludeKenticoNodes"), true);

            var breadcrumbs = new List<BreadcrumbViewModel>();

            if (includeKenticoNodes)
            {
                var documentsOnPath = CurrentDocument.DocumentsOnPath;

                foreach (var node in documentsOnPath)
                {
                    if (!node.NodeAlias.IsNullOrWhiteSpace() &&
                        PageTemplateInfoProvider.GetPageTemplateInfo(node.GetUsedPageTemplateId()).DisplayName != "Catalog" &&
                        PageTemplateInfoProvider.GetPageTemplateInfo(node.GetUsedPageTemplateId()).DisplayName != "Product")
                    {
                        breadcrumbs.Add(
                            new BreadcrumbViewModel
                            {
                                BreadcrumbName = node.NodeAlias,
                                BreadcrumbUrl = node.AbsoluteURL
                            });
                        breadcrumbs.Add(new BreadcrumbViewModel
                        {
                            BreadcrumbName = delimiter
                        });
                    }
                }
            }

            var delimiterBreadcrumb = new BreadcrumbViewModel
            {
                BreadcrumbName = delimiter
            };

            Product product = SiteContext.Current.CatalogContext.CurrentProduct;
            Category lastCategory = SiteContext.Current.CatalogContext.CurrentCategory;

            if (product != null || lastCategory != null)
            {
                breadcrumbs.Add(new BreadcrumbViewModel
                {
                    BreadcrumbName = SiteContext.Current.CatalogContext.CurrentCatalog.Name,
                    BreadcrumbUrl = ""
                });
                breadcrumbs.Add(delimiterBreadcrumb);
            }

            if (SiteContext.Current.CatalogContext.CurrentCategories.Any())
            {
                foreach (var category in SiteContext.Current.CatalogContext.CurrentCategories)
                {
                    var breadcrumb = new BreadcrumbViewModel
                    {
                        BreadcrumbName = category.DisplayName(),
                        BreadcrumbUrl = CatalogLibrary.GetNiceUrlForCategory(category)
                    };

                    lastCategory = category;
                    breadcrumbs.Add(breadcrumb);

                    if (category == SiteContext.Current.CatalogContext.CurrentCategory && product == null)
                    {
                        break;
                    }
                    breadcrumbs.Add(delimiterBreadcrumb);
                }
            }

            if (product != null)
            {
                var breadcrumb = new BreadcrumbViewModel
                {
                    BreadcrumbName = product.DisplayName(),
                    BreadcrumbUrl = CatalogLibrary.GetNiceUrlForProduct(product, lastCategory)
                };
                breadcrumbs.Add(breadcrumb);
            }

            Breadcrumbs.DataSource = breadcrumbs;
            Breadcrumbs.DataBind();
        }
    }

    public class BreadcrumbViewModel
    {
        public string BreadcrumbName { get; set; }
        public string BreadcrumbUrl { get; set; }
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