<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmationEmail.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.ConfirmationEmail" %>

<html lang="en">
<head id="head" runat="server" enableviewstate="false">
    <title>Email Confirmation</title>
</head>
<body>
    <div runat="server" id="contentDiv">
        <div class="row-fluid well">
            <div class="span6">
                <h3>Billing address</h3>
                <br />
                <address>
                    <strong>
                        <asp:Literal ID="litBillingName" runat="server" />
                    </strong>
                    <br />
                    <asp:Literal ID="litBillingStreet" runat="server" /><br />
                    <asp:Literal ID="litBillingPostalCode" runat="server" />
                    <asp:Literal ID="litBillingCity" runat="server" /><br />
                    <asp:Literal ID="litBillingCountry" runat="server" />
                    <asp:Literal ID="litBillingAttention" runat="server" /><br />
                    <abbr title="Phone">P:</abbr><asp:Literal ID="litBillingPhone" runat="server" /><br />
                    <abbr title="Mobile">M:</abbr><asp:Literal ID="litBillingMobilePhone" runat="server" /><br />
                    <abbr title="Email">E:</abbr><asp:HyperLink ID="lnkBillingMail" runat="server"></asp:HyperLink>
                </address>
            </div>

            <div id="divShippingAddress" runat="server" class="span6">
                <h3>Shipping address</h3>
                <br />
                <address>
                    <strong>
                        <asp:Literal ID="litShippingName" runat="server" />
                    </strong>
                    <br />
                    <asp:Literal ID="litShippingCompany" runat="server" />
                    <asp:Literal ID="litShippingAttention" runat="server" /><br />
                    <asp:Literal ID="litShippingStreet" runat="server" /><br />
                    <asp:Literal ID="litShippingPostalCode" runat="server" />
                    <asp:Literal ID="litShippingCity" runat="server" /><br />
                    <asp:Literal ID="litShippingCountry" runat="server" /><br />
                    <abbr title="Phone">P:</abbr><asp:Literal ID="litShippingPhone" runat="server" /><br />
                    <abbr title="Mobile">M:</abbr><asp:Literal ID="litShippingMobilePhone" runat="server" /><br />
                    <abbr title="Email">E:</abbr><asp:HyperLink ID="lnkShippingMail" runat="server"></asp:HyperLink>
                </address>
            </div>

        </div>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Item no. </th>
                    <th class="span8">Description</th>
                    <th class="span1 money">Price</th>
                    <th class="span1 number">Quantity</th>
                    <th class="span1 money">Total</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPreviewItems" runat="server" OnItemDataBound="rptPreviewItems_DataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litItemSku" runat="server" /></td>
                            <td>
                                <asp:Literal ID="litItemName" runat="server" /></td>
                            <td class="money">
                                <asp:Literal ID="litPrice" runat="server" /></td>
                            <td class="number">
                                <asp:Literal ID="litQuantity" runat="server" /></td>
                            <td class="money">
                                <asp:Literal ID="litTotal" runat="server" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <asp:Literal ID="litRowSpan" runat="server" />
                    <td colspan="3">Sub total: </td>
                    <td class="money">
                        <asp:Literal ID="litSubTotal" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">VAT: </td>
                    <td class="money">
                        <asp:Literal ID="litVat" runat="server" />
                    </td>
                </tr>
                <tr id="trDiscounts" runat="server">
                    <td colspan="3">Order discounts: </td>
                    <td class="money">
                        <asp:Literal ID="litDiscount" runat="server" /></td>
                </tr>

                <tr id="trShipping" runat="server">
                    <td colspan="3">Shipping
                <asp:Literal ID="litShipments" runat="server" />
                    </td>
                    <td class="money">
                        <asp:Literal ID="litShippingTotal" runat="server" />
                    </td>
                </tr>

                <tr id="trPaymentTotal" runat="server">
                    <td colspan="3">Payment
                <asp:Literal ID="litPaymentMethods" runat="server" />


                    </td>
                    <td class="money">
                        <asp:Literal ID="litPaymentTotal" runat="server" />
                    </td>
                </tr>


                <tr>
                    <td colspan="3">Order total: </td>
                    <td class="money">
                        <asp:Literal ID="litOrderTotal" runat="server" /></td>
                </tr>
            </tbody>
        </table>
        <asp:PlaceHolder runat="server" ID="plcManagers">
            <asp:ScriptManager ID="manScript" runat="server" ScriptMode="Release" EnableViewState="false" />
            <cms:CMSPortalManager ID="portalManager" runat="server" EnableViewState="false" />
        </asp:PlaceHolder>
    </div>
</body>
</html>
