<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Inherits="CMSModules_ImportExport_Pages_ImportSite"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" CodeBehind="ImportSite.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/ImportExport/Controls/ImportWizard.ascx" TagName="ImportWizard"
    TagPrefix="cms" %>

<asp:Content ID="plcContent" ContentPlaceHolderID="plcContent" runat="server" EnableViewState="false">
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageBody">
        <asp:Panel runat="server" ID="pnlTitle" CssClass="PageHeader SimpleHeader">
            <cms:PageTitle ID="titleElem" runat="server" HideTitle="true" />
        </asp:Panel>
        <asp:Panel ID="pnlImport" runat="server" CssClass="PageContent">
            <cms:MessagesPlaceHolder runat="server" ID="pnlMessages" />
            <cms:ImportWizard ID="wzdImport" ShortID="w" runat="server" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
