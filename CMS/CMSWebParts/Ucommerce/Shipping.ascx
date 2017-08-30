<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_Shipping"  CodeBehind="~/CMSWebParts/Ucommerce/Shipping.ascx.cs" %>
<div class="inline-labels"> 
    <asp:RadioButtonList ID="rblShippingMethods" runat="server" ClientIDMode="static"></asp:RadioButtonList>
</div>
<p id="pPaymentAlert" runat="server" class="alert"><asp:Literal ID="litAlert" runat="server"/></p>