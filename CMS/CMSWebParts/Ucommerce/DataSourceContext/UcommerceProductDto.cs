using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSApp.CMSWebParts.Ucommerce.DataSourceContext
{
    public class UcommerceProductDto
    {
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Tax { get; set; }
    }
}