<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="wishlist.aspx.cs" Inherits="strutt.wishlist" %>

<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Wishlist</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Wishlist (<asp:Label ID="lblTotal" runat="server">0</asp:Label>
                Items)</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <!-- wishlist start -->
        <div class="wishlist-main-area  section-space--ptb_90">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3">
                        <div class="blog-widget widget-blog-categories mt-40 border p-3">
                            <ul class="widget-nav-list">
                                <li><a href="account/orderhistory.aspx">Order History</a></li>
                                <li><a href="account/cancelorder.aspx">Cancel Order</a></li>
                                <li><a href="account/leavefeedback.aspx">Leave Feedback</a></li>
                                <li><a href="account/leavecomplaint.aspx">Leave a Complaint</a></li>
                                <li><a href="account/addresses.aspx">Saved Addresses</a></li>
                                <li><a href="account/changepassword.aspx">Change Password</a></li>
                                <li><a href="../wishlist.aspx">Wishlist</a></li>
                                <li><asp:HyperLink ID="hlAddBlog" runat="server" NavigateUrl="~/account/addreview.aspx">Add Blog</asp:HyperLink>
                                <li><a href="../Login.aspx?type=lo">Log out</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-success" Visible="true"> </asp:Label>
                        <asp:Label ID="lblQtyMsg" runat="server" CssClass="alt-msg err" Visible="false"></asp:Label>
                      <div class="table-content table-responsive cart-table">
                            <asp:Label ID="lblOrderCancel" runat="server"></asp:Label>
                            <table>
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Product</th>
                                        <th>Product Name</th>
                                        <th>Unit Price</th>
                                        <th>Stock Status</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                  <asp:Repeater ID="rptWishList" runat="server" OnItemCommand="rptWishList_ItemCommand" OnItemDataBound="rptLatestProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="product-remove">
                                                    <asp:LinkButton ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("product_id") %>'
                                                         UseSubmitBehavior="false" ToolTip="Remove Item"><i class="icon-cross2"></i></asp:LinkButton></td>
                                                <td class="product-img">
                                                    <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                    DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                    DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'>
                                                        <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' Width="100"/>
                                                    </a>
                                                </td>
                                                <td class="product-stock-status w-50">
                                                    <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                    DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                    DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'><%# Eval("product_name")%></a></td>
                                                <td class="product-price w-25"><span class="amount">Rs. <%# Eval("sale_price")%></span></td>
                                                <td class="product-stock-status w-50">
                                                        <asp:HiddenField ID="hfieldLatestProduct" runat="server" Value='<%#Eval("in_stock")%>'></asp:HiddenField>
                                                     <asp:Label ID="lblStock" runat="server" CssClass="text-red"></asp:Label>
                                                </td>
                                                <td class="product-wishlist-cart w-50">
                                                    <asp:Button ID="btnAddToCart" runat="server" Text="Add To Cart" CssClass="btn--lg btn--black font-weight--reguler text-white" CommandName="addtocard" CommandArgument='<%# Eval("product_id") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     </tbody>
                            </table>

                            
                        </div>
                        <%--<div class="">
                            <table>
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th></th>
                                        <th>Product</th>
                                        <th>Unit Price</th>
                                        <th>Stock Status</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptWishList" runat="server" OnItemCommand="rptWishList_ItemCommand" OnItemDataBound="rptLatestProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="product-remove">
                                                    <asp:LinkButton ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("product_id") %>'
                                                         UseSubmitBehavior="false" ToolTip="Remove Item"><i class="icon-cross2"></i></asp:LinkButton></td>
                                                <td class="product-img">
                                                    <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                    DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                    DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'>
                                                        <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>' Width="100"/>
                                                    </a>
                                                </td>
                                                <td class="product-stock-status w-50">
                                                    <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                    DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                    DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'><%# Eval("product_name")%></a></td>
                                                <td class="product-price w-25"><span class="amount">Rs. <%# Eval("sale_price")%></span></td>
                                                <td class="product-stock-status w-50">
                                                        <asp:HiddenField ID="hfieldLatestProduct" runat="server" Value='<%#Eval("in_stock")%>'></asp:HiddenField>
                                                     <asp:Label ID="lblStock" runat="server" CssClass="text-red"></asp:Label>
                                                </td>
                                                <td class="product-wishlist-cart w-50">
                                                    <asp:Button ID="btnAddToCart" runat="server" Text="Add To Cart" CssClass="btn--lg btn--black font-weight--reguler text-white" CommandName="addtocard" CommandArgument='<%# Eval("product_id") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
        <!-- wishlist end -->
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
    <div style="display:inline;">
    <img height="1" width="1" style="border-style:none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0"/>
    </div>
    </noscript>
</asp:Content>
