<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="strutt._default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="BLL" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="google-site-verification" content="NdGYBoaVysvJyeSLxvbpwfWiraTxlMf43eTQdXyu7w8" />
    <title>The Strutt Store</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/png" href="img/favicon.ico"/>

    <link href="../new-design/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../new-design/css/slick.css" rel="stylesheet" type="text/css" />
    <link href="../new-design/css/hamburgers.min.css" rel="stylesheet" type="text/css" />
    <link href="../new-design/css/lightbox.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../new-design/css/util.css" rel="stylesheet" type="text/css" />
    <link href="../../new-design/css/main.css" rel="stylesheet" type="text/css" />
    <link href="../../css/normalize.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />


    <script src="../js/jquery.min.js" type="text/javascript"></script>
         <script type = "text/javascript">
             $(function () {
                 $("#scrollToBottom").click(function () {
                     $('html, body').animate({
                         scrollTop: $("#Scrolling1").offset().top
                     }, 1000);
                 });
             });

             function checkLogin() {
                 if ('<%= Session["CustomerLoginDetails"]%>' == '') {
                     login();
                     return false;
                 }
             }
    </script>
    <!----New------->
   
<!-- Facebook Pixel Code -->
    <script type = "text/javascript">
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        } (window, document, 'script',
      'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '204142523253386');
        fbq('track', 'PageView');
    </script>
    <noscript><img height="1" width="1" style="display:none" src="https://www.facebook.com/tr?id=204142523253386&ev=PageView&noscript=1"/></noscript>
<!-- End Facebook Pixel Code -->

<style type="text/css">
    .toolbar-btn {
        background-color:transparent;
        padding: 10px;
        font-size: 14px;
        color: #333333 !important;
        text-transform: none;
        cursor: pointer;
    }
    .toolbar-btn.active {
        background-color: transparent;
        color: #333333 !important;
        font-weight: 400;
        border-bottom: 1px solid #333333;
    }
</style>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=AW-825088951"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', 'UA-110991640-1');
        gtag('config', 'AW-825088951');
    </script>
</head>
 
<body class="animsition">
 <form id="form1" runat="server">
    <asp:ScriptManager ID="smanager" runat="server">
    </asp:ScriptManager>
<!-- Start Header ---->
<%--<span><asp:Label ID="lblLoginName" runat="server"></asp:Label></span>--%>
<header class="header1">
		<!-- Header desktop -->
		<div class="container-menu-header">
			<div class="topbar">
				<div class="topbar-social">
					<a href="https://www.facebook.com/theSTRUTTstore/" target="_blank" class="topbar-social-item fa fa-facebook"></a>
					<a href="https://www.instagram.com/thestruttstore/" target="_blank" class="topbar-social-item fa fa-instagram"></a>
				</div>

				<span class="topbar-child1">
					Free shipping across India for orders over Rs.750
				</span>
                <span class="topbar-child1 orange p-l-15">
				Avail 20% discount on your purchase. Use code FLAT20.
				</span>
				<div class="topbar-child2">
					<span class="topbar-email">
						connect@thestruttstore.com
					</span>

					<!--<div class="topbar-language rs1-select2">
						<select class="selection-1" name="time">
							<option>USD</option>
							<option>EUR</option>
						</select>
					</div>-->
				</div>
			</div>

			<div class="wrap_header">
				<!-- Logo -->
				<a href="Default.aspx" class="logo">
					<img src="img/logo.png" alt="IMG-LOGO">
				</a>

				<!-- Menu -->
				<div class="wrap_menu">
					<nav class="menu">
						<ul class="main_menu">
							<li><a href="default.aspx">Home</a></li>
                            <li><a class="toolbar-btn" data-target="#category">Shop</a></li>
                             <li class="sale-noti"><a  href="../sales">Sale</a></li>
                            <li><a href="../exclusive">Exclusive</a></li>
                            <li><a href="../bestseller">Best Seller</a></li>
                            <li><a href="blog.aspx">Blog</a></li>
                            <li><a href="about-us.aspx">About</a></li>
                            <li><a href="contact-us.aspx">Contact</a></li>
                           
						</ul>
					</nav>
				</div>
                <div class="container">
   <div class="margin-t-20">
    <div class="flex gutter">
      </div>
    </div>
  </div>
    <div id="category" class="toolbar-menu hidden">
    <div class="toolbar-div">
      <div class="container margin-t-30">
        <div class="flex gutter">
          <div class="col-12-12 col-sm-4-12">
            <div class="main-category"> <a href="../../bags" class="link  primary">BAGS</a> </div>
             <asp:Repeater ID="rptMenu1" runat="server">
            <ItemTemplate>
                <div class="sub-category"> <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link ">
                    <%#Eval("sub_menu_name")%></a> </div>
                <%--<li><a  href='<%# "Category.aspx?1" + Convert.ToString(Eval("sub_menu_id"))%>'><span><%#Eval("sub_menu_name")%></span></a></li>--%>
            </ItemTemplate>
            </asp:Repeater>
          </div>
       <%-- <div class="col-12-12 col-sm-3-12">
            <div class="main-category"> <a href="#" class="link  primary"> STATIONARY</a> </div>
             <asp:Repeater ID="rptMenu2" runat="server">
                            <ItemTemplate>
                             <div class="sub-category"> <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link ">
                     <%#Eval("sub_menu_name")%></a> </div>
                            </ItemTemplate>
                            </asp:Repeater>
            
          </div>--%>
          <div class="col-12-12 col-sm-4-12">
            <div class="main-category"> <a href="../../accessories" class="link  primary">ACCESSORIES</a> </div>
            <ul class="main-category">
                            <asp:Repeater ID="rptMenu3" runat="server">
                            <ItemTemplate>
                             <div class="sub-category"> <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link ">
                                <%#Eval("sub_menu_name")%></a> </div>
                            </ItemTemplate>
                            </asp:Repeater>
                          </ul>
            
          </div>
          <%--<div class="col-12-12 col-sm-3-12">
            <div class="main-category"> <a href="#" class="link  primary">GIFTS</a> </div>
            <ul class="main-category">
                            <asp:Repeater ID="rptMenu4" runat="server">
                            <ItemTemplate>
                             <div class="sub-category"> <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link ">
                                <%#Eval("sub_menu_name")%></a> </div>
                            </ItemTemplate>
                            </asp:Repeater>
                          </ul>
            
          </div>--%>
        </div>
      </div>
    </div>
  </div>

  </div>
                  <asp:Label ID="lblQtyMsg" runat="server" CssClass="alt-msg"></asp:Label>
				<!-- Header Icon -->
				<div class="header-icons">
                <asp:Label ID="lblLoginName" runat="server" CssClass="login-item hidden-xs m-t-3" Text="Welcome Guest" ClientIDMode="Static"></asp:Label>
                    <a href="login.aspx" class="dropdown-toggle" data-toggle="dropdown"><img src="img/icon-header-01.png" class="header-icon1" alt="ICON"/> <!--<b class="caret"></b>--></a>
                    <ul class="dropdown-menu p-2">
                      <li><asp:HyperLink ID="linkLogin" runat="server" NavigateUrl="~/Login.aspx">Login</asp:HyperLink></li>
                      <li><asp:HyperLink ID="linkAccount" runat="server" NavigateUrl="~/account/addresses.aspx">My Account</asp:HyperLink></li>
                      <li><asp:HyperLink ID="linkOrder" runat="server" NavigateUrl="~/account/orderhistory.aspx">Order</asp:HyperLink></li>
                      <li><asp:HyperLink ID="linkLogout" runat="server" NavigateUrl="~/Login.aspx?type=lo">Logout</asp:HyperLink></li>
                    </ul>
					<span class="linedivide1"></span>
                   
                    <div class="header-wrapicon2">
                        <a href="wishlist.aspx">
						    <img src="img/icon-header-03.png"  class="header-icon1 js-show-header-dropdown" alt="ICON"/>
                            <span class="header-icons-noti"><asp:Label ID="lblWishlist" runat="server">0</asp:Label></span>   
                        </a>
                    </div>
                     <span class="linedivide1"></span>
					<div class="header-wrapicon2">
						<img src="img/icon-header-02.png" class="header-icon1 js-show-header-dropdown" alt="ICON"/>
						<span class="header-icons-noti"><asp:Literal  ID="lblCartCount"  runat="server"></asp:Literal></span>

						<!-- Header cart noti -->
						<div class="header-cart header-dropdown">
							<ul class="header-cart-wrapitem">
                                <asp:Repeater ID="rptCartPc" runat="server">
                                <ItemTemplate>
                                    <li class="header-cart-item">
									    <div class="header-cart-item-img">
										    <img src='<%#Eval("thumb_image") %>' alt='<%# Eval("product_name")%>' />
									    </div>
									    <div class="header-cart-item-txt">
										    <a href="#" class="header-cart-item-name">
											    <%#Eval("product_name")%>
										    </a>
										    <span class="header-cart-item-info">
											    <%#Eval("quantity")%> x Rs.<%#Eval("sale_price")%>
										    </span>
									    </div>
								    </li>
                                </ItemTemplate>
                                </asp:Repeater>
							</ul>

							<div class="header-cart-total">
								Total: Rs.<asp:Literal ID="litTotalAmt" runat="server"></asp:Literal>
							</div>

							<div class="header-cart-buttons">
								<div class="header-cart-wrapbtn">
									<!-- Button -->
									<a href="cart.aspx" class="flex-c-m size1 bg1 bo-rad-20 hov1 s-text1 trans-0-4">
										View Cart
									</a>
								</div>

								<div class="header-cart-wrapbtn">
									<!-- Button -->
									<a href="proceedtopayment.aspx" class="flex-c-m size1 bg1 bo-rad-20 hov1 s-text1 trans-0-4">
										Check Out
									</a>
								</div>
							</div>
						</div>
					</div>
               
           	        
				</div>
			</div>
		</div>

		<!-- Header Mobile -->
		<div class="wrap_header_mobile">
			<!-- Logo moblie -->
			<a href="index.aspx" class="logo-mobile">
				<img src="img/logo.png" alt="IMG-LOGO">
			</a>

			<!-- Button show menu -->
			<div class="btn-show-menu">
				<!-- Header Icon mobile -->
				<div class="header-icons-mobile">
					<a href="#" class="header-wrapicon1 dis-block">
						<img src="img/icon-header-01.png" class="header-icon1" alt="ICON">
					</a>

					<span class="linedivide2"></span>

					<div class="header-wrapicon2">
						<img src="img/icon-header-02.png" class="header-icon1 js-show-header-dropdown" alt="ICON">
						<span class="header-icons-noti"><asp:Literal ID="lblCartCount2" runat="server"></asp:Literal></span>

						<!-- Header cart noti -->
						<div class="header-cart header-dropdown">
							<ul class="header-cart-wrapitem">
                                <asp:Repeater ID="rptCartMob" runat="server">
                                <ItemTemplate>
                                    <li class="header-cart-item">
									    <div class="header-cart-item-img">
										    <img src='<%#Eval("thumb_image") %>' alt='<%# Eval("product_name")%>' />
									    </div>

									    <div class="header-cart-item-txt">
										    <a href="#" class="header-cart-item-name">
											    <%#Eval("product_name")%>
										    </a>

										    <span class="header-cart-item-info">
											    <%#Eval("quantity")%> x Rs.<%#Eval("sale_price")%>
										    </span>
									    </div>
								    </li>
                                </ItemTemplate>
                                </asp:Repeater>
							</ul>

							<div class="header-cart-total">
								Total: Rs.<asp:Literal ID="litTotalAmt2" runat="server"></asp:Literal>
							</div>

							<div class="header-cart-buttons">
								<div class="header-cart-wrapbtn">
									<!-- Button -->
									<a href="cart.aspx" class="flex-c-m size1 bg1 bo-rad-20 hov1 s-text1 trans-0-4">
										View Cart
									</a>
								</div>

								<div class="header-cart-wrapbtn">
									<!-- Button -->
									<a href="#" class="flex-c-m size1 bg1 bo-rad-20 hov1 s-text1 trans-0-4">
										Check Out
									</a>
								</div>
							</div>
						</div>
					</div>
				</div>

				<div class="btn-show-menu-mobile hamburger hamburger--squeeze">
					<span class="hamburger-box">
						<span class="hamburger-inner"></span>
					</span>
				</div>
			</div>
		</div>

		<!-- Menu Mobile -->
		<div class="wrap-side-menu" >
			<nav class="side-menu">
				<ul class="main-menu">
					<li class="item-topbar-mobile p-l-20 p-t-8 p-b-8">
						<span class="topbar-child1">
							Free shipping across India for orders over Rs.750 | Avail 10% discount on your purchase. Use code TEN10.
						</span>
					</li>
					<li class="item-topbar-mobile p-l-20 p-t-8 p-b-8">
						<div class="topbar-child2-mobile">
							<span class="topbar-email">
								connect@thestruttstore.com
							</span>

							<!--<div class="topbar-language rs1-select2">
								<select class="selection-1" name="time">
									<option>USD</option>
									<option>EUR</option>
								</select>
							</div>-->
						</div>
					</li>

					<li class="item-topbar-mobile p-l-10">
						<div class="topbar-social-mobile">
							<a href="https://www.facebook.com/theSTRUTTstore/" target="_blank" class="topbar-social-item fa fa-facebook"></a>
							<a href="https://www.instagram.com/thestruttstore/" target="_blank" class="topbar-social-item fa fa-instagram"></a>
						</div>
					</li>

                    
					<li class="item-menu-mobile"><a href="default.aspx">Home</a></li>
                    <%--<li class="item-menu-mobile"><a href="../men">Men</a></li>
                    <li class="item-menu-mobile"><a href="../women">Women</a></li>--%>
                    <li class="item-menu-mobile"><a href="category.aspx">Shop</a></li>
                    <li class="item-menu-mobile"><a href="../sales">Sale</a></li>
                    <li class="item-menu-mobile"><a href="../exclusive">Exclusive</a></li>
                    <li class="item-menu-mobile"><a href="../bestSeller">Best Seller</a></li>
                    <li class="item-menu-mobile"><a href="blog.aspx">Blog</a></li>
                    <li class="item-menu-mobile"><a href="about-us.aspx">About</a></li>
                    <li class="item-menu-mobile"><a href="contact-us.aspx">Contact</a></li>
				</ul>
			</nav>
		</div>
	</header>
<!-- End Header ---->

	
	<!-- Main Banner Slide1 -->
    <section class="slide1">
		<div class="wrap-slick1">
			<div class="slick1">
                <asp:Repeater ID="rptBanner" runat="server">
                <ItemTemplate>
                    <div class="item-slick1 item1-slick1" style='<%# "background-image:url(images/Banner/" + DataBinder.Eval(Container.DataItem, "Image") + ");" %>'>
					    <div class="wrap-content-slide1 sizefull flex-col-c-m p-l-15 p-r-15 p-t-150 p-b-170">
						    <h2 class="caption1-slide1 xl-text2 t-center bo14 p-b-6 animated visible-false m-b-22" data-appear="fadeInUp">
							    <%# Eval("title") %>
						    </h2>
						    <span class="caption2-slide1 m-text1 t-center animated visible-false m-b-33" data-appear="fadeInDown">New Collection 2018</span>
						    <div class="wrap-btn-slide1 w-size1 animated visible-false" data-appear="zoomIn">
							    <a href='<%# Eval("url_path") %>' class="flex-c-m size2 bo-rad-23 s-text2 bgwhite hov1 trans-0-4">Shop Now</a>
						    </div>
					    </div>
					</div>
                </ItemTemplate>
                </asp:Repeater>
			</div>
        </div>
	</section>

	<!-- Category Banner -->
	<div class="banner bgwhite p-t-20 p-b-20">
		<div class="container-fluid">
			<div class="row">
				<div class="col-sm-10 col-md-8 col-lg-4 m-l-r-auto">
					<!-- block1 -->
					<div class="block1 hov-img-zoom pos-relative m-b-30">
						<img src="img/01.jpg" alt="IMG-BENNER">
						<div class="block1-wrapbtn w-size2">
							<a href="men/bags/duffle-bags" class="flex-c-m size2 m-text2 bg3 hov1 trans-0-4">Duffel Bags</a>
						</div>
					</div>
				</div>
				<div class="col-sm-10 col-md-8 col-lg-4 m-l-r-auto">
					<!-- block1 -->
					<div class="block1 hov-img-zoom pos-relative m-b-30">
						<img src="img/02.jpg" alt="IMG-BENNER">
						<div class="block1-wrapbtn w-size2">
							<a href="men/bags/laptop-bags" class="flex-c-m size2 m-text2 bg3 hov1 trans-0-4">Laptop Bags</a>
						</div>
					</div>
				</div>
				<div class="col-sm-10 col-md-8 col-lg-4 m-l-r-auto">
					<!-- block1 -->
					<div class="block1 hov-img-zoom pos-relative m-b-30">
						<img src="img/03.jpg" alt="IMG-BENNER">
						<div class="block1-wrapbtn w-size2">
							<a href="men/bags/tote-bags" class="flex-c-m size2 m-text2 bg3 hov1 trans-0-4">Tote Bags</a>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<!-- Banner -->
	<div class="banner bgwhite p-t-20 p-b-20">
		<div class="container-fluid">
			<div class="row">
            <asp:Repeater ID="rptHandBag" onitemdatabound="rptHandBag_ItemDataBound" runat="server" Visible="false">
              <ItemTemplate>
				<div class="col-sm-10 col-md-8 col-lg-4 m-l-r-auto">
					<!-- block1 -->
					<div class="block1 hov-img-zoom pos-relative m-b-30">
                        <img id="Img1" runat="server" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>'/>
						<div class="block1-wrapbtn w-size2">
                         <div style="padding-left:275px;">
                          <asp:HiddenField ID="hfieldproductid" runat="server" Value='<%#Eval("product_id")%>'></asp:HiddenField>
                          <cc1:Rating ID="RatingLatest" runat="server" ReadOnly="true" StarCssClass="ratingEmpty" CurrentRating="0"
                           WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                          </cc1:Rating>
                             </div>
							<!-- Button -->
                              <a id="A1" class="flex-c-m size2 m-text2 bg3 hov1 trans-0-4" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                               DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                               DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>' runat="server" 
                               title='<%# Eval("product_name")%>'>
                               <%# Eval("product_name")%>
                              </a>
						</div>
					</div>
				</div>
              </ItemTemplate>
           </asp:Repeater>
           <asp:Repeater ID="rptDuffleBag" onitemdatabound="rptDuffleBag_ItemDataBound" runat="server" Visible="false">
           <ItemTemplate>
				<div class="col-sm-10 col-md-8 col-lg-4 m-l-r-auto">
					<!-- block1 -->
					<div class="block1 hov-img-zoom pos-relative m-b-30">
                           <img id="Img1" runat="server" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' />
						<div class="block1-wrapbtn w-size2">
							<!-- Button -->
                             <div style="padding-left:110px;">
                             <asp:HiddenField ID="hfieldproductid" runat="server" Value='<%#Eval("product_id")%>'></asp:HiddenField>
                                <cc1:Rating ID="RatingLatest" runat="server" ReadOnly="true" StarCssClass="ratingEmpty" CurrentRating="0"
                                 WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                   </cc1:Rating>
                                     </div>
                                     <a id="A2"  class="flex-c-m size2 m-text2 bg3 hov1 trans-0-4" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                       DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                       DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>' runat="server" 
                                       title='<%# Eval("product_name")%>'>
                                        <%# Eval("product_name")%>
                                      </a>
					</div>
					</div>
				</div>
           </ItemTemplate>
        </asp:Repeater>

         <asp:Repeater ID="rptLaptopBag" onitemdatabound="rptLaptopBag_ItemDataBound" runat="server" Visible="false">
             <ItemTemplate>
				<div class="col-sm-10 col-md-8 col-lg-4 m-l-r-auto">
					<!-- block1 -->
					<div class="block1 hov-img-zoom pos-relative m-b-30">
                         <img id="Img1" runat="server" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' />
						<div class="block1-wrapbtn w-size2">
							<!-- Button -->
                            <div style="padding-left:275px;">
                              <asp:HiddenField ID="hfieldproductid" runat="server" Value='<%#Eval("product_id")%>'></asp:HiddenField>
                              <cc1:Rating ID="RatingLatest" runat="server" ReadOnly="true" StarCssClass="ratingEmpty" CurrentRating="0"
                               WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                 </cc1:Rating>
                                </div>
                                  <a id="A1" class="flex-c-m size2 m-text2 bg3 hov1 trans-0-4" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                    DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                    DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>' runat="server" 
                                    title='<%# Eval("product_name")%>'>
                              </a>
						</div>
					</div>
				</div>
        </ItemTemplate>
     </asp:Repeater>
			</div>
		</div>
	</div>
    	<!-- Start New Product -->
	<section class="newproduct bgwhite p-t-45 p-b-105">
		<div class="container">
			<div class="sec-title p-b-60">
				<h3 class="m-text5 t-center">
					Latest Products
				</h3>
			</div>

			<!-- Slide2 -->
			<div class="wrap-slick2">
				<div class="slick2">
					<asp:Repeater ID="rptLatestArrivals" runat="server" OnItemCommand="rptLatestArrivals_ItemCommand">
                    <ItemTemplate>
					    <div class="item-slick2 p-l-15 p-r-15">
						    <!-- Block2 -->
						    <div class="block2">
							    <div class="block2-img wrap-pic-w of-hidden pos-relative block2-labelnew">
                                    <img src='<%# "images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>'/>
								    <div class="block2-overlay trans-0-4">
									    <div class="block2-btn-addcart w-size1 trans-0-4">
                                        <a id="A2"  class="flex-c-m size1 bg4 bo-rad-23 hov1 s-text1 trans-0-4" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>' runat="server" 
                                                title='<%# Eval("product_name")%>'>View Now</a>
									    </div>
								    </div>
							    </div>
							    <div class="block2-txt p-t-20">
                                    <a id="A3" class="block2-name dis-block s-text3 p-b-5" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                        DataBinder.Eval(Container.DataItem,"sub_menu_name"),DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>' 
                                        runat="server" title='<%# Eval("product_name")%>'><%# Eval("product_name")%></a>
                                          <p><strike>Rs.<%#Eval("price")%></strike>
                            <span style="margin-left:128px">Rs.<%#Eval("sale_price")%></span></p>

								 <%--  <p><span class="block2-price m-text6 p-r-5">Rs.<%#Eval("sale_price")%></span>
                                    <strike>Rs.<%#Eval("price")%></strike></p>--%>
							    </div>
						    </div>
					    </div>
                    </ItemTemplate>
                    </asp:Repeater>
				</div>
			</div>

		</div>
	</section>
  
    
	<!-- Banner video -->
    <a href="../sales"><section class="parallax0 parallax100" style="background-image: url(images/bg-video-01.jpg);">
		<div class="overlay0 p-t-190 p-b-200">
			<div class="flex-col-c-m p-l-15 p-r-15">
				<!--<span class="m-text9 p-t-45 fs-20-sm">
					The Beauty
				</span>

				<h3 class="l-text1 fs-35-sm">
					Lookbook
				</h3>

				<span class="btn-play s-text4 hov5 cs-pointer p-t-25" data-toggle="modal" data-target="#modal-video-01">
					<i class="fa fa-play" aria-hidden="true"></i>
					SHOP NOW
				</span>-->
               
			</div>
		</div>
	</section></a>

    <section class="blog bgwhite p-t-94 p-b-65">
		<div class="container">
			<div class="sec-title p-b-52">
				<h3 class="m-text5 t-center">
					Our Blog
				</h3>
			</div>

			<div class="row">
                <asp:Repeater ID="rptBlog" runat="server">
              <ItemTemplate>
				<div class="col-sm-10 col-md-3 p-b-30 m-l-r-auto">
					<!-- Block3 -->
					<div class="block3">
                        <a href="blog.aspx?blgId=<%#Eval("blog_id")%>" class="block3-img dis-block hov-img-zoom"><img src='<%# "images/BlogImages/" + Eval("image") %>' alt='<%#Eval("name") %>' height=""></a>
						</a>
						<div class="block3-txt p-t-14">
							<span class="s-text6">By</span> <span class="s-text7"><a href="blog.aspx?blgId=<%#Eval("blog_id")%>"><%#Eval("name")%></a></span>
							<span class="s-text6">on</span> <span class="s-text7"><%# Eval("created_date", "{0:dd MMMMM, yyyy}")%></em></span>

							<p class="s-text8 p-t-16">
								<%# Eval("short_desc")%>
                                 <a href="blog.aspx?blgId=<%#Eval("blog_id")%>" class="read-more-btn">Read More</a>  
							</p>
						</div>
					</div>
				</div>
                 </ItemTemplate>
                 </asp:Repeater>
			</div>
		</div>
	</section>
    	<!-- Instagram -->
	<div class="container">
      <div class="col-md-12 mx-auto">
	<section class="instagram p-t-20">
		<div class="sec-title p-b-52 p-l-15 p-r-15">
			<h3 class="m-text5 t-center">
				@ follow us on Instagram
			</h3>
		</div>

		<div class="flex-w">
			<!-- Block4 -->
	  <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/p/BjuN1pllugB/?taken-by=thestruttstore" target="_blank"> <img src="img/insta85.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta86.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta87.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta88.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
       <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta89.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta90.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta91.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta92.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta93.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
      
      <div class="block4 wrap-pic-w">
		<a href="https://www.instagram.com/thestruttstore/" target="_blank"> <img src="img/insta94.jpg" alt="IMG-INSTAGRAM"></a>
      </div>
		</div>
	</section>
      </div>
      
    </div>

	<!-- Shipping -->
	<section class="shipping bgwhite p-t-62 p-b-46">
		<div class="flex-w p-l-15 p-r-15">
			<div class="flex-col-c w-size5 p-l-15 p-r-15 p-t-16 p-b-15 respon1">
				<h4 class="m-text12 t-center">
					Delivery in India only
				</h4>
				<a href="#" class="s-text11 t-center">
					Click here for more info
				</a>
			</div>
			<div class="flex-col-c w-size5 p-l-15 p-r-15 p-t-16 p-b-15 bo2 respon2">
				<h4 class="m-text12 t-center">
					7 days return policy 
				</h4>
                <a href="terms-conditions.aspx" class="s-text11 t-center">
						Simply return it within 7 days for an exchange.
				</a>
			</div>
           <%-- <div class="flex-col-c w-size5 p-l-15 p-r-15 p-t-16 p-b-15 bo2 respon1">
				<h4 class="m-text12 t-center">
					100% Secure Payments
				</h4>

				<span class="s-text11 t-center">
					Moving your card details to a much more
				</span>
			</div>--%>
              <div class="flex-col-c w-size5 p-l-15 p-r-15 p-t-16 p-b-15 bo2 respon1">
				<h4 class="m-text12 t-center">
					<a href="#">
				<img src="../images/card-icon-1.jpg" alt="">
			</a>
            <a href="#">
				<img  src="../images/card-icon-2.jpg" alt="">
			</a>
            <a href="#">
				<img  src="../images/card-icon-3.jpg" alt="">
			</a>
            <a href="#">
				<img src="../images/card-icon-4.jpg" alt="">
			</a>
				</h4>

				<span class="s-text11 t-center">
					100% Secure Payments
				</span>
			</div>
			<div class="flex-col-c w-size5 p-l-15 p-r-15 p-t-16 p-b-15 respon1">
				<h4 class="m-text12 t-center">
					Store at Mall Of India 
				</h4>

				<span class="s-text11 t-center">
					Shop open from Monday to Sunday
				</span>
			</div>
		</div>
	</section>
<!-- Footer -->
	<footer class="bg6 p-t-45 p-b-43 p-l-45 p-r-45">
	<div class="row">
    <div class="col-sm-12 col-md-12 text-center">
        <div class="margin-t-10"> 
              <a href="https://www.facebook.com/theSTRUTTstore/"><span class="margin-r-20"><i class="fa fa-facebook" aria-hidden="true"></i></span> </a>
              <a href="https://www.instagram.com/thestruttstore/"><span><i class="fa fa-instagram" aria-hidden="true"></i></span></a> 
              </div>
            <div class="margin-t-10"> <a class="link"  href="../privacy-policy.aspx">privacy-policy</a> <span>|</span> <a class="link"  href="../terms-conditions.aspx">terms & conditions</a> <span>|</span> <a class="link"  href="../return-policy.aspx">exchanges & returns</a><span>|</span> <a class="link"  href="sitemap.aspx">site map</a> </div>
			<div class="margin-t-10"> Copyright © Strutt 2018 All rights reserved. | Developed  by: <a href="http://carbonmedia.in" target="_blank">Carbon Media</a></div>
           
     </div>
     </div>
	</footer>
 
    	<!-- Back to top -->
	<div class="btn-back-to-top bg0-hov" id="myBtn">
		<span class="symbol-btn-back-to-top">
			<i class="fa fa-angle-double-up" aria-hidden="true"></i>
		</span>
	</div>

	<!-- Container Selection1 -->
	<div id="dropDownSelect1"></div>

	<!-- Modal Video 01-->
	<div class="modal fade" id="modal-video-01" tabindex="-1" role="dialog" aria-hidden="true">

		<div class="modal-dialog" role="document" data-dismiss="modal">
			<div class="close-mo-video-01 trans-0-4" data-dismiss="modal" aria-label="Close">&times;</div>

			<div class="wrap-video-mo-01">
				<div class="w-full wrap-pic-w op-0-0"><img src="images/icons/video-16-9.jpg" alt="IMG"></div>
				<!--<div class="video-mo-01">
					<iframe src="https://www.youtube.com/embed/Nt8ZrWY2Cmk?rel=0&amp;showinfo=0" allowfullscreen></iframe>
				</div>-->
			</div>
		</div>
	</div>
<!--===============================================================================================-->
 <script src="../../../new-design/js/jquery-3.2.1.min.js" type="text/javascript"></script>
<!--===============================================================================================-->
  <script src="../../../js/listing.js" type="text/javascript"></script>
<!--===============================================================================================-->
 <script src="../../../new-design/js/animsition.min.js" type="text/javascript"></script>
 <script src="../../../new-design/js/main.js" type="text/javascript"></script>
<!--===============================================================================================-->
 <script src="../../../new-design/js/popper.js" type="text/javascript"></script>
 <script src="../../../new-design/js/bootstrap.min.js" type="text/javascript"></script>
<!--===============================================================================================-->
 <script src="../../../new-design/js/slick.min.js" type="text/javascript"></script>
 <script src="../../../new-design/js/slick-custom.js" type="text/javascript"></script>
<!--===============================================================================================-->
 <script src="../../../new-design/js/countdowntime.js" type="text/javascript"></script>
<!--===============================================================================================-->
 <script src="../../../new-design/js/lightbox.min.js" type="text/javascript"></script>
<!--===============================================================================================--->
  <script src="../../../new-design/js/sweetalert.min.js" type="text/javascript"></script>
	<script type="text/javascript">
    /*
	    $('.block2-btn-addcart').each(function () {
	        var nameProduct = $(this).parent().parent().parent().find('.block2-name').html();
	        $(this).on('click', function () {
	            swal(nameProduct, "is added to cart !", "success");
	        });
	    });

	    $('.block2-btn-addwishlist').each(function () {
	        var nameProduct = $(this).parent().parent().parent().find('.block2-name').html();
	        $(this).on('click', function () {
	            swal(nameProduct, "is added to wishlist !", "success");
	        });
	    });
        */
	</script>
 
<!--===============================================================================================-->
	<%--<script type="text/javascript" src="vendor/parallax100/parallax100.js"></script>
	<script type="text/javascript">
	    $('.parallax100').parallax100();
	</script>--%>
<!--===============================================================================================-->
<script src="../../../js/main.js" type="text/javascript"></script>

    <!-- BEGIN JIVOSITE CODE {literal} -->
   <script type='text/javascript' data-cfasync='false'>       window.purechatApi = { l: [], t: [], on: function () { this.l.push(arguments); } }; (function () { var done = false; var script = document.createElement('script'); script.async = true; script.type = 'text/javascript'; script.src = 'https://app.purechat.com/VisitorWidget/WidgetScript'; document.getElementsByTagName('HEAD').item(0).appendChild(script); script.onreadystatechange = script.onload = function (e) { if (!done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) { var w = new PCWidget({ c: 'b56ed792-3599-45de-86a1-807b90dd92de', f: true }); done = true; } }; })();</script>
     <script type="text/javascript" src="https://cdn.ywxi.net/js/1.js" async></script>
    </form>
   
     <%--<script type='text/javascript'>
(function(){ var widget_id = 'xr9FZlgsir';var d=document;var w=window;function l(){
var s = document.createElement('script'); s.type = 'text/javascript'; s.async = true; s.src = '//code.jivosite.com/script/widget/'+widget_id; var ss = document.getElementsByTagName('script')[0]; ss.parentNode.insertBefore(s, ss);}if(d.readyState=='complete'){l();}else{if(w.attachEvent){w.attachEvent('onload',l);}else{w.addEventListener('load',l,false);}}})();</script>--%>
<!-- {/literal} END JIVOSITE CODE -->
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
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0"/>
</div>
</noscript>

</body>
</html>