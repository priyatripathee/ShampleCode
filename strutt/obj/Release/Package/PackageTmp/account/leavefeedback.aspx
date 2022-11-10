<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="leavefeedback.aspx.cs" Inherits="strutt.account.leavefeedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
            padding-top: 4px;
        }


        .text {
            overflow: hidden;
            text-overflow: ellipsis;
            display: -webkit-box;
            line-height: 16px; /* fallback */
            max-height: 52px; /* fallback */
            -webkit-line-clamp: 2; /* number of lines to show */
            -webkit-box-orient: vertical;
            -o-text-overflow: ellipsis;
            white-space: nowrap;
        }

        table.form tr td {
            width: auto !important;
            line-height: 33px;
        }

        .form-controlnew {
            width: 100%;
            border-radius: 2px;
            background-color: #fff;
            border: 1px solid #e4e4e4;
        }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>
    <script>
        var $d = jQuery.noConflict();
        $d(document).ready(function () {

            $d("#txtOrderDate").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Leave FeedBack</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="../default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Leave FeedBack</li>
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
                       <h2><strong>Leave FeedBack</strong></h2>
                          <asp:Label ID="lblMsg" runat="server" CssClass="msg1" Visible="false" ForeColor="Red"></asp:Label>
                         <table class="form" style="width: 70%; margin-left: 20px;">
                                <tr>
                                    <td>Order No: <span class="text-red"> *</span><span style="float: right;">STR10001</span> </td>
                                    <td>
                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-controlnew" placeholder="Order No"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validators" ForeColor="Red" Display="Dynamic"
                                            ControlToValidate="txtOrderNo" runat="server" ValidationGroup="checkOrder" ErrorMessage="Please enter Order No."></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="checkOrder" ForeColor="Red" Display="Dynamic"
                                            CssClass="Validators" ControlToValidate="txtOrderNo" ErrorMessage="Please enter in numeric only." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_Search" runat="server" CssClass="btn btn-dark" Text="Search" OnClick="Btn_Search_Click" ValidationGroup="checkOrder" Style="line-height: 1.35 !important" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Product Name:</td>
                                    </br>
                        <td>
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-controlnew" Enabled="false"></asp:TextBox>
                        </td>
                                    <td>Order Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-controlnew" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Star Rating:</td>
                                    <td>
                                        <cc1:Rating ID="ratingControl" AutoPostBack="true" runat="server" CssClass="Rating" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved"
                                            EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                        </cc1:Rating>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Order Delivered Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtDeliveredDate" runat="server" CssClass="form-controlnew" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>Item Arrived By: <span class="text-red"> *</span></td>
                                    <td>
                                        <asp:RadioButtonList ID="rbtnListArrived" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvListArrived"  ForeColor="Red" Display="Dynamic"
                                            ControlToValidate="rbtnListArrived" runat="server" ValidationGroup="review" ErrorMessage="Please select Item Arrived By." />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Item As Described: <span class="text-red"> *</span></td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="rbtnListDescribed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvListDescribed"  ForeColor="Red" Display="Dynamic"
                                            ControlToValidate="rbtnListDescribed" runat="server" ValidationGroup="review" ErrorMessage="Please select Item As Described." />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Departure on Time: <span class="text-red"> *</span></td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="rbtnTime" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvListTime"  ForeColor="Red" Display="Dynamic"
                                            ControlToValidate="rbtnTime" runat="server" ValidationGroup="review" ErrorMessage="Please select Departure on Time." />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Comment:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtMessage" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-controlnew"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Btn_LeaveFeedback" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" Text="Submit" ValidationGroup="review" OnClick="Btn_LeaveFeedback_Click" />
                                    </td>
                                </tr>
                            </table>
                         <asp:Label ID="lblValMes" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
