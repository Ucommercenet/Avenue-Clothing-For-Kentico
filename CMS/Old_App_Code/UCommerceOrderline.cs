using CMS.DataEngine;
using System.Collections.Generic;
using System;
using UCommerce;
using UCommerce.EntitiesV2;

namespace CMSApp.Old_App_Code.Custom
{
    public class UCommerceOrderline
    {
        public int OrderlineId { get; set; }
        //public Product Product { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string VariantSKU { get; set; }
        public string ProductLink { get; set; }
        public Money Price { get; set; }
        public Money Vat { get; set; }
        public Money Total { get; set; }
        public int Quantity { get; set; }


    }
}