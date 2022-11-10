<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default1.aspx.cs" Inherits="strutt.default1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>The Strutt Store</title>
<style type="text/css">
img.wp-smiley, img.emoji {
	display: inline !important;
	border: none !important;
	box-shadow: none !important;
	height: 1em !important;
	width: 1em !important;
	margin: 0 .07em !important;
	vertical-align: -0.1em !important;
	background: none !important;
	padding: 0 !important;
}
</style>
<link rel="stylesheet" href="css/font-awesome.min.css" type="text/css" media="all" />
<link rel="stylesheet" href="css/font-stroke.min.css" type="text/css" media="all" />
<link rel="stylesheet" href="css/style3.css" type="text/css" media="all" />
<link rel="stylesheet" href="css/js_composer.min.css" type="text/css" media="all" />
<script type="text/javascript" src="js/jquery.min.js"></script>
    <link href="assets/css/vendor/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
<style type="text/css">
.jas-page > div {
	margin: 0;
}
h3 {
	margin: 0;
}
</style>
<style>
.wpb_animate_when_almost_visible {
	opacity: 1;
}
</style>
    <link href="fonts/font_style.css" rel="stylesheet" />
    <link href="assets/css/vendor/linearicons.min.css" rel="stylesheet" />
</head>
<body class="page-template-default page page-id-3812 theme-gecko woocommerce-no-js yith-wcan-free has-btn-sticky jan-atc-behavior-popup wpb-js-composer js-comp-ver-6.5.0 vc_responsive">
    <form id="form1" runat="server">
    <div id="jas-wrapper">
  <header id="jas-header" class="header-3"  role="banner" itemscope="itemscope" itemtype="http://schema.org/WPHeader" >
    <div class="header__top bgbl pl__15 pr__15">
      <div class="jas-row middle-xs">
        <div class="jas-col-md-3 jas-col-sm-6 jas-col-xs-12 flex start-md start-sm center-xs social-logo">
          <div class="jas-socials">
              
              <a class="dib br__50 tc" href="https://www.facebook.com/struttstore/" target="_blank">
                  <img src="images/social/fb_logo.png" />
              </a>
              <a class="dib br__50 tc" href="https://twitter.com/thestruttstore" target="_blank">
                  <img src="images/social/twitter_logo.png" />
              </a>
              <a class="dib br__50 tc" href="https://www.instagram.com/thestruttstore/" target="_blank">
                  <img src="images/social/insta_logo.png" />
              </a>

              <a class="dib br__50 tc" href="https://in.linkedin.com/company/the-strutt-store" target="_blank">
                  <img src="images/social/Linkedin_logo.png" />
              </a>
              <a class="dib br__50 tc" href="https://in.pinterest.com/thestruttstore/?eq=thestruttstore&etslf=5276" target="_blank">
                  <img src="images/social/pintrst_logo.png" />
              </a>
              <a class="dib br__50 tc" href="https://api.whatsapp.com/send?phone=8800400570" target="_blank">
                  <img src="images/social/whatsapp_logo.png" />
              </a>
              <a class="dib br__50 tc" href="https://www.youtube.com/channel/UC6W3x4sVRDD0v0-0fIfYOUw" target="_blank">
                  <img src="images/social/Youtube_logo.png" />
              </a>
          </div>
        </div>
        <div class="jas-col-md-9 jas-col-sm-6 jas-col-xs-12 flex end-lg end-sm center-xs">
          <marquee   onmouseover="stop()"  onmouseout="start()" style="color:white;">
              <asp:Label ID="lblMarquee1" runat="server" ForeColor="White"></asp:Label>
              <asp:Label ID="lblMarquee2" runat="server" ForeColor="Orange"></asp:Label>

          </marquee>
        </div>
          <div id="divAfterLogin" runat="server" visible="false" class="jas-col-md-12 jas-col-sm-12 jas-col-xs-12 flex end-lg end-sm center-xs" style="color:#fff; font-size:12px;">
            <asp:Label ID="lblCustName" runat="server"></asp:Label>
            </div>

        <%--<div class="jas-col-md-4 jas-col-sm-6 jas-col-xs-12 flex end-lg end-sm center-xs">
          <div class="jas-currency dib pr"><span class="current">USD<i class="fa fa-angle-down ml__5"></i></span>
            <ul class="pa tr ts__03">
              <li><a class="currency-item cw chp" href="javascript:void(0);" data-currency="EUR">EUR</a></li>
              <li><a class="currency-item cw chp" href="javascript:void(0);" data-currency="USD">USD</a></li>
            </ul>
          </div>
        </div>--%>

      </div>
      <!-- .jas-row --> 

    </div>
    <!-- .header__top -->
    
    <div class="header__mid pl__30 pr__30">
      <div class="jas-row middle-xs">
          <div class="hidden-md visible-sm jas-col-sm-3 jas-col-xs-3"> <a href="javascript:void(0);" class="jas-push-menu-btn"> 
              <img src="img/hamburger-black.svg" width="25" height="22" alt="Menu" /> </a> </div>
        <div class="jas-col-md-2 jas-col-sm-6 jas-col-xs-6 start-md center-sm center-xs">
          <div class="jas-branding ts__05"><a class="db" href="/">
              <img class="regular-logo" src="assets/images/logo/strutt_newlogo.png" width="200" height="25" alt="Structt" />
              </a></div>
        </div>
        <div class="jas-col-md-8 hidden-sm">
          <nav class="jas-navigation flex center-xs" role="navigation">
            <ul id="jas-main-menu" class="jas-menu clearfix">
                <li class="menu-item menu-item-type-post_type_archive menu-item-object-portfolio menu-item-has-children menu-item-4241"><a href="/category.aspx">Shop</a> </li>
                <li class="menu-item menu-item-type-post_type_archive menu-item-object-portfolio menu-item-has-children menu-item-4241"><a href="/sales">Sale</a> </li>
                <li class="menu-item menu-item-type-post_type_archive menu-item-object-portfolio menu-item-has-children menu-item-4241"><a href="/lookbook">Corporates</a> </li>
                <li class="menu-item menu-item-type-post_type_archive menu-item-object-portfolio menu-item-has-children menu-item-4241"><a href="/comingsoon.aspx">Lookbook</a> </li>
                <li class="menu-item menu-item-type-post_type_archive menu-item-object-portfolio menu-item-has-children menu-item-4241"><a href="/review.aspx">Blog</a> </li>
                <li class="menu-item menu-item-type-post_type_archive menu-item-object-portfolio menu-item-has-children menu-item-4241"><a href="/bultgift">Bulk Gift</a> </li>
            </ul>
          </nav>
          <!-- .jas-navigation --> 
        </div>
        <div class="jas-col-md-2 jas-col-sm-3 jas-col-xs-3">
          <div class="jas-action flex end-xs middle-xs"> <a class="sf-open cb chp" href="javascript:void(0);" title="Search function only use for product search"><i class="pe-7s-search"></i></a>
            <div class="jas-my-account hidden-xs ts__05 pr"><a class="cb chp db" href="#"><i class="pe-7s-user"></i></a>
              <ul class="pa tc">
                <li>
                    <a id="aLogin" runat="server" class="db cg chp" href="login.aspx">Login / Register</a>
                    <a id="aOrderDetails" runat="server" class="db cg chp" href="account/orderhistory.aspx" visible="false">Order Details</a>
                </li>
              </ul>
            </div>
            <%--<a class="cb chp hidden-xs" href="#"><i class="pe-7s-like"></i></a>--%>
              <div class="cb chp hidden-xs">
                    <asp:LinkButton ID="btnWishlist" runat="server" CssClass="header-cart" PostBackUrl="~/wishlist.aspx" ToolTip="Wishlist">
                        <i class="icon-heart"></i>
                        <asp:Label ID="lblWishlist" CssClass="item-counter" runat="server" OnClientClick="ShowPopup()">0</asp:Label>
                    </asp:LinkButton>
                </div>

            <div class="jas-icon-cart1 pr">
                <%--<a class="cart-contents pr cb chp db" href="#miniCart" title="View your shopping cart">--%>
                <a class="cart-contents pr cb chp db" href="/cart.aspx" title="View your shopping cart">
                    <i class="pe-7s-shopbag"></i><span class="pa count bgb br__50 cw tc"><asp:Literal ID="lblCartCount" Text="0" runat="server"></asp:Literal></span>
                </a>
            </div>
          </div>
          <!-- .jas-action --> 
        </div>
      </div>
      <!-- .jas-row --> 
    </div>
    <!-- .header__mid -->
    <div class="header__search w__100 dn pf">
      <div class="pa">
       <asp:TextBox runat="server" class="search-field"  ID="txtSearch" placeholder="Search Anything..." autocomplete="off"></asp:TextBox>
                <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click" CssClass="search-icon" TabIndex="0" ForeColor="White"><i class="icon-magnifier"></i></asp:LinkButton>
      </div>
      <a id="sf-close" class="pa" href="#"><i class="pe-7s-close"></i></a>
    </div>
    <!-- #header__search -->
    
    <div class="jas-canvas-menu jas-push-menu">
      <h3 class="mg__0 tc cw bgb tu ls__2">Menu <i class="close-menu pe-7s-close pa"></i></h3>
      <div class="jas-action flex center-xs middle-xs hidden-md hidden-sm visible-xs mt__30"> <a class="sf-open cb chp" href="javascript:void(0);"><i class="pe-7s-search"></i></a>
        <div class="jas-my-account hidden-xs ts__05 pr"><a class="cb chp db" href="#"><i class="pe-7s-user"></i></a>
          <ul class="pa tc">
            <li><
                <asp:LinkButton ID="btnlogin" runat="server" CssClass="db cg chp" PostBackUrl="~/Login.aspx" ToolTip="Click here to login"> <i class="icon-user" style="font-size:x-large"></i>
                                </asp:LinkButton>
            </li>
          </ul>
        </div>
        <a class="cb chp" href="#"><i class="pe-7s-like"></i></a> </div>
      <!-- .jas-action -->
      
    </div>
    <!-- .jas-canvas-menu -->
    

      <div class="jas-canvas-menu jas-push-menu">
      <h3 class="mg__0 tc cw bgb tu ls__2">Menu <i class="close-menu pe-7s-close pa"></i></h3>
      <%--<div class="jas-action flex center-xs middle-xs hidden-md hidden-sm visible-xs mt__30"> 
          <a class="sf-open cb chp" href="javascript:void(0);"><i class="pe-7s-search"></i></a>
        <div class="jas-my-account hidden-xs ts__05 pr">
            <a class="cb chp db" href="http://janstudio.net/gecko/fashion/my-account/"><i class="pe-7s-user"></i></a>
          <ul class="pa tc">
            <li><a class="db cg chp" href="http://janstudio.net/gecko/fashion/my-account/">Login / Register</a></li>
          </ul>
        </div>
        <a class="cb chp" href="http://janstudio.net/gecko/fashion/wishlist/"><i class="pe-7s-like"></i></a> </div>--%>
      <!-- .jas-action -->
      <div id="jas-mobile-menu" class="menu-main-menu-container">
        <ul id="menu-main-menu" class="menu">
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="/category.aspx"><span>SHOP</span></a></li>
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="/sales"><span>SALE</span></a></li>
            <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="/lookbook"><span>Corporates</span></a></li>
            <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="/comingsoon.aspx"><span>Lookbook</span></a></li>
            <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="/review.aspx"><span>Blog</span></a></li>
            <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="/login.aspx"><span>Login / Register</span></a></li>
        </ul>
      </div>
    </div>

      
    <div class="jas-mini-cart jas-push-menu">
      <div class="jas-mini-cart-content">
        <h3 class="mg__0 tc cw bgb tu ls__2">Mini Cart <i class="close-cart pe-7s-close pa"></i></h3>




          <div class="widget_shopping_cart_content">

	<ul class="woocommerce-mini-cart cart_list product_list_widget ">
						


        <asp:Repeater ID="rptCartPc" runat="server" OnItemCommand="rptCartPc_ItemCommand">
                            <ItemTemplate>
                                <li class="woocommerce-mini-cart-item mini_cart_item">
                                    <asp:LinkButton ID="btnRemove" class="remove remove_from_cart_button" CommandName="Remove" 
                                        CommandArgument='<%# Eval("product_id") %>' runat="server">×</asp:LinkButton>									
                                    <a href="#">
							                <%--<img width="570" height="760" src="http://janstudio.net/gecko/fashion/wp-content/uploads/2016/04/p-46-570x760.jpg" 
                                                class="attachment-woocommerce_thumbnail size-woocommerce_thumbnail" alt="" loading="lazy" 
                                                srcset="#" sizes="(max-width: 570px) 100vw, 570px">--%>
                                        <img class="img-fluid" width="60" src='<%#Eval("thumb_image") %>' alt='<%# Eval("product_name")%>'>
                                        <%#Eval("product_name")%>
                                     </a>
										                <%--<dl class="variation">
			                            <dt class="variation-Size">Size:</dt>
		                            <dd class="variation-Size"><p>S</p>
                                </dd>
	                                </dl>--%>
					                <span class="quantity"><%#Eval("quantity")%> × <span class="woocommerce-Price-amount amount"><bdi>
                                        <span class="woocommerce-Price-currencySymbol">Rs.</span><%#Eval("sale_price")%></bdi></span></span>				
						        </li>
                            </ItemTemplate>
                        </asp:Repeater>


								
					</ul>

	<p class="woocommerce-mini-cart__total total" style="font-size:14px;">
		<strong>Subtotal:</strong> <span class="woocommerce-Price-amount amount"><bdi>
            <span class="woocommerce-Price-currencySymbol">Rs.</span><asp:Literal ID="litSubTotalAmt" runat="server"></asp:Literal></bdi></span>	</p>

              <p class="woocommerce-mini-cart__total total" style="font-size:14px;">
		<strong>Discount:</strong> <span class="woocommerce-Price-amount amount"><bdi>
            <span class="woocommerce-Price-currencySymbol">Rs.</span><asp:Literal ID="litTotalDiscount" runat="server"></asp:Literal></bdi></span>	</p>

              <p class="woocommerce-mini-cart__total total">
		<strong>Total:</strong> <span class="woocommerce-Price-amount amount"><bdi>
            <span class="woocommerce-Price-currencySymbol">Rs.</span><asp:Literal ID="litTotalAmt" runat="server"></asp:Literal></bdi></span>	</p>

	
	<p class="woocommerce-mini-cart__buttons buttons">
        <a href="cart.aspx" class="button wc-forward">View cart</a>
        <a href="proceedtopayment.aspx" class="button checkout wc-forward">Checkout</a>
	</p>
