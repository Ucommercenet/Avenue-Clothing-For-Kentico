<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_ShippingDataSource"  CodeBehind="~/CMSWebParts/Ucommerce/ShippingDataSource.ascx.cs" %>

<%@ Register Src="~/CMSWebParts/Ucommerce/UCommerceShippingDataControl.ascx" TagPrefix="uCommerce" TagName="ShippingDataControl" %>

<uCommerce:ShippingDataControl ID="srcShippingMethods" runat="server" />