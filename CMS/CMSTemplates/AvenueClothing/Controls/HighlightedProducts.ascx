<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HighlightedProducts.ascx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Controls.HighlightedProducts" %>
<%@ Register TagPrefix="uc1" TagName="Product" Src="~/CMSTemplates/AvenueClothing/Controls/Product.ascx" %>
<%@ Import Namespace="UCommerce.EntitiesV2" %>

<div class="row-fluid margin-top">
    <div class="span12 product-list">
        <asp:ListView runat="server" ID="lvProducts">
            <ItemTemplate>
                <uc1:Product runat="server" id="Product1" CurrentProduct="<%# (Product)Container.DataItem %>"/>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>