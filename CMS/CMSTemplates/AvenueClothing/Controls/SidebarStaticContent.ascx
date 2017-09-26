<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SidebarStaticContent.ascx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Controls.SidebarStaticContent" %>
<%@ Register Src="~/CMSTemplates/AvenueClothing/Controls/CategoryNavigation.ascx" TagPrefix="uc1" TagName="CategoryNavigation" %>



<div class="well sidebar-nav" id="why-buy">
    <ol>
        <li><i class="icon-grey-star"></i>5 star satisfaction</li>
        <li><i class="icon-truck"></i>Free delivery</li>
        <li>Payment Information<br />
            <img src="~/AvenueClothing/img/ucommerce/cards.png" alt="We accept VISA, SOLO, PayPal, MasterCard" title="We accept VISA, SOLO, PayPal, MasterCard" /></li>
    </ol>
    <p id="powered-by-ucommerce">
        <a href="http://ucommerce.net/" title="The most feature rich e-commerce package for Kentico" target="_blank">
            <img src="~/AvenueClothing/img/ucommerce/ucommerce.png" alt="Powered by uCommerce" /></a>
    </p>
</div>
<div class="well sidebar-nav" id="about">
    <h5>About Avenue Clothing</h5>
    <p>Avenue Clothing is an example store to demonstrate the simple -yet powerful e-commerce functionality uCommerce brings to Kentico.</p>
    <p>All products listed are from existing retailers but no orders will be fulfilled.</p>
    <p><a href="http://ucommerce.net" class="btn btn-secondary" title="The most feature rich e-commerce package for Kentico">More...</a></p>
</div>
<div class="well sidebar-nav" id="newsletter">
    <h5>Keep up-to-date</h5>
    <p>Sign up to our newsletter.</p>
    <form action="http://thesitedoctor.createsend.com/t/y/s/xbjtk/" method="post" class="validate" id="newsletter-form">
        <div class="input-append">
            <input type="email" name="cm-xbjtk-xbjtk" id="xbjtk-xbjtk" class="required email span9" />
            <button type="submit" class="btn btn-secondary">></button>
        </div>
    </form>
</div>