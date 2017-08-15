using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.Api;

public partial class CMSWebParts_Ucommerce_Voucher : CMSAbstractWebPart
{
    #region "Properties"

    

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            
        }
    }

    protected void btnAddVoucher_Click(object sender, EventArgs e)
    {
        MarketingLibrary.AddVoucher(txtVoucherCode.Value);
        TransactionLibrary.ExecuteBasketPipeline();

        Response.Redirect(Request.RawUrl);
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



