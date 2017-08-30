<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_PaymentPicker"  CodeBehind="~/CMSWebParts/Ucommerce/PaymentPicker.ascx.cs" %>

<div class="row-fluid">
        <div class="span6">
            <legend>Payment method</legend>
            <div class="inline-labels"> 
                <asp:RadioButtonList ID="rblPaymentMethods" runat="server" ClientIDMode="static" CssClass="payment"></asp:RadioButtonList>
            </div>
            <p id="pPaymentAlert" runat="server" class="alert"><asp:Literal ID="litAlert" runat="server"/></p>
        </div>
    </div>