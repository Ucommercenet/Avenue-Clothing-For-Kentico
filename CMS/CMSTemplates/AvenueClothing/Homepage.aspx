<%@ Page Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothingSite.CMSTemplates_AvenueClothingSite_Homepage" %>
<%@ Register src="Controls/HomepageBanner.ascx" tagname="HomepageBanner" tagprefix="uc1" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/HighlightedProducts.ascx" TagPrefix="uc1" TagName="HighlightedProducts" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceholder" >
  
<%--    <uc1:HomepageBanner ID="HomepageBanner1" runat="server" />--%>

    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
        <cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>  

</asp:Content>
