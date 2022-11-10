<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="account.aspx.cs" Inherits="strutt.account.account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/normalize.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="container">
        <ul id="breadcrumb" class="margin-t-20" itemscope itemtype="http://schema.org/BreadcrumbList">
            <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a class="link" itemprop="item" href="../default.aspx"><span itemprop="name">Home</span></a>
                <meta itemprop="position" content="1" />
            </li>
            <span>›</span>
            <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">
                <span itemprop="name">Account</span>
                <meta itemprop="position" content="2" />
            </li>
        </ul>
        <div class="margin-t-50">
            <div class="flex gutter">
                <div class="flex gutter">
                    <div id="block-details" class="col-12-12 col-sm-4-12 col-md-3-12">
                        <div class="bd bg-grey-light padding-25">
                            <div class="flex gutter-sm">
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="account/orderhistory.aspx">Order History</a>
                                </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="account/cancelorder.aspx">Cancel Order</a>
                                </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="account/leavefeedback.aspx">Leave Feedback</a>
                                </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="account/leavecomplaint.aspx">Leave a Complaint</a>
                                </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="account/addresses.aspx">Saved Addresses</a>
                                </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="account/changepassword.aspx">Change Password</a>
                                </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="../wishlist.aspx">Wishlist</a>
                                </div>
                                 <div class="col-sm-12-12 col-xs-4-12">
                                <a href="addreview.aspx">Add Blog</a>
                            </div>
                                <div class="col-sm-12-12 col-xs-4-12">
                                    <a href="../
                                        
                                        
                                        Login.aspx?type=lo">Log out</a>
                                </div>
                            </div>
                        </div>
                    </div>



                </div>
            </div>
        </div>
    </div>
</asp:Content>
