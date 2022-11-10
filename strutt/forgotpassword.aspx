<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="forgotpassword.aspx.cs" Inherits="strutt.forgotpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Forgot Password</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Forgot Password</li>
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
                        <asp:Label ID="lblLoginMsg" runat="server"></asp:Label>
                        <div class="myaccount-box-wrapper">
                            <div class="order-tracking-form-box">
                                <div class="billing-info mb-25">
                                    <label>Email / Username</label>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                        SetFocusOnError="True" ErrorMessage="Please enter Email." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="reEmail" Display="Dynamic" ForeColor="Red"
                                        runat="server" SetFocusOnError="true" ErrorMessage="Invalid Email"
                                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="button-box mt-25">
                                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn--lg btn--black" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                    
                                    <asp:LinkButton ID="btnLogin" runat="server" Visible="false" CssClass="btn btn--lg btn--black pull-right" PostBackUrl="~/login.aspx" CausesValidation="false">Login</asp:LinkButton>
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
