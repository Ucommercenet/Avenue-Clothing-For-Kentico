<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_MiniBasket"  CodeBehind="~/CMSWebParts/Ucommerce/MiniBasket.ascx.cs" %>
<div class="pull-right">
    <ul class="nav">
        <li>
            <a id="hlMinicart" runat="server">
                <i id="imgMinicart" runat="server" />
                <asp:label ID="lblMinicartAmount" runat="server" />
                <asp:label ID="lblMinicartPrice" runat="server" />
            </a>
        </li>
    </ul>
</div>