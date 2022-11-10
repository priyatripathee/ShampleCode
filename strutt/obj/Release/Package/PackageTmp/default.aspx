<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="strutt._default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="BLL" %>

<!DOCTYPE html>
<html class="no-js" lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="google-site-verification" content="NdGYBoaVysvJyeSLxvbpwfWiraTxlMf43eTQdXyu7w8" />
    <title>The Strutt Store</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/png" href="img/favicon.ico" />
    <!-- Bootstrap Css -->
    <link href="assets/css/vendor/bootstrap.min.css" rel="stylesheet" />
    <!-- Icons Css -->
    <link href="assets/css/vendor/linearicons.min.css" rel="stylesheet" />
    <link href="assets/css/vendor/fontawesome-all.min.css" rel="stylesheet" />
    <!-- Animation Css -->
    <link href="assets/css/plugins/animation.min.css" rel="stylesheet" />
    <!-- Slick Slier Css -->
    <link href="assets/css/plugins/slick.min.css" rel="stylesheet" />
    <!-- Magnific Popup CSS -->
    <link href="assets/css/plugins/magnific-popup.css" rel="stylesheet" />
    <!-- Easyzoom CSS -->
    <link href="assets/css/plugins/easyzoom.css" rel="stylesheet" />
      <link href="../assets/css/starability-all.min.css" rel="stylesheet" />
    <!-- Vendor & Plugins CSS (Please remove the comment from below vendor.min.css & plugins.min.css for better website load performance and remove css files from avobe) -->
    <!-- 
        <link rel="stylesheet" href="assets/css/vendor/vendor.min.css">
        <link rel="stylesheet" href="assets/css/plugins/plugins.min.css">
 -->
    <!-- Main Style CSS -->
    <link href="assets/css/style.css" rel="stylesheet" />
    <!-- jQuery JS -->
        <script src="../../assets/js/vendor/jquery-3.3.1.min.js"></script>
    <script>
        //Added by chandni
        function ShowPopup() {
            $('#modalWE').modal('show');
        }
    </script>
    <!-- Google Tag Manager -->
    <script>(function (w, d, s, l, i) {
    w[l] = w[l] || []; w[l].push({
        'gtm.start':
            new Date().getTime(), event: 'gtm.js'
    }); var f = d.getElementsByTagName(s)[0],
    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
})(window, document, 'script', 'dataLayer', 'GTM-MRRB3ZZ');</script>
    <!-- End Google Tag Manager -->

    <script type="text/javascript">
        //$(function () {
        //    //jQuery('#hfOffer').click();

        //    $("#scrollToBottom").click(function () {
        //        $('html, body').animate({
        //            scrollTop: $("#Scrolling1").offset().top
        //        }, 1000);
        //    });
        //});

        function checkLogin() {
            if ('<%= Session["CustomerLoginDetails"]%>' == '') {
                login();
                return false;
            }
        }
        // Added by chandni wishlist
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

        function OnSuccessAdd(response) {
            if (response.d == 1) {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = "Added item to wish list Successfully.";
                alert("Added item to wish list Successfully.");
            }
            else if (response.d == -1) {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = "Item already in wish list.";
                alert("Item already in Wish list.");
            }
            else {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = "Please login before add item in Wish list.";
                //alert("Please login before add item in Wish list.");
            }
    }
    </script>
    <!----New------->
    <!-- Facebook Pixel Code -->
    <script type="text/javascript">
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script',
      'https://connect.facebook.net/en_US/fbevents.js');
      
        fbq('init', '292896398414200');
        fbq('track', 'PageView');
    </script>
    <noscript>
        <img height="1" width="1" style="display: none" src="https://www.facebook.com/tr?id=292896398414200&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=AW-827193669"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', 'UA-110991640-1');
        gtag('config', 'AW-827193669');
    </script>

    <%-- start: Wigzo Integration script Added by Hetal Patel on 25-07-2020--%>
    <script>
        (function (w, i, g, z, o) {
            var a, m; w['WigzoObject'] = o; w[o] = w[o] || function () {
                (w[o].q = w[o].q || []).push(arguments)
            }, w[o].l = 1 * new Date(); w[o].h = z; a = i.createElement(g),
            m = i.getElementsByTagName(g)[0]; a.async = 1; a.src = z; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//app.wigzo.com/wigzo.compressed.js', 'wigzo');
        wigzo('configure', 'b-0-nzLWTtOhipZg2nl3zg');
    </script>
    <%-- end: Wigzo Integration script Added by Hetal Patel on 25-07-2020--%>
    <%--Added this javascript by chandni--%>
    <script type="text/javascript">        //Google Authentication
        function googleLogin() {
            gapi.load('auth2', function () {
                var auth2 = gapi.auth2.getAuthInstance();
                auth2.signIn().then(function () {
                    //console.log(auth2.currentUser.get().getId());
                    var profile = auth2.currentUser.get().getBasicProfile();
                    document.getElementById('<%=hfLoginType.ClientID %>').value = "google";
                    document.getElementById('<%=hffbid.ClientID %>').value = profile.getId();
                    document.getElementById('<%=hfpersonaName.ClientID %>').value = profile.getName();
                    document.getElementById('<%=hfreceiveremail.ClientID %>').value = profile.getEmail();
                    document.getElementById('<%=btnFbGoogle.ClientID%>').click();
                    document.getElementById('lblLoginName').value = "Welcome, " + profile.getEmail();
                });
            });
        }
    </script>
    <script type="text/javascript">        //
        function identifyNewuser() {
            wigzo("identify", { email: document.getElementById('<%=txtsignoutEmail.ClientID %>').value, phone: document.getElementById('<%=txtsignoutMobile.ClientID %>').value, fullName: document.getElementById('<%=txtsignoutUserName.ClientID %>').value });
        }

    </script>
    <script type="text/javascript">    //Facebook Authentication
        window.fbAsyncInit = function () {
            FB.init({
                appId: '739222963134104',
                cookie: true,
                xfbml: true,
                version: 'v3.0'
            });
            FB.AppEvents.logPageView();
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "https://connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));

        function fblogin() {
            FB.getLoginStatus(function (response) {
                if (response.status === 'connected') {
                    console.log('Logged in.');
                    FB.api('/me?fields=name,id,email', function (me) {
                        if (me.name) {
                            document.getElementById('<%=hfLoginType.ClientID %>').value = "fb";
                            document.getElementById('<%=hffbid.ClientID %>').value = me.id;
                            document.getElementById('<%=hfpersonaName.ClientID %>').value = me.name;
                            document.getElementById('<%=hfreceiveremail.ClientID %>').value = me.email;
                            document.getElementById('<%=btnFbGoogle.ClientID%>').click();
                            document.getElementById('lblLoginName').value = "Welcome, " + me.email;
                        }
                    })
                }
                else {
                    FB.login(function (response) {
                    }, { scope: 'public_profile,email' });
                }
            });
        }
    </script>
    <style>
        @media (min-width:324px) {
            .pr-2 pl-2 pt-2 pb-2 {
                display: none !important;
            }
        }
        .hero-product-image:hover h4 span {
            color: #dcb14a !important;
        }
    </style>
    <%--Added this javascript by chandni--%>
    
</head>

<body class="">
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-MRRB3ZZ"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->

    <form id="form1" runat="server">
        <asp:ScriptManager ID="smanager" runat="server">
        </asp:ScriptManager>
        <!-- Start Header ---->
        <div class="header-area header-area--default">
      <marquee   onmouseover="stop()"  onmouseout="start()" style="background-color:black"><asp:Label ID="lblMarquee1" runat="server" CssClass="text-white"></asp:Label><asp:Label ID="lblMarquee2" runat="server" CssClass="text-orange"></asp:Label></marquee>
            <!-- Header Bottom Wrap Start -->
            <header class="header-area  header_height-90 header-sticky">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-4 col-md-4 d-none d-md-block">
                            <div class="header-left-search">
                                <asp:Panel ID="pnlFind" runat="server" DefaultButton="lbtngo">
                                    <div class="header-search-box">
                                        <asp:TextBox runat="server" class="search-field"  ID="txtSearch" placeholder="Search Anything..."  autocomplete="off"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click" CssClass="search-icon"><i class="icon-magnifier"></i></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                         <div class="col-lg-4 col-md-4 col-6">
                        <div class="logo text-md-center">
                            <a href="../default.aspx"><img src="assets/images/logo/logo.png" alt=""></a>
                        </div>
                    </div>
                        <div class="col-lg-4 col-md-4 col-6">
                            <div class="header-right-side text-right">
                                 <a href="category.aspx" class="pr-2 pl-2 pt-2 pb-2 d-none d-md-block">Shop</a> 
                                <a href="../sales" class="pr-2 pl-2 pt-2 pb-2 d-none d-md-block" >Sale </a>
                                <div class="header-right-items  d-none d-md-block">
                                    <asp:LinkButton ID="btnlogin" runat="server" CssClass="header-cart" PostBackUrl="~/Login.aspx" ToolTip="Click here to login"> <i class="icon-user" style="font-size:x-large"></i>
                                </asp:LinkButton>
                                </div>
                                <div class="header-right-items d-none d-md-block">
                                    <asp:LinkButton ID="btnWishlist" runat="server" CssClass="header-cart" PostBackUrl="~/wishlist.aspx" ToolTip="Wishlist">
                                        <i class="icon-heart"></i>
                                        <asp:Label ID="lblWishlist" CssClass="item-counter" runat="server" OnClientClick="ShowPopup()">0</asp:Label>
                                    </asp:LinkButton>
                                </div>
                                <div class="header-right-items">
                                    <a href="#miniCart" class=" header-cart minicart-btn toolbar-btn header-icon" title="Cart"><i class="icon-bag2"></i><span class="item-counter">
                                        <asp:Literal ID="lblCartCount" runat="server"></asp:Literal></span>  </a>
                                </div>
                                <div class="header-right-items d-block d-md-none"><a href="javascript:void(0)" class="search-icon" id="search-overlay-trigger"><i class="icon-magnifier"></i></a></div>
                                <div class="header-right-items"><a href="#" class="mobile-navigation-icon" id="mobile-menu-trigger"><i class="icon-menu"></i></a></div>
                            </div>
                        </div>
                    </div>
                </div>
            </header>
            <!-- Header Bottom Wrap End -->
        </div>
        <div class="site-wrapper-reveal">
            <div class="hero-box-area">
                <div class="container">
                    <div class="row row--5">
                        <div class="col-lg-3 col-md-3">
                            <div class="hero-product-image mt-10">
                                <a id="banner1"  runat="server">
                                    <asp:Image ID="lblBanner1" runat="server" class="img-fluid" alt="Banner images" />
                                
                                <div class="product-banner-title">
                                    <h4 class="hover-style-link">
                                        <b class="color-w"><asp:Label ID="lblBName1" runat="server"></asp:Label></b></h4>
                                    <h6 class="hidden">
                                        <asp:Label ID="lbltitle1" runat="server"></asp:Label></h6>
                                </div>
                                    </a>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="hero-product-image mt-10">
                                 <a id="banner2"  runat="server">
                                    <asp:Image ID="lblBanner2" runat="server" class="img-fluid" alt="Banner images" />
                                <div class="product-banner-title">
                                    <h4>
                                        <asp:Label ID="lblBName2" runat="server"></asp:Label></h4>
                                    <h6 class="hidden">
                                        <asp:Label ID="lbltitle2" runat="server"></asp:Label></h6>
                                </div>
                                     </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3">
                            <div class="hero-product-image mt-10">
                                 <a id="banner3"  runat="server" >
                                    <asp:Image ID="lblBanner3" runat="server" class="img-fluid" alt="Banner images" />
                                <div class="product-banner-title">
                                    <h4>
                                        <asp:Label ID="lblBName3" runat="server"></asp:Label></h4>
                                    <h6 class="hidden">
                                        <asp:Label ID="lbltitle3" runat="server"></asp:Label></h6>
                                </div>
                                     </a>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="hero-product-image mt-10">
                                <a id="banner4"  runat="server" >
                                    <asp:Image ID="lblBanner4" runat="server" class="img-fluid" alt="Banner images" />
                               
                                <div class="product-banner-title">
                                    <h4>
                                        <asp:Label ID="lblBName4" runat="server"></asp:Label></h4>
                                    <h6 class="hidden">
                                        <asp:Label ID="lbltitle4" runat="server"></asp:Label></h6>
                                </div>
                                    </a>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="hero-product-image mt-10">
                                <a id="banner5"  runat="server" >
                                    <asp:Image ID="lblBanner5" runat="server" class="img-fluid" alt="Banner images" />
                                <div class="product-banner-title">
                                    <h4>
                                        <asp:Label ID="lblBName5" runat="server"></asp:Label></h4>
                                    <h6 class="hidden">
                                        <asp:Label ID="lbltitle5" runat="server"></asp:Label></h6>
                                </div>
                                    </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- About Us Area Start -->
            <div class="about-us-area section-space--ptb_120">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="about-us-content_6 text-center">
                                <h2>The Strutt Store</h2>
                                <p><asp:Label ID="lblHomeText" runat="server"></asp:Label> </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- About Us Area End -->
            <!-- Banner Video Area Start -->

            <div class="banner-video">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="banner-video-box">
                                <img src="assets/images/bg/bg-video-01.jpg" alt="">
                                <div class="video-icon"><a href="https://www.youtube.com/embed/aiJOu3aKbVo?start=0" class="popup-youtube"><i class="linear-ic-play"></i></a></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Banner Video Area End -->
            <!-- Product Area Start -->
            <div class="product-wrapper section-space--ptb_120">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-4">
                            <div class="section-title text-lg-left text-center mb-20">
                                   <h2 class="section-title">Popular Products </h2>
                                <asp:Label ID="lblMessage" runat="server" Visible="true" CssClass="text-red"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <ul class="nav product-tab-menu justify-content-lg-end justify-content-center" role="tablist">
                                <li class="tab__item nav-item active">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_list_01" role="tab">All</a>
                                </li>
                                <li class="tab__item nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_list_02" role="tab">Bags</a>
                                </li>
                                <li class="tab__item nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_list_03" role="tab">Accessories</a>
                                </li>
                                <li class="tab__item nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_list_04" role="tab">Travel Esentials</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="tab-content mt-30">
                        <div class="tab-pane fade show active" id="tab_list_01">
                            <!-- product-slider-active -->
                            <div class="row">
                                <asp:Repeater ID="rptLatestArrivals" runat="server" OnItemCommand="rptLatestArrivals_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-lg-3 col-md-4 col-sm-6">
                                            <!-- Single Product Item Start -->
                                            <div class="single-product-item text-center">
                                                <div class="products-images">
                                                    <a id="A2" class="product-thumbnail" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server"
                                                        title='<%# Eval("product_name")%>'>
                                                        <img src='<%# "images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' class="img-fluid" />
                                                        <span class='<%# Convert.ToBoolean(Eval("in_stock"))?"hidden":"ribbon out-of-stock" %>'>Out of Stock</span>
                                                        <span class='<%# Convert.ToInt16(Eval("discount")) == 0 ?"hidden":"ribbon-left onsale" %>'>- <%# Convert.ToInt16(Eval("discount"))%> %</span>
                                                    </a>
                                                    <div class="product-actions">
                                                        <asp:LinkButton ID="btnQuickView" runat="server" OnClientClick="ShowPopup()" CommandName="latest" CommandArgument='<%# Eval("product_id") %>'><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></asp:LinkButton>
                                                        <a href='<%# "/cart.aspx?proid=" + Eval("product_id")%>' runat="server" title='<%# Eval("product_name")%>'><i class="p-icon icon-bag2"></i><span class="tool-tip">Add to cart</span></a>
                                                        <a onclick="addWishList(<%# Eval("product_id")%>);"><i class="p-icon icon-heart"></i><span class="tool-tip">Add to Wishlist</span></a>
                                                    </div>
                                                </div>
                                                <div class="product-content">
                                                    <h6 class="prodect-title"><a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                        DataBinder.Eval(Container.DataItem,"sub_menu_name"),DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server" title='<%# Eval("product_name")%>'><%# Eval("product_name")%></a></h6>
                                                    <div class="prodect-price">
                                                         <span class="old-price text-center">Rs.<%#Eval("sale_price")%></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Single Product Item End -->
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_list_02">
                            <div class="row ">
                                <asp:Repeater ID="rptHandBag" runat="server" OnItemCommand="rptHandBag_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-lg-3 col-md-4 col-sm-6">
                                            <!-- Single Product Item Start -->
                                            <div class="single-product-item text-center">
                                                <div class="products-images">
                                                    <a id="A2" class="product-thumbnail" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server"
                                                        title='<%# Eval("product_name")%>'>
                                                        <img src='<%# "images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' class="img-fluid" />
                                                        <span class='<%# Convert.ToBoolean(Eval("in_stock"))?"hidden":"ribbon out-of-stock" %>'>Out of Stock</span>
                                                         <span class='<%# Convert.ToInt16(Eval("discount")) == 0 ?"hidden":"ribbon-left onsale" %>'>- <%# Convert.ToInt16(Eval("discount"))%> %</span>
                                                    </a>
                                                    <div class="product-actions">
                                                        <asp:LinkButton ID="btnViewHB" runat="server" OnClientClick="ShowPopup()" CommandName="qviewhand" CommandArgument='<%# Eval("product_id") %>'><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></asp:LinkButton>
                                                        <a href='<%# "/cart.aspx?proid=" + Eval("product_id")%>' runat="server" title='<%# Eval("product_name")%>'><i class="p-icon icon-bag2"></i><span class="tool-tip">Add to cart</span></a>
                                                        <a onclick="addWishList(<%# Eval("product_id")%>);"><i class="p-icon icon-heart"></i><span class="tool-tip">Add to Wishlist</span></a>
                                                    </div>
                                                </div>
                                                <div class="product-content">
                                                    <h6 class="prodect-title"><a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                     DataBinder.Eval(Container.DataItem,"sub_menu_name"),DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server" title='<%# Eval("product_name")%>'><%# Eval("product_name")%></a></h6>
                                                    <div class="prodect-price">
                                                         <span class="old-price text-center">Rs.<%#Eval("sale_price")%></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Single Product Item End -->
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab_list_03">
                            <div class="row ">
                                <asp:Repeater ID="rptDuffleBag" runat="server" OnItemCommand="rptDuffleBag_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-lg-3 col-md-4 col-sm-6">
                                            <!-- Single Product Item Start -->
                                            <div class="single-product-item text-center">
                                                <div class="products-images">
                                                    <a id="A2" class="product-thumbnail" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server"
                                                        title='<%# Eval("product_name")%>'>
                                                        <img src='<%# "images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' class="img-fluid" />
                                                        <span class='<%# Convert.ToBoolean(Eval("in_stock"))?"hidden":"ribbon out-of-stock" %>'>Out of Stock</span>
                                                         <span class='<%# Convert.ToInt16(Eval("discount")) == 0 ?"hidden":"ribbon-left onsale" %>'>- <%# Convert.ToInt16(Eval("discount"))%> %</span>
                                                    </a>
                                                    <div class="product-actions">
                                                        <asp:LinkButton ID="btnquickduffle" runat="server" OnClientClick="ShowPopup()" CommandName="quickduffle" CommandArgument='<%# Eval("product_id") %>'><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></asp:LinkButton>
                                                        <a href='<%# "/cart.aspx?proid=" + Eval("product_id")%>' runat="server" title='<%# Eval("product_name")%>'><i class="p-icon icon-bag2"></i><span class="tool-tip">Add to cart</span></a>
                                                        <a onclick="addWishList(<%# Eval("product_id")%>);"><i class="p-icon icon-heart"></i><span class="tool-tip">Add to Wishlist</span></a>
                                                    </div>
                                                </div>
                                                <div class="product-content">
                                                    <h6 class="prodect-title"><a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                       DataBinder.Eval(Container.DataItem,"sub_menu_name"),DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server" title='<%# Eval("product_name")%>'><%# Eval("product_name")%></a></h6>
                                                    <div class="prodect-price">
                                                         <span class="old-price text-center">Rs.<%#Eval("sale_price")%></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Single Product Item End -->
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab_list_04">
                            <div class="row">
                                <asp:Repeater ID="rptLaptopBag" runat="server" OnItemCommand="rptLaptopBag_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-lg-3 col-md-4 col-sm-6">
                                            <!-- Single Product Item Start -->
                                            <div class="single-product-item text-center">
                                                <div class="products-images">
                                                    <a id="A1" class="product-thumbnail" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server"
                                                        title='<%# Eval("product_name")%>'>
                                                        <img src='<%# "images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%#Eval("product_name") %>' class="img-fluid" />
                                                        <span class='<%# Convert.ToBoolean(Eval("in_stock"))?"hidden":"ribbon out-of-stock" %>'>Out of Stock</span>
                                                        <span class='<%# Convert.ToInt16(Eval("discount")) == 0 ?"hidden":"ribbon-left onsale" %>'>- <%# Convert.ToInt16(Eval("discount"))%> %</span>
                                                    </a>
                                                    <div class="product-actions">
                                                        <asp:LinkButton ID="btnquickleptop" runat="server" OnClientClick="ShowPopup()" CommandName="quickleptop" CommandArgument='<%# Eval("product_id") %>'><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></asp:LinkButton>
                                                        <a href='<%# "/cart.aspx?proid=" + Eval("product_id")%>' runat="server" title='<%# Eval("product_name")%>'><i class="p-icon icon-bag2"></i><span class="tool-tip">Add to cart</span></a>
                                                        <a onclick="addWishList(<%# Eval("product_id")%>);"><i class="p-icon icon-heart"></i><span class="tool-tip">Add to Wishlist</span></a>
                                                    </div>
                                                </div>
                                                <div class="product-content">
                                                    <h6 class="prodect-title"><a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                       DataBinder.Eval(Container.DataItem,"sub_menu_name"),DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'
                                                        runat="server" title='<%# Eval("product_name")%>'><%# Eval("product_name")%></a></h6>
                                                    <div class="prodect-price">
                                                         <span class="old-price text-center">Rs.<%#Eval("sale_price")%></span>
                                                    </div>
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
            <!-- Product Area End -->
            <!-- Our Blog Area Start -->
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
            <!-- Our Blog Area End -->
            <!-- Our Newsletter Area Start -->
            <div class="our-newsletter-area section-space--pb_120 border-bottom">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                        </div>
                    </div>
                </div>
            </div>
            <!-- Our Newsletter Area End -->
        </div>
        <!--====================  footer area ====================-->
        <div class="footer-area-wrapper">
            <div class="footer-area section-space--ptb_120">
                <div class="container">
                    <div class="row footer-widget-wrapper text-center">
                        <div class="col-lg-4 col-md-4 col-sm-6 footer-widget">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">Get In Touch</h6>

                            <ul class="footer-widget__list">
                                <li><i class="icon_pin"></i>C 272, 1st Floor, Sector 10, <br /> Noida U.P. 201301, India.</li>
                                <li><i class="icon_phone"></i><a href="tel:846677028028" class="hover-style-link">+91-8800400570</a></li>
                                <li><i class="icon_mail"></i><a href="tel:846677028028" class="hover-style-link">connect@thestruttstore.com</a></li>
                            </ul>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 footer-widget">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">Help & Information</h6>
                            <ul class="footer-widget__list">
                                <li><a href="../return-policy.aspx" class="hover-style-link">Returns & Refunds</a></li>
                                <li><a href="../terms-conditions.aspx" class="hover-style-link">Terms & Conditions</a></li>
                                <li><a href="../privacy-policy.aspx" class="hover-style-link">Privacy-Policy</a></li>
                                <li><a href="../sitemap.aspx">Sitemap</a></li>
                            </ul>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 footer-widget">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">About Us</h6>
                            <ul class="footer-widget__list">
                                <li><a href="../about-us.aspx" class="hover-style-link">About Us</a></li>
                                <li><a href="../exclusive" class="hover-style-link">Exclusive</a></li>
                                 <li><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/review.aspx">Places</asp:HyperLink>
                                <li><a href="../contact-us.aspx" class="hover-style-link">Contact Us</a></li>
                            </ul>
                        </div>
                        <%--<div class="col-lg-3 col-md-6 col-sm-6 footer-widget">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">Newsletter</h6>
                            <div class="footer-widget__newsletter mt-30">
                                <input type="text" placeholder="Your email address">
                                <button class="submit-button"><i class="icon-arrow-right"></i></button>
                            </div>
                            <ul class="footer-widget__footer-menu  section-space--mt_60 d-none d-lg-block">
                                <li><a href="../terms-conditions.aspx">Term & Condition</a></li>
                                <li><a href="../return-policy.aspx">Policy</a></li>
                                
                            </ul>
                        </div>--%>
                    </div>
                </div>
            </div>
            <div class="footer-copyright-area section-space--pb_30">
                <div class="container">
                    <div class="row align-items-center text-center">
                        <div class="col-lg-4 col-md-5 text-center text-md-left">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">Payment Gateway Partner</h6>
                            <h4>
                                <asp:Image runat="server" ImageUrl="../images/Razorpay.png"  Width="100" /></h4>
                        </div>
                        <div class="col-lg-4 col-md-2 text-center">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">7 days return policy </h6>
                            <p class="margin-t-10"><a class="link" href="../terms-conditions.aspx">Simply return it within 7 days for a refund.</a> </p>
                        </div>
                        <div class="col-lg-4 col-md-5 order-md-3">
                            <h6 class="footer-widget__title mb-20 font-weight-bold">We Accept</h6>
                            <p class="margin-t-10">
                                <a href="#">
                                    <asp:Image runat="server" ImageUrl="../images/card-icon-1.jpg" />
                                </a>
                                <a href="#">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../images/card-icon-2.jpg" />
                                </a>
                                <a href="#">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="../images/card-icon-3.jpg" />
                                </a>
                                <a href="#">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="../images/card-icon-4.jpg" />
                                </a>
                            </p>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row align-items-center">
                        <div class="col-lg-4 col-md-5 text-center text-md-left"><span>Copyright © <a href="#" target="_blank">Strutt 2018 All rights reserved</a></span> </div>
                        <div class="col-lg-4 col-md-2 text-center">
                            <div class="footer-logo">
                                <a href="../../default.aspx">
                                    <img src="../../assets/images/logo/logo.png" alt="" /></a>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-5 order-md-3">
                            <div class="footer-bottom-social">
                                <h6 class="title">Follow Us</h6>
                                <ul class="list footer-social-networks ">
                                    <li class="item"><a href="https://www.facebook.com/theSTRUTTstore/" target="_blank" aria-label="Facebook"><i class="social social_facebook"></i></a></li>
                                    <li class="item"><a href="https://www.instagram.com/thestruttstore/" target="_blank" aria-label="Instagram"><i class="social social_instagram_square"></i></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--====================  End of footer area  ====================-->
        <!-- Modal -->
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
                                    <asp:Repeater ID="rptimgZoomLarge" runat="server">
                                        <ItemTemplate>
                                             <a href="#" class="images center"> 
                                            <%--<div class="easyzoom-style">
                                                <div class="easyzoom easyzoom--overlay">--%>
                                                    <img id="imgzoomsmall" runat="server" src='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' alt="product image thumb" class="img-fluid" />
                                                <%--</div>
                                            </div>--%>
                                              </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="product-details-content quickview-content-wrap ">
                                    <h5 class="font-weight--reguler mb-10">
                                        <asp:Label ID="lblProductName" runat="server"></asp:Label></h5>
                                    <p>
                                        <asp:Literal ID="litReviewHead" runat="server"></asp:Literal>
                                    </p>
                                    <%--<div class="quickview-ratting-review mb-10">
                                        <div class="quickview-ratting-wrap">
                                            <div class="quickview-ratting">
                                                <asp:HiddenField ID="hfieldproductid" runat="server" Value='<%#Eval("product_id")%>'></asp:HiddenField>
                                                <cc1:Rating ID="ratingShow" runat="server" ReadOnly="true" CssClass="yellow icon_star" StarCssClass="yellow icon_star"
                                                    WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                </cc1:Rating>
                                               <asp:Label ID="lblReview" CssClass="text-success" runat="server" ></asp:Label>
                                                
                                            </div>
                                        </div>
                                    </div>--%>
                                     <p class="starability-result" data-rating="0" id="ProReviewId"></p>
                                                 <asp:HiddenField ID="hfProId" runat="server"></asp:HiddenField>
                                    <h3 class="price">Rs.
                                        <asp:Label ID="lblSalePrice" runat="server"></asp:Label>
                                    </h3>
                                    <div class="stock in-stock mt-10">
                                        <p>
                                            Available: <span>
                                                <asp:Label ID="lblStock" runat="server" Text="Out of Stock" CssClass="text-red"></asp:Label></span>
                                        </p>
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
                                                    <asp:LinkButton ID="btnAddToCart" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" OnClick="btnAddToCart_Click"> ADD TO BAG</asp:LinkButton>
                                                </div>
                                                <div class="quickview-wishlist button">
                                                    <asp:LinkButton ID="lbtnAddWishlist" runat="server" OnClick="lbtnAddWishlist_Click"><i class="icon-heart"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                 </div>
                                    <div class="product_meta mt-10">
                                        <span class="s-b">
                                            Material: <span class="s-g">
                                                <asp:Label ID="lblPDMeterial" runat="server"></asp:Label></span>
                                        </span>
                                    </div>
                                    <div class="product_meta mt-10">
                                       <span class="s-b">
                                            Color: <span class="s-g">
                                                <asp:Label ID="lblPDColor" runat="server"></asp:Label></span>
                                       </span>
                                    </div>
                                <div class="product_meta mt-10">
                                        <span class="s-b">
                                            Size: <span class="s-g">
                                                <asp:Label ID="lblPDSize" runat="server"></asp:Label></span>
                                       </span>
                                    </div>
                                <div class="product_socials section-space--mt_60">
                                    <span class="label">Share this items :</span>
                                    <ul class="helendo-social-share socials-inline">
                                        <li><a href="https://wa.me/+918800400570" target="_blank">
                                            <img src="../../images/Whatsapp.png" alt="" /></a> </li>
                                        <li><a href="tel:+918800400570" target="_blank">
                                            <img src="../../images/Callus.png" alt="" /></a></li>
                                        <li><a href="http://www.facebook.com/sharer.php?u=<%= System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl %>" target="_blank">
                                            <img src="../../images/fb.jpg" alt="" /></a></li>
                                        <li><a href="https://www.instagram.com/thestruttstore/" target="_blank">
                                            <img src="../../images/insta.jpg" /></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Offer Modal -->
        <div class="modal fade modal-video" id="modal-offer" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" role="document" data-dismiss="modal">
                <div class="close-mo-video-01 trans-0-4" data-dismiss="modal" aria-label="Close">&times;</div>
                <div class="p-t-190 p-b-200">
                    <div class="">
                        <div class="flex-col-c-m p-l-15 p-r-15">
                            <h3 class="l-text1 fs-35-sm">Offer
                            </h3>
                            <span class="m-text9 p-t-45 fs-20-sm" style="text-align: center;">The Monsoon Sale is on - Get Flat 20% OFF using promo code "HY20" + Extra 5% OFF on online payments.(Offer valid for limited time only)
                            </span>
                            <span class="m-text9 p-t-45 fs-20-sm" style="text-align: center;">We are a safe working environment maintaining safety guidelines and looking out for the emotional wellbeing of all people involved.
                            </span>

                            <span class="m-text9 p-t-45 fs-20-sm" style="text-align: center;">Our safety approach: Daily temperature checks, One meter distance, Fresh masks used daily, Frequent sanitization & Handwash, Arogya Setu app essential, Disposable gloves while packaging & sanitizing products.</span>

                            <hr>
                            <span class="m-text9 p-t-45 fs-20-sm" style="text-align: center;">Dont forget to check out the new hand made masks just for you.</span>
                            <hr>

                            <span class="s-text4 p-t-25" style="text-align: center;">(Offer not valid on Products already on sale)
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Modal Video 01-->

        <!-- Modal  Login-->
        <div class="header-login-register-wrapper modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <asp:HiddenField ID="hfLoginType" runat="server" />
                    <asp:HiddenField ID="hffbid" runat="server" />
                    <asp:HiddenField ID="hfpersonaName" runat="server" />
                    <asp:HiddenField ID="hfpersonamobile" runat="server" />
                    <asp:HiddenField ID="hfreceiveremail" runat="server" />
                    <asp:Label ID="lblLoginMsg" runat="server" ForeColor="Red" CssClass="ValidatorsMsg"></asp:Label>
                    <div class="modal-box-wrapper">
                        <div class="helendo-tabs">
                            <ul class="nav" role="tablist">
                                <li class="tab__item nav-item active"><a class="nav-link active" data-toggle="tab" href="#tab_list_06" role="tab">Login</a> </li>
                                <li class="tab__item nav-item"><a class="nav-link" data-toggle="tab" href="#tab_list_07" role="tab">Our Register</a> </li>
                            </ul>
                        </div>
                        <div class="tab-content content-modal-box">

                            <div class="tab-pane fade show active" id="tab_list_06" role="tabpanel">
                                <div>
                                    <div class="col-lg-6 col-md-5 col-6 text-center">
                                        <fb:login-button scope="public_profile,email" onlogin="fblogin()" data-max-rows="3" data-size="large" data-button-type="continue_with"></fb:login-button>
                                        <br />
                                        <br />
                                    </div>
                                    <div class="col-lg-6 col-md-5 col-6 text-center">
                                        <div class="g-signin2" onclick="googleLogin()" data-width="255" data-height="40" data-longtitle="true" data-theme="dark" style="display: table; margin-left: 26px"></div>
                                        <asp:Button ID="btnFbGoogle" runat="server" Text="Proceed" ValidationGroup="facebook" OnClick="btnFbGoogle_Click" Height="0" Width="0" Style="visibility: hidden" />
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="Defult" DefaultButton="lbtnLogin">
                                    <div class="account-form-box">
                                        <h6>Login your account</h6>
                                        <div class="single-input">
                                            <asp:TextBox ID="txtLoginEmail" runat="server" placeholder="Enter your email address"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="txtLoginEmail"
                                                runat="server" Display="Dynamic" ValidationGroup="log" SetFocusOnError="true"
                                                ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic"
                                                ForeColor="Red" runat="server" SetFocusOnError="true" ErrorMessage="Invalid Email"
                                                ValidationGroup="log" ControlToValidate="txtLoginEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="single-input">
                                            <asp:TextBox ID="txtLoginPwd" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVTlogepass" runat="server" ForeColor="Red"
                                                SetFocusOnError="true" ErrorMessage="Password is required" Display="Dynamic"
                                                ControlToValidate="txtLoginPwd" ValidationGroup="log"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                ControlToValidate="txtLoginPwd" ID="RegularExpressionValidator8" ValidationExpression="^[\s\S]{6,}$"
                                                runat="server" ValidationGroup="log" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="checkbox-wrap mt-10">
                                            <asp:LinkButton ID="lbtnForgotPassword" runat="server" OnClick="lbtnForgotPassword_Click"
                                                class="mt-10" CausesValidation="false">Lost your password?</asp:LinkButton>
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </div>
                                        <div class="button-box mt-25">
                                            <asp:LinkButton ID="lbtnLogin" runat="server" class="btn btn--full btn--black" ValidationGroup="log" OnClick="lbtnLogin_Click">Log In</asp:LinkButton>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="tab-pane fade" id="tab_list_07" role="tabpanel">
                                <input type="hidden" name="_token" value="GhHgeiFnOYNSsfNgZhJlktvtObijpOiWWq52qzTm" />
                                <asp:Panel runat="server" ID="Register" DefaultButton="btnSignUp">
                                    <div class="account-form-box">
                                        <h6>Register An Account</h6>
                                        <div class="single-input">
                                            <asp:TextBox ID="txtsignoutUserName" runat="server" placeholder="Enter your First Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtsignoutUserName" runat="server" ValidationGroup="signup"
                                                ErrorMessage="First Name is required."></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="single-input">
                                            <asp:TextBox ID="txtsignoutMobile" runat="server" placeholder="Enter your Mobile number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtsignoutMobile" runat="server" ValidationGroup="signup"
                                                ErrorMessage="Mobile Number is required."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="signup"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtsignoutMobile"
                                                ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="single-input">
                                            <asp:TextBox ID="txtsignoutEmail" runat="server" placeholder="Enter your email Address"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtsignoutEmail" runat="server" SetFocusOnError="true" ValidationGroup="signup"
                                                ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                ErrorMessage="Invalid Email Address" SetFocusOnError="true" ValidationGroup="signup"
                                                ControlToValidate="txtsignoutEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="single-input">
                                            <asp:TextBox ID="txtsignoutPassword" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtsignoutPassword" runat="server" ValidationGroup="signup"
                                                ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtsignoutPassword"
                                                ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{6,}$" runat="server"
                                                ValidationGroup="signup" ForeColor="Red" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="button-box mt-25">
                                            <asp:Button ID="btnSignUp" runat="server" Text="Register" class="btn btn--full btn--black"
                                                ValidationGroup="signup" OnClientClick="identifyNewuser();" OnClick="btnSignUp_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--  offcanvas Minicart Start -->
        <div class="offcanvas-minicart_wrapper" id="miniCart">
            <div class="offcanvas-menu-inner">
                <div class="close-btn-box"><a href="#" class="btn-close"><i class="icon-cross2"></i></a></div>
                <div class="minicart-content">
                    <ul class="minicart-list">
                        <asp:Repeater ID="rptCartPc" runat="server" OnItemCommand="rptCartPc_ItemCommand">
                            <ItemTemplate>
                                <ul class="minicart-list">
                                    <li class="minicart-product">
                                        <asp:LinkButton ID="btnRemove" class="product-item_remove" CommandName="Remove" CommandArgument='<%# Eval("product_id") %>' runat="server"><i class="icon-cross2"></i></asp:LinkButton>
                                        <a class="product-item_img">
                                            <img class="img-fluid" width="60" src='<%#Eval("thumb_image") %>' alt='<%# Eval("product_name")%>'>
                                        </a>
                                        <div class="product-item_content">
                                            <a class="product-item_title" href="#"><%#Eval("product_name")%></a>
                                            <label>Qty : <span><%#Eval("quantity")%></span></span></label>
                                            <label class="product-item_quantity">Price: <span>Rs.<%#Eval("sale_price")%></span></label>
                                        </div>
                                    </li>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="minicart-item_sub_total"><span class="font-weight--reguler">Subtotal:</span> 
                    <span class="ammount font-weight--reguler">Rs.<asp:Literal ID="litSubTotalAmt" runat="server"></asp:Literal></span> </div>
                <div class="minicart-item_sub_total"><span class="font-weight--reguler">Discount:</span> 
                    <span class="ammount font-weight--reguler">Rs.<asp:Literal ID="litTotalDiscount" runat="server"></asp:Literal></span> </div>
                <div class="minicart-item_total"><span class="font-weight--reguler">Total:</span> 
                    <span class="ammount font-weight--reguler">Rs.<asp:Literal ID="litTotalAmt" runat="server"></asp:Literal></span> </div>
                <div class="minicart-btn_area"><a href="cart.aspx" class="btn btn--full btn--border_1 text-center btn--lg">View cart</a> </div>
                <div class="minicart-btn_area"><a href="proceedtopayment.aspx" class="btn--black btn--full text-center btn--lg">Checkout</a> </div>
            </div>
        </div>
        <!--  offcanvas Minicart End -->

        <!--====================  search overlay ====================-->
        <div class="search-overlay" id="search-overlay">
            <div class="search-overlay__header">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-6 col-8">
                            <div class="search-title">
                                <h4 class="font-weight--normal">Search</h4>
                            </div>
                        </div>
                        <div class="col-md-6 ml-auto col-4">
                            <!-- search content -->
                            <div class="search-content text-right"><span class="mobile-navigation-close-icon" id="search-close-trigger"><i class="icon-cross"></i></span></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="search-overlay__inner">
                <div class="search-overlay__body">
                    <div class="search-overlay__form">
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-9 ml-auto mr-auto">
                                    <div>
                                        <div class="product-cats section-space--mb_60 text-center">
                                            <label>
                                                <input type="radio" name="product_cat" value="" checked="checked">
                                                <span class="line-hover"><a href="../default.aspx">Home</a></span>
                                            </label>
                                            <label>
                                                <input type="radio" name="product_cat" value="decoration">
                                                <span class="line-hover"><a href="/bags">Travel Bags
                                        <%--<asp:Repeater ID="rptMenu1" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>--%>
                                    </a></span>
                                            </label>
                                            <label>
                                                <input type="radio" name="product_cat" value="furniture">
                                                <span class="line-hover">
                                                    <asp:Repeater ID="rptMenu2" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </span>
                                            </label>
                                            <label>
                                                <input type="radio" name="product_cat" value="table">
                                                <span class="line-hover"><a href="/limited-edition-series">LIMITED EDITION SERIES
                                        <asp:Repeater ID="rptMenu5" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                                </a></span>
                                            </label>
                                            <label>
                                                <input type="radio" name="product_cat" value="chair">
                                                <span class="line-hover"><a href="/the-voyager-series">THE VOYAGER SERIES<asp:Repeater ID="rptMenu6" runat="server">
                                                    <ItemTemplate>
                                                        <div class="sub-category">
                                                            <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </a></span>
                                            </label>
                                            <label>
                                                <input type="radio" name="product_cat" value="chair">
                                                <span class="line-hover"><a href="/accessories">ACCESSORIES<asp:Repeater ID="rptMenu3" runat="server">
                                                    <ItemTemplate>
                                                        <div class="sub-category">
                                                            <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </a></span>
                                            </label>
                                            <label>
                                                <input type="radio" name="product_cat" value="chair">
                                                <span class="line-hover"><a href="/when-we-collaborate">WHEN WE COLLABORATE<asp:Repeater ID="rptMenu4" runat="server">
                                                    <ItemTemplate>
                                                        <div class="sub-category">
                                                            <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </a></span>
                                            </label>
                                        </div>
                                        <div class="search-fields">
                                            <asp:TextBox runat="server" name="q" ID="txtname" autocomplete="off"  placeholder="Search Anything..."></asp:TextBox>
                                            <asp:LinkButton ID="lbtngo" runat="server" OnClick="lbtngo_Click" CssClass="submit-button"><i class="icon-magnifier"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="mobile-menu-overlay" id="mobile-menu-overlay">
            <div class="mobile-menu-overlay__inner">
                <div class="mobile-menu-close-box text-left"><span class="mobile-navigation-close-icon" id="mobile-menu-close-trigger"><i class="icon-cross2"></i></span></div>
                <div class="mobile-menu-overlay__body">
                    <nav class="offcanvas-navigation">
                        <ul>
                            <li class="has-children"><a href="../default.aspx">Home</a>
                            </li>
                            <li class="has-children"><a href="../""> Category</a>
                                <ul class="sub-menu">
                                    <li><a href="/bags">TRAVEL BAGS
                                        <%--<asp:Repeater ID="rptMenuM1" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>--%>
                                    </a>
                                    </li>
                                    <%--<li>
                                        <asp:Repeater ID="rptMenuM2" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </li>--%>
                                    <li><a href="/limited-edition-series">LIMITED EDITION SERIES
                                        <asp:Repeater ID="rptMenuM3" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </a></li>
                                    <li><a href="/the-voyager-series">THE VOYAGER SERIES<asp:Repeater ID="rptMenuM4" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </a></li>
                                    <li><a href="/accessories">ACCESSORIES<asp:Repeater ID="rptMenuM5" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </a></li>
                                    <li><a href="/when-we-collaborate">WHEN WE COLLABORATE<asp:Repeater ID="rptMenuM6" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </a></li>
                                </ul>
                            </li>
                            <li class="has-children"><a href="../"">Shop</a>
                                <ul class="sub-menu">
                                    <li><a href="../sales"><span>Sales</span></a></li>
                                    <li><a href="../exclusive"><span>Exclusive</span></a></li>
                                    <li><a href="../bestseller"><span>Best Seller</span></a></li>
                                </ul>
                            </li>
                            <li class="has-children"><a href="#">Info</a>
                                <ul class="sub-menu">
                                    <li><a href="../about-us.aspx"><span>About Us</span></a></li>
                                    <li><a href="../contact-us.aspx"><span>Contact</span></a></li>
                                    <li><a href="../terms-conditions.aspx"><span>Terms Conditions</span></a></li>
                                    <li><a href="../return-policy.aspx"><span>exchanges & Returns</span></a></li>
                                    <li><a href="../sitemap.aspx"><span>Site Map</span></a></li>
                                </ul>
                            </li>
                            <li class="has-children"><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/review.aspx">Places- The Blog</asp:HyperLink>
                            </li>
                           <li><asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="~/Login.aspx">My Account</asp:HyperLink>
                             <li class="has-children"></li>
                        </ul>
                    </nav>
                    <div class="mobile-menu-contact-info section-space--mt_60">
                        <h6>Contact Us</h6>
                        <p>
                        C 272, 1st Floor, Sector 10, Noida - 201301, Uttar Pradesh, India.
                            <br>
                            connect@thestruttstore.com
                            <br>
                            +91- 8800400570
                        </p>
                    </div>
                    <div class="mobile-menu-social-share section-space--mt_60">
                        <h6>Follow Us</h6>
                        <ul class="social-share">
                            <li><a href="https://www.facebook.com/theSTRUTTstore/"><i class="social social_facebook"></i></a></li>
                            <li><a href="https://www.instagram.com/thestruttstore/"><i class="social social_instagram_square"></i></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!--====================  End of search overlay  ====================-->

        <!--====================  scroll top ====================-->
        <a href="#" class="scroll-top" id="scroll-top"><i class="arrow-top icon-arrow-up"></i><i class="arrow-bottom icon-arrow-up"></i></a>
        <!--====================  End of scroll top  ====================-->
        <!-- JS
    ============================================ -->
        
        <!-- Modernizer JS -->
        <script src="../../assets/js/vendor/modernizr-2.8.3.min.js"></script>

        <script src="../../assets/js/plugins/plugins.js"></script>
        <!-- Bootstrap JS -->
        <script src="../../assets/js/vendor/bootstrap.min.js"></script>
        <!-- Fullpage JS -->
        <script src="../../assets/js/plugins/fullpage.min.js"></script>
        <!-- Slick Slider JS -->
        <script src="../assets/js/plugins/slick.min.js"></script>
        <!-- Countdown JS -->
        <script src="../assets/js/plugins/countdown.min.js"></script>
        <!-- Magnific Popup JS -->
        <script src="../../assets/js/plugins/magnific-popup.js"></script>
        <!-- Easyzoom JS -->
        <script src="../../assets/js/plugins/easyzoom.js"></script>
        <!-- ImagesLoaded JS -->
        <script src="../../assets/js/plugins/images-loaded.min.js"></script>
        <!-- Isotope JS -->
        <script src="../../assets/js/plugins/isotope.min.js"></script>
        <!-- YTplayer JS -->
        <%--  <script src="../../assets/js/plugins/YTplayer.js"></script>--%>
        <!-- Instagramfeed JS -->
        <script src="../../assets/js/plugins/jquery.instagramfeed.min.js"></script>
        <!-- Ajax Mail JS -->
        <script src="../../assets/js/plugins/ajax.mail.js"></script>
        <!-- wow JS -->
        <script src="../../assets/js/plugins/wow.min.js"></script>
        <!-- Plugins JS (Please remove the comment from below plugins.min.js for better website load performance and remove plugin js files from avobe) -->
        <!-- Main JS -->
        <script src="../../assets/js/main.js"></script>
        <script type="text/javascript">
            $('.quickview-product-active').slick({
                slidesToShow: 1,
                autoplay: false,
                slidesToScroll: 1,
                prevArrow: '<i class="icon-chevron-left arrow-prv"></i>',
                nextArrow: '<i class="icon-chevron-right arrow-next"></i>',
                button: false,
            });
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
    <script>
        $(function () {
            document.getElementById("ProReviewId").attributes["data-rating"].value = document.getElementById('<%=hfProId.ClientID %>').value;
         });
    </script>
    <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
    </script>
    <noscript>
        <div style="display: inline;">
            <img height="1" width="1" style="border-style: none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0" />
        </div>
    </noscript>
</body>
</html>
