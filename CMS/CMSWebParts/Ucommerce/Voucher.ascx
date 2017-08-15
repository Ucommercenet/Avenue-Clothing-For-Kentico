<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_Voucher"  CodeBehind="~/CMSWebParts/Ucommerce/Voucher.ascx.cs" %>
<div class="margin-top margin-bottom">
    <div class="input-append span12">
        <input name="voucher-code" type="text" runat="server" id="txtVoucherCode" placeholder="Enter your voucher code here"/>
        <button  name="add-voucher" class="btn" onclick="btnAddVoucher_Click" style="background: #ffffff; color:black" type="button">Add Voucher</button>
    </div>
</div>