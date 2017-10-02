<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Basket" %>

<%@ Register Src="~/CMSWebParts/Ucommerce/DeleteOrderline.ascx" TagPrefix="uc1" TagName="DeleteOrderline" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
            <cms:CMSWebPartZone runat="server" ID="BasketContentZone"/>
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>
    
    <div class="pull-right">
        <a href="~/basket/address/" class="btn btn-large btn-success">Continue to Checkout <i class="icon-arrow-right icon-white"></i></a>
    </div>
</asp:Content>
