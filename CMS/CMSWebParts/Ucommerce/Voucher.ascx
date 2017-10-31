<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_Voucher"  CodeBehind="~/CMSWebParts/Ucommerce/Voucher.ascx.cs" %>
<div class="margin-top-bottom">
    <div class="input-append">
        <input id="txtVoucherCode" class="voucher" type="text" runat="server"  placeholder="Enter your voucher code here" />
        <asp:button runat="server" ID="btnAddVoucher" CssClass="voucher-button"  OnClick="UseVoucher"></asp:button>
    </div>
</div>