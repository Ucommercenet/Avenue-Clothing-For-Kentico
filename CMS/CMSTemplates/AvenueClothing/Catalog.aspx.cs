using System;
using System.Linq;
using CMS.UIControls;
using UCommerce.Extensions;
using UCommerce.Infrastructure;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;
using CMS.PortalEngine;
using UCommerce.Content;
using UCommerce.EntitiesV2;

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
                catalogContext.CurrentCategory = catalogContext.CurrentCatalog.Categories.FirstOrDefault(x => x.DisplayOnSite && !x.Deleted);
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
