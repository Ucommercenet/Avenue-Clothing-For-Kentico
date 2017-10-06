<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Catalog" %>

<%@ Import Namespace="UCommerce.EntitiesV2" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" TagPrefix="uc1" TagName="Product" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder1">
        <LayoutTemplate>
            <cms:CMSWebPartZone runat="server" ID="FacetsContentZone" />
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid">

        <asp:Image runat="server" ID="CategoryImage" CssClass="catalog-image"/>
        <h1 runat="server" id="CategoryName"></h1>

        <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
            <LayoutTemplate>
                <cms:CMSWebPartZone runat="server" ID="MainContentZone" />
            </LayoutTemplate>
        </cms:CMSPagePlaceholder>

    </div>
</asp:Content>
