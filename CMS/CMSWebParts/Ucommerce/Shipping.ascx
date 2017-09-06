<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_Shipping"  CodeBehind="~/CMSWebParts/Ucommerce/Shipping.ascx.cs" %>

<div class="row-fluid">
        <div class="span6">
            <legend>Shipping method</legend>
            <div class="inline-labels"> 
                <asp:RadioButtonList ID="rblShippingMethods" runat="server" ClientIDMode="static" CssClass="payment"></asp:RadioButtonList>
            </div>
            <p id="pPaymentAlert" runat="server" class="alert"><asp:Literal ID="litAlert" runat="server"/></p>
        </div>
</div>