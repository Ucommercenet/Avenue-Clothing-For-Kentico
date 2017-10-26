using System;
using CMS.UIControls;
using UCommerce.Extensions;
using UCommerce.Infrastructure;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;
using CMS.PortalEngine;

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

            var imageService = new ImageService();
            var catalogContext = ObjectFactory.Instance.Resolve<ICatalogContext>();
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
