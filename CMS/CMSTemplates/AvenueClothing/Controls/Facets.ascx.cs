using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCommerce.Api;
using UCommerce.Runtime;
using UCommerce.Search.Facets;

namespace CMSApp.CMSTemplates.AvenueClothing.Controls
{
    public partial class Facets : System.Web.UI.UserControl
    {
        private bool _anyFacetHits = false;
        public List<Button> _controls = new List<Button>();
        public string _currentQueryStringKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var category = SiteContext.Current.CatalogContext.CurrentCategory;
            var product = SiteContext.Current.CatalogContext.CurrentProduct;
            if (category == null || product != null)
            {
                return;
            }

            IList<Facet> facetsForQuerying = GetFacets();
            IList<Facet> facets = SearchLibrary.GetFacetsFor(category, facetsForQuerying);

            rptFacets.DataSource = facets;
            rptFacets.DataBind();
            FindChildControlsRecursive(rptFacets);
            EnsureCheckboxesAreChecked();

            if (!_anyFacetHits)
            {
                facetsDiv.Visible = false;
            }
        }

        public void FindChildControlsRecursive(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.GetType() == typeof(Button))
                {
                    Button currentButton = (Button)childControl;
                    currentButton.Attributes.Add("class", "checkbox");
                    _controls.Add((Button)childControl);
                }
                else
                {
                    FindChildControlsRecursive(childControl);
                }
            }
        }

        public void btnCheckBox_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> queryStrings = new Dictionary<string, string>();
            var btnSender = (Button)sender;
            foreach (Button currentBtn in _controls)
            {
                if (currentBtn.Attributes["queryStringKey"] == btnSender.Attributes["queryStringKey"] && currentBtn.Attributes["queryStringValue"] == btnSender.Attributes["queryStringValue"])
                {
                    //if the button was checked when it was clicked, then i can just continue, since it should not be checked after
                    if (btnSender.Attributes["class"] == "checkbox checked")
                    {
                        continue;
                    }
                }
                else
                {
                    if (currentBtn.Attributes["class"] != "checkbox checked")
                    {
                        continue;
                    }
                }

                if (queryStrings.Count == 0)
                {
                    queryStrings.Add(currentBtn.Attributes["queryStringKey"], currentBtn.Attributes["queryStringValue"] + "|");
                    continue;
                }

                IEnumerable<KeyValuePair<string, string>> value = queryStrings.Where(x => x.Key == currentBtn.Attributes["queryStringKey"]);

                //if we already have that key in the dictionary
                if (value.Any())
                {
                    var valueHolder = "";
                    if (!queryStrings.TryGetValue(currentBtn.Attributes["queryStringKey"], out valueHolder))
                    {
                        continue;
                    }
                    queryStrings.Remove(currentBtn.Attributes["queryStringKey"]);
                    queryStrings.Add(
                        currentBtn.Attributes["queryStringKey"],
                        valueHolder + currentBtn.Attributes["queryStringValue"] + "|");
                }
                else
                {
                    queryStrings.Add(
                        currentBtn.Attributes["queryStringKey"], currentBtn.Attributes["queryStringValue"] + "|");
                }
            }

            //string redirectUrl = HttpContext.Current.Request.Url.AbsoluteUri.Split('&')[0];
            string redirectUrl =
                CatalogLibrary.GetNiceUrlForCategory(SiteContext.Current.CatalogContext.CurrentCategory, null);
            if (redirectUrl.Last() != '?')
                redirectUrl += "?";

            foreach (var pair in queryStrings)
            {
                redirectUrl += "&" + pair.Key + "=" + pair.Value;
            }

            HttpContext.Current.Response.Redirect(redirectUrl);
        }

        public void EnsureCheckboxesAreChecked()
        {
            List<KeyValuePair<string, string>> queryStrings = new List<KeyValuePair<string, string>>();

            foreach (string key in Request.QueryString.AllKeys)
            {
                queryStrings.Add(new KeyValuePair<string, string>(key, Request.QueryString[key]));
            }

            foreach (var pair in queryStrings)
            {
                string[] values = pair.Value.Split('|');

                if (pair.Key == "category")
                {
                    continue;
                }

                foreach (var value in values)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }

                    //Now i just have to find the corresponding buttons and give them the css class.
                    foreach (Button control in _controls)
                    {
                        if (control.Attributes["queryStringKey"] == pair.Key && control.Attributes["queryStringValue"] == value)
                        {
                            control.Attributes.Add("class", "checkbox checked");
                        }
                    }
                }
            }
        }

        public void rptFacets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            Facet currentItem = (Facet)e.Item.DataItem;
            var litHeadline = (Literal)e.Item.FindControl("litHeadline");
            var rptCheckBoxes = (Repeater)e.Item.FindControl("rptCheckBoxes");

            litHeadline.Text = currentItem.DisplayName;
            _currentQueryStringKey = currentItem.Name;

            rptCheckBoxes.DataSource = currentItem.FacetValues.Where(x => x.Hits > 0);
            rptCheckBoxes.DataBind();
        }

        public void rptCheckBoxes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            FacetValue currentItem = (FacetValue)e.Item.DataItem;

            Button btnCheckBox = (Button)e.Item.FindControl("btnCheckBox");
            btnCheckBox.Attributes.Add("queryStringKey", _currentQueryStringKey);
            btnCheckBox.Attributes.Add("queryStringValue", currentItem.Value);
            btnCheckBox.Text = currentItem.Value + "(" + currentItem.Hits + ")";

            _anyFacetHits = true;
        }

        public static IList<Facet> GetFacets()
        {
            var parameters = new Dictionary<string, string>();
            foreach (var queryString in HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (queryString == null) continue;

                parameters[queryString] = HttpContext.Current.Request.QueryString[queryString];
            }
            if (parameters.ContainsKey("product"))
            {
                parameters.Remove("product");
            }
            if (parameters.ContainsKey("category"))
            {
                parameters.Remove("category");
            }
            if (parameters.ContainsKey("catalog"))
            {
                parameters.Remove("catalog");
            }
            if (parameters.ContainsKey("aliaspath"))
            {
                parameters.Remove("aliaspath");
            }
            if (parameters.ContainsKey("viewmode"))
            {
                parameters.Remove("viewmode");
            }
            var facetsForQuerying = new List<Facet>();

            foreach (var parameter in parameters)
            {
                var facet = new Facet();
                facet.FacetValues = new List<FacetValue>();
                facet.Name = parameter.Key;
                foreach (var value in parameter.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    facet.FacetValues.Add(new FacetValue() { Value = value });
                }
                facetsForQuerying.Add(facet);
            }

            return facetsForQuerying;
        }
    }
}