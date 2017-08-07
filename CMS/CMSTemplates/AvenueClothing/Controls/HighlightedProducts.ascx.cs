using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class HighlightedProducts : System.Web.UI.UserControl
    {
        private ICollection<UCommerce.EntitiesV2.Product> _products = new List<UCommerce.EntitiesV2.Product>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _products = UCommerce.EntitiesV2.Product.All()
                .Where(x => x.ProductProperties.Any(
                    pp => pp.ProductDefinitionField.Name == "ShowOnHomepage" && pp.Value == "true")).ToList();

            lvProducts.DataSource = _products;
            lvProducts.DataBind();
        }
    }
}