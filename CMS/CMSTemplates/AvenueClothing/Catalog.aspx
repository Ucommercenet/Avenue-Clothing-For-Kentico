<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Catalog" %>
<%@ Import Namespace="UCommerce.EntitiesV2" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" TagPrefix="uc1" TagName="Product" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Facets.ascx" TagPrefix="uc1" TagName="Facets" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlaceholder" runat="server">
    <uc1:Facets runat="server" id="Facets" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid">

        <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
            <LayoutTemplate>
                <cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
            </LayoutTemplate>
        </cms:CMSPagePlaceholder>

    </div>
</asp:Content>
