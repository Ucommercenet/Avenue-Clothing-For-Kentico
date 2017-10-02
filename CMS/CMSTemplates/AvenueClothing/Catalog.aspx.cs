using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.UIControls;
using UCommerce.Api;
using UCommerce.Catalog;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Infrastructure;
using UCommerce.Kentico.Content;
using UCommerce.Runtime;
using UCommerce.Search.Facets;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Catalog : TemplatePage
    {

        private ICollection<Product> _products = new List<Product>();

        private void Page_Load(object sender, EventArgs e)
        {
            var viewMode = Convert.ToInt32(Request.QueryString["viewmode"]);

            if (viewMode == 6 || viewMode == 3)
            {
                return;
            }
        }

    }
}
