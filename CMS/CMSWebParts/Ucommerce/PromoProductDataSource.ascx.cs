using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;

public partial class CMSWebParts_Ucommerce_PromoProductDataSource : CMSAbstractWebPart
{
    #region "Properties"

    public string AltProductSKU
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AltProductSKU"), "");
        }
        set
        {
            this.SetValue("AltProductSKU", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        srcProducts.AltProductSKUControl = AltProductSKU;
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
            return;

        srcProducts.FilterName = (string)GetValue("WebPartControlID");
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}



