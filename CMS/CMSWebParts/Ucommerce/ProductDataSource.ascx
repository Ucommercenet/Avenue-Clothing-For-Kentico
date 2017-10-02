<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_ProductDataSource"  CodeBehind="~/CMSWebParts/Ucommerce/ProductDataSource.ascx.cs" %>

<%@ Register Src="~/CMSWebParts/Ucommerce/UCommerceProductsDataControl.ascx" TagPrefix="uCommerce" TagName="ProductsDataControl" %>

<uCommerce:ProductsDataControl ID="srcProducts" runat="server" />