<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="changepassword.aspx.cs" Inherits="strutt.account.changepassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Change Password</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="../default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Change Password</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
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
                        <asp:UpdatePanel ID="UPnlBanner" runat="server">
                            <ContentTemplate>
                                <div class="">
                                    <div class="order-tracking-form-box">
                                        <h2><strong>CHANGE PASSWORD</strong></h2>
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="ValidatorsMsg"></asp:Label>
                                        <div class="billing-info mb-25">
                                            <label>Old Password</label>
                                            <asp:TextBox ID="txtoldpassword" runat="server" Placeholder="Old Password" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVName" runat="server" ErrorMessage="Please enter old password" Display="Dynamic"
                                                ControlToValidate="txtoldpassword" CssClass="text-red" ValidationGroup="changeP"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="billing-info mb-25">
                                            <label>New Password</label>
                                            <asp:TextBox ID="txtnewpassword" runat="server" Placeholder="New Password" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter new password" Display="Dynamic"
                                                ControlToValidate="txtnewpassword" CssClass="text-red" ValidationGroup="changeP"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" CssClass="text-red"
                                                ControlToValidate="txtnewpassword" ID="RegularExpressionValidator8" ValidationExpression="^[\s\S]{6,}$"
                                                runat="server" ValidationGroup="changeP" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="billing-info mb-25">
                                             <label>Confirm Password</label>
                                            <asp:TextBox ID="txtconformpassword" runat="server" Placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter confirm password" Display="Dynamic"
                                                ControlToValidate="txtconformpassword" CssClass="text-red" ValidationGroup="changeP"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CPVConPass" runat="server" ErrorMessage="New Password and confirm new password should be same !!"
                                                ControlToCompare="txtnewpassword" ValidationGroup="prct" Display="Dynamic" CssClass="text-red"
                                                SetFocusOnError="true" ControlToValidate="txtconformpassword" Operator="Equal"></asp:CompareValidator>
                                        </div>
                                        <div class="button-box mt-25">
                                            <asp:LinkButton ID="lbtnSubmit" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" ValidationGroup="changeP" OnClick="lbtnSubmit_Click">Submit</asp:LinkButton>
                                        </div>
                                        </>
                                    </div>
                                    <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UPnlBanner">
                                        <ProgressTemplate>
                                            <img src="../images/loading.gif" title="Please wait.." alt="Please wait.." />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
