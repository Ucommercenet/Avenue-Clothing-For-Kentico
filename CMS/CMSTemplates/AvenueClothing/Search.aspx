<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Search" %>

<%@ Import Namespace="UCommerce.EntitiesV2" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" TagPrefix="uc1" TagName="Product" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid">
        <div class="span12 product-list">
            <asp:ListView runat="server" ID="lvProducts">
                <ItemTemplate>
                    <uc1:Product runat="server" ID="Product" CurrentProduct="<%# (Product)Container.DataItem %>" />
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
