using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;

public partial class CMSWebParts_Ucommerce_ProductDataSource : CMSAbstractWebPart
{
    #region "Properties"



    #endregion


    #region "Methods"


    public string DataFiltering
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DataFiltering"), "");
        }
        set
        {
            this.SetValue("DataFiltering", value);
        }
    }

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        srcProducts.DataFilteringControl = DataFiltering;
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