</div>

      </div>
    </div>
    <!-- .jas-mini-cart --> 
    
  </header>
  <!-- #jas-header -->
  
  <div id="jas-content">
    <div class="jas-row jas-page">
      <div class="jas-col-md-12 jas-col-xs-12 mt__60 mb__60" role="main"  itemscope="itemscope" itemtype="http://schema.org/CreativeWork" >
        <div class="vc_row">
          <div class="wpb_column vc_column_container vc_col-sm-12">
            <div class="vc_column-inner ">
              <div class="wpb_wrapper">
                <div id="metaslider-id-3801" style="width: 100%; margin: 0 auto;" class="ml-slider-3-20-0 metaslider metaslider-flex metaslider-3801 ml-slider">
                  <div id="metaslider_container_3801">
                    <div id="metaslider_3801">
                      <ul aria-live="polite" class="slides">
                        <li style="display: block; width: 100%;" class="slide-3803 ms-image"><img src="img/home_banner1.jpg" height="600" width="1920" alt="" class="slider-3801 slide-3803" />
                          <div class="caption-wrap">
                            <div class="caption">
                              <h3>mega summer sale</h3>
                              <h2><span class="cp">Big discount</span> up to 50%</h2>
                              <a href="/sales" class="button-o-w">Start shopping</a></div>
                          </div>
                        </li>
                        <li style="display: none; width: 100%;" class="slide-3802 ms-image"><img src="img/home_banner2.jpg" height="600" width="1920" alt="" class="slider-3801 slide-3802" />
                          <div class="caption-wrap">
                            <div class="caption">
                              <h3>Spring - summer 2021</h3>
                              <h2><span class="cp">New Arrivals</span> collection</h2>
                              <a href="/exclusive" class="button-o-w">Shop Now</a></div>
                          </div>
                        </li>
                        <li style="display: none; width: 100%;" class="slide-3804 ms-image"><img src="img/home_banner3.jpg" height="600" width="1920" alt="" class="slider-3801 slide-3804" />
                          <div class="caption-wrap">
                            <div class="caption">
                              <h3>HOT TRENDING</h3>
                              <h2><span class="cp">Places</span> The Travel Blog</h2>
                              <a href="/review.aspx" class="button-o-w">Read More</a></div>
                          </div>
                        </li>
                          <li style="display: none; width: 100%;" class="slide-3804 ms-image"><img src="img/home_banner4.jpg" height="600" width="1920" alt="" class="slider-3801 slide-3804" />
                          <div class="caption-wrap">
                            <div class="caption">
                              <h3>Spring - summer 2021</h3>
                              <h2><span class="cp">Travel</span> Exclusive</h2>
                              <a href="/category.aspx" class="button-o-w">Shop Now</a></div>
                          </div>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="jas-container">
          <div class="vc_row women-collection-section vc_custom_1460979474370">
            <div class="wpb_column vc_column_container vc_col-sm-12">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper" style="margin-top:40px;">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_appear appear" >
                    <div class="wpb_wrapper">
                      <div class="women-collection pr">
                        <h3 class="left pa tu custom-head-1">Travel Bags <span class="f__libre tl db"><a href="#">Shop now →</a></span></h3>
                        <p><a href="/travel-bags"><img loading="lazy" class="alignnone size-full wp-image-3814" src="img/img_home_travel_ags.jpg" 
                            alt="Travel Bags" width="570" height="712" 
                            sizes="(max-width: 570px) 100vw, 570px" /></a></p>
                        <h3 class="right pa custom-head-1">01 <span class="tu">Monsoon Sale 2021</span></h3>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="jas-container">
          <div class="vc_row">
            <div class="wpb_column vc_column_container vc_col-sm-6">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_left-to-right left-to-right" >
                    <div class="wpb_wrapper">
                      <div class="accessories-collection pr">
                        <h3 class="left pa mg__0 tu custom-head-1">Backpacks <span class="db f__libre fs__14"><a href="#">Shop now →</a></span></h3>
                        <p><a href="/Backpacks"><img loading="lazy" class="alignnone size-full wp-image-3828" src="img/img_home_backpacks.jpg" 
                            alt="Backpacks" width="504" height="401" 
                            sizes="(max-width: 504px) 100vw, 504px" /></a></p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="wpb_column vc_column_container vc_col-sm-6">
              <div class="vc_column-inner vc_custom_1459614834944">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_bottom-to-top bottom-to-top" >
                    <div class="wpb_wrapper">
                      <div class="handbags-collection pr">
                        <h3 class="pa mg__0 tu custom-head-1">Business Travel <span class="db fs__14 f__libre"><a href="#">Shop now →</a></span></h3>
                        <p><a href="/business-travel"><img loading="lazy" class="alignnone size-full wp-image-3829" 
                            src="img/img_home_business_travel.jpg" alt="Business Travel" width="570" height="408" 
                            sizes="(max-width: 570px) 100vw, 570px" /></a></p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="jas-container">
          <div class="vc_row vc_custom_1459611186065">
            <div class="wpb_column vc_column_container vc_col-sm-6">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_left-to-right left-to-right vc_custom_1463286743742" >
                    <div class="wpb_wrapper">
                      <div class="lookbook-collection pr tc dib">
                        <h3 class="pa tu custom-head-1">Lookbook<span class="fs__14 f__libre db"><a href="#">View collection →</a></span></h3>
                        <p><a href="#"><img loading="lazy" class="alignnone size-full wp-image-3839" 
                            src="img/img_home__lookbook.jpg" alt="Lookbook" width="465" height="354" 
                            sizes="(max-width: 465px) 100vw, 465px" /></a></p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="wpb_column vc_column_container vc_col-sm-6">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_right-to-left right-to-left" >
                    <div class="wpb_wrapper">
                      <div class="clothing-collection pr tr">
                        <h3 class="pa tu custom-head-1">One Day Travel<span class="fs__14 f__libre db"><a href="#">Shop now →</a></span></h3>
                        <p><a href="/one-day-travel"><img loading="lazy" class="alignnone size-full wp-image-3838" 
                            src="img/img_home__one_day_travel.jpg" alt="One Day Travel" width="524" height="438" 
                            sizes="(max-width: 524px) 100vw, 524px" /></a></p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="jas-container">
          <div class="vc_row vc_custom_1459613775958">
            <div class="wpb_column vc_column_container vc_col-sm-12">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_appear appear" >
                    <div class="wpb_wrapper">
                      <div class="men-collection pr tc">
                        <h3 class="right pa tu custom-head-1">Leisure Travel <span class="f__libre tl db"><a href="#">Shop now →</a></span></h3>
                        <p><a href="/leisure-travel"><img loading="lazy" class="alignnone size-full wp-image-3845" src="img/img_home_lesiure _travel.jpg" alt="Leisure Travel" width="570" height="856" sizes="(max-width: 570px) 100vw, 570px" /></a></p>
                        <h3 class="left pa custom-head-1">02 <span class="tu">Monsoon Sale 2021</span></h3>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="jas-container">
          <div class="vc_row vc_custom_1459616075217">
            <div class="wpb_column vc_column_container vc_col-sm-6">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_bottom-to-top bottom-to-top vc_custom_1463286819340" >
                    <div class="wpb_wrapper">
                      <div class="footwear-collection pr dib">
                        <h3 class="pa tu custom-head-1">Travel Accessories<span class="fs__14 f__libre db"><a href="#">Shop now →</a></span></h3>
                        <p><a href="/travel-accessories"><img loading="lazy" class="alignnone size-full wp-image-3849" src="img/img_home_travel_accessories.jpg" 
                            alt="Travel Accessories" width="570" height="408" sizes="(max-width: 570px) 100vw, 570px" /></a></p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="wpb_column vc_column_container vc_col-sm-6">
              <div class="vc_column-inner ">
                <div class="wpb_wrapper">
                  <div class="wpb_text_column wpb_content_element  wpb_animate_when_almost_visible wpb_right-to-left right-to-left vc_custom_1463286838626" >
                    <div class="wpb_wrapper">
                      <div class="lookbook-collection pr tc dib fr">
                        <h3 class="pa tu custom-head-1">Travel Essentials<span class="fs__14 f__libre db"><a href="/travel-essentials">Shop now →</a></span></h3>
                        <p><a href="/travel-essentials"><img loading="lazy" class="alignnone size-full wp-image-3850" src="img/img_home_travel_essentials.jpg" alt="our-journey" 
                            width="490" height="650" sizes="(max-width: 490px) 100vw, 490px" /></a></p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
       
    </div>
    <!-- $classes --> 
    
  </div>
  <!-- .jas-row --> 
