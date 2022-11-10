<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="sitemap.aspx.cs" Inherits="strutt.sitemap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Site Map</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Site Map</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
       <div class="site-wrapper-reveal border-bottom">
  <div class="about-us-pages-area">
    <div class="banner-video-area overflow-hidden section-space--pt_90">
      
    <div class="footer-area-wrapper">
        <div class="footer-area section-space--ptb_120">
            <div class="container">
                <div class="row footer-widget-wrapper">
                    <div class="col-lg-4 col-md-4 col-sm-6 footer-widget">
                        <h6 class="footer-widget__title mb-20">All Bags</h6>
                        <ul class="footer-widget__list">
                            <li><a href="/bags">Bags
                                        <asp:Repeater ID="rptMenu1" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                            </a>
                            </li>
                            <li><a href="/travel-essentials">TRAVEL&nbsp;ESSENTIALS
                                        <br />
                                <u>"PROJECT SAFETY"</u>
                                <asp:Repeater ID="rptMenu2" runat="server">
                                    <ItemTemplate>
                                        <div class="sub-category">
                                            <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </a></li>
                            <li><a href="/limited-edition-series">LIMITED EDITION SERIES
                                        <asp:Repeater ID="rptMenu5" runat="server">
                                            <ItemTemplate>
                                                <div class="sub-category">
                                                    <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                            </a></li>
                            <li><a href="/the-voyager-series">THE VOYAGER SERIES<asp:Repeater ID="rptMenu6" runat="server">
                                <ItemTemplate>
                                    <div class="sub-category">
                                        <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            </a></li>
                            <li><a href="/accessories">ACCESSORIES<asp:Repeater ID="rptMenu3" runat="server">
                                <ItemTemplate>
                                    <div class="sub-category">
                                        <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            </a></li>
                            <li><a href="/when-we-collaborate">WHEN WE COLLABORATE<asp:Repeater ID="rptMenu4" runat="server">
                                <ItemTemplate>
                                    <div class="sub-category">
                                        <a href='<%# "../../../"+Eval("menu_url")+"/"+Eval("sub_menu_url") %>' class='<%# Convert.ToBoolean(Eval("is_new"))? "link new ":"link " %>'><%#Eval("sub_menu_name")%></a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            </a></li>
                        </ul>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 footer-widget">
                        <h6 class="footer-widget__title mb-20">MY ACCOUNT</h6>
                        <ul class="footer-widget__list">
                            <li><a href="account/orderhistory.aspx">Order History</a></li>
                            <li><a href="account/orderstatus.aspx">Order Status </a></li>
                            <li><a href="account/cancelorder.aspx">Cancel Order</a></li>
                            <li><a href="account/leavefeedback.aspx">Leave Feedback</a></li>
                            <li><a href="account/leavecomplaint.aspx">Leave Complaint</a></li>
                            <li><a href="account/addresses.aspx">Addresses</a></li>
                            <li><a href="account/changepassword.aspx">Change Password</a></li>
                            <li><a href="../wishlist.aspx">Wishlist</a></li>
                        </ul>
                    </div>
                   <div class="col-lg-4 col-md-4 col-sm-6 footer-widget">
                        <h6 class="footer-widget__title mb-20">Other Pages</h6>
                        <ul class="footer-widget__list">
                            <li><a href="../sales">Sale</a> </li>
                                <li><a href="../bestseller">Best Seller</a> </li>
                                <li><a href="../exclusive">Exclusive</a> </li>
                                <li><a href="about-us.aspx">About Us</a> </li>
                                <li><a href="contact-us.aspx">Contact Us</a></li>
                                <li><a href="review.aspx">Places</a></li>
                                <li><a href="blog.aspx">Blog</a></li>
                                <li><a href="privacy-policy.aspx">Privacy Policy</a></li>
                                <li><a href="terms-conditions.aspx">Terms Conditions</a></li>
                                <li><a href="return-policy.aspx">Return Policy</a></li>
                        </ul>
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
