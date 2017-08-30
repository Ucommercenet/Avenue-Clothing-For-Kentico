<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Shipping.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Shipping" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid well">
        <div class="span6">
            <h3>Shipping method</h3>
            <br />

            <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
                <LayoutTemplate>
                <cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
                </LayoutTemplate>
            </cms:CMSPagePlaceholder>

        </div>
    </div>
    <a href="~/basket/address" class="btn btn-small">Back to Address</a>
    <asp:Button UseSubmitBehavior="true" id="btnUpdateShipment" CssClass="pull-right btn btn-large btn-success btn-arrow-right" runat="server" Text="Continue to payment" OnClick="btnUpdateShipment_Click"/>
</asp:Content>
