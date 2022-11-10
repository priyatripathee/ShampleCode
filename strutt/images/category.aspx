<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" Buffer="true" CodeBehind="category.aspx.cs" Inherits="strutt.category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/normalize.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var pageIndex = 1;
        var pageCount = 3;
        //        $(window).scroll(function () {
        //            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        //                GetRecords();
        //            }
        //        });
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

                if ($(".toolbar-btn.view-toggle").hasClass("active"))
                    strItem = '<div class="product col-12-12"> ';
                else
                    strItem = '<div class="product col-6-12 col-sm-4-12 col-md-3-12"> ';

                strItem += '<div class="' + (product.find("in_stock").text() == 'true' ? "hidden" : "btn-outofstock") + '">Out of Stock</div>'
	                + '<div>'
	                + '	<div class="block2-img wrap-pic-w of-hidden pos-relative block2-labeldis">'
	                + '	  <a class="product-link clearfix" href="' + product.find("url").text() + '">'
	                + '			<div class="' + (product.find("in_stock").text() == 'true' ? "product-thumbnail" : "product-thumbnail out-of-stock") + '">'
	                + '			 <img class="img-normal" src="../images/Product/Thumb/' + product.find("thumb_image").text() + '" alt=\'' + product.find("product_name").text() + '\' />'
	                + '			</div>'
	                + '		</a>'
	                + '	</div>'
	                + '	<div class="margin-t-10">'
	                + '	  <div class="flex gutter-between no-wrap">'
	                + '		  <a href="' + product.find("url").text() + '" title=\'' + product.find("product_name").text() + '\' class="w-full">'
	                + '			<div>'
	                + '				<p class="bold primary">' + (product.find("product_name").text().lenght <= 25 ? product.find("product_name").text() : product.find("product_name").text().substring(0, 25) + '...') + '</p>'
	                + '				<strike>' + (product.find("discount").text() == 0 ? "" : parseFloat(product.find("Price").text()).toFixed(2)) + '</strike>'
	                + '				<span style="float:right;">Rs.' + parseFloat(product.find("sale_price").text()).toFixed(2) + '</span>'
	                + '			</div>'
	                + '		   </a>'
	                + '		<div>'
	                + '			<a title="Add to Wishlist" class="btn btn-raw col-12-12" onclick="addWishList(' + product.find("product_id").text() + ');"><i class="fa fa-heart" aria-hidden="true"></i></a>'
	                + '		</div>'
	                + '	  </div>'
	                + '	</div>'
	                + '</div>'
                    + '</div>';

                $("#products").append(strItem);

            });
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
    jQuery(document).ready(function (e) {
        if (window.location.href == 'https://thestruttstore.com' || window.location.href == 'https://thestruttstore.com/')
            jQuery('#hfOffer').click();
    });
    </script>
    <%--<script type="text/javascript">
        jQuery(document).ready(function (e) {
            jQuery('#mymodal').trigger('click');
        });
    </script>
    <style type="text/css">
        #myModal {
            background-color: rgba(0,0,0, 0.4);
        }
    </style>--%>
    <style type="text/css">
        .wrap-video-mo-01 {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <button type="button" id="hfOffer" class="btn hidden" data-toggle="modal" data-target="#modal-offer"></button>
    <div class="container">
        <div class="margin-t-20">
            <div class="flex gutter" id="divSubMenu" runat="server">
                <div class="col-4-12">
                    <div class="toolbar-btn text-center" data-target="#category">Category </div>
                </div>
                <div class="col-4-12">
                    <div class="toolbar-btn text-center" data-target="#sort">Sort By </div>
                </div>
                <div class="col-4-12">
                    <div class="toolbar-btn view-toggle text-center"><span class="margin-r-20"><i class="fa fa-th" aria-hidden="true"></i></span><span><i class="fa fa-square" aria-hidden="true"></i></span></div>
                </div>
            </div>
        </div>
        <div class="margin-t-30">
            <p > <asp:Literal ID="lblDecsHeader" runat="server"></asp:Literal></p>
        </div>
        <div id="category" class="toolbar-menu hidden">
            <div class="toolbar-div">
                <div class="container margin-t-30">
                    <div class="flex gutter">

                        <div class="col-12-12 col-sm-4-12">
                            <div class="main-category"><a href="/bags" class="link primary">BAG</a> </div>
                            <asp:Repeater ID="rptMenu1" runat="server">
                                <ItemTemplate>
                                    <div class="sub-category">
                                        <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                        <%--<a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link "><%#Eval("sub_menu_name")%></a>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <%-- <div class="col-12-12 col-sm-2-12">
                            <asp:Repeater ID="rptMenu2" runat="server">
                            <ItemTemplate>
                             <div class="sub-category"> <a href='<%# "../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link ">
                     <%#Eval("sub_menu_name")%></a> </div>
                            </ItemTemplate>
                            </asp:Repeater>
          </div>--%>
                        <div class="col-12-12 col-sm-4-12">
                            <div class="main-category"><a href="/accessories" class="link  primary">ACCESSORIES</a> </div>
                            <asp:Repeater ID="rptMenu3" runat="server">
                                <ItemTemplate>
                                    <div class="sub-category">
                                        <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                        <%--<a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class="link "><%#Eval("sub_menu_name")%></a>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <%-- <div class="col-12-12 col-sm-2-12">
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
        <asp:Label ID="lblMessage" runat="server" Visible="true" CssClass="red"></asp:Label>
        <div id="sort" class="toolbar-menu hidden">
            <div class="toolbar-div">
                <div class="container margin-t-30">
                    <div class="col-4-12 pull-left">
                        <div class="padding-10">
                            <div><a href="index6e9e.html?sort=popular%20desc" class="link bold primary"><i class="fa fa-stop-circle-o" aria-hidden="true"></i>By Gender </a></div>
                            <asp:HiddenField ID="amountFrom" runat="server" />
                            <asp:HiddenField ID="amountTo" runat="server" />
                            <hr class="hr-light margin-t-10" />
                            <div class="margin-t-10">
                                <div class="box-filter" style="height: 75px;">
                                    <asp:RadioButtonList ID="rbtGendertype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtGendertype_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="Men"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Women"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div><a href="index6e9e.html?sort=popular%20desc" class="link bold primary"><i class="fa fa-stop-circle-o" aria-hidden="true"></i>Most Popular </a></div>
                            <hr class="hr-light margin-t-10" />
                            <div class="margin-t-10">
                                <div class="box-filter" style="height: 75px;">
                                    <asp:RadioButtonList ID="rbtPrice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPrice_SelectedIndexChanged">
                                        <asp:ListItem Value="sale_price asc" Text="Price low to high"></asp:ListItem>
                                        <asp:ListItem Value="sale_price desc" Text="Price high to low"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-4-12 pull-left">
                        <div class="padding-10">
                            <div><a href="#" class="link bold primary"><i class="fa fa-stop-circle-o" aria-hidden="true"></i>Material </a></div>
                            <hr class="hr-light margin-t-10" />
                            <div class="box-filter">
                                <asp:CheckBoxList ID="chkMaterialName" AutoPostBack="true" runat="server" OnSelectedIndexChanged="chkMaterialName_SelectedIndexChanged" />
                            </div>
                        </div>
                    </div>
                    <div class="col-4-12 pull-left">
                        <div class="padding-10">
                            <div><a href="#" class="link bold primary"><i class="fa fa-stop-circle-o" aria-hidden="true"></i>Color </a></div>
                            <hr class="hr-light margin-t-10" />
                            <div class="box-filter">
                                <asp:CheckBoxList ID="chkColorName" AutoPostBack="true" CssClass="lblchkbox" runat="server" OnSelectedIndexChanged="chkColorName_SelectedIndexChanged" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <asp:UpdatePanel ID="upCategory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfCategory" runat="server" />
                <asp:HiddenField ID="hfSubCategory" runat="server" />
                <asp:HiddenField ID="hfColorIDs" runat="server" />
                <asp:HiddenField ID="hfMaterialIDs" runat="server" />
                <asp:HiddenField ID="hfOrderBy" runat="server" />
                <asp:HiddenField ID="hfGnr" runat="server" />

                <div class="p-t-10">
                    <asp:Repeater ID="rpt_naviCategory" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlcategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"menu_name").ToString().Substring(0, 1).ToUpper() + Eval("menu_name").ToString().Substring(1, (Eval("menu_name").ToString().Length - 1)).ToLower()%>'
                                NavigateUrl='<%# String.Format("{0}", Eval("menu_name").ToString().ToLower().Replace(" ", "-")) %>' CssClass="bold"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="hlSubcategory" runat="server"></asp:Label>
                </div>
                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblProductName" runat="server"></asp:Label>
                <div class="flex main-center margin-t-20">
                    <div id="products-container" class="col-12-12">
                        <div id="dvproduct">
                            <div id="products" class="flex gutter" data-id="1">
                                <asp:Repeater ID="rptproduct" runat="server">
                                    <ItemTemplate>
                                        <div class="product col-6-12 col-sm-4-12 col-md-3-12">
                                            <div class='<%# Convert.ToBoolean(Eval("in_stock"))?"hidden":"btn-outofstock" %>'>Out of Stock</div>
                                            <div>
                                                <div class="block2-img wrap-pic-w of-hidden pos-relative block2-labeldis">
                                                    <a class="product-link clearfix" href='<%# Eval("url") %>'>
                                                        <div class='<%# Convert.ToBoolean(Eval("in_stock"))?"product-thumbnail":"product-thumbnail out-of-stock" %>'>
                                                            <img class="img-normal" src='<%# "../images/Product/Thumb/" + Eval("thumb_image") %>' alt='<%# Eval("product_name")%>' />
                                                        </div>
                                                    </a>
                                                </div>
                                                <div class="margin-t-10">
                                                    <div class="flex gutter-between no-wrap">
                                                        <a href='<%# Eval("url") %>' title='<%# Eval("product_name")%>' class="w-full">
                                                            <div>
                                                                <p class="bold primary"><%# Eval("product_name").ToString().Length <= 25 ? Eval("product_name") : Eval("product_name").ToString().Substring(0, 25) + "..."%></p>
                                                                <strike><%# Convert.ToInt16(Eval("discount")) == 0 ? "" : "Rs." + (Convert.ToDouble(Eval("price"))).ToString("0.00")%></strike>
                                                                <span style="float: right;">Rs.<%#(Convert.ToDouble(Eval("sale_price"))).ToString("0.00")%></span>
                                                            </div>
                                                        </a>
                                                        <div>
                                                            <a title="Add to Wishlist" class="btn btn-raw col-12-12" onclick="addWishList(<%# Eval("product_id")%>);"><i class="fa fa-heart" aria-hidden="true"></i></a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="margin-t-30">
            <div class="text-center">
                <asp:HyperLink ID="hlLoadMore" runat="server" CssClass="btn btn-primary loadMore" onclick="GetRecords();">LOAD MORE</asp:HyperLink>
                <div class="clear"></div>
            </div>
        </div>


        <p class="topbar-child1 orange">Valentine Special : Avail a Flat 15% Discount Sitewide . Use Code : STRUTT15</p>

         <div class="margin-t-30">
            <p > <asp:Literal ID="lblDecsFooter" runat="server"></asp:Literal></p>
        </div>
        <div class="margin-t-50">
            <p class="bold">Strutt..... “Everything Handcrafted” !!</p>
            <p>We at Strutt Handcraft all our products from the finest materials . Our superior craftsmanship and attention to Detail is reflected in our commitment to enduring quality.</p>
            <p>Who says you need to empty your pockets to build or buy a Beautiful Product . Our team at Strutt believes in Providing the most Beautiful products at unbelievable prices. <a href="about-us.aspx" target="_blank" class="link">Read More</a> </p>
        </div>
    </div>
    <div id="AlertDiv" class="AlertStyle"></div>
    <div class="templates hidden">
        <div class="toast">
            <div class="message"></div>
            <span class="remove"><i class="fa fa-times" aria-hidden="true"></i></span>
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
                        <span class="m-text9 p-t-45 fs-20-sm">Get flat 20% on two products and flat 25% three or more product.
                        </span>
                        <span class="s-text4 p-t-25">(Offer not valid on Products already on sale)
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">

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
