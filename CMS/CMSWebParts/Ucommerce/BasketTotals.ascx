<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_BasketTotals"  CodeBehind="~/CMSWebParts/Ucommerce/BasketTotals.ascx.cs" %>
    
    
    <tr id="subOrder" runat="server">
        <td rowspan="8" colspan="4" class="white-background"></td>
        <td>Sub total:</td>
        <td class="money order-subtotal">
            <asp:literal runat="server" id="litSubtotal"></asp:literal>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="vatOrder" runat="server">
        <td>VAT:</td>
        <td class="money order-tax">
            <asp:literal runat="server" id="litTax"></asp:literal>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="discountOrder" runat="server">
        <td>Discounts:</td>
        <td class="money order-discounts">
            <asp:literal runat="server" id="litDiscount"></asp:literal>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="shippingOrder" runat="server">
        <td>Shipping</td>
        <td class="money order-shipping">
            <asp:literal runat="server" id="litShipping"></asp:literal>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="paymentOrder" runat="server">
        <td>Payment</td>
        <td class="money order-payment">
            <asp:literal runat="server" id="litPayment"></asp:literal>
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
    