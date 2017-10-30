<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_AddressPicker" CodeBehind="~/CMSWebParts/Ucommerce/AddressPicker.ascx.cs" %>

<div runat="server" id="Address" class="row-fluid">
    <div id="billingDiv" class="span12" runat="server" clientidmode="Static">
        <legend>Billing address</legend>
        <div class="span12">
            <div class="span6 control-group">
                <label>First name</label>
                <asp:TextBox ClientIDMode="static" ID="billingFirstName" CssClass="span12 boxborder" runat="server"></asp:TextBox>
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
            <div class="span6 control-group">
                <label>Last name</label>
                <asp:TextBox ClientIDMode="static" ID="billingLastName" CssClass="span12 boxborder" runat="server"></asp:TextBox>
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
            <div class="span6 control-group">
                <label>E-mail</label>
                <asp:TextBox ClientIDMode="static" ID="billingEmail" CssClass="span12 boxborder" runat="server"></asp:TextBox>
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
            <div class="span6 control-group">
                <label>Company</label>
                <asp:TextBox ClientIDMode="static" ID="billingCompany" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Attention</label>
                <asp:TextBox ClientIDMode="static" ID="billingAttention" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
            <div class="span6 control-group">
                <label>Street</label>
                <asp:TextBox ClientIDMode="static" ID="billingStreet" CssClass="span12 boxborder" runat="server"></asp:TextBox>
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
            <div class="span6 control-group">
                <label>Street 2</label>
                <asp:TextBox ClientIDMode="static" ID="billingStreetTwo" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
            <div class="span6 control-group">
                <label>City</label>
                <asp:TextBox ClientIDMode="static" ID="billingCity" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Postal code</label>
                <asp:TextBox ClientIDMode="static" ID="billingPostalCode" CssClass="span12 boxborder" runat="server"></asp:TextBox>
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
            <div class="span6 control-group">
                <label>Country</label>
                <asp:DropDownList ID="billingCountry" CssClass="span12 boxborder" DataTextField="Name" DataValueField="CountryId" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Phone</label>
                <asp:TextBox ClientIDMode="static" ID="billingPhone" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
            <div class="span6 control-group">
                <label>Mobile phone</label>
                <asp:TextBox ClientIDMode="static" ID="billingMobile" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div id="shippingDiv" class="span6 hide">
        <legend>Shipping address</legend>
        <div class="span12">
            <div class="span6 control-group">
                <label>First name</label>
                <asp:TextBox ClientIDMode="static" ID="shippingFirstName" CssClass="span12 boxborder" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                    ControlToValidate="shippingFirstName"
                    ValidationGroup="Shipping"
                    ErrorMessage="Name required"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
            <div class="span6 control-group">
                <label>Last name</label>
                <asp:TextBox ClientIDMode="static" ID="shippingLastName" CssClass="span12 boxborder" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                    ControlToValidate="shippingLastName"
                    ValidationGroup="Shipping"
                    ErrorMessage="Name required"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>E-mail</label>
                <asp:TextBox ClientIDMode="static" ID="shippingEmail" CssClass="span12 boxborder" runat="server"></asp:TextBox>
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
            <div class="span6 control-group">
                <label>Company</label>
                <asp:TextBox ClientIDMode="static" ID="shippingCompany" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Attention</label>
                <asp:TextBox ClientIDMode="static" ID="shippingAttention" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
            <div class="span6 control-group">
                <label>Street</label>
                <asp:TextBox ClientIDMode="static" ID="shippingStreet" CssClass="span12 boxborder" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                    ControlToValidate="shippingStreet"
                    ValidationGroup="Shipping"
                    ErrorMessage="Street required"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Street 2</label>
                <asp:TextBox ClientIDMode="static" ID="shippingStreetTwo" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
            <div class="span6 control-group">
                <label>City</label>
                <asp:TextBox ClientIDMode="static" ID="shippingCity" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Postal code</label>
                <asp:TextBox ClientIDMode="static" ID="shippingPostalCode" CssClass="span12 boxborder" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                    ControlToValidate="shippingPostalCode"
                    ValidationGroup="Shipping"
                    ErrorMessage="Postal code required"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
            <div class="span6 control-group">
                <label>Country</label>
                <asp:DropDownList ID="shippingCountry" CssClass="span12 boxborder" runat="server" DataTextField="Name" DataValueField="CountryId"></asp:DropDownList>
            </div>
        </div>
        <div class="span12">
            <div class="span6 control-group">
                <label>Phone</label>
                <asp:TextBox ClientIDMode="static" ID="shippingPhone" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
            <div class="span6 control-group">
                <label>Mobile phone</label>
                <asp:TextBox ClientIDMode="static" ID="shippingMobile" CssClass="span12 boxborder" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="span12">
        <label>
            Use a different address for shipping
            <input runat="server" type="checkbox" id="UseDIfferentShippingAddress" ClientIDMode="Static" />
        </label>
    </div>
</div>
<div id="cartIsEmpty" runat="server" visible="False" class="alert alert-info">
    <p>Your cart is empty. Please return to our store and add some items.</p>
</div>
