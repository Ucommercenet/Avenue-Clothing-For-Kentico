<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ucommerce_BasketPreview" CodeBehind="~/CMSWebParts/Ucommerce/BasketPreview.ascx.cs" %>
<asp:Panel ID="cartPanel" runat="server">
    <table class="table table-striped table-basket" id="cart">
        <thead>
            <tr>
                <th id="itemno" class="span1 money" runat="server">Item no.</th>
                <th class="span4">Description</th>
                <th id="price" class="span2 money" runat="server">Price</th>
                <th id="vat" class="span2 money" runat="server">VAT</th>
                <th id="quantity" class="span2" runat="server">Quantity</th>
                <th class="span2 money">Total</th>
                <th></th>
            </tr>
        </thead>
        <tbody>

            <asp:Repeater ID="rptCart" runat="server" OnItemDataBound="CartRepeaterItemDataBound">
                <ItemTemplate>
                    <tr class="order-line">
                        <td class="money">
                            <asp:Literal ID="litItemNo" runat="server" /></td>
                        <td>
                            <asp:HyperLink ID="lnkItemLink" runat="server" /></td>
                        <td class="money">
                            <asp:Literal ID="litPrice" runat="server" /></td>
                        <td class="money">
                            <asp:Literal ID="litVat" runat="server" /></td>
                        <td>
                            <div class="input-append">
                                <asp:TextBox ClientIDMode="static" CssClass="qty update-cart" ID="txtQuantity" runat="server" value="13"></asp:TextBox>
                                <asp:Button runat="server" ID="btnUpdateQuantities" ClientIDMode="static" OnClick="btnUpdateQuantities_Click" name="update-basket-line" class="icon-refresh update-cart-button"></asp:Button>
                                <asp:Label ID="lblQuantity" runat="server" CssClass="update-cart">Hey!</asp:Label>
                            </div>
                        </td>
                        <td class="money line-total">
                            <asp:Literal ID="litTotal" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnRemoveLine" ClientIDMode="static" OnClick="btnRemoveLine_Click" name="basket-remove-item" class="line-remove" runat="server" Text="x" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td rowspan="8" colspan="2" class="white-background"></td>
            </tr>
            <tr>
                <td rowspan="8" colspan="2" class="white-background"></td>
            </tr>
            <tr id="subOrder" runat="server">

                <td>Sub total:</td>
                <td class="money order-subtotal">
                    <asp:Literal runat="server" ID="litSubtotal"></asp:Literal>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="vatOrder" runat="server">
                <td>VAT:</td>
                <td class="money order-tax">
                    <asp:Literal runat="server" ID="litTax"></asp:Literal>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="discountOrder" runat="server">
                <td>Discounts:</td>
                <td class="money order-discounts">
                    <asp:Literal runat="server" ID="litDiscount"></asp:Literal>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="shippingOrder" runat="server">
                <td>Shipping</td>
                <td class="money order-shipping">
                    <asp:Literal runat="server" ID="litShipping"></asp:Literal>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="paymentOrder" runat="server">
                <td>Payment</td>
                <td class="money order-payment">
                    <asp:Literal runat="server" ID="litPayment"></asp:Literal>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Order total:</td>
                <td class="money order-total">
                    <asp:Literal runat="server" ID="litTotal"></asp:Literal>
                </td>
                <td>&nbsp;</td>
            </tr>
        </tbody>
    </table>
    <div class="pull-right">
        <a href="~/basket/address/" class="btn btn-large btn-success">Continue</a>
    </div>
</asp:Panel>
<div id="test" runat="server" visible="False" class="alert alert-info">
    <p>Your cart is empty. Please return to our store and add some items.</p>
</div>
