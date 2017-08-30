﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" CodeBehind="Address.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.Address" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceholder" >
	
<cms:CMSPagePlaceholder runat="server" ID="WebPartZonePlaceholder">
	<LayoutTemplate>
		<cms:CMSWebPartZone runat="server" ID="MainContentZone"/>
	</LayoutTemplate>
</cms:CMSPagePlaceholder>

<div class="row-fluid">
<div class="span6 well">
	<legend>Billing address</legend>
	<div class="span12">
		<div class="span5 control-group">
			<label>First name</label>
			<asp:TextBox ClientIDMode="static" ID="billingFirstName" CssClass="span12" runat="server" placeholder="First name"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
			                            ControlToValidate="billingFirstName"
			                            ValidationGroup="Billing"
			                            ErrorMessage="Name required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
			                            ControlToValidate="billingFirstName"
			                            ValidationGroup="Shipping"
			                            ErrorMessage=""
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
		<div class="span5 control-group">
			<label>Last name</label>
			<asp:TextBox ClientIDMode="static" ID="billingLastName" CssClass="span12" runat="server" placeholder="Last name"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
			                            ControlToValidate="billingLastName"
			                            ValidationGroup="Billing"
			                            ErrorMessage="Name required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
			                            ControlToValidate="billingLastName"
			                            ValidationGroup="Shipping"
			                            ErrorMessage=""
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>E-mail</label>
			<asp:TextBox ClientIDMode="static" ID="billingEmail" CssClass="span12" runat="server" placeholder="Email"></asp:TextBox>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator1"
			                                runat="server"
			                                ValidationExpression=".+@.+\..*"
			                                ControlToValidate="billingEmail"
			                                ValidationGroup="Billing"
			                                Display="Dynamic"
			                                ErrorMessage="Email not valid"
			                                ForeColor="Red">
			</asp:RegularExpressionValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
			                            ControlToValidate="billingEmail"
			                            ErrorMessage="Email Required"
			                            Display="Dynamic"
			                            ValidationGroup="Billing"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator3"
			                                runat="server"
			                                ValidationExpression=".+@.+\..*"
			                                ControlToValidate="billingEmail"
			                                ValidationGroup="Shipping"
			                                Display="Dynamic"
			                                ErrorMessage=""
			                                ForeColor="Red">
			</asp:RegularExpressionValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
			                            ControlToValidate="billingEmail"
			                            ErrorMessage=""
			                            Display="Dynamic"
			                            ValidationGroup="Shipping"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
		<div class="span5 control-group">
			<label>Company</label>
			<asp:TextBox ClientIDMode="static" ID="billingCompany" CssClass="span12" runat="server" placeholder="Company"></asp:TextBox>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Attention</label>
			<asp:TextBox ClientIDMode="static" ID="billingAttention" CssClass="span12" runat="server" placeholder="Attention"></asp:TextBox>
		</div>
		<div class="span5 control-group">
			<label>Street</label>
			<asp:TextBox ClientIDMode="static" ID="billingStreet" CssClass="span12" runat="server" placeholder="Street"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
			                            ControlToValidate="billingStreet"
			                            ValidationGroup="Billing"
			                            ErrorMessage="Street required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
			                            ControlToValidate="billingStreet"
			                            ValidationGroup="Shipping"
			                            ErrorMessage=""
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Street 2</label>
			<asp:TextBox ClientIDMode="static" ID="billingStreetTwo" CssClass="span12" runat="server" placeholder="Street 2"></asp:TextBox>
		</div>
		<div class="span5 control-group">
			<label>City</label>
			<asp:TextBox ClientIDMode="static" ID="billingCity" CssClass="span12" runat="server" placeholder="City"></asp:TextBox>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Postal code</label>
			<asp:TextBox ClientIDMode="static" ID="billingPostalCode" CssClass="span12" runat="server" placeholder="Postal Code"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
			                            ControlToValidate="billingPostalCode"
			                            ValidationGroup="Billing"
			                            ErrorMessage="Postal code required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
			                            ControlToValidate="billingPostalCode"
			                            ValidationGroup="Shipping"
			                            ErrorMessage=""
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
		<div class="span5 control-group">
			<label>Country</label>
			<asp:DropDownList ID="billingCountry" CssClass="span12" DataTextField="Name" DataValueField="CountryId" runat="server"></asp:DropDownList>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Phone</label>
			<asp:TextBox ClientIDMode="static" ID="billingPhone" CssClass="span12" runat="server" placeholder="Phone"></asp:TextBox>
		</div>
		<div class="span5 control-group">
			<label>Mobile phone</label>
			<asp:TextBox ClientIDMode="static" ID="billingMobile" CssClass="span12" runat="server" placeholder="Mobile phone"></asp:TextBox>
		</div>
	</div>
	<input type="checkbox" id="checkHideShipping" checked />Shipping address same as billing
