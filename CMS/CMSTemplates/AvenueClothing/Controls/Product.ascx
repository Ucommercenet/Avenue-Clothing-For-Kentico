<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Product.ascx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Controls.Product" %>


<div class="product span4">
    <div class="product-image">
        <asp:Hyperlink runat="server" ID="hlIMage" Visible="False">
            <%--<store:Image runat="server" ID="imgProduct" MaxHeight="400" MaxWidth="300"/>--%>
            <asp:Image runat="server" ID="productImage"/>
        </asp:Hyperlink>
        <asp:PlaceHolder runat="server" ID="phRatings">
            <p class="ratings">
                <%--<store:RatingStars runat="server" ID="storeRating" CssClass="star-rating" />--%>
            </p>
        </asp:PlaceHolder>
    </div>
    <p class="item-name"><asp:HyperLink runat="server" ID="hlProduct"/></p>
    <p class="item-price"><asp:Literal runat="server" ID="litPrice"/></p>
    <p class="btn-group view-details"><asp:HyperLink runat="server" ID="hlViewMore" CssClass="btn" >View more <span><i class="icon-shopping-cart icon-white"></i></span></asp:HyperLink></p>
</div>