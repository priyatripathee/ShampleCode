<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="strutt.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <meta name="google-signin-client_id" content="299776664174-93q3859fbfvqv6eis18hsuuu834e0ln8.apps.googleusercontent.com">
    <meta name="google-signin-scope" content="https://www.googleapis.com/auth/analytics.readonly">
     <script type="text/javascript">        //Google Authentication
        function googleLogin() {
            gapi.load('auth2', function () {
                var auth2 = gapi.auth2.getAuthInstance();
                auth2.signIn().then(function () {
                    //console.log(auth2.currentUser.get().getId());
                    var profile = auth2.currentUser.get().getBasicProfile();
                    document.getElementById('<%=hfLoginType.ClientID %>').value = "google";
                    document.getElementById('<%=hffbid.ClientID %>').value = profile.getId();
                    document.getElementById('<%=hfpersonaName.ClientID %>').value = profile.getName();
                    document.getElementById('<%=hfreceiveremail.ClientID %>').value = profile.getEmail();
                   document.getElementById('<%=btnFbGoogle.ClientID%>').click();
                    document.getElementById('lblLoginName').value = "Welcome, " + profile.getEmail();
                });
            });
        }
    </script>
    <script type="text/javascript">        //
        function identifyNewuser() {
            wigzo("identify", { email: document.getElementById('<%=txtsignoutEmail.ClientID %>').value, phone: document.getElementById('<%=txtsignoutMobile.ClientID %>').value, fullName: document.getElementById('<%=txtsignoutUserName.ClientID %>').value });
        }

    </script>
    <script type="text/javascript">    //Facebook Authentication
        window.fbAsyncInit = function () {
            FB.init({
                //appId: '739222963134104',
                appId: '902327343653081',
                cookie: true,
                xfbml: true,
                version: 'v3.0'
            });
            FB.AppEvents.logPageView();
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "https://connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));

        function fblogin() {
            FB.getLoginStatus(function (response) {
                if (response.status === 'connected') {
                    console.log('Logged in.');
                    FB.api('/me?fields=name,id,email', function (me) {
                        if (me.name) {
                            document.getElementById('<%=hfLoginType.ClientID %>').value = "fb";
                            document.getElementById('<%=hffbid.ClientID %>').value = me.id;
                            document.getElementById('<%=hfpersonaName.ClientID %>').value = me.name;
                            document.getElementById('<%=hfreceiveremail.ClientID %>').value = me.email;
                            document.getElementById('<%=btnFbGoogle.ClientID%>').click();
                            document.getElementById('lblLoginName').value = "Welcome, " + me.email;
                        }
                    })
                }
                else {
                    FB.login(function (response) {
                    }, { scope: 'public_profile,email' });
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <asp:HiddenField ID="hfLoginType" runat="server" />
    <asp:HiddenField ID="hffbid" runat="server" />
    <asp:HiddenField ID="hfpersonaName" runat="server" />
    <asp:HiddenField ID="hfpersonamobile" runat="server" />
    <asp:HiddenField ID="hfreceiveremail" runat="server" />
    
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">My Account</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                          
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">My Account</li>
                            </ul>
                         
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <div class="my-account-page-warpper section-space--ptb_120">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6 col-md-7 ml-auto mr-auto">
                        <div class="myaccount-box-wrapper">
                            <div class="helendo-tabs">
                                <ul class="nav" role="tablist">
                                    <li class="tab__item nav-item active">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_list_06" role="tab">Sign-In</a>
                                    </li>
                                    <li class="tab__item nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_list_07" role="tab">Sign-Up</a>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-content content-modal-box">
                                <div class="tab-pane fade show active" id="tab_list_06" role="tabpanel">
                                        <div class="ml-auto mr-auto text-center" >
                                            <fb:login-button scope="public_profile,email" onlogin="fblogin()" data-max-rows="3" data-size="large" data-button-type="continue_with"></fb:login-button>
                                        </div>
                                    <br />
                                        <div class="ml-auto mr-auto text-center">
                                            <div class="g-signin2" onclick="googleLogin()" data-width="255" data-height="40" data-longtitle="true" data-theme="dark" style="display: table;"></div>
                                            <asp:Button ID="btnFbGoogle" runat="server" Text="Proceed" ValidationGroup="facebook" class="hidden" OnClick="btnFbGoogle_Click" Height="0" Width="0" CssClass="p-0 border-0"/>
                                        </div>
                                    <asp:Panel runat="server" ID="Defult" DefaultButton="btnLogin">
                                        <div class="account-form-box">
                                            <asp:Label ID="lblLoginMsg" runat="server" ForeColor="Red" CssClass="ValidatorsMsg"></asp:Label>
                                            <h6>Login your account</h6>
                                            <div class="single-input">
                                                <asp:TextBox ID="txtLoginEmail" runat="server" placeholder="Enter your email address"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="txtLoginEmail"
                                                    runat="server" Display="Dynamic" ValidationGroup="log" SetFocusOnError="true"
                                                    ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic"
                                                    ForeColor="Red" runat="server" SetFocusOnError="true" ErrorMessage="Invalid Email"
                                                    ValidationGroup="log" ControlToValidate="txtLoginEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                            <div class="single-input">
                                                <asp:TextBox ID="txtLoginPwd" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVTlogepass" runat="server" ForeColor="Red"
                                                    SetFocusOnError="true" ErrorMessage="Password is required" Display="Dynamic"
                                                    ControlToValidate="txtLoginPwd" ValidationGroup="log"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                    ControlToValidate="txtLoginPwd" ID="RegularExpressionValidator8" ValidationExpression="^[\s\S]{6,}$"
                                                    runat="server" ValidationGroup="log" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="checkbox-wrap mt-10">
                                                <%--<asp:LinkButton ID="lbtnForgotPassword" runat="server" OnClick="lbtnForgotPassword_Click"
                                                    class="mt-10" CausesValidation="false">Lost your password?</asp:LinkButton>--%>
                                                 <asp:LinkButton ID="lbtnForgotPassword" runat="server" PostBackUrl="~/forgotpassword.aspx" 
                                                     CausesValidation="false">Reset Password</asp:LinkButton>
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </div>
                                            <div class="button-box mt-25">
                                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" class="btn btn--full btn--black b-login" ValidationGroup="log"/>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="tab-pane fade" id="tab_list_07" role="tabpanel">
                                    <input type="hidden" name="_token" value="GhHgeiFnOYNSsfNgZhJlktvtObijpOiWWq52qzTm" />
                                    <asp:Panel runat="server" ID="Register" DefaultButton="btnSignUp">
                                        <div class="account-form-box">
                                            <h6>Register An Account</h6>
                                            <div class="single-input">
                                                <asp:TextBox ID="txtsignoutUserName" runat="server" placeholder="Enter your First Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="txtsignoutUserName" runat="server" ValidationGroup="signup"
                                                    ErrorMessage="First Name is required."></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="single-input">
                                                <asp:TextBox ID="txtsignoutMobile" runat="server" placeholder="Enter your Mobile number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic"
                                                    ControlToValidate="txtsignoutMobile" runat="server" ValidationGroup="signup"
                                                    ErrorMessage="Mobile Number is required."></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="signup"
                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtsignoutMobile"
                                                    ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="single-input">
                                                <asp:TextBox ID="txtsignoutEmail" runat="server" placeholder="Enter your email Address"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic"
                                                    ControlToValidate="txtsignoutEmail" runat="server" SetFocusOnError="true" ValidationGroup="signup"
                                                    ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                    ErrorMessage="Invalid Email Address" SetFocusOnError="true" ValidationGroup="signup"
                                                    ControlToValidate="txtsignoutEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                            <div class="single-input">
                                                <asp:TextBox ID="txtsignoutPassword" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic"
                                                    ControlToValidate="txtsignoutPassword" runat="server" ValidationGroup="signup"
                                                    ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtsignoutPassword"
                                                    ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{6,}$" runat="server"
                                                    ValidationGroup="signup" ForeColor="Red" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="button-box mt-25">
                                                <asp:Button ID="btnSignUp" runat="server" Text="Register" class="btn btn--full btn--black b-login"
                                                    ValidationGroup="signup" OnClientClick="identifyNewuser();" OnClick="btnSignUp_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>
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
