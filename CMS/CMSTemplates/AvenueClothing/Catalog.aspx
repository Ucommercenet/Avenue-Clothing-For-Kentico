﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Catalog" %>
<%@ Import Namespace="UCommerce.EntitiesV2" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" TagPrefix="uc1" TagName="Product" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder1">
    <layouttemplate>
        <cms:CMSWebPartZone runat="server" ID="FacetsContentZone"/>
    </layouttemplate>
</cms:CMSPagePlaceholder>
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
