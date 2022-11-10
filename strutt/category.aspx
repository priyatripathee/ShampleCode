<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" Buffer="true" CodeBehind="category.aspx.cs" Inherits="strutt.category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="assets/css/cust_new29621.css" rel="stylesheet" />
    <style>
        .label {
            padding-right: 3px;
        }

        .ratingEmpty {
            background-image: url(/images/ratingStarEmpty.gif);
            width: 20px;
            height: 20px;
        }

        .ratingFilled {
            background-image: url(/images/ratingStarFilled.gif);
            width: 20px;
            height: 20px;
        }

        .ratingSaved {
            background-image: url(/images/ratingStarSaved.gif);
            width: 20px;
            height: 20px;
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
        .hero-product-image:hover h4 {
            color: #dcb14a !important;
        }

    </style>
    <style type="text/css">
        .wrap-video-mo-01 {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">

    <script type="text/javascript">
        var pageIndex = 1;
        var pageCount = 3;
        $(window).scroll(function () {
            if ($(document).scrollTop() >= $(document).height() - 1350) {
                GetRecords();	
            }
        });
        function OnSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var strItem = '';
            pageCount = parseInt(xml.find("PageCount").eq(0).find("PageCount").text());
            var products = xml.find("product");

            if (pageIndex >= pageCount)
                $('.loadMore').hide();

            products.each(function () {
                var product = $(this);

                if ($(".tab-three").hasClass("active"))
                    strItem = '<div class="col-lg-4 col-md-4 col-sm-6">';
                else if ($(".tab-four").hasClass("active"))
                    strItem = '<div class="col-lg-4 col-md-4 col-sm-6">';

                strItem += '<div class="single-product-item text-center">';

                strItem += '<div class="products-images mb-10">'
                 + '<a href="' + product.find("url").text() + '" title=\'' + product.find("product_name").text() + '\' class="product-thumbnail">'
                              + '<img class="img-fluid" src="../images/Product/Thumb/' + product.find("thumb_image").text() + '" alt=\'' + product.find("product_name").text() + '\' />'
                              + '<span  class="' + (product.find("in_stock").text() == 'true' ? "hidden" : "ribbon out-of-stock") + '">'
                              + '</span>';
                var discount = parseFloat(product.find("discount")[0].innerHTML);
                if (discount != 0) {
                    strItem += '<span class="ribbon-left onsale"> -' + (product.find("discount")[0].innerHTML) + '  %</span>'
                }
                strItem += '</a>'
                               + '<div class="product-actions">'
                            // +' <asp:LinkButton ID="btnquiclProduct" runat="server" OnClientClick="ShowPopup()" CommandName="allproduct" CommandArgument='<%# Eval("product_id") %>'><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></asp:LinkButton>'
                               + '<a onclick="ShowProduct(' + product.find("product_id").text() + ');" data-toggle="modal" data-target="#prodect-modal"><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></a>'
                               + '<a href="/cart.aspx?proid=' + product.find("product_id").text() + '"><i class="p-icon icon-bag2"></i> <span class="tool-tip">Add to cart</span></a>'
                               + '<a onclick="addWishList(' + product.find("product_id").text() + ');"><i class="p-icon icon-heart"></i> <span class="tool-tip">Add to Wishlist</span></a>'
                               + '</div>'
                + '</div>';

                strItem += '<div class="product-content">'
                               + '<h6 class="prodect-title">'
                               + '<a href="' + product.find("url").text() + '">' + product.find("product_name").text().lenght <= 25 ? product.find("product_name").text() : product.find("product_name").text().substring(0, 25) + '</a>'
                               + '</h6>'
                               + '<div class="prodect-price">'
                               //+ '<strike><span class="new-price pull-left">' + (product.find("discount").text() == 0 ? "" : parseFloat(product.find("Price").text()).toFixed(2)) + '</span></strike>'
                               + '<span class="old-price">Rs.' + parseFloat(product.find("sale_price").text()).toFixed(2) + '</span>'
                               + '</div>'
                               + '</div>';
                strItem += '</div>';
                $("#products").append(strItem);
            });
        }
        function GetRecords() {
            var pName = "";
            if (getUrlVars()["s"] != undefined) pName = getUrlVars()["s"];

            var isSales = false;
            if (window.location.href.match("/sales")) isSales = true;

            var isEx = false;
            if (window.location.href.match("/exclusive")) isEx = true;

            var isBest = false;
            if (window.location.href.match("/bestseller")) isBest = true;

            var gid = null;
            if (document.getElementById('<%=hfGnr.ClientID%>').value != "") gid = document.getElementById('<%=hfGnr.ClientID%>').value;

            pageIndex++;

            var params = '{';
            params += '"cId":' + document.getElementById('<%=hfCategory.ClientID%>').value;
            params += ',"scId":' + document.getElementById('<%=hfSubCategory.ClientID%>').value;
            params += ',"minPrice":' + document.getElementById('<%=amountFrom.ClientID%>').value;
            params += ',"maxPrice":' + document.getElementById('<%=amountTo.ClientID%>').value;
            params += ',"colors":"' + document.getElementById('<%=hfColorIDs.ClientID%>').value + '"';
            params += ',"materials":"' + document.getElementById('<%=hfMaterialIDs.ClientID%>').value + '"';
            params += ',"pName":"' + pName + '"';
            params += ',"isSales":' + isSales;
            params += ',"isEx":' + isEx;
            params += ',"isBest":' + isBest;
            params += ',"gid":' + gid;
            params += ',"orderBy":"' + document.getElementById('<%=hfOrderBy.ClientID%>').value + '"';
            params += ',"pageIndex":' + pageIndex;
            params += '}';

            var jsonParam = JSON.parse(params);
            console.log(params);

            if (pageIndex == 2 || pageIndex <= pageCount) {
                $.ajax({
                    type: "POST",
                    url: "/DataServices.asmx/GetProducts",
                    data: JSON.stringify(jsonParam),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    cache: false,
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response, textStatus, errorThrown) {
                        alert(response.d);
                    }
                });

            }
            else {
                $('.loadMore').hide();
            }
        }


        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        function addWishList(id) {
            if (id == undefined)
                id = recode;
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
                alert("Item already in wish list.");
            }
            else {
                document.getElementById('<%=lblMessage.ClientID%>').innerHTML = "Please login before add item in Wish list.";
                alert("Please login before add item in Wish list.");
            }
    }
    

    function ShowProduct(id) {
        $.ajax({
            type: "POST",
            url: "/DataServices.asmx/GetOneProduct",
            data: '{id: ' + id + '}',
            // data:JSON.stringify(jsonParam),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: SetProductData,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }

    function SetProductData(response) {
        //recode = response.d;
        //alert(recode + "Done");
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        // var strItem = '';
        //pageCount = parseInt(xml.find("PageCount").eq(0).find("PageCount").text());
        var products = xml.find("Product");


        products.each(function () {
            debugger;
            var product = $(this);
            $("#hfpId").val(product.find("product_id").text());

             //document.getElementById('linkAddToCart').attributes["onclick"].value = "ShowCart(" + $("#hfpId").val() + ")";
            //document.getElementById('linkAddToCart').attributes["href"].value = "/cart.aspx?proid=" + $("#hfpId").val();


            document.getElementById('lblProductName').innerHTML = product.find("product_name").text();
            document.getElementById('lblSalePrice').innerHTML = product.find("sale_price").text();
            if (product.find("in_stock").text() == 'true') {
                document.getElementById('lblStock').innerHTML = "In Stock";
                document.getElementById('lblStock').style.color = "Green";

                $("#linkAddToCart").attr("href", "/cart.aspx?proid=" + $("#hfpId").val());
            }
            else {
                document.getElementById('lblStock').innerHTML = "Out of Stock";
                document.getElementById('lblStock').style.color = "Red";

                $("#linkAddToCart").attr("disabled", true);
            }
            document.getElementById('lblSortDesc').innerHTML = product.find("short_description").text();
            document.getElementById('lblPDMeterial').innerHTML = product.find("material_name").text();
            document.getElementById('lblPDColor').innerHTML = product.find("size").text();
            document.getElementById('lblPDSize').innerHTML = product.find("color_name").text();
            document.getElementById('totalrating').attributes["data-rating"].value = product.find("rating").text();
           

        });

        $("#more").empty();
        $("#more").attr("class", "quickview-product-active mr-lg-5");

        
        var strItem1 = '';
        var productImage = xml.find("ProductImage");
        productImage.each(function () {
            var product1 = $(this);
            strItem1 = '<a href="#" class="images center"> <img class="img-fluid" src="../images/Product/Thumb/' + product1.find("thumb_image").text() + '" alt="' + product1.find("product_name").text() + '"> </a>'
            $("#more").append(strItem1);
        });

        $('.quickview-product-active').slick({
            slidesToShow: 1,
            autoplay: false,
            slidesToScroll: 1,
            prevArrow: '<i class="icon-chevron-left arrow-prv"></i>',
            nextArrow: '<i class="icon-chevron-right arrow-next"></i>',
            button: false,
        });
        $('.modal').on('shown.bs.modal', function (e) {
            $('.quickview-product-active').resize();
        })

    }
    </script>
    <script>
        function InsetCart(product_id, size) {
            $.ajax({
                type: "GET",
                url: "/DataServices.asmx/AddToCart?" + "product_id=" + product_id + "&size=" + size,
                //data:JSON.stringify(jsonParam),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: openCartWin,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }

        function ShowCart(id) {
            $.ajax({
                type: "POST",
                url: "/DataServices.asmx/InsetCart",
                data: '{id: ' + id + '}',
                // data:JSON.stringify(jsonParam),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: openCartWin,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    //alert(response.d);
                    alert("Test");
                }
            });
        }

        function openCartWin(response) {
            $("#hfpId").val(product.find("product_id").text());
            document.getElementById('linkAddToCart').attributes["onclick"].value = "InsetCart(" + $("#hfpId").val() + "; " + $("#size").val() + ")";
            var strItem2 = '';
            //recode = response.documentElement;
            //alert(recode);
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var cartItem = xml.find("CartItems1");
            cartItem.each(function () {
                var CartItems = $(this);
                strItem2 += '<div class="offcanvas-minicart_wrapper" id="miniCart">';
                strItem2 += '<div class="offcanvas-minicart_wrapper">'
                            + '<div class="close-btn-box"> <a href="#" class="btn-close"><i class="icon-cross2"></i></a> </div>'
                            + '<div class="minicart-content">';
                strItem2 += '<ul class="minicart-list">'
                            + '<li class="minicart-product">'
                            + ' <a class="product-item_remove" href="javascript:void(0)"><i class="icon-cross2"></i></a> '
                            + '<a class="product-item_img">'
                            + '<img class="img-fluid" src="../images/Product/Thumb/' + CartItems.find("thumb_image").text() + '" alt="' + CartItems.find("product_name").text() + '">'
                            + '</a>'
                            + '<div class="product-item_content"> <a class="product-item_title" href="#">' + CartItems.find("product_name").text() + '</a>'
                            + '<label>Qty : <span>' + CartItems.find("quantity").text() + '</span></label>'
                            + '<label class="product-item_quantity">Price: <span> ' + CartItems.find("sale_price").text() + '</span></label>'
                            + '</div>'
                            + '</li>'
                            + '</ul>';
                strItem2 += '</div>'
                            + '<div class="minicart-item_total"> <span class="font-weight--reguler">Subtotal:</span> <span class="ammount font-weight--reguler">$60.00</span> </div>'
                            + '<div class="minicart-btn_area"> <a href="cart.html" class="btn btn--full btn--border_1">View cart</a> </div>'
                            + '<div class="minicart-btn_area"> <a href="checkout.html" class="btn btn--full btn--black">Checkout</a> </div>'
                            + '</div>'
                            + '</div>';
                $("#fillcart").append(strItem2);

                $('#miniCart').modal('show');
            })

        }

    </script>
 
    <div id="jas-content">
    <div class="page-head pr tc" style="background: url(https://thestruttstore.com/images/shop-banner.jpg) no-repeat center center / cover;">
            <div class="jas-container pr" style="text-align:center;">
                <h1 class="tu mb__10 cw" itemprop="headline"><asp:Literal ID="lblMenuHead" runat="server" ></asp:Literal></h1>
                <p></p>
                <ul class="jas-breadcrumb dib oh">
                    <li class="fl home"><a href="/" title="Home">Home</a></li>
                    <li class="fl separator"><i class="fa fa-angle-right"></i></li>
                    <li class="fl current"><asp:Literal ID="lblMenuHeadRight" runat="server" /></li>
                </ul>
            </div>
        </div>
    <button type="button" id="hfOffer" class="btn d-none" data-toggle="modal"></button>
    <%--data-target="#modal-offer" Coomentedby Hetal Patel on 21-10-2020 as per client request--%>
    <!-- breadcrumb-area start -->
    <!-- breadcrumb-area end -->
    <div class="site-wrapper-reveal border-bottom">
        <!-- Product Area Start -->
        <div class="product-wrapper section-space--ptb_120">
            <div class="container">
                <%--desktop--%>
                <div class="hidden-xs">
                    <div class="row">
                        <%--<div class="col-lg-1 col-md-1"></div>--%>
                    <div class="col-lg-12 col-md-12">
                        <div class="shop-toolbar__items shop-toolbar__item--left">
                            <div class="shop-toolbar__items-wrapper">
                                <div class="shop-toolbar__item shop-toolbar__item--filter">
                                    <a href="../category.aspx" style="color:#b59677; font-size:12px;">All</a>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Travel Bags<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu1" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Backpacks<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu2" runat="server">
                                                    <ItemTemplate>
                                                        <div class="sub-category">
                                                            <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Business Travel<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu5" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Leisure Travel<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu6" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">One Day Travel<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu3" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Travel Accessories<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu4" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Collaborations<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptCollaborations" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>

                                <div class="shop-toolbar__item shop-short-by">
                                    <ul>
                                        <li>
                                            <a href="#">Travel Essentials<i class="fa fa-angle-down angle-down"></i></a>
                                            <ul class="widget-nav-list">
                                                <li>
                                                    <asp:Repeater ID="rptMenu7" runat="server">
                                                        <ItemTemplate>
                                                            <div class="sub-category">
                                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </li>

                                            </ul>
                                        </li>
                                    </ul>
                                </div>


                                <div class="shop-toolbar__item shop-toolbar__item--filter">
                                    <div class="shop-filter-active" style="color:#b59677; font-size:11px;">Filter<i class="icon-plus"></i></div>
                                </div>
                                <div class="shop-toolbar__item shop-short-by">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1"></div>
                    <div class="product-filter-wrapper">
                    <div class="row">
                        <div class=" mb-20 col__20">
                            <div class="product-filter">
                                <h5>Color</h5>
                                <%--<ul class="widget-nav-list">
                                    <li>
                                        <asp:CheckBoxList ID="chkColorName" AutoPostBack="true" CssClass="lblchkbox" runat="server" OnSelectedIndexChanged="chkColorName_SelectedIndexChanged" /></li> 
                                </ul>--%>
                                <ul class="widget-nav-list">
                                    <asp:Repeater ID="rptColor" runat="server">
                                        <ItemTemplate>
                                             <ul class="widget-nav-list">
                                                 <li>
                                                      <%--<asp:HiddenField ID="hfColor" runat="server" Value=<%# Eval("color_code")%> />--%>
                                                     <asp:CheckBox ID="chkColorName" runat="server" AutoPostBack="true" OnCheckedChanged="chkColorName_CheckedChanged" ColorName='<%# Eval("color_id")%>'/>
                                                    <span><%# Eval("color_name")%></span>
                                                     <span style="width: 18px; height: 18px;border-radius: 50%; border: 2px solid #dfdfdf; display: inline-block;  opacity: .8; margin-bottom: -3px;margin-right: 10px;background-color: <%# Eval("color_code")%>;"> </span>
                                                 </li> 
                                                 </ul>
                                            </ItemTemplate>
                                    </asp:Repeater>
                                    </ul>


                            </div>
                        </div>
                        <div class=" mb-20 col__20">
                            <div class="product-filter">
                                <h5>Material</h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:CheckBoxList ID="chkMaterialName" AutoPostBack="true" runat="server" OnSelectedIndexChanged="chkMaterialName_SelectedIndexChanged" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class=" mb-20 col__20">
                            <div class="product-filter">
                                <h5>Gender</h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:RadioButtonList ID="rbtGendertype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtGendertype_SelectedIndexChanged" CssClass="padding-left:3px;">
                                            <asp:ListItem Value="1" Text="Men"> </asp:ListItem>
                                            <asp:ListItem Value="2" Text="Women"> </asp:ListItem>
                                        </asp:RadioButtonList></li>
                                </ul>
                            </div>
                        </div>
                        <div class=" mb-20 col__20">
                            <div class="product-filter">
                                <h5>Sort by</h5>
                                <ul class="widget-nav-list">
                                                <%--<li class="active"><a href="#">Sales Price sorting</a></li>--%>
                                                <li>
                                                    <asp:HiddenField ID="amountFrom" runat="server" />
                                                    <asp:HiddenField ID="amountTo" runat="server" />
                                                    <asp:RadioButtonList ID="rbtPrice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPrice_SelectedIndexChanged">
                                                        <asp:ListItem Value="sale_price asc" Text=" Price low to high"> </asp:ListItem>
                                                        <asp:ListItem Value="sale_price desc" Text=" Price high to low"> </asp:ListItem>
                                                        <asp:ListItem Value="discount desc" Text="Discount"> </asp:ListItem>
                                                    </asp:RadioButtonList></li>
                                            </ul>
                            </div>
                        </div>

                        <div class="p-t-10">
                            <div class="flex gutter" id="divSubMenu" runat="server">
                                <asp:Repeater ID="rpt_naviCategory" runat="server">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlcategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"menu_name").ToString().Substring(0, 1).ToUpper() + Eval("menu_name").ToString().Substring(1, (Eval("menu_name").ToString().Length - 1)).ToLower()%>'
                                            NavigateUrl='<%# String.Format("{0}", Eval("menu_name").ToString().ToLower().Replace(" ", "-")) %>' CssClass="bold"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="hlSubcategory" runat="server"></asp:Label>
                                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>
                </div>
                </div>
                
                <%--mobile--%>
                <div class="hidden-xl" style="display:none;">
                    <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="shop-toolbar__items shop-toolbar__item--left">
                            <div class="shop-toolbar__items-wrapper">
                                <div class="shop-toolbar__item shop-toolbar__item--filter">
                                    <div class="shop-category-active" style="color:#b59677; font-size:12px;">Category<i class="icon-plus"></i></div>
                                </div>
                                <div class="shop-toolbar__item shop-toolbar__item--filter">
                                    <div class="shop-filter-active" style="color:#b59677; font-size:12px;">Filter<i class="icon-plus"></i></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="shop-toolbar__items shop-toolbar__item--right">
                            <div class="shop-toolbar__items-wrapper">
                                <%--<div class="shop-toolbar__item shop-toolbar__item--filter ">
                                    <a class="shop-filter-active" href="#">Filter<i class="icon-plus"></i></a>
                                </div>--%>
                                <div class="shop-toolbar__item">
                                    <ul class="nav toolber-tab-menu justify-content-start" role="tablist">
                                        <li class="tab__item nav-item active">
                                            <a class="nav-link  tab-three" data-toggle="tab" href="#tab_columns_01" role="tab">
                                                <img src="../../assets/images/svg/column-03.svg" class="img-fluid" alt="Columns 03" />
                                            </a>
                                        </li>
                                        <li class="tab__item nav-item active">
                                            <a class="nav-link active tab-four" data-toggle="tab" href="#tab_columns_02" role="tab">
                                                <img src="../../assets/images/svg/column-04.svg" class="img-fluid" alt="Columns 03" />
                                            </a>
                                        </li>
                                        <%--<li class="tab__item nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_columns_03" role="tab">
                                                <img src="assets/images/svg/column-05.svg" class="img-fluid" alt="Columns 03" />
                                            </a>
                                        </li>--%>
                                    </ul>
                                </div>
                               <%-- <div class="shop-toolbar__item shop-toolbar__item--result">
                                    <p class="result-count">Showing 1–9 of 21 results</p>
                                </div>--%>
                            </div>
                        </div>
                    </div>

                </div>
                    <div class="product-category-wrapper" style="display: none;">
                    <div class="row">
                        <div class="mb-20 col__20">
                            <div class="product-category">
                               <h5><a href="/travel-bags" class="hover-style-link" style="color:#b59677"> Travel Bags</a></h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:Repeater ID="rptMenu1_mob" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </li>
                                </ul>
                                <h5><a href="/one-day-travel" class="hover-style-link" style="color:#b59677"> One Day Travel</a></h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:Repeater ID="rptMenu3_mob" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </li>
                                </ul>
                                <h5><a href="/travel-accessories" class="hover-style-link" style="color:#b59677">Travel Accessories</a></h5>
                                <ul class="widget-nav-list">
                                    <li><asp:Repeater ID="rptMenu4_mob" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="mb-20 col__20">
                            <div class="product-category">
                                  <h5> <a href="/backpacks" class="hover-style-link" style="color:#b59677">Backpacks</a></h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:Repeater ID="rptMenu2_mob" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </li>
                                    <li><a href="/business-travel" style="color:#b59677"><b> Business Travel</b>
                                        <asp:Repeater ID="rptMenu5_mob" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </a></li>
                                    <li><a href="/leisure-travel" style="color:#b59677"><b>Leisure Travel</b>
                                        <asp:Repeater ID="rptMenu6_mob" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </a></li>
                                    <li><a href="/when-we-collaborate" style="color:#b59677"><b>Collaborations</b>
                                        <asp:Repeater ID="rptMenu7_mobColl" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </a></li>
                                    <li><a href="/travel-essentials" style="color:#b59677"><b>Travel Essentials</b>
                                        <asp:Repeater ID="rptMenu7_mob" runat="server">
                                        <ItemTemplate>
                                            <div class="sub-category">
                                                <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                    <div class="product-filter-wrapper">
                    <div class="row">
                        <div class=" mb-20 col__20">
                            <div class="product-filter">
                                <h5>Color</h5>
                                <%--<ul class="widget-nav-list">
                                    <li>
                                        <asp:CheckBoxList ID="chkColorName" AutoPostBack="true" CssClass="lblchkbox" runat="server" OnSelectedIndexChanged="chkColorName_SelectedIndexChanged" /></li> 
                                </ul>--%>
                                <ul class="widget-nav-list">
                                    <asp:Repeater ID="rptColor_mob" runat="server">
                                        <ItemTemplate>
                                             <ul class="widget-nav-list">
                                                 <li>
                                                      <%--<asp:HiddenField ID="hfColor" runat="server" Value=<%# Eval("color_code")%> />--%>
                                                     <asp:CheckBox ID="chkColorName" runat="server" AutoPostBack="true" OnCheckedChanged="chkColorName_mob_CheckedChanged" ColorName='<%# Eval("color_id")%>'/>
                                                    <span style="width: 18px; height: 18px;border-radius: 50%; border: 2px solid #dfdfdf; display: inline-block;  opacity: .8; margin-bottom: -3px;margin-right:0px;background-color: <%# Eval("color_code")%>;"> </span>
                                                     <span><%# Eval("color_name")%></span>
                                                     
                                                 </li> 
                                                 </ul>
                                            </ItemTemplate>
                                    </asp:Repeater>
                                    </ul>


                            </div>
                        </div>
                        <div class=" mb-20 col__20">
                            <div class="product-filter">
                                <h5>Material</h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:CheckBoxList ID="chkMaterialName_mob" AutoPostBack="true" runat="server" OnSelectedIndexChanged="chkMaterialName_mob_SelectedIndexChanged" />
                                    </li>
                                </ul>
                                <h5>Gender</h5>
                                <ul class="widget-nav-list">
                                    <li>
                                        <asp:RadioButtonList ID="rbtGendertype_mob" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtGendertype_mob_SelectedIndexChanged" CssClass="padding-left:3px;">
                                            <asp:ListItem Value="1" Text="Men"> </asp:ListItem>
                                            <asp:ListItem Value="2" Text="Women"> </asp:ListItem>
                                        </asp:RadioButtonList></li>
                                </ul>
                                <h5>Sort By</h5>
                                <ul>
                                    <li>
                                        <asp:HiddenField ID="amountFrom_mob" runat="server" />
                                        <asp:HiddenField ID="amountTo_mob" runat="server" />
                                        <asp:RadioButtonList ID="rbtPrice_mob" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPrice_mob_SelectedIndexChanged">
                                            <asp:ListItem Value="sale_price asc" Text=" Price low to high"> </asp:ListItem>
                                            <asp:ListItem Value="sale_price desc" Text=" Price high to low"> </asp:ListItem>
                                            <asp:ListItem Value="discount desc" Text="Discount"> </asp:ListItem>
                                        </asp:RadioButtonList></li>
                                </ul>
                            </div>
                        </div>
                        <div class="p-t-10">
                            <div class="flex gutter" id="div1" runat="server">
                                <asp:Repeater ID="rpt_naviCategory_mob" runat="server">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlcategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"menu_name").ToString().Substring(0, 1).ToUpper() + Eval("menu_name").ToString().Substring(1, (Eval("menu_name").ToString().Length - 1)).ToLower()%>'
                                            NavigateUrl='<%# String.Format("{0}", Eval("menu_name").ToString().ToLower().Replace(" ", "-")) %>' CssClass="bold"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="hlSubcategory_mob" runat="server"></asp:Label>
                                <asp:Label ID="lblMsg_mob" runat="server" Visible="false"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>
                </div>

                <asp:Label ID="lblMessage" runat="server" Visible="true" CssClass="text-green"></asp:Label>

                <asp:UpdatePanel ID="upCategory" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfCategory" runat="server" />
                        <asp:HiddenField ID="hfSubCategory" runat="server" />
                        <asp:HiddenField ID="hfColorIDs" runat="server" />
                        <asp:HiddenField ID="hfMaterialIDs" runat="server" />
                        <asp:HiddenField ID="hfOrderBy" runat="server" />
                        <asp:HiddenField ID="hfGnr" runat="server" />
                        <div class="tab-content">
                            <%--<div class="tab-pane fade show active" id="tab_columns_01">
                                <div class="row">
                                </div>
                            </div>--%>
                            <div class="tab-pane fade active" id="tab_columns_01">
                                 <div id="dvproduct">
                                <div class="row" id="products" data-id="1">
                                    <asp:Repeater ID="rptproduct" runat="server" OnItemCommand="rptproduct_ItemCommand">
                                        <ItemTemplate>

                                            <div class="col-lg-4 col-md-4 col-sm-6">
                                                <!-- Single Product Item Start -->
                                                <div class="single-product-item text-center">
                                                    <div class="products-images">
                                                        <a href='<%# Eval("url") %>' class="product-thumbnail">
                                                            <img class="img-fluid" src='<%# "../images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%# Eval("product_name")%>' />
                                                            <span class='<%# Convert.ToBoolean(Eval("in_stock"))?"hidden":"ribbon out-of-stock" %>'>Out of Stock</span>
                                                            <span class='<%# Convert.ToInt16(Eval("discount")) == 0 ?"hidden":"ribbon-left onsale" %>'>- <%# Convert.ToInt16(Eval("discount"))%> %</span>
                                                        </a>
                                                        <div class="product-actions">
                                                            <a onclick="ShowProduct(<%# Eval("product_id")%>);" data-toggle="modal" data-target="#prodect-modal"><i class="p-icon icon-plus"></i><span class="tool-tip">Quick View</span></a>
                                                            <a href='<%# "/cart.aspx?proid=" + Eval("product_id")%>' runat="server" title='<%# Eval("product_name")%>'><i class="p-icon icon-bag2"></i><span class="tool-tip">Add to cart</span></a>
                                                            <a onclick="addWishList(<%# Eval("product_id")%>);"><i class="p-icon icon-heart"></i><span class="tool-tip">Add to Wishlist</span></a>
                                                        </div>
                                                    </div>
                                                    <div class="product-content">
                                                        <h6 class="prodect-title">
                                                            <a href='<%# Eval("url") %>' title='<%# Eval("product_name")%>'>
                                                                <%# Eval("product_name").ToString().Length <= 25 ? Eval("product_name") : Eval("product_name").ToString().Substring(0, 25) + "..."%>
                                                            </a></h6>
                                                         <div class="prodect-price">
                                                           <span class="old-price text-center">Rs.<%#Eval("sale_price")%></span></div>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- Product Area End -->
    </div>
    <div class="container">
        <div class="m-3">
            <div class="text-center">
                <asp:HyperLink ID="hlLoadMore" runat="server" CssClass="btn btn--lg btn--black loadMore" BackColor="Gray" onclick="GetRecords();">LOAD MORE</asp:HyperLink>
                <%--<asp:HyperLink ID="hlLoadMore" runat="server" CssClass="btn btn--lg btn--black" BackColor="Gray"  onclick="GetRecords();">LOAD MORE</asp:HyperLink>--%>
                <div class="clear"></div>
            </div>
        </div>
        <p class="topbar-child1 text-orange"> <asp:Label ID="lblCouponCode" runat="server"></asp:Label> <br />
            <asp:Label ID="lblOffer" runat="server" Visible="false"></asp:Label>
            
        </p>
        <div class="margin-t-50">
            <p class="bold">Oh Darling, Grab your Coat, leave a note and run away with me! Strutt is here to Fly, Roam, Explore and Discover with you</p>
            <asp:Literal ID="lblDecsHeader" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="lblDecsFooter" runat="server" Visible="false"></asp:Literal>
            <p>STRUTT MOLDS BEAUTIFUL, DURABLE, ARTISAN TRAVEL BAGS THAT WILL LAST A LIFETIME!</p>
            <p>Strutt has set out to be the purveyor of the finest quality handcrafted travel bags, becoming the first ever one stop solution catering to the travel needs of each traveler and providing a complete travel story..<asp:HyperLink ID="read" runat="server" NavigateUrl="~/about-us.aspx" CssClass="text-orange" > Read More</asp:HyperLink> </p>
        </div>
    </div>
    <!-- Offer Modal -->
    <div class="modal fade modal-video" id="modal-offer" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document" data-dismiss="modal">
            <div class="close-mo-video-01 trans-0-4" data-dismiss="modal" aria-label="Close">&times;</div>
            <div class="wrap-video-mo-01 p-t-190 p-b-200">
                <div class="modal-content">
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
    <!-- Modal -->
    <div class="product-modal-box modal fade" id="prodect-modal" tabindex="-1" role="dialog">
        <div class="modal-dialog  modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span class="icon-cross" aria-hidden="true"></span></button>
                </div>
                <div class="modal-body container">
                    <div class="row align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="quickview-product-active mr-lg-5" id="more">
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="product-details-content quickview-content-wrap">
                                <h5 class="font-weight--reguler mb-10">
                                    <input type="hidden" id="hfpId" />
                                    <label id="lblProductName"></label>
                                </h5>
                                <p>
                                    <label id="litReviewHead"></label>
                                </p>
                                <p class="starability-result w-40" data-rating="0" id="totalrating"></p>
                                <%--<div class="quickview-ratting-review">
                                    <div class="quickview-ratting-wrap">
                                        <div class="quickview-ratting">
                                            <cc1:Rating ID="ratingShow" runat="server" ReadOnly="true" CssClass="yellow icon_star" StarCssClass="yellow icon_star"
                                                WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                            </cc1:Rating>
                                            <label id="lblReview" class="text-success"></label>
                                        </div>
                                    </div>
                                </div>--%>
                                <h3 class="price">
                                    <label id="lblSalePrice"></label>
                                    Rs.</h3>
                                <div class="stock in-stock mt-10">
                                    <p>
                                        Available: <span>
                                            <label id="lblStock"></label>
                                        </span>
                                    </p>
                                </div>
                                <div class="quickview-peragraph mt-10">
                                    <p>
                                        <label id="lblSortDesc"></label>
                                    </p>
                                </div>
                                <div class="quickview-action-wrap mt-30">
                                    <div class="quickview-cart-box">
                                        <div class="quickview-button">
                                            <div class="quickview-cart button">
                                                <a id="linkAddToCart" class="btn--lg btn--black font-weight--reguler text-white"><span class="tool-tip">ADD TO CART</span></a>
                                            </div>
                                            <div class="quickview-wishlist button">
                                                <a onclick="addWishList(<%# Eval("product_id")%>);"><i class="icon-heart"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                   <div class="product_meta mt-10">
                                        <span class="s-b">
                                            Material: <span class="s-g">
                                                <label id="lblPDMeterial"></label>
                                             </span>
                                        </span>
                                    </div>
                                    <div class="product_meta mt-10">
                                       <span class="s-b">
                                            Color: <span class="s-g"><label id="lblPDColor"></label>
                                                </span>
                                       </span>
                                    </div>
                                <div class="product_meta mt-10">
                                        <span class="s-b">
                                            Size: <span class="s-g"><label id="lblPDSize"></label>
                                               </span>
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
    </div>
    
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
<script>
jQuery(document).ready(function(jQuery){
   myDivScript();
});

function myDivScript(){
    jQuery('.tab-three').on('click', function(){
        jQuery('#products > div').removeClass('col-lg-3').addClass('col-lg-4');
    });
	jQuery('.tab-four').on('click', function(){
        jQuery('#products > div').removeClass('col-lg-4').addClass('col-lg-3');
    });
};
</script>



    <script>
        $(function () {
            if (window.location.href == 'https://thestruttstore.com' || window.location.href == 'https://thestruttstore.com/')
                jQuery('#hfOffer').click();
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
        <div style="display: inline;">
            <img height="1" width="1" style="border-style: none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0" />
        </div>
    </noscript>
</asp:Content>
