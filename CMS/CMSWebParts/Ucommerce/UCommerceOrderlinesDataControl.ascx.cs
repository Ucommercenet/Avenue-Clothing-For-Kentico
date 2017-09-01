using CMS.DataEngine;
using CMS.DocumentEngine.Web.UI;
using CMS.Membership;
using CMSApp.Old_App_Code.Custom;
using System;
using System.Collections.Generic;
using System.Data;

namespace CMSApp.CMSWebParts.Custom
{
    public partial class UCommerceOrderlinesDataControl : CMSBaseDataSource
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override object GetDataSource(int offset, int maxRecords)
        {
            var data = base.GetDataSource(offset, maxRecords) as List<UCommerceOrderline>;

            UCommerceContext.SetOrderlines(data);

            return data.ToDataSet();
        }
        protected override object GetDataSourceFromDB()
        {
            var ipad = new UCommerceOrderline
            {
                OrderlineId = "ipadpro97"
            };
            var iphone = new UCommerceOrderline
            {
                OrderlineId = "iphone7"
            };
            var macbook = new UCommerceOrderline
            {
                OrderlineId = "macbookpro",
            };
            var data = new List<UCommerceOrderline>
            {
                ipad,
                iphone,
                macbook
            };

            return data;

        }
    }
}