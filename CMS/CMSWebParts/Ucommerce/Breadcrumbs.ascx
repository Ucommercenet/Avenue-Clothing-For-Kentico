<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_Breadcrumbs" CodeBehind="~/CMSWebParts/Ucommerce/Breadcrumbs.ascx.cs" %>

<div class="breadcrumb">
<asp:ListView runat="server" ID="Breadcrumbs" class="breadcrumb" ItemType="CMSWebParts_Ucommerce_Breadcrumbs.BreadcrumbViewModel">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
    </LayoutTemplate>
    <ItemTemplate>
        <a href="<%# Item.BreadcrumbUrl %>"><%# Item.BreadcrumbName %> </a>
    </ItemTemplate>
    <EmptyDataTemplate></EmptyDataTemplate>
</asp:ListView>
</div>