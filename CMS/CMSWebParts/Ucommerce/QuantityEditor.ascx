<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_QuantityEditor"  CodeBehind="~/CMSWebParts/Ucommerce/QuantityEditor.ascx.cs" %>

<div class="input-append">

    <asp:TextBox ClientIDMode="static" CssClass="qty" ID="txtQuantity" runat="server" value="13"></asp:TextBox>
    <asp:button runat="server" id="btnUpdateQuantities" ClientIDMode="static" OnClick="btnUpdateQuantities_Click" name="update-basket-line" class="icon-refresh update-cart-button"></asp:button>

</div>