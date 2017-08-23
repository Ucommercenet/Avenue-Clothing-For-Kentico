<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Preview" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid well">
        <div class="span6">
            <h3>Billing address</h3>
            <br />
            <address>
                <strong>
                    <asp:Literal ID="litBillingName" runat="server" />
                </strong>
                <br />
                <asp:Literal ID="litBillingStreet" runat="server" /><br />
                <asp:Literal ID="litBillingPostalCode" runat="server" />
                <asp:Literal ID="litBillingCity" runat="server" /><br />
                <asp:Literal ID="litBillingCountry" runat="server" />
                <asp:Literal ID="litBillingAttention" runat="server" /><br />
                <abbr title="Phone">P:</abbr><asp:Literal ID="litBillingPhone" runat="server" /><br />
                <abbr title="Mobile">M:</abbr><asp:Literal ID="litBillingMobilePhone" runat="server" /><br />
                <abbr title="Email">E:</abbr><asp:HyperLink ID="lnkBillingMail" runat="server"></asp:HyperLink>
            </address>
        </div>

        <div class="span6">
            <h3>Shipping address</h3>
            <br />
            <address>
                <strong>
                    <asp:Literal ID="litShippingName" runat="server" />
                </strong>
                <br />
                <asp:Literal ID="litShippingCompany" runat="server" />
                <asp:Literal ID="litShippingAttention" runat="server" /><br />
                <asp:Literal ID="litShippingStreet" runat="server" /><br />
                <asp:Literal ID="litShippingPostalCode" runat="server" />
                <asp:Literal ID="litShippingCity" runat="server" /><br />
                <asp:Literal ID="litShippingCountry" runat="server" /><br />
                <abbr title="Phone">P:</abbr><asp:Literal ID="litShippingPhone" runat="server" /><br />
                <abbr title="Mobile">M:</abbr><asp:Literal ID="litShippingMobilePhone" runat="server" /><br />
                <abbr title="Email">E:</abbr><asp:HyperLink ID="lnkShippingMail" runat="server"></asp:HyperLink>
            </address>
        </div>

    </div>
        <cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
                         <LayoutTemplate>
                         <cms:CMSWebPartZone runat="server" ID="PreviewContentZone"/>
                         </LayoutTemplate>
                    </cms:CMSPagePlaceholder>
    <a href="~/Basket/Payment" class="btn btn-small">Back to Payment Method</a>
    <asp:Button ID="btnContinue" runat="server" OnClick="btnContinue_Click" class="pull-right btn btn-large btn-success btn-arrow-right" Text="Complete Order" />

</asp:Content>
