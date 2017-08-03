<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Payment" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <div class="row-fluid well">
        <div class="span6">
            <h3>Payment method</h3>
            <br/>
            <div class="inline-labels"> 
                <asp:RadioButtonList ID="rblPaymentMethods" runat="server" ClientIDMode="static"></asp:RadioButtonList>
            </div>
            <p id="pPaymentAlert" runat="server" class="alert"><asp:Literal ID="litAlert" runat="server"/></p>
            
        </div>
    </div>
    <a href="/cart/shipping.aspx" class="btn btn-small">Back to Shipping Method</a>
    <asp:button CssClass="pull-right btn btn-large btn-success btn-arrow-right" OnClick="btnContinue_Click" ID="btnContinue" runat="server" Text="Continue to Preview Page"/>

</asp:Content>
