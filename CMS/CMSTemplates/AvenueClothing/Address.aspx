<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" CodeBehind="Address.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Address" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceholder" >
	
<cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
	<LayoutTemplate>
		<cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
	</LayoutTemplate>
</cms:CMSPagePlaceholder>
<a class="btn btn-small" href="~/Basket">Back to Cart</a>
<asp:Button runat="server" ID="btnBillingAndShippingUpdate" class="pull-right btn btn-large btn-success btn-arrow-right hide" ClientIDMode="static" Text="Continue to Shipping" OnClick="btnBillingAndShippingUpdate_Click" ValidationGroup="Shipping" />
<asp:Button runat="server" ID="btnBillingUpdate" class="pull-right btn btn-large btn-success btn-arrow-right" Text="Continue to Shipping" ClientIDMode="static" OnClick="btnBillingUpdate_Click" ValidationGroup="Billing" />

</asp:Content>
