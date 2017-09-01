using CMS.DataEngine;
using System.Collections.Generic;
using System;

namespace CMSApp.Old_App_Code.Custom
{
    public class UCommerceProduct
    {
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public List<UCommerceProduct> RelatedProducts { get; set; }
    }
}