</div>
<!-- #jas-content -->
        <div class="about-us-area section-space--ptb_120" style="padding-top:80px;">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="about-us-content_6 text-center">
                            <h2 style="font-family: mr de haviland,cursive;">The Strutt Store</h2>
                            <p><asp:Label ID="lblHomeText" runat="server" Text=""></asp:Label> </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="our-blog-area section-space--pb_90">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-6">
                            <div class="section-title mb-20">
                                <h2 class="section-title">Life @ Strutt </h2>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="more-button text-right"><a href="blog.aspx" class="text-btn-normal font-weight--reguler font-lg-p" tabindex="0">View All <i class="icon-arrow-right"></i></a></div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Repeater ID="rptBlog" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-4 col-md-6 col-sm-6 col-12">
                                    <!-- Single Blog Item Start -->
                                    <div class="single-blog-item mt-30">
                                        <div class="blog-thumbnail-box">
                                            <a  href='<%#"blogdetail.aspx?id="+Eval("blog_id")%>' class="thumbnail">
                                                <%--<img src='<%# "images/BlogImages/" + Eval("image") %>' class="img-r" Height="327" Width="570" alt='<%#Eval("title") %>'>--%>
                                                <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/BlogImages/" + Eval("image") %>'
                                                CssClass="img-r" Height="327" Width="570" />
                                            </a><a href="blogdetail.aspx" class="btn-blog">Read more </a>
                                        </div>
                                        <div class="blog-contents">
                                            <h6 class="blog-title"><a href="blogdetail.aspx"><%#Eval("title") %></a></h6>
                                            <div class="meta-tag-box">
                                                <div class="meta author"><span>By <a href="#"><%#Eval("name") %></a></span></div>
                                                <div class="meta date"><span><%# Eval("created_date", "{0:dd MMMMM, yyyy}")%></span></div>

                                            </div>
                                        </div>
                                    </div>
                                    <!-- Single Blog Item End -->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <div class="our-blog-area section-space--pb_90">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-6">
                            <div class="section-title mb-20">
                                <h2 class="section-title">Places- The Blog </h2>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="more-button text-right"><a href="review.aspx" class="text-btn-normal font-weight--reguler font-lg-p" tabindex="0">View All <i class="icon-arrow-right"></i></a></div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Repeater ID="rptTop5" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-4 col-md-6 col-sm-6 col-12">
                                    <!-- Single Blog Item Start -->
                                    <div class="single-blog-item mt-30">
                                        <div class="blog-thumbnail-box">
                                            <a class="thumbnail" href='<%#"reviewcustomer.aspx?id="+Eval("id")%>'>
                                          <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/Review/" + Eval("image_name") %>'
                                                CssClass="img-r" Height="327" Width="570" />
                                            </a><a href="review.aspx" class="btn-blog">Read more </a>
                                        </div>
                                        <div class="blog-contents">
                                            <h6 class="blog-title"><a href="reviewcustomer.aspx"><%#Eval("title") %></a></h6>
                                            <div class="meta-tag-box">
                                                <div class="meta author"><span>By <a href="#"><%#Eval("customer_name") %></a></span></div>
                                                <div class="meta date"><span><%# Eval("createddate", "{0:dd MMMMM, yyyy}")%></span></div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Single Blog Item End -->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>


        <div class="our-blog-area section-space--pb_90">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-6">
                            <div class="section-title mb-20">
                                <h2 class="section-title">Follow Us On Instagram</h2>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <!-- Single Product Item Start -->
                        <asp:Repeater ID="rptBanner" runat="server">
                            <ItemTemplate>
                                <div class="col__20">
                                    <div class="single-product-item text-center">
                                        <div class="products-images">
                                            <a href='<%# Eval("url_path") %>' class="product-thumbnail" target="_blank">
                                                <img id="Img1" runat="server" src='<%# "~/images/Banner/" + Eval("image") %>' alt='<%#Eval("image") %>' class="img-fluid" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <!-- Single Product Item End -->
                    </div>
                </div>
            </div>

        <%--<div class="our-blog-area section-space--pb_90">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-6">
                            <div class="section-title mb-20">
                                <h2 class="section-title">Follow Us On Instagram</h2>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <img src="img/why_stop_at_strutt.gif" />
                    </div>
                </div>
            </div>--%>
        <div class="hidden-sm">
            <img src="img/why_stop_at_strutt.gif" />
        </div>
        <div class="hidden-xl">
        <div class="col-lg-4 col-md-6 col-sm-12 col-12" style="padding-bottom:10px;">
        <img src="img/gif1.gif" />
            </div>
        <div class="col-lg-4 col-md-6 col-sm-12 col-12" style="padding-bottom:10px;">
        <img src="img/gif2.gif" />
            </div>
        <div class="col-lg-4 col-md-6 col-sm-12 col-12" style="padding-bottom:10px;">
            <img src="img/gif3.gif" />
        </div>
        </div>

