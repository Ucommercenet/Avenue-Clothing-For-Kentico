<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Shipping.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Shipping" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid well">
        <div class="span6">
            <h3>Shipping method</h3>
            <br />
            <div class="inline-labels"> 
                <asp:RadioButtonList ID="rblShippingMethods" runat="server" ClientIDMode="static"></asp:RadioButtonList>
            </div>
            <p id="pPaymentAlert" runat="server" class="alert"><asp:Literal ID="litAlert" runat="server"/></p>

        </div>
    </div>
    <a href="/basket/address" class="btn btn-small">Back to Address</a>
    <asp:Button UseSubmitBehavior="true" id="btnUpdateShipment" CssClass="pull-right btn btn-large btn-success btn-arrow-right" runat="server" Text="Continue to confirmation" OnClick="btnUpdateShipment_Click"/>
</asp:Content>
