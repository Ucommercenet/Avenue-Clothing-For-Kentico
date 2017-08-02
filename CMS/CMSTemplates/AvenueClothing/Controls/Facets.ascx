<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Facets.ascx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Controls.Facets" %>
<div id="facetsDiv" runat="server">
    <asp:Repeater ID="rptFacets" runat="server" OnItemDataBound="rptFacets_ItemDataBound">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="well">
                <legend>
                    <asp:Literal ID="litHeadline" runat="server"/>
                </legend>
                <asp:Repeater ID="rptCheckBoxes" runat="server" OnItemDataBound="rptCheckBoxes_ItemDataBound">
                    <ItemTemplate>
                        <asp:Button ID="btnCheckBox" runat="server" OnClick="btnCheckBox_Click"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</div>