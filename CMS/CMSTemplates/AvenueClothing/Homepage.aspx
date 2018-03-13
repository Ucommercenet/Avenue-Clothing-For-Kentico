<%@ Page Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothingSite.CMSTemplates_AvenueClothingSite_Homepage" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceholder" >

    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
        <cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>  

</asp:Content>
