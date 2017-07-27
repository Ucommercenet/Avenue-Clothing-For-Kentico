using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UIControls;
using UCommerce.EntitiesV2;

namespace CMSApp.CMSTemplates.AvenueClothing
{
    public partial class Search : TemplatePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var keyword = HttpContext.Current.Request.QueryString["search"];
            IEnumerable<Product> products = new List<Product>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                products = Product.Find(p =>
                    p.VariantSku == null
                    && p.DisplayOnSite
                    &&
                    (
                        p.Sku.Contains(keyword)
                        || p.Name.Contains(keyword)
                        || p.ProductDescriptions.Any(d => d.DisplayName.Contains(keyword)
                                                          || d.ShortDescription.Contains(keyword)
                                                          || d.LongDescription.Contains(keyword)
                        )
                    )
                );
            }

            lvProducts.DataSource = products;
            lvProducts.DataBind();
        }
    }
}