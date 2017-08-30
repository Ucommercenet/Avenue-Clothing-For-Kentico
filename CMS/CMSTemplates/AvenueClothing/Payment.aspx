<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Payment" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
                         <LayoutTemplate>
                         <cms:CMSWebPartZone runat="server" ID="PaymentContentZone"/>
                         </LayoutTemplate>
                    </cms:CMSPagePlaceholder>
    
    <a href="~/Basket/Shipping" class="btn btn-small">Back to Shipping Method</a>
    <asp:button CssClass="pull-right btn btn-large btn-success btn-arrow-right" OnClick="btnContinue_Click" ID="btnContinue" runat="server" Text="Continue to Preview Page"/>

</asp:Content>
