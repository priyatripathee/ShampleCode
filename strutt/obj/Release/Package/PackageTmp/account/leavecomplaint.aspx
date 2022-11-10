<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="leavecomplaint.aspx.cs" Inherits="strutt.account.leavecomplaint" %>

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
                    <div class="col-lg-6">
                        <div class="myaccount-box-wrapper">
                              <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
                            <div class="order-tracking-form-box">
                                <h2><strong>Leave Complaint</strong></h2>
                                <div class="billing-info mb-25">
                                    <label>Name <span class="text-red"> *</span></label>
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVName" runat="server" ErrorMessage="Please enter name."
                                        ControlToValidate="txtName" ForeColor="Red" ValidationGroup="raise" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="billing-info mb-25">
                                    <label>Phone No <span class="text-red"> *</span></label>
                                    <asp:TextBox ID="txtMobileNo" runat="server" placeholder="Phone No"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Phone no."
                                        ControlToValidate="txtMobileNo" ForeColor="Red" ValidationGroup="raise" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator3" runat="server" ValidationGroup="raise" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtMobileNo" ErrorMessage="Please enter 10 digits."
                                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                </div>
                                <div class="billing-info mb-25">
                                    <label>Email <span class="text-red"> *</span></label>
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter email."
                                        ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="raise" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic"
                                        ForeColor="Red" runat="server" SetFocusOnError="true" ErrorMessage="Invalid email address."
                                        ValidationGroup="raise" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="billing-info mb-25">
                                    <label>Order Id <span class="text-red"> *</span></label>
                                    <asp:TextBox ID="txtComplainType" runat="server" placeholder="Order Id"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter order id."
                                        ControlToValidate="txtComplainType" ForeColor="Red" ValidationGroup="raise" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="comment-form-comment">
                                    <asp:TextBox ID="txtQuery" placeholder="Details" TextMode="MultiLine" runat="server" CssClass="comment-notes"></asp:TextBox>
                                </div>
                                <div class="button-box mt-25">
                                    <asp:LinkButton ID="lbtnSubmit" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" OnClick="lbtnSubmit_Click" ValidationGroup="raise">Submit</asp:LinkButton>
                                  
                                </div>
                                <div>
                                    You can also write to us at connect@thestruttstore.com
                            <br />
                                    or call us at no : 8800400570
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
