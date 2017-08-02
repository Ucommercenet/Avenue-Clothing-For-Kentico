<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Basket" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    
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
            <td rowspan="4">
                <div class="span6">
                    <div class="input-append">
                        <asp:textbox name="voucher-code" type="text" runat="server" id="txtVoucherCode" />
                        <asp:button name="add-voucher" class="btn" runat="server" onclick="btnAddVoucher_Click" text="Add Voucher"></asp:button>
                    </div>
                  
                </div>
            </td>
            <td rowspan="4" colspan="2">
                <asp:button name="update-basket" runat="server" class="btn btn-success update-basket" id="btnUpdateAll" onclick="btnUpdateAll_Click" text="Update"><%--<i class="icon-refresh"></i>--%></asp:button>
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
        <a href="/cart/address/" class="btn btn-large btn-success">Continue to Checkout <i class="icon-arrow-right icon-white"></i></a>
    </div>

</asp:Content>
