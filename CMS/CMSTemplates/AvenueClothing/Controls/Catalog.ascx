<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Catalog.ascx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Controls.Catalog" %>
<%@ Import Namespace="UCommerce.EntitiesV2" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" TagPrefix="uc1" TagName="Product" %>

<div class="row-fluid">
    <div class="span12 product-list">
        <asp:ListView runat="server" ID="lvProducts">
            <ItemTemplate>
                <uc1:Product runat="server" id="Product1" CurrentProduct="<%# (Product)Container.DataItem %>"/>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>