<footer id="jas-footer" class="footer-1 pr cw"  role="contentinfo" itemscope="itemscope" itemtype="http://schema.org/WPFooter" >
  <div class="footer__top pb__80 pt__80">
    <div class="jas-container pr">
      <div class="jas-row">
        <div class="jas-col-md-3 jas-col-sm-6 jas-col-xs-12">
          <aside id="text-3" class="widget widget_text">
            <div class="textwidget">
              <div class="footer-contact">
                <p>
                    <a href="/">
                        <img src="img/logo_white_footer.png" class="mt__5 mb__15 size-full" width="199" height="23" />
                    </a>
                    
                </p>
                <p><i class="fa fa-briefcase"> </i>C 272, 1st Floor, Sector 10, Noida U.P. - 201301
                  </p>
                <p><i class="fa fa-envelope-o"></i> <a href="mailto:connect@thestruttstore.com">connect@thestruttstore.com</a></p>
                <p><i class="fa fa-phone"></i> 0120 4256583</p>
                <ul class="jas-social mt__15">
                  <li><a href="https://www.facebook.com/struttstore/" title="Facebook" target="_blank"> <i class="fa fa-facebook"></i> </a></li>
                  <li><a href="https://twitter.com/thestruttstore" title="Twitter" target="_blank"> <i class="fa fa-twitter"></i> </a></li>
                  <li><a href="https://www.instagram.com/thestruttstore/" title="Instagram" target="_blank"> <i class="fa fa-instagram"></i> </a></li>
                  <li><a href="https://in.pinterest.com/thestruttstore/?eq=thestruttstore&etslf=5276" title="Pinterest" target="_blank"> <i class="fa fa-pinterest-p"></i> </a></li>
                </ul>
              </div>
            </div>
          </aside>
        </div>
        <div class="jas-col-md-3 jas-col-sm-6 jas-col-xs-12">
          <aside id="nav_menu-3" class="widget widget_nav_menu">
            <h3 class="widget-title tu">Quick Links</h3>
            <div class="menu-quick-links-container">
              <ul id="menu-quick-links" class="menu">
                <li id="menu-item-4365" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4365"><a href="../../about-us.aspx">About Us</a></li>
                <li id="menu-item-4369" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4369"><a href="../../contact-us.aspx">Contact Us</a></li>
                <li id="menu-item-4366" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4366"><a href="../../Login.aspx">My Account</a></li>
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4366"><a href="../../about-your-product">
                    <p style="margin: 2px 0px -8px 0px;">Get to know your</p><p> Travel Product</p> </a></li>
              </ul>
            </div>
          </aside>
        </div>
        <div class="jas-col-md-3 jas-col-sm-6 jas-col-xs-12">
          <aside id="nav_menu-2" class="widget widget_nav_menu">
            <h3 class="widget-title tu">Help &#038; Support</h3>
            <div class="menu-help-support-container">
              <ul id="menu-help-support" class="menu">
                <li id="menu-item-4372" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4372"><a href="../../return-policy.aspx">Returns & Refunds</a></li>
                <li id="menu-item-4373" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4373"><a href="../../privacy-policy.aspx">Privacy Policy</a></li>
                <li id="menu-item-4375" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4375"><a href="../../terms-conditions.aspx">Terms &#038; Conditions</a></li>
                  <li id="menu-item-4375" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4375"><a href="../../sitemap.aspx">Sitemap</a></li>
              </ul>
            </div>
          </aside>
        </div>
        <div class="jas-col-md-3 jas-col-sm-6 jas-col-xs-12">
          <aside id="text-2" class="widget widget_text">
            <div class="textwidget">
                <h3 class="widget-title tu">SIGNUP</h3>
              <script>(function() {
	window.mc4wp = window.mc4wp || {
		listeners: [],
		forms: {
			on: function(evt, cb) {
				window.mc4wp.listeners.push(
					{
						event   : evt,
						callback: cb
					}
				);
			}
		}
	}
})();
</script>
              <div id="mc4wp-form-1" class="mc4wp-form mc4wp-form-4073">
                <div class="mc4wp-form-fields">
                  <div class="signup-newsletter-form dib mb__15" style="height:40px;">
                    <input type="email" class="input-text" name="EMAIL" placeholder="Your email address" required />
                    <input type="submit" class="submit-btn" value="Sign up" />
                  </div>
                </div>
                <label style="display: none !important;">Leave this field empty if you're human:
                  <input type="text" name="_mc4wp_honeypot" value="" tabindex="-1" autocomplete="off" />
                </label>
                <input type="hidden" name="_mc4wp_timestamp" value="1625049736" />
                <input type="hidden" name="_mc4wp_form_id" value="4073" />
                <input type="hidden" name="_mc4wp_form_element_id" value="mc4wp-form-1" />
                <div class="mc4wp-response"></div>
              </div>
              <!-- / Mailchimp for WordPress Plugin -->
                <h3 class="widget-title tu" style="margin-top:30px;">SECURED BY</h3>
              <p>
                  <img src="../img/img_mcafee.jpg" width="110px" alt="accept payment" />
                  <img src="../img/img_razorpay.jpg" width="110px" alt="accept payment" />
              </p>
                  <p>
                  <img src="../img/img_sectigo.jpg" width="110px" alt="accept payment" />
                  <img src="../img/img_trustedsite.jpg" width="110px" alt="accept payment" />
              </p>
            </div>
          </aside>
        </div>
      </div>
      <!-- .jas-row --> 
    </div>
    <!-- .jas-container --> 
  </div>
  <!-- .footer__top -->
  <div class="footer__bot">
    <div class="jas-container pr tc">
      <div class="jas-row">
        <div class="jas-col-md-6 jas-col-sm-12 jas-col-xs-12 start-md center-sm center-xs">
          <ul id="jas-footer-menu" class="clearfix">
            <li id="menu-item-4076" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4076"><a href="/">Home</a></li>
            <li id="menu-item-4077" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4077"><a href="../../category.aspx">Shop</a></li>
            <li id="menu-item-4078" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4078"><a href="../../about-us.aspx">About Us</a></li>
            <li id="menu-item-4079" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4079"><a href="../../contact-us.aspx">Contact</a></li>
            <li id="menu-item-4080" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-4080"><a href="../../review.aspx">Blog</a></li>
          </ul>
        </div>
        <div class="jas-col-md-6 jas-col-sm-12 jas-col-xs-12 end-md end-sm center-xs flex"> Copyright © Strutt 2018 All rights reserved </div>
      </div>
    </div>
  </div>
  <!-- .footer__bot --> 
</footer>
<!-- #jas-footer --> 
<!-- #jas-wrapper -->
</div>

        <!--====================  scroll top ====================-->
        <a href="#" class="scroll-top" id="scroll-top"><i class="arrow-top icon-arrow-up"></i><i class="arrow-bottom icon-arrow-up"></i></a>
        <!--====================  End of scroll top  ====================-->
        <!-- JS
    ============================================ -->
       
       
        <script src="../../assets/js/plugins/wow.min.js"></script>
        <script src="../../assets/js/main.js"></script>

        <script type='text/javascript' data-cfasync='false'>       window.purechatApi = { l: [], t: [], on: function () { this.l.push(arguments); } }; (function () { var done = false; var script = document.createElement('script'); script.async = true; script.type = 'text/javascript'; script.src = 'https://app.purechat.com/VisitorWidget/WidgetScript'; document.getElementsByTagName('HEAD').item(0).appendChild(script); script.onreadystatechange = script.onload = function (e) { if (!done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) { var w = new PCWidget({ c: 'b56ed792-3599-45de-86a1-807b90dd92de', f: true }); done = true; } }; })();</script>
        
    </form>
   
    
    <link rel="stylesheet" id="metaslider-flex-slider-css"  href="css/flexslider.css" type="text/css" media="all" property="stylesheet" />
<link rel="stylesheet" id="metaslider-public-css"  href="css/public.css" type="text/css" media="all" property="stylesheet" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/animatecss/3.5.2/animate.min.css" type="text/css" media="all" />
<script type="text/javascript" src="js/slick.min.js"></script> 
<script type="text/javascript" src="js/wow.js"></script> 
<script type='text/javascript' src='js/theme.js' id='jas-gecko-script-js'></script> 
<script type='text/javascript' src='js/3rd.js' id='jas-vendor-jquery-cookies-js'></script> 
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script> 
<script type="text/javascript" src="js/js_composer_front.min.js" id="wpb_composer_front_js-js"></script> 
<script type="text/javascript" src="js/jquery.flexslider.min.js" id="metaslider-flex-slider-js"></script> 
    
    
     
<script type="text/javascript" id="metaslider-flex-slider-js-afterk7644">
var metaslider_3801 = function($) {$('#metaslider_3801').addClass('flexslider');
            $('#metaslider_3801').flexslider({ 
                slideshowSpeed:3000,
                animation:"fade",
                controlNav:true,
                directionNav:true,
                pauseOnHover:true,
                direction:"horizontal",
                reverse:false,
                animationSpeed:600,
                prevText:"←",
                nextText:"→",
                fadeFirstSlide:false,
                slideshow:true
            });
            $(document).trigger('metaslider/initialized', '#metaslider_3801');
        };
        var timer_metaslider_3801 = function() {
            var slider = !window.jQuery ? window.setTimeout(timer_metaslider_3801, 100) : !jQuery.isReady ? window.setTimeout(timer_metaslider_3801, 1) : metaslider_3801(window.jQuery);
        };
        timer_metaslider_3801();
</script>

   
</body>
</html>
