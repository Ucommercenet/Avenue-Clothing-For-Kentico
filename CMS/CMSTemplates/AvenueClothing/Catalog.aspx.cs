using System;
using CMS.UIControls;
using UCommerce.Extensions;
using UCommerce.Infrastructure;
using UCommerce.Runtime;
using CMS.PortalEngine;
using CMSApp.CMSWebParts.Ucommerce.Services;
using UCommerce.Content;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Catalog : TemplatePage
    {
        private void Page_Load(object sender, EventArgs e)
        {
            if (!SetupIsNeeded())
            {
                return;
            }

            var imageService = ObjectFactory.Instance.Resolve<IImageService>();
            var catalogContext = ObjectFactory.Instance.Resolve<ICatalogContext>();

            if (catalogContext.CurrentCategory == null)
            {
                var defaultCatalogDataProvider = ObjectFactory.Instance.Resolve<IDefaultCatalogDataProvider>();

                catalogContext.CurrentCategory = defaultCatalogDataProvider.GetDefaultCategory();
            }

            CategoryImage.ImageUrl = imageService.GetImage(catalogContext.CurrentCategory.ImageMediaId).Url;
            CategoryName.InnerText = catalogContext.CurrentCategory.DisplayName();
        }

        private bool SetupIsNeeded()
        {
            if (PortalContext.ViewMode.IsDesign() || PortalContext.ViewMode.IsEdit())
            {
                return false;
            }

            return true;
        }
    }
}
