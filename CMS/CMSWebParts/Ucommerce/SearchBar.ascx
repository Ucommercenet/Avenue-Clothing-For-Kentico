<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_SearchBar"  CodeBehind="~/CMSWebParts/Ucommerce/SearchBar.ascx.cs" %>

<div class="navbar-search form-search" id="search-form">
    <div class="input-append">
        <input name="search" class="search-query" runat="server" id="siteSearch" type="text" autocomplete="off"/>
        <button class="btn" type="submit" id="submitSearch" onclick="btnSubmitSearch_Click"><i class="icon-search"></i></button>
    
    </div>
</div>