<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" CodeBehind="Address.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Address" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceholder">

    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
        <LayoutTemplate>
            <cms:CMSWebPartZone runat="server" ID="MainContentZone" />
        </LayoutTemplate>
    </cms:CMSPagePlaceholder>
    <a class="btn btn-small btn-secondary" href="~/Basket">Back</a>
    <asp:Button runat="server" ID="btnBillingUpdate" class="pull-right btn btn-large btn-success" Text="Continue to shipping options" ClientIDMode="static" OnClick="UpdateAddresses" ValidationGroup="Billing" />

</asp:Content>
