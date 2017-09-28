<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" CodeBehind="Address.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Address" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceholder" >
	
<cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
	<LayoutTemplate>
		<cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
	</LayoutTemplate>
</cms:CMSPagePlaceholder>
<a class="btn btn-small btn-secondary" href="~/Basket">Back</a>
<asp:Button runat="server" ID="btnBillingAndShippingUpdate" class="pull-right btn btn-large btn-success hide" ClientIDMode="static" Text="Continue to shipping options" OnClick="btnBillingAndShippingUpdate_Click" ValidationGroup="Shipping" />
<asp:Button runat="server" ID="btnBillingUpdate" class="pull-right btn btn-large btn-success" Text="Continue to shipping options" ClientIDMode="static" OnClick="btnBillingUpdate_Click" ValidationGroup="Billing" />

</asp:Content>
