<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Catalog" %>
<%@ Import Namespace="UCommerce.EntitiesV2" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" TagPrefix="uc1" TagName="Product" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Facets.ascx" TagPrefix="uc1" TagName="Facets" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlaceholder" runat="server">
    <uc1:Facets runat="server" id="Facets" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid">
        <div class="hero-unit">
            <asp:Image runat="server" ID="CategoryImage" Width="870" Height="350"/>    
        </div>
        <div class="span12 product-list">
            <asp:ListView runat="server" ID="lvProducts">
                <ItemTemplate>
                    <uc1:Product runat="server" ID="Product" CurrentProduct="<%# (Product)Container.DataItem %>" />
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
