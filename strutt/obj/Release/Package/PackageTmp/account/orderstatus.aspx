<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="orderstatus.aspx.cs" Inherits="strutt.account.orderstatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Leave Complaint</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="../default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Leave Complaint</li>
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
                                <li><a href="orderhistory.aspx">Order History</a></li>
                                <li><a href="cancelorder.aspx">Cancel Order</a></li>
                                <li><a href="leavefeedback.aspx">Leave Feedback</a></li>
                                <li><a href="leavecomplaint.aspx">Leave a Complaint</a></li>
                                <li><a href="addresses.aspx">Saved Addresses</a></li>
                                <li><a href="changepassword.aspx">Change Password</a></li>
                                <li><a href="../wishlist.aspx">Wishlist</a></li>
                                 <li><asp:HyperLink ID="hlAddBlog" runat="server" NavigateUrl="~/account/addreview.aspx">Add Blog</asp:HyperLink>
                                <li><a href="../Login.aspx?type=lo">Log out</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <h2><strong>Order Status</strong></h2>
                        <table style="margin-bottom: 10px; width: 100%;">
                            <tr>
                                <td style="height: 20px; width: 45%; padding: 5px 7px; text-align: left; vertical-align: top;">Order ID:<b style="color: #8e471e; font-size: 14px;"> STR10001<asp:Label ID="lblOrdId" runat="server"></asp:Label>
                                </b>
                                </td>
                                <td style="height: 20px; width: 45%; text-align: center; padding: 5px 7px; vertical-align: top;">Order Placed on: <strong style="color: #683415; font-weight: normal;">
                                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label></strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="table table-bordered shop_table">
                                        <tr>
                                            <td class="text-center">Order Status
                                            </td>
                                            <td class="text-center">Status Date
                                            </td>
                                        </tr>
                                        <tr id="trconfirm" runat="server" visible="false">
                                            <td class="text-center">
                                                <strong style="color: #683415; font-weight: normal;">
                                                    <asp:Label ID="lblConfirm" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                <strong style="color: #683415; font-weight: normal;">
                                                    <asp:Label ID="lblConfirmDate" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr id="trpacked" runat="server" visible="false">
                                            <td class="text-center">
                                                <strong style="color: #683415; font-weight: normal;">
                                                    <asp:Label ID="lblPacked" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                <strong style="color: #683415; font-weight: normal;">
                                                    <asp:Label ID="lblPacketDate" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr id="trdispatch" runat="server" visible="false">
                                            <td class="text-center">
                                                <strong style="color: #683415; font-weight: normal;">
                                                    <asp:Label ID="lblDispatch" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                <strong style="color: #683415; font-weight: normal;">
                                                    <asp:Label ID="lblDispatchDate" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr id="trdeliver" runat="server" visible="false">
                                            <td class="text-center">
                                                <strong style="color: Green; font-weight: normal;">
                                                    <asp:Label ID="lblDelivered" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                <strong style="color: Green; font-weight: normal;">
                                                    <asp:Label ID="lblDeliveredDate" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr id="trcancel" runat="server" visible="false">
                                            <td class="text-center">
                                                <strong style="color: Red; font-weight: normal;">
                                                    <asp:Label ID="lblCancel" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                <strong style="color: Red; font-weight: normal;">
                                                    <asp:Label ID="lblCancelDate" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right;">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary"
                                        OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
