<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="productdetails.aspx.cs" Inherits="strutt.productdetails" %>
<%@ Import Namespace="BLL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
  <script>
        //Added by chandni
        function ShowPopup() {
            $('#modalWE').modal('show');
        }
    </script>
  <style type="text/css">
.ratingEmpty {
    background-image: url(/images/Star1.gif);
    width: 25px;
    height: 25px;
}
.ratingFilled {
    background-image: url(/images/Star2.gif);
    width: 25px;
    height: 25px;
}
.ratingSaved {
    background-image: url(/images/Star3.gif);
    width: 25px;
    height: 25px;
}
.Rating {
    padding-left: 0px;
    padding-top: 10px;
}
.rating-lable {
    padding-left: 5px;
    float: left;
    margin: 10px;
}
</style>
  <script type="text/javascript">
        function checkLogin() {
            if ('<%= Session["CustomerLoginDetails"]%>' == '') {
                login();
                return false;
            }
        }


        function addWishList(id) {
            $.ajax({
                type: "POST",
                url: "/DataServices.asmx/AddWishlist",
                data: '{id: ' + id + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessAdd,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>
  <style type="text/css">
.no-padding {
    padding: 0px 0px 6px 6px;
}
.spec > span {
    width: 300px;
    display: inline;
}
</style>
  <script>
$(window).scroll(function() {
  if ($(this).scrollTop() > 600) {
    
	  $('.bottom-addtocart-button').fadeIn();
  } else {
    $('.bottom-addtocart-button').fadeOut();
  }
});
</script>
  <style>
.bottom-addtocart-button {
    position: fixed;
    bottom: 0;
    padding: 20px 0px;
    background: #ECECEC;
    z-index: 99999999;
    width: 100%;
    text-align: center;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">

<div class="breadcrumb-area">
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="row breadcrumb_box  align-items-center">
          <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
            <h2 class="breadcrumb-title">Product Details</h2>
          </div>
          <div class="col-lg-6  col-md-6 col-sm-6"> 
            <!-- breadcrumb-list start -->
            <%--<ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="#">Home</a></li>
                                <li class="breadcrumb-item active">Product Details</li>
                            </ul>--%>
            <!-- breadcrumb-list end --> 
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<div class="container">
  <div class="row">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
      <ul class="breadcrumb-list text-center text-sm-right pull-left m-3">
        <li class="breadcrumb-item">
          <asp:LinkButton ID="btnHome" runat="server" PostBackUrl="~/category.aspx"><span itemprop="name">Shop</span></asp:LinkButton>
        </li>
        <li class="breadcrumb-item">
          <asp:Repeater ID="rpt_naviCategory" runat="server">
            <ItemTemplate>
              <asp:HyperLink ID="hlcategory" runat="server" CssClass="link" Text='<%#DataBinder.Eval(Container.DataItem,"menu_name").ToString().Substring(0, 1).ToUpper() + Eval("menu_name").ToString().Substring(1, (Eval("menu_name").ToString().Length - 1)).ToLower()%>'
                                    NavigateUrl='<%# String.Format("{0}", Eval("menu_name").ToString().ToLower().Replace(" ", "-")) %>'></asp:HyperLink>
            </ItemTemplate>
          </asp:Repeater>
        </li>
        /
        <li>
          <asp:Repeater ID="rpt_naviSubCategory" runat="server">
            <ItemTemplate>
              <asp:HyperLink ID="hlcreativepartner" runat="server" CssClass="link" Text='<%#DataBinder.Eval(Container.DataItem,"sub_menu_name")%>'
                                NavigateUrl='<%# String.Format("{0}{1}{2}", Eval("menu_name").ToString().ToLower().Replace(" ", "-"),"/", Eval("sub_menu_name").ToString().ToLower().Replace(" ", "-").Replace("-and-", "-")) %>'></asp:HyperLink>
            </ItemTemplate>
          </asp:Repeater>
        </li>
        /
        <li>
          <asp:Label ID="lblProNameNavigation" runat="server"></asp:Label>
        </li>
      </ul>
    </div>
  </div>
</div>
<div class="site-wrapper-reveal">
  <div class="single-product-wrap section-space--pt_90 border-bottom">
    <div class="container">
      <div class="row">
        <div class="col-lg-7 col-md-6 col-sm-12 col-xs-12"> 
          <!-- Product Details Left -->
          <div class="product-details-left">
            <div class="product-details-images-2 slider-lg-image-2">
              <asp:Repeater ID="rptimgZoomLarge" runat="server">
                <ItemTemplate>
                  <div class="easyzoom-style">
                    <div class=""> <a id="linkzoomlarg" runat="server" href='<%# "~/images/Product/Large/" + Eval("large_image") %>' class="poppu-img"> <img id="imgzoomLarge" runat="server" src='<%# "~/images/Product/Large/" + Eval("large_image") %>' class="img-fluid" alt="" /> </a> </div>
                  </div>
                </ItemTemplate>
              </asp:Repeater>
            </div>
            <div class="product-details-thumbs-2 slider-thumbs-2">
              <asp:Repeater ID="rptimgZoom" runat="server">
                <ItemTemplate>
                  <div class="sm-image"> <img id="imgzoomsmall" runat="server" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt="product image thumb" width="80" /> </div>
                </ItemTemplate>
              </asp:Repeater>
            </div>
            <!--// Product Details Left --> 
          </div>
        </div>
        <div class="col-lg-5 col-md-6 col-sm-12 col-xs-12">
          <div class="product-details-content">
            <h5 class="font-weight--reguler mb-10">
              <asp:Label ID="lblProductName" runat="server"></asp:Label>
            </h5>
            <h3 class="price">Rs.
              <asp:Label ID="lblSalePrice" runat="server"></asp:Label>
              <strike>
              <asp:Label ID="lblDiscPrice" runat="server"></asp:Label>
              </strike> </h3>
            <p class="starability-result" data-rating="0" id="ProReviewId"></p>
            <asp:HiddenField ID="hfProId" runat="server"></asp:HiddenField>
            <div class="quickview-peragraph">
              <div class="stock out-of-stock">
                <p> Available: <span>
                  <asp:Label ID="lblStock" runat="server" Text="Out of Stock" CssClass="text-success"></asp:Label>
                  </span> </p>
              </div>
              <asp:TextBox ID="txtCheckPincode" Width="275px"  runat="server" CssClass="single-input pt-1 f-14"  placeholder="Enter Pincode to check COD availabilty" MaxLength="6"></asp:TextBox>
              <asp:LinkButton ID="btnCheckPincode" runat="server" OnClick="btnCheckPincode_Click" CssClass="s-btn"><i class="icon-magnifier" style="color:white;"></i></asp:LinkButton>
              <div class="quickview-wishlist button">
                <asp:LinkButton ID="lbtnAddWishlist" runat="server" OnClick="lbtnAddWishlist_Click"><i class="icon-heart"></i></asp:LinkButton>
              </div>
              <span id="siteseal">
              <%-- <script async type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=CUhuxumBKCWGuUMfyeOz2N8aFMngNh1bBV8o0XECy8ruK2I8C6Rhc9doEOJ3"></script>--%>
              <asp:Image runat="server" ImageUrl="images/Razorpay.png" Width="160" />
              </span>
              <asp:Label ID="litCheckPincodeMsg" runat="server" CssClass="err"></asp:Label>
              <asp:CompareValidator runat="server" CssClass="red margin-t-10" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtCheckPincode" ErrorMessage="Value must be a number." Display="Dynamic" ForeColor="Red" />
              <asp:Label ID="lblQty" runat="server" CssClass="red margin-t-10"></asp:Label>
            </div>
            <div class="quickview-action-wrap mt-30">
              <div class="quickview-cart-box">
                <div class="quickview-button">
                  <div class="quickview-cart button">
                    <asp:LinkButton ID="btnAddToCart" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" OnClick="btnAddToCart_Click"> ADD TO BAG</asp:LinkButton>
                  </div>
                  <br>
                  <div class="quickview-cart button">
                    <asp:LinkButton ID="btnbuynow" runat="server" class="btn--lg btn--black font-weight--reguler text-white" OnClick="btnbuynow_Click"> BUY NOW</asp:LinkButton>
                  </div>
                  <div class="quickview-cart button">
                    <asp:LinkButton ID="btnCustomBug" runat="server" CssClass="btn btn-primary col-12-12" Visible="false" OnClick="btnCustomBug_Click"><i class="fa fa-suitcase" aria-hidden="true"></i> PERSONALIZE BAG</asp:LinkButton>
                  </div>
                </div>
              </div>
            </div>
            <p class="text-orange mt-1 mb-1">
              <asp:Label ID="lblTax" runat="server" Visible="false"></asp:Label>
            </p>
            <p class="text-orange mt-1 mb-1">
              <asp:Label ID="lblCOD" runat="server" Visible="false"></asp:Label>
            </p>
            <p class="text-orange mt-1 mb-1">
              <asp:Label ID="lblDiscount" runat="server" Visible="false"></asp:Label>
            </p>
            <p class="text-orange mt-1 mb-1">
              <asp:Label ID="lblGet" runat="server" Visible="false"></asp:Label>
            </p>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
          <div class="quickview-action-wrap mt-30">
            <ul class="offer_three">
              <li>
                <div class="sku_wrapper item_meta offer_three"> <span class="label"><a href="https://wa.me/+918800400570" target="_blank"> <img src="../../images/Whatsapp.png" alt="" /> </a></span><span class="sku">Can't decide, have more questions, Ask your Travel Expert now ... </span> </div>
              </li>
              <li>
                <div class="posted_in item_meta offer_three">
                  <div class="sku_wrapper item_meta"> <span class="label"><a href="tel:+918800400570" target="_blank"> <img src="../../images/Callus.png" alt="" /> </a></span><span class="sku">Want to know more? Call us now and have your questions answered right away... </span> </div>
                </div>
              </li>
              <li>
                <div class="tagged_as item_meta offer_three">
                  <div class="sku_wrapper item_meta"> <span class="label"><a href="http://www.facebook.com/sharer.php?u=<%= System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl %>" target="_blank"> <img src="../../images/fb.jpg" alt="" /> </a> <a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="../../images/insta.jpg" /></a> </span><span class="sku">Can't decide, take an opinion from your friends .. Share Now </span> </div>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div class="product-details-tab section-space--pt_90">
            <ul role="tablist" class=" nav">
              <li class="active" role="presentation"><a data-toggle="tab" role="tab" href="#description" class="active">Description</a> </li>
              <li role="presentation"><a data-toggle="tab" role="tab" href="#info">Additional information</a> </li>
              <li role="presentation"><a data-toggle="tab" role="tab" href="#reviews">Reviews</a> </li>
            </ul>
          </div>
        </div>
        <div class="col-12">
          <div class="product_details_tab_content tab-content mt-30"> 
            <!-- Start Single Content -->
            <div class="product_tab_content tab-pane active" id="description" role="tabpanel">
              <div class="pro_feature">
                <table class="shop_attributes">
                  <tbody>
                    <tr>
                      <td><asp:Literal ID="lblPDFullDescription" runat="server"></asp:Literal>
                      <td>
                    </tr>
                    <tr>
                      <td><asp:Literal ID="lblFeatures" runat="server"></asp:Literal>
                      <td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                    </tr>
                    <tr>
                      <th>Weight:</th>
                      <td><asp:Literal ID="lblPDWeight" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                      <th>Material:</th>
                      <td><asp:Literal ID="lblPDMeterial" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                      <th>Color:</th>
                      <td><asp:Literal ID="lblPDColor" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                      <th>Size:</th>
                      <td><asp:Literal ID="lblPDSize" runat="server"></asp:Literal></td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
            <div class="product_tab_content tab-pane " id="info" role="tabpanel">
              <div class="product_description_wrap">
                <div class="product-details-wrap">
                  <div class="row align-items-center">
                    <div class="col-lg-8 order-md-1 order-2">
                      <div class="details mt-30">
                        <h5 class="mb-10">Info</h5>
                        <ul class="feature_list">
                          <li><a href="#"><i class="fa fa-info-circle" aria-hidden="true"></i>Cant decide still ?!? Why don't you call your personal Strutt shopper to assist you in your purchase # 8800400570</a></li>
                          <li><a href="#"><i class="fa fa-info-circle" aria-hidden="true"></i>Still Undecided ?!?! Place a COD order or else Remember you can always courier the product back to us within 7 days & we will refund your cash immediately </a></li>
                        </ul>
                      </div>
                    </div>
                    <%--<div class="col-lg-4 order-md-2 order-1">
                                                <div class="images">
                                                    <img src="../../../assets/images/product/single-product-01.jpg" class="img-fluid" alt="">
                                                </div>
                                            </div>--%>
                  </div>
                </div>
                <div class="product-details-wrap">
                  <div class="row align-items-center">
                    <div class="col-lg-8 order-md-1 order-2">
                      <div class="details mt-30">
                        <div class="pro_feature">
                          <h5 class="title_3 mb-10">Delivery & returns</h5>
                          <ul class="feature_list">
                            <li><a href="#"><i class="fa fa-refresh"></i>
                              <asp:Literal ID="litRetunText" runat="server"></asp:Literal>
                              </a></li>
                            <li><a href="#"><i class="fa fa-truck"></i>Get free delivery for orders above Rs. 750</a></li>
                            <li><a href="#"><i class="fa fa-map-marker"></i>Check for COD availability</a></li>
                          </ul>
                        </div>
                      </div>
                    </div>
                    <%--<div class="col-lg-4 order-md-2 order-1">
                                                <div class="images">
                                                    <img src="../../../assets/images/product/single-product-02.jpg" class="img-fluid" alt="">
                                                </div>
                                            </div>--%>
                  </div>
                </div>
                <div class="product-details-wrap">
                  <div class="row align-items-center">
                    <div class="col-lg-8 order-md-1 order-2">
                      <div class="details mt-30">
                        <div class="pro_feature">
                          <h5 class="title_3 mb-10">Warranty</h5>
                          <p>
                            <asp:Literal ID="litWarrantyText" runat="server"></asp:Literal>
                          </p>
                        </div>
                      </div>
                    </div>
                    <%--<div class="col-lg-4 order-md-2 order-1">
                                                <div class="images">
                                                    <img src="../../../assets/images/product/single-product-01.jpg" class="img-fluid" alt="">
                                                </div>
                                            </div>--%>
                  </div>
                </div>
                <div class="product-details-wrap">
                  <div class="row align-items-center">
                    <div class="col-lg-8 order-md-1 order-2">
                      <div class="details mt-30">
                        <div class="pro_feature">
                          <h5 class="title_3 mb-10">Color Disclaimer</h5>
                          <p>Please allow a 10% variation in the Actual colours of the product . This is due to the fact that every computer monitor has a different capability to display colours and that everyone sees these colours differently. It is our endeavour to show the product as life-like as possible,However please account for a slight variation in colour due to the computer settings.</p>
                        </div>
                      </div>
                    </div>
                    <%--<div class="col-lg-4 order-md-2 order-1">
                                                <div class="images">
                                                    <img src="../../../assets/images/product/single-product-02.jpg" class="img-fluid" alt="">
                                                </div>
                                            </div>--%>
                  </div>
                </div>
              </div>
            </div>
            <div class="product_tab_content tab-pane" id="reviews" role="tabpanel">
              <asp:UpdatePanel ID="pnlRating" runat="server">
                <ContentTemplate> 
                  <!-- Start RAting Area -->
                  <div class="rating_wrap mb-30">
                    <h4 class="rating-title-2">Review this product</h4>
                    <p>Your rating</p>
                    <%--<asp:HiddenField ID="hfieldProductId" runat="server" />
                                            <cc1:Rating ID="ratingControl" AutoPostBack="true" runat="server" CssClass="Rating" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                            </cc1:Rating>
                                            <asp:Label ID="lbltxt" runat="server" ForeColor="Red" />--%>
                    <asp:HiddenField ID="hfieldProductId" runat="server" />
                    <div class="rating_list">
                      <cc1:Rating ID="ratingControl" AutoPostBack="true" runat="server" CssClass="Rating" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled"> </cc1:Rating>
                      <asp:Label ID="lbltxt" runat="server" ForeColor="Red" />
                    </div>
                  </div>
                  <!-- End RAting Area -->
                  
                  <div class="comments-area comments-reply-area">
                    <div class="row">
                      <div class="col-lg-6">
                        <asp:Label ID="lblValMes" runat="server" Visible="false" ForeColor="Green"></asp:Label>
                        <div class="comment-form-area">
                          <p class="comment-form-comment">
                            <label>Your review <span class="text-red">*</span></label>
                            <asp:TextBox ID="txtReview" placeholder="Your Review" runat="server" TextMode="MultiLine" Height="150px" CssClass="comment-notes"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVComment" runat="server" ForeColor="Red" ErrorMessage="Please enter review"
                                                                ControlToValidate="txtReview" CssClass="Validators" SetFocusOnError="true" Display="Dynamic" ValidationGroup="review"></asp:RequiredFieldValidator>
                          </p>
                          <div class="comment-input">
                            <p class="comment-form-author">
                              <label>Name <span class="text-red">*</span></label>
                              <asp:TextBox ID="txtUserName" runat="server" placeholder="Your Name"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RFVName" runat="server"
                                                                    ControlToValidate="txtUserName" CssClass="Validators" SetFocusOnError="true" Display="Dynamic"
                                                                    ErrorMessage="Please enter name" ForeColor="Red" ValidationGroup="review"></asp:RequiredFieldValidator>
                            </p>
                            <p class="comment-form-email">
                              <label>Email <span class="text-red">*</span></label>
                              <asp:TextBox ID="txtemail" runat="server" placeholder="Email"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" Display="Dynamic"
                                                                    ControlToValidate="txtemail" CssClass="Validators"
                                                                    ErrorMessage="Please enter email id" ForeColor="Red" ValidationGroup="review"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                                    ControlToValidate="txtemail" CssClass="Validators" Display="Dynamic"
                                                                    ErrorMessage="Invalid Email Address" ForeColor="Red" SetFocusOnError="true"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="review"> </asp:RegularExpressionValidator>
                            </p>
                            <p class="comment-form-email">
                              <label>Title <span class="text-red">*</span></label>
                              <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Title of Your Review"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RFVTitle" runat="server"
                                                                    ControlToValidate="txtTitle" CssClass="Validators" SetFocusOnError="true" Display="Dynamic"
                                                                    ErrorMessage="Please enter title review" ForeColor="Red"
                                                                    ValidationGroup="review"></asp:RequiredFieldValidator>
                            </p>
                          </div>
                          <div class="comment-form-submit">
                            <asp:Button ID="Btn_Reviews" runat="server" CssClass="comment-submit"
                                                                OnClick="Btn_Reviews_Click" Text="Post Review" ValidationGroup="review" />
                          </div>
                        </div>
                      </div>
                      <div class="col-lg-6">
                        <h3><strong>Read review</strong></h3>
                        <asp:Literal ID="litReviewHead" runat="server"></asp:Literal>
                        </br>
                        <asp:Repeater ID="Rvp_ShowReview" runat="server">
                          <ItemTemplate>
                            <div class="comment-text"> <span style="background: #49ac31; color: #fff; padding: 2px 5px; font-weight: bold;"><%#Eval("rating")%>&nbsp; &#9733;</span> <strong><%#Eval("title")%> </strong>
                              <p><%#Eval("reviews")%></p>
                              <span style="font-size: 10px;"><%#Eval("user_name")%> - <%#Eval("CDate")%></span> </div>
                            <hr class="hr-light margin-t-10" />
                          </ItemTemplate>
                        </asp:Repeater>
                      </div>
                    </div>
                  </div>
                </ContentTemplate>
              </asp:UpdatePanel>
            </div>
            <!-- End Single Content --> 
          </div>
        </div>
      </div>
      <%--<div class="product_socials section-space--mt_60"> <span class="label">Share this items :</span>
        <ul class="helendo-social-share socials-inline">
          <li><a class="share-facebook helendo-facebook" href="https://www.facebook.com/theSTRUTTstore/" target="_blank"><i class="social social_facebook"></i></a></li>
          <li><a class="share-instagram helendo-instagram" href="https://www.instagram.com/thestruttstore/" target="_blank"><i class="social social_instagram_square"></i></a></li>
        </ul>
      </div>--%>
      <div class="related-products section-space--ptb_90">
        <div class="row">
          <div class="col-lg-12">
            <div class="section-title text-center mb-30">
              <h4>Related products</h4>
            </div>
          </div>
        </div>
        <div class="row product-slider-active">
          <asp:Repeater ID="rptRelatedProduct" runat="server" OnItemDataBound="rptRelatedProduct_ItemDataBound" OnItemCommand="rptRelatedProduct_ItemCommand">
            <ItemTemplate>
              <div class="col-lg-12"> 
                <!-- Single Product Item Start -->
                <div class="single-product-item text-center">
                  <div class="products-images"> <a id="A2" class="product-thumbnail" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                            DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                            DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                runat="server"
                                                title='<%# Eval("product_name")%>'>
                    <%--<img src='<%# "~/images/Product/Large/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' class="img-fluid">--%>
                    <img id="Img1" runat="server" class="img-fluid" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' /> </a>
                    <div class="product-actions">
                      <asp:LinkButton ID="btnRelatedMP" runat="server" OnClientClick="ShowPopup()" CommandName="quickrelated" CommandArgument='<%# Eval("product_id") %>'><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></asp:LinkButton>
                      <a href='<%# "/cart.aspx?proid=" + Eval("product_id")%>' runat="server" title='<%# Eval("product_name")%>'><i class="p-icon icon-bag2"></i><span class="tool-tip">Add to cart</span></a>
                      <asp:LinkButton ID="lbtnAddWishlist" runat="server" OnClick="lbtnAddWishlist_Click"><i class="icon-heart"></i></asp:LinkButton>
                    </div>
                  </div>
                  <div class="product-content">
                    <h6 class="prodect-title"><a id="A1" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                            DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                            DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                runat="server"
                                                title='<%# Eval("product_name")%>'><%# Eval("product_name")%></a></h6>
                    <div class="prodect-price"> <span class="old-price">Rs.<%#Eval("sale_price")%></span> </div>
                  </div>
                </div>
                <!-- Single Product Item End --> 
              </div>
            </ItemTemplate>
          </asp:Repeater>
        </div>
      </div>
    </div>
  </div>
</div>
<div class="product-modal-box modal fade" id="modalWE" tabindex="-1" role="dialog">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span class="icon-cross" aria-hidden="true"></span></button>
      </div>
      <div class="modal-body container">
        <div class="row align-items-center">
          <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="quickview-product-active mr-lg-5">
              <asp:Repeater ID="rptRelatedZoom2" runat="server">
                <ItemTemplate>
                  <div class="easyzoom-style">
                    <div class="easyzoom easyzoom--overlay"> <img id="imgzoomsmall" runat="server" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt="product image thumb" class="img-fluid" /> </div>
                  </div>
                </ItemTemplate>
              </asp:Repeater>
            </div>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="product-details-content quickview-content-wrap ">
              <h5 class="font-weight--reguler mb-10">
                <asp:Label ID="lblProName" runat="server"></asp:Label>
              </h5>
              <p class="starability-result" data-rating="0" id="ProReview2Id"></p>
              <asp:HiddenField ID="hfPro2Id" runat="server"></asp:HiddenField>
              <h3 class="price">Rs.
                <asp:Label ID="lblSalePrice2" runat="server"></asp:Label>
              </h3>
              <div class="stock in-stock mt-10">
                <p> Available: <span>
                  <asp:Label ID="lblStock2" runat="server" Text="Out of Stock" CssClass="text-red"></asp:Label>
                  </span> </p>
              </div>
              <div class="quickview-peragraph mt-10">
                <p>
                  <asp:Label ID="lblSortDesc" runat="server"></asp:Label>
                </p>
              </div>
              <div class="quickview-action-wrap mt-30">
                <div class="quickview-cart-box">
                  <div class="quickview-button">
                    <div class="quickview-cart button">
                      <asp:LinkButton ID="btnAddToCart2" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" OnClick="btnAddToCart2_Click"> ADD TO BAG</asp:LinkButton>
                    </div>
                    <div class="quickview-wishlist button">
                      <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbtnAddWishlist_Click"><i class="icon-heart"></i></asp:LinkButton>
                    </div>
                  </div>
                  <%--<div class="quickview-wishlist button">
                                            <asp:LinkButton ID="lbtnAddWishlist1" runat="server" ><i class="icon-heart"></i></asp:LinkButton>
                                        </div>--%>
                  <%--<div class="quickview-cart button">
                                            <asp:LinkButton ID="btnbuynow" runat="server" class="btn--lg btn--black font-weight--reguler text-white" OnClick="btnbuynow_Click"> Buy Now </asp:LinkButton>
                                        </div>--%>
                </div>
              </div>
            </div>
            <div class="product_meta mt-30">
              <div class="sku_wrapper item_meta"> <span class="label">Material: </span><span class="sku">
                <asp:Literal ID="lblPDMeterial2" runat="server"></asp:Literal>
                </span> </div>
              <div class="posted_in item_meta"> <span class="label">Color: </span><span class="sku">
                <asp:Literal ID="lblPDSize2" runat="server"></asp:Literal>
                </span> </div>
              <div class="tagged_as item_meta"> <span class="label">Size: </span><span class="sku">
                <asp:Literal ID="lblPDColor2" runat="server"></asp:Literal>
                </span> </div>
            </div>
            <div class="product_socials section-space--mt_60"> <span class="label">Share this items :</span>
              <ul class="helendo-social-share socials-inline">
                <li><a href="https://wa.me/+918800400570" target="_blank"> <img src="../../images/Whatsapp.png" alt="" /></a> </li>
                <li><a href="tel:+918800400570" target="_blank"> <img src="../../images/Callus.png" alt="" /></a></li>
                <li><a href="http://www.facebook.com/sharer.php?u=<%= System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl %>" target="_blank"> <img src="../../images/fb.jpg" alt="" /></a></li>
                <li><a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="../../images/insta.jpg" /></a></li>
              </ul>
                
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
  <div id="FixBuyNow" class="bottomMenu hidef"></div>
  <script>
      myID = document.getElementById("FixBuyNow");

var myScrollFunc = function () {
    var y = window.scrollY;
    if (y >= 300) {
        myID.className = "bottomMenu showf"
    } else {
        myID.className = "bottomMenu hidef"
    }
};

window.addEventListener("scroll", myScrollFunc);
    </script> 
  <script>
     $(document).ready(function(){
		$("a#cph_main_btnAddToCart").clone().appendTo("#FixBuyNow"); 
	 })
    </script>

  <script>
        function openReview() {
            $("#pnlreview").click();
        }
        $(function () {
            document.getElementById("ProReviewId").attributes["data-rating"].value = document.getElementById('<%=hfProId.ClientID %>').value;
             document.getElementById("ProReview2Id").attributes["data-rating"].value = document.getElementById('<%=hfPro2Id.ClientID %>').value;
         });
    </script> 
  <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 827193669;
        var google_custom_params = window.google_tag_params;
        var google_remarketing_only = true;
        /* ]]> */
    </script> 
  <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
    </script>
  <noscript>
  <div style="display: inline;"> <img height="1" width="1" style="border-style: none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0" /> </div>
  </noscript>
  <!--	<div class="bottom-addtocart-button">
 Test
</div>--> 
  
</asp:Content>
