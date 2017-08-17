<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_Voucher"  CodeBehind="~/CMSWebParts/Ucommerce/Voucher.ascx.cs" %>
<div style="margin-top: 1rem; margin-bottom: 1rem;">
    <div class="input-append">
        <input id="txtVoucherCode" class="voucher" type="text" runat="server"  placeholder="Enter your voucher code here" />
        <%--<button id="btnAddVoucher" class="voucher" type="button"  runat="server" onclick="btnAddVoucher_Click" >Add Voucher</button>--%>
        <asp:button runat="server" ID="btnAddVoucher" CssClass="voucher-button"  OnClick="btnAddVoucher_Click"></asp:button>
    </div>
</div>