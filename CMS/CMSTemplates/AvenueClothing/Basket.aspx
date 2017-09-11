<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Basket" %>

<%@ Register Src="~/CMSWebParts/Ucommerce/DeleteOrderline.ascx" TagPrefix="uc1" TagName="DeleteOrderline" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
            <cms:CMSWebPartZone runat="server" ID="BasketContentZone"/>
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>
    <table class="table table-striped" id="cart">
        <thead>
        <tr>
            <th class="span7">Description</th>
            <th class="span1 money">Price</th>
            <th class="span1 money">VAT</th>
            <th class="span2">Quantity</th>
            <th class="span1 money">Total</th>
            <th></th>
        </tr>
        </thead>
        <tbody>

        <asp:repeater id="rptCart" runat="server" onitemdatabound="CartRepeaterItemDataBound">
            <ItemTemplate>
                <tr class="order-line">
                    <td><asp:HyperLink ID="lnkItemLink" runat="server"/></td>
                    <td class="money"><asp:Literal ID="litPrice" runat="server"/></td>
                    <td class="money"><asp:Literal ID="litVat" runat="server"/></td>
                    <td>
                        <div class="input-append">
                            <asp:TextBox ClientIDMode="static" CssClass="qty" ID="txtQuantity" runat="server" value="13"></asp:TextBox>
                            <asp:button runat="server" id="btnUpdateQuantities" ClientIDMode="static" OnClick="btnUpdateQuantities_Click" name="update-basket-line" class="btn btn-success update-cart javascriptHide" Text="Update"></asp:button>
                        </div>
                    </td>
                    <td class="money line-total"><asp:Literal ID="litTotal" runat="server"/>
                    </td>
                    <td>
                        <asp:Button ID="btnRemoveLine" ClientIDMode="static" onCLick="btnRemoveLine_Click" name="basket-remove-item" class="line-remove" runat="server" Text="x" /></td>
                </tr>
            </ItemTemplate>
        </asp:repeater>
        <tr>
            <td rowspan="4" colspan="3">
                
            </td>
            <td>Sub total:</td>
            <td class="money order-subtotal">
                <asp:literal runat="server" id="litSubtotal"></asp:literal>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>VAT:</td>
            <td class="money order-tax">
                <asp:literal runat="server" id="litTax"></asp:literal>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Discounts:</td>
            <td class="money order-discounts">
                <asp:literal runat="server" id="litDiscount"></asp:literal>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Order total:</td>
            <td class="money order-total">
                <asp:literal runat="server" id="litTotal"></asp:literal>
            </td>
            <td>&nbsp;</td>
        </tr>
        </tbody>
    </table>
    <div class="pull-right">
        <a href="~/basket/address/" class="btn btn-large btn-success">Continue to Checkout <i class="icon-arrow-right icon-white"></i></a>
    </div>

</asp:Content>
