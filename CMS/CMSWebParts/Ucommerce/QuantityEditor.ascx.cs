using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using UCommerce.Api;
using System.Linq;

public partial class CMSWebParts_Ucommerce_QuantityEditor : CMSAbstractWebPart
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

    public int Quantity
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Quantity"), 0);
        }
        set
        {
            this.SetValue("Quantity", value);
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
            var txtQuantity = (TextBox)FindControl("txtQuantity");
            int quantity = Quantity;
            txtQuantity.Text = quantity.ToString();
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

    public void btnUpdateQuantities_Click(object sender, EventArgs e)
    {
        var senderButton = (Button)sender;
        var orderLineNumber = OrderlineNumber;
        var repeaterItem = (RepeaterItem)senderButton.NamingContainer;
        TextBox txtQuantity = (TextBox)repeaterItem.FindControl("txtQuantity" + orderLineNumber);

        var didItSucceed = UpdateCartLine(orderLineNumber, txtQuantity.Text);

        Response.Redirect(Request.RawUrl);
    }

    public static bool? UpdateCartLine(string lineNumberString, string quantityString)
    {
        var basket = TransactionLibrary.GetBasket().PurchaseOrder;
        int lineNumber = 0;
        int quantity = 0;

        if (!Int32.TryParse(lineNumberString, out lineNumber) || !Int32.TryParse(quantityString, out quantity))
        {
            //if we cant parse the input to ints, we cant go on
            return false;
        }

        var listOfOrderLineIds = basket.OrderLines.Select(x => x.OrderLineId).ToList();
        var currentOrderLineId = listOfOrderLineIds[lineNumber];

        TransactionLibrary.UpdateLineItem(currentOrderLineId, quantity);

        TransactionLibrary.ExecuteBasketPipeline();
        return true;
    }

    #endregion
}