</div>
    
<div id="shippingDiv" class="span6 well hide" >
	<legend>Shipping address</legend>
	<div class="span12">
		<div class="span5 control-group">
			<label>First name</label>
			<asp:TextBox ClientIDMode="static" ID="shippingFirstName" CssClass="span12" runat="server" placeholder="First name"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
			                            ControlToValidate="shippingFirstName"
			                            ValidationGroup="Shipping"
			                            ErrorMessage="Name required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
		<div class="span5 control-group">
			<label>Last name</label>
			<asp:TextBox ClientIDMode="static" ID="shippingLastName" CssClass="span12" runat="server" placeholder="Last name"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
			                            ControlToValidate="shippingLastName"
			                            ValidationGroup="Shipping"
			                            ErrorMessage="Name required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>E-mail</label>
			<asp:TextBox ClientIDMode="static" ID="shippingEmail" CssClass="span12" runat="server" placeholder="Email"></asp:TextBox>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator2"
			                                runat="server"
			                                ValidationExpression=".+@.+\..*"
			                                ControlToValidate="shippingEmail"
			                                ValidationGroup="Shipping"
			                                Display="Dynamic"
			                                ErrorMessage="Email not valid"
			                                ForeColor="Red">
			</asp:RegularExpressionValidator>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
			                            ControlToValidate="shippingEmail"
			                            ErrorMessage="Email Required"
			                            Display="Dynamic"
			                            ValidationGroup="Shipping"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
		<div class="span5 control-group">
			<label>Company</label>
			<asp:TextBox ClientIDMode="static" ID="shippingCompany" CssClass="span12" runat="server" placeholder="Company"></asp:TextBox>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Attention</label>
			<asp:TextBox ClientIDMode="static" ID="shippingAttention" CssClass="span12" runat="server" placeholder="Attention"></asp:TextBox>
		</div>
		<div class="span5 control-group">
			<label>Street</label>
			<asp:TextBox ClientIDMode="static" ID="shippingStreet" CssClass="span12" runat="server" placeholder="Street"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
			                            ControlToValidate="shippingStreet"
			                            ValidationGroup="Shipping"
			                            ErrorMessage="Street required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Street 2</label>
			<asp:TextBox ClientIDMode="static" ID="shippingStreetTwo" CssClass="span12" runat="server" placeholder="Street 2"></asp:TextBox>
		</div>
		<div class="span5 control-group">
			<label>City</label>
			<asp:TextBox ClientIDMode="static" ID="shippingCity" CssClass="span12" runat="server" placeholder="City"></asp:TextBox>
		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Postal code</label>
			<asp:TextBox ClientIDMode="static" ID="shippingPostalCode" CssClass="span12" runat="server" placeholder="Postal code"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
			                            ControlToValidate="shippingPostalCode"
			                            ValidationGroup="Shipping"
			                            ErrorMessage="Postal code required"
			                            ForeColor="Red">
			</asp:RequiredFieldValidator>
		</div>
		<div class="span5 control-group">
			<label>Country</label>
			<asp:DropDownList ID="shippingCountry" CssClass="span12" runat="server" DataTextField="Name" DataValueField="CountryId"></asp:DropDownList>

		</div>
	</div>
	<div class="span12">
		<div class="span5 control-group">
			<label>Phone</label>
			<asp:TextBox ClientIDMode="static" ID="shippingPhone" CssClass="span12" runat="server" placeholder="Phone"></asp:TextBox>
		</div>
		<div class="span5 control-group">
			<label>Mobile phone</label>
			<asp:TextBox ClientIDMode="static" ID="shippingMobile" CssClass="span12" runat="server" placeholder="Mobile phone"></asp:TextBox>
		</div>
	</div>
</div>
</div>
<a class="btn btn-small" href="~/Basket">Back to Cart</a>
<asp:Button runat="server" ID="btnBillingAndShippingUpdate" class="pull-right btn btn-large btn-success btn-arrow-right hide" ClientIDMode="static" Text="Continue to Shipping" OnClick="btnBillingAndShippingUpdate_Click" ValidationGroup="Shipping" />
<asp:Button runat="server" ID="btnBillingUpdate" class="pull-right btn btn-large btn-success btn-arrow-right" Text="Continue to Shipping" ClientIDMode="static" OnClick="btnBillingUpdate_Click" ValidationGroup="Billing" />

</asp:Content>