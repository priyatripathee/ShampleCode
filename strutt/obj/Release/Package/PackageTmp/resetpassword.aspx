<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="resetpassword.aspx.cs" Inherits="strutt.resetpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Reset Password</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Reset Password</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <div class="order-tracking-page-warpper section-space--ptb_120">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6 col-md-9 ml-auto mr-auto">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
                        <div class="myaccount-box-wrapper">
                            <div class="order-tracking-form-box">
                                <div class="billing-info mb-25">
                                    <label>New Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="True" ErrorMessage="Please enter New Password."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" CssClass="text-red"
                                        ControlToValidate="txtPassword" ID="rfvPassword1" ValidationExpression="^[\s\S]{6,}$"
                                        runat="server" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                                </div>
                                <div class="billing-info mb-25">
                                    <label>Confirm Password</label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="True" ErrorMessage="Please enter Confirm Password."></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CPVConPass" runat="server" ErrorMessage="New Password and confirm new password should be same !!"
                                        ControlToCompare="txtPassword" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="true" ControlToValidate="txtConfirmPassword" Operator="Equal"></asp:CompareValidator>
                                </div>
                                <div class="button-box mt-25">
                                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn--lg btn--black pull-left" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn btn--lg btn--black pull-right" PostBackUrl="~/login.aspx" CausesValidation="false">Login</asp:LinkButton>
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
</asp:Content>
