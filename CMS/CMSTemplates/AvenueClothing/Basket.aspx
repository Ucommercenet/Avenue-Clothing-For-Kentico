<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Basket" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
            <cms:CMSWebPartZone runat="server" ID="BasketContentZone"/>
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>
    <div class="pull-right">
        <a href="~/basket/address/" class="btn btn-large btn-success">Continue</a>
    </div>
</asp:Content>
