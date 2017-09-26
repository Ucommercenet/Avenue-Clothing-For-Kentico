<%@ Page Title="" Language="C#" MasterPageFile="~/CMSTemplates/AvenueClothing/Main.master" AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="CMSApp.CMSTemplates.AvenueClothing.ProductPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceholder" runat="server">
    <asp:PlaceHolder runat="server" ID="phInBasket" Visible="false">
        <div class="row-fluid">
            <div class="alert alert-info">
                <button type="button" class="close" data-dismiss="alert">×</button>
                <p><strong>Note!</strong> This item is already in your basket. <a class="btn btn-primary pull-right" href="/cart.aspx">View Your Cart</a></p>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phAddToCartError" Visible="false">
        <div class="row-fluid">
            <div class="alert alert-error">
                <button type="button" class="close" data-dismiss="alert">×</button>
                <p><strong>Oh no!</strong> Something went wrong when we tried to add this to your cart. It could be we don't have the variation you selected. Please try again.</p>
            </div>
        </div>
    </asp:PlaceHolder>
    <cms:BasicRepeater runat="server" ID="paymentsRepeater"/>
    
    <div class="row-fluid" id="product-details" itemtype="http://schema.org/Product" itemscope="">
        <div class="span6 row">
            <asp:Image ID="imgTop" runat="server" />
        </div>
        <section class="span6">
            <header class="span12 page-header">
                <h1 itemprop="name">
                    <asp:Literal ID="litHeadline" runat="server" /></h1>
                <h6>
                    <asp:Literal ID="litProductReviews" runat="server" /></h6>
                <small><em>
                    <asp:Literal ID="litProductSmallDesc" runat="server" /></em></small>
            </header>
            <div class="span12 well">
                <aside class="span5" itemtype="http://schema.org/Offer" itemscope="" itemprop="offers">
                    <p class="item-price" itemprop="price">
                        <asp:Literal ID="litPrice" runat="server" />
                    </p>
                    <p class="item-tax">
                        <abbr title="Including">Inc.</abbr>
                        <asp:Literal ID="litTax" runat="server" />
                        tax
                    </p>
                </aside>
                <asp:Repeater ID="rptVariant" runat="server" OnItemDataBound="VariantRepeaterItemDataBound">
                    <HeaderTemplate>
                        <div class="span7">
                    </HeaderTemplate>
                    <ItemTemplate>

                        <asp:Label ID="lbLabel" runat="server" for="variation-collarsize">
                            <asp:Literal ID="litLabel" runat="server"></asp:Literal></asp:Label>
                        <asp:Literal ID="litSelect" runat="server" />

                        <option value="">-- Please Select --</option>
                        <asp:Literal ID="litOptions" runat="server" />
                        </select>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:TextBox type="hidden" runat="server" ID="productSku" ClientIDMode="static" />
                <input name="quantity-to-add" id="quantity-to-add" type="hidden" value="1" />
                <asp:Button runat="server" ID="btnAddToBasket" ClientIDMode="static" class="btn btn-block btn-success" Text="Add to basket!" OnClick="btnAddToBasket_Click" />
           
            </div>
            <div class="tabbable">
                <ul class="nav nav-tabs" role="tablist">
                    <li class="active"><a href="#product-description" data-toggle="tab" role="tab">Details</a></li>
                    <li><a href="#delivery-info" data-toggle="tab" role="tab">Delivery</a></li>
                    <li><a href="#returns-info" data-toggle="tab" role="tab">Returns</a></li>
                    <li><a href="#customer-reviews" data-toggle="tab" role="tab">Reviews</a></li>
                </ul>
                <div class="tab-content">
                    <article class="tab-pane active" id="product-description" itemprop="description">
                        <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                    </article>
                    <div class="tab-pane" id="delivery-info">
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas placerat elementum tristique. Ut ut pretium massa. Nullam mollis lobortis rutrum. Integer quis tellus enim. Donec viverra aliquam faucibus. Nam ac eros velit. Mauris vel adipiscing turpis. Mauris id tortor et augue tincidunt molestie in sed lectus.</p>
                    </div>
                    <div class="tab-pane" id="returns-info">
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas placerat elementum tristique. Ut ut pretium massa. Nullam mollis lobortis rutrum. Integer quis tellus enim. Donec viverra aliquam faucibus. Nam ac eros velit. Mauris vel adipiscing turpis. Mauris id tortor et augue tincidunt molestie in sed lectus.</p>
                    </div>
                    <div class="tab-pane" id="customer-reviews">
                        <asp:Literal ID="litReviewHeadline" runat="server" />
                        <asp:Repeater ID="rptReviews" runat="server" OnItemDataBound="ReviewRepeaterItemDataBound">
                            <ItemTemplate>
                                <section itemprop="review" itemscope itemtype="http://schema.org/Review" class="review">
                                    <header>
                                        <div itemprop="reviewRating" itemscope itemtype="http://schema.org/Rating" class="review-stars">
                                            <asp:Literal ID="litDisplayStars" runat="server" />
                                        </div>
                                        <p itemprop="name" class="review-headline">
                                            <asp:Literal ID="litSpecificReviewHeadline" runat="server" />
                                        </p>
                                    </header>
                                    <aside class="review-by">
                                        <p>
                                            by <span itemprop="author">
                                                <asp:Literal ID="litCustomerFirstName" runat="server" /></span> on
                                        <asp:Literal ID="litAbbr" runat="server" />
                                        </p>
                                    </aside>
                                    <p itemprop="description">
                                        <asp:Literal ID="litReviewText" runat="server" />
                                    </p>
                                    <meta itemprop="ratingValue" id="metaReviewRating" runat="server" content="" />
                                    <meta itemprop="worstRating" content="1">
                                    <meta itemprop="bestRating" content="5">
                                    <meta itemprop="datePublished" id="metaDatePublished" runat="server" content="" />
                                    <asp:Literal ID="litComments" runat="server" />
                                </section>
                            </ItemTemplate>
                        </asp:Repeater>

                        <h5>Send Us Your Review</h5>
                        <%--<div class="control-group">
                            <label class="control-label" for="review-rating">Rating</label>
                            <div class="controls rating">
                                <label>
                                    <input type="radio" name="review-rating" value="1" /><i class="icon-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="2" /><i class="icon-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="3" /><i class="icon-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="4" /><i class="icon-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="5" /><i class="icon-star"></i></label>
                            </div>
                        </div>--%>
                        <div class="control-group">
                            <label class="control-label" for="review-rating">Rating</label>
                            <div class="controls rating">
                                <label>
                                    <input type="radio" name="review-rating" value="1" /><i class="fa fa-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="2" /><i class="fa fa-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="3" /><i class="fa fa-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="4" /><i class="fa fa-star"></i></label>
                                <label>
                                    <input type="radio" name="review-rating" value="5" /><i class="fa fa-star"></i></label>
                            </div>
                        </div>
                        <div id="review-form">
                            <div class="control-group">
                                <br/>
                                <label class="control-label" for="review-name">Your Name</label>
                                <div class="controls">
                                    <input type="text" id="reviewName" runat="server" name="review-name" placeholder="Name" class="required span12" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="reviewName"
                                        ValidationGroup="Review"
                                        ErrorMessage="Name required"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="review-email">Your Email</label>
                                <div class="controls">
                                    <input type="text" id="reviewEmail" runat="server" name="review-email" placeholder="Email" class="required span12" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        runat="server"
                                        ValidationExpression=".+@.+\..*"
                                        ControlToValidate="reviewEmail"
                                        ValidationGroup="Review"
                                        Display="Dynamic"
                                        ErrorMessage="Email not valid"
                                        ForeColor="Red">
                                    </asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="reviewEmail"
                                        ErrorMessage="Email Required"
                                        Display="Dynamic"
                                        ValidationGroup="Review"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="review-headline">Review Title</label>
                                <div class="controls">
                                    <input type="text" id="reviewHeadline" clientidmode="static" runat="server" name="review-headline" placeholder="Title" class="required span12" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="reviewHeadline"
                                        ValidationGroup="Review"
                                        ErrorMessage="Review headline required"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="review-text">Comments</label>
                                <div class="controls">
                                    <textarea rows="3" id="reviewText" runat="server" name="review-text" placeholder="Your review" class="required span12"></textarea>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="reviewText"
                                        ValidationGroup="Review"
                                        ErrorMessage="Review Required"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <asp:Button runat="server" ID="btnSubmitReview" name="review-product" class="btn btn-primary saveButton" Text="Save" OnClick="btnSubmitReview_Click" ValidationGroup="Review" />
                                    <input type="hidden" id="reviewProductSku" name="reviewed-product-sku" />
                                    <input type="hidden" name="review-product" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
