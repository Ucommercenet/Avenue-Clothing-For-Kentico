using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;

public partial class CMSWebParts_Ucommerce_DeleteOrderline : CMSAbstractWebPart
{
    #region "Properties"

    public string OrderlineNumber
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderlineNumber"), "0");
        }
        set
        {
            this.SetValue("OrderlineNumber", value);
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
            string orderlineNumber = OrderlineNumber;
            //string orderlineNumber = ValidationHelper.GetString(GetValue("OrderlineNumber"), "0");
            btnRemoveLine.Attributes.Add("orderlinenumber", orderlineNumber);
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    //public void btnUpdateQuantities_Click(object sender, EventArgs e)
    //{
    //    var senderButton = (Button)sender;
    //    var orderLineNumber = senderButton.Attributes["orderlinenumber"];
    //    var repeaterItem = (RepeaterItem)senderButton.NamingContainer;
    //    TextBox txtQuantity = (TextBox)repeaterItem.FindControl("txtQuantity" + orderLineNumber);

    //    var didItSucceed = UpdateCartLine(orderLineNumber, txtQuantity.Text);

    //    Response.Redirect(Request.RawUrl);
    //}

    //public void btnRemoveLine_Click(object sender, EventArgs e)
    //{
    //    var senderButton = (Button)sender;
    //    var orderLineNumber = senderButton.Attributes["orderlinenumber"];

    //    var didItSucceed = UpdateCartLine(orderLineNumber, "0");

    //    Response.Redirect(Request.RawUrl);
    //}

    #endregion
}



