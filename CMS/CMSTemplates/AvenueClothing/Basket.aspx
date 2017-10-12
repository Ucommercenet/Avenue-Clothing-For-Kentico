<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Basket" %>

<%@ Register Src="~/CMSWebParts/Ucommerce/DeleteOrderline.ascx" TagPrefix="uc1" TagName="DeleteOrderline" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
            <cms:CMSWebPartZone runat="server" ID="BasketContentZone"/>
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>
</asp:Content>
