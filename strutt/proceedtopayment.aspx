<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="proceedtopayment.aspx.cs" Inherits="strutt.proceedtopayment" %>

<%@ Import Namespace="BLL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/new29621.css" rel="stylesheet" />
    <%--Google Login realted--%>
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <meta name="google-signin-client_id" content="299776664174-93q3859fbfvqv6eis18hsuuu834e0ln8.apps.googleusercontent.com">
    <meta name="google-signin-scope" content="https://www.googleapis.com/auth/analytics.readonly">
    <style>
        .panel-h {
            padding: .75rem 1.25rem;
            margin-bottom: 0;
            background-color: rgba(0, 0, 0, .03);
        }

        .razorpay-payment-button {
            display: none;
        }

        .text-orange {
            color: orange;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            /// Hide Header/footer  chandni 6-April-2021
            //$('#hideHeader').hide();
            $('#hideFooter').hide();
        });
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
$(document).ready(function(){
    $(".aLogin").click(function () {
        $("#dLogin").toggle("slow");
    });
    $(".acCode").click(function () {
        $("#dcCode").toggle("slow");
    });
});
</script>
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
    <%-- <div class="modal-backdrop"></div>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>

    <div id="jas-content">
        <asp:Label ID="lblCheckMsg" runat="server" CssClass="text-red" Visible="false"></asp:Label>
        <asp:Label ID="lbllogin" runat="server" ForeColor="Red" CssClass="text-red"></asp:Label>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

        <div class="page-head pr tc" style="background: url(images/banner_checkout.jpg) no-repeat center center / cover;">
            <div class="jas-container pr">
                <h1 class="tu mb__10 cw" itemprop="headline">Checkout</h1>
                <p></p>
                <ul class="jas-breadcrumb dib oh">
                    <li class="fl home"><a href="/cart.aspx" title="Cart">Cart</a></li>
                    <li class="fl separator"><i class="fa fa-angle-right"></i></li>
                    <li class="fl current">Information</li>
                    <li class="fl separator"><i class="fa fa-angle-right"></i></li>
                    <li class="fl current">Shipping</li>
                    <li class="fl separator"><i class="fa fa-angle-right"></i></li>
                    <li class="fl current">Payment</li>
                </ul>
            </div>
        </div>
        <div class="jas-container">
            <div class="jas-row jas-page">
                <div class="jas-col-md-12 jas-col-xs-12 mt__60 mb__60" role="main" itemscope="itemscope">
                    <div class="woocommerce">
                        <div class="woocommerce-notices-wrapper"></div>
                        <div class="woocommerce-form-login-toggle">

                            <div class="woocommerce-info">
                              <div id="divLogin" runat="server" style="display:none;">Returning customer? 
                                  <%--<a href="login.aspx?url=proceedtopayment.aspx" class="showlogin">Click here to login</a>--%>
                                  <a class="aLogin" style="color:#b59677">Click here to login</a>
                              </div>

                                <asp:Label ID="lblhead1" runat="server" CssClass="h6"></asp:Label>
                                <asp:LinkButton ID="btnChangeUserDetails" type="button" runat="server" class="btn btn-sm btn-edit fr change-btn pull-right p-0" OnClick="btnChangeUserDetails_Click">Change</asp:LinkButton>
                            </div>
                        </div>
                        <div id="dLogin" class="woocommerce-form woocommerce-form-login login" style="display: none;">


                            <p>If you have shopped with us before, please enter your details below. If you are a new customer, please proceed to the Billing section.</p>

                            <p class="form-row form-row-first">
                                <label for="username">Username or email&nbsp;<span class="required">*</span></label>
                                <asp:TextBox ID="txtLoginEmail" CssClass="input-text" runat="server" placeholder="Enter your email address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="txtLoginEmail"
                                    runat="server" Display="Dynamic" ValidationGroup="log" SetFocusOnError="true"
                                    ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic"
                                    ForeColor="Red" runat="server" SetFocusOnError="true" ErrorMessage="Invalid Email"
                                    ValidationGroup="log" ControlToValidate="txtLoginEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                </asp:RegularExpressionValidator>

                            </p>
                            <p class="form-row form-row-last">
                                <label for="password">Password&nbsp;<span class="required">*</span></label>
                                <asp:TextBox ID="txtLoginPwd" runat="server" CssClass="input-text" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVTlogepass" runat="server" ForeColor="Red"
                                                    SetFocusOnError="true" ErrorMessage="Password is required" Display="Dynamic"
                                                    ControlToValidate="txtLoginPwd" ValidationGroup="log"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                    ControlToValidate="txtLoginPwd" ID="RegularExpressionValidator8" ValidationExpression="^[\s\S]{6,}$"
                                                    runat="server" ValidationGroup="log" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                            </p>
                            <div class="clear"></div>
                            <p class="form-row-btn">
                               <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" class="woocommerce-button button woocommerce-form-login__submit" ValidationGroup="log"/>
                                </p>
                            <div class="clear"></div>
                            <p class="form-row-btn">
                                &nbsp;<fb:login-button scope="public_profile,email" onlogin="fblogin()" data-max-rows="3" data-size="large" data-button-type="continue_with"></fb:login-button>
                                </p>
                            
                            <p class="form-row-btn">
                                <asp:Panel ID="pbleGoogle" runat="server" Visible="false">
                                    &nbsp;<div class="g-signin2" onclick="googleLogin()" data-width="255" data-height="40" data-longtitle="true" data-theme="dark" style="display: table;"></div>
                                &nbsp; <asp:Button ID="btnFbGoogle" runat="server" Text="Proceed" ValidationGroup="facebook" class="hidden" OnClick="btnFbGoogle_Click" Height="0" Width="0" CssClass="p-0 border-0"/>
                                    </asp:Panel>
                                </p>
                            
                            <%--<p class="form-row">--%>
                                <%--<label class="woocommerce-form__label woocommerce-form__label-for-checkbox woocommerce-form-login__rememberme">
                                    <input class="woocommerce-form__input woocommerce-form__input-checkbox" name="rememberme" type="checkbox" id="rememberme" value="forever">
                                    <span>Remember me</span>
                                </label>--%>
                                <%--<button type="submit" class="woocommerce-button button woocommerce-form-login__submit" name="login" value="Login">Login</button>--%>
                                
                            <%--</p>--%>
                        </div>
                        <p style="font-size:12px;">
                            <asp:Label ID="lblLoginMsg" ForeColor="Red" runat="server"></asp:Label>
                        </p>
                        <div class="woocommerce-form-coupon-toggle" style="display: inline-block !important; width: 100%;">

                            <div class="woocommerce-info">
                                Have a coupon? <a class="acCode" style="color:#b59677">Click here to enter your code</a>
                            </div>
                        </div>

                        <div id="dcCode" class="checkout_coupon woocommerce-form-coupon" style="display: none;">

                            <p>If you have a coupon code, please apply it below.</p>

                            <p class="form-row form-row-first">
                                <asp:TextBox ID="txtCouponCode" runat="server" placeholder="Coupon code" CssClass="input-text"></asp:TextBox>
                            </p>

                            <p class="form-row form-row-last">
                                <asp:Button ID="lbtnApply" runat="server" OnClick="lbtnApply_Click" Text="Apply Coupon" CssClass="button" />
                            </p>
                            <p class="form-row form-row-first" style="color:#b59677; font-weight:bold">
                                Use Code : MONSOON15 (Get EXTRA 5% OFF on prepaid orders)
                            </p>
                            <p style="font-size:12px;">
                                <%--<asp:Label ID="lblMsgCoupon" runat="server"></asp:Label>--%>
                                <asp:Label ID="lblDis25" runat="server" CssClass="lblcolor"></asp:Label>
                                <asp:Label ID="lblDis20" runat="server" CssClass="lblcolor"></asp:Label>
                            </p>

                            <div class="clear"></div>
                        </div>
                        <p style="font-size:12px;">
                            <asp:Label ID="lblMsgCoupon" runat="server"></asp:Label>
                        </p>
                        <div class="woocommerce-notices-wrapper"></div>
                        <div id="collapseOne" runat="server" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                            <asp:Label ID="lblMsgGuest" runat="server" CssClass="text-danger"></asp:Label>
                            <div class="checkout woocommerce-checkout jas-row mt__60 mb__60">

                                <div class="jas-col-md-6 jas-col-sm-6 jas-col-xs-12">
                                    <div id="customer_details">
                                        <div class="woocommerce-billing-fields">

                                            <div class="woocommerce-billing-fields__field-wrapper">
                                                <p class="form-row form-row-wide" id="billing_company_field" data-priority="30">
                                                    <label for="billing_company" class="">Username or email &nbsp;<span class="optional">*</span></label>
                                                    <span class="woocommerce-input-wrapper">

                                                        <asp:TextBox ID="txtguestemail" runat="server" CssClass="input-text" placeholder="Username or email *" AutoPostBack="true" OnTextChanged="txtguestemail_OnTextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvguestemail" runat="server" ErrorMessage="Please enter your email id"
                                                            SetFocusOnError="true" ControlToValidate="txtguestemail" Display="Dynamic" ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="retxtguestemail" Display="Dynamic"
                                                            ControlToValidate="txtguestemail" Text="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            runat="server" ValidationGroup="receiver" ForeColor="Red" />


                                                        <asp:HiddenField ID="hfLoginType" runat="server" />
                                                        <asp:HiddenField ID="hffbid" runat="server" />
                                                        <asp:HiddenField ID="hfpersonaName" runat="server" />
                                                        <asp:HiddenField ID="hfpersonamobile" runat="server" />
                                                        <asp:HiddenField ID="hfreceiveremail" runat="server" />
                                                        <asp:HiddenField ID="productinfo" runat="server" Value="productinfo" />

                                                    </span>




                                                </p>
                                                <br />
                                            </div>

                                            <h3>Contact Details</h3>
                                            <div class="woocommerce-billing-fields__field-wrapper">
                                                <div class="your-order-wrappwer tablet-mt__60 small-mt__60" id="headReceiverAddress" runat="server">
                                                    <div class="your-order-area" style="background-color: white!important">
                                                        <div class="your-order-wrap">
                                                            <div class="your-order-info-wrap">
                                                                <div class="your-order">
                                                                    <div class="row">
                                                                        <asp:Repeater ID="repReceiverAddress" runat="server" OnItemCommand="repReceiverAddress_ItemCommand">
                                                                            <ItemTemplate>
                                                                                <div class="col-lg-4 col-md-4" style="border: 1px solid #e6dace; padding: 5px; background-color: #ffedf6;">
                                                                                    <div class="h6" style="font-size:11px; color:#000"><%# Eval("full_name")%> - <%# Eval("contact_number")%> </div>
                                                                                    <div style="font-size:11px; color:#000"><%# Eval("address")%>, <%# Eval("city")%>, <%# Eval("state")%> - <%# Eval("pin_code")%> </div>
                                                                                    <div style="text-align: center;margin-top: 15px;">
                                                                                        <asp:Button ID="btnUseAddress" runat="server" Text="Use this address" Font-Size="9px" style="padding:15px;" CommandName="useThisAddress" CssClass="btn--sm btn--grey btn-md text-center" CommandArgument='<%# Eval("customer_details_id") %>' />
                                                                                        <%--<asp:LinkButton ID="lnkAddressRemove" class="pull-right margin-t-5" runat="server" CommandName="Remove" CommandArgument='<%# Eval("customer_details_id") %>' ToolTip="Delete Address"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <h3>Billing Details</h3>
                                            <div class="woocommerce-billing-fields__field-wrapper">
                                                <p class="form-row form-row-first validate-required" id="billing_first_name_field" data-priority="10">
                                                    <label for="billing_first_name" class="">First name&nbsp;<abbr class="required" title="required">*</abbr></label>
                                                    <span class="woocommerce-input-wrapper">
                                                        <asp:TextBox ID="txtreceivername" runat="server" CssClass="input-text" placeholder="First name *"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please enter your name" CssClass="Validators"
                                                            SetFocusOnError="true" ControlToValidate="txtreceivername" Display="Dynamic" ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>


                                                    </span>
                                                </p>
                                                <p class="form-row form-row-last validate-required" id="billing_last_name_field" data-priority="20">
                                                    <label for="billing_last_name" class="">Last name&nbsp;<abbr class="required" title="required">*</abbr></label>
                                                    <span class="woocommerce-input-wrapper">
                                                        <asp:TextBox ID="txtreceiverlastname" runat="server" CssClass="input-text" placeholder="Last name *"></asp:TextBox>

                                                    </span>
                                                </p>

                                                <p class="form-row form-row-wide address-field update_totals_on_change validate-required" id="billing_country_field" data-priority="40">
                                                    <label for="billing_company" class="">Street address&nbsp;<span class="optional">(optional)</span></label>
                                                    <span class="woocommerce-input-wrapper">


                                                        <asp:TextBox ID="txtreceiveraddress" runat="server" class="input-text" placeholder="Street address *"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please enter your Street address"
                                                            SetFocusOnError="true" ControlToValidate="txtreceiveraddress" Display="Dynamic" CssClass="Validators"
                                                            ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>

                                                    </span>
                                                </p>


                                                <p class="form-row form-row-wide address-field update_totals_on_change validate-required" data-priority="40">
                                                    <label for="billing_company" class="">LandMark&nbsp;<span class="optional">(optional)</span></label>
                                                    <span class="woocommerce-input-wrapper">

                                                        <asp:TextBox ID="txtLandMark" runat="server" class="input-text" placeholder="LandMark"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter your address"
                                                            SetFocusOnError="true" ControlToValidate="txtreceiveraddress" Display="Dynamic" CssClass="Validators"
                                                            ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>
                                                    </span>
                                                </p>



                                                <p class="form-row form-row-wide address-field update_totals_on_change validate-required" data-priority="40">
                                                    <label for="billing_country" class="">State&nbsp;<abbr class="required" title="required">*</abbr></label>
                                                    <span class="woocommerce-input-wrapper">
                                                        <asp:DropDownList ID="ddlReceiverState" runat="server" AutoPostBack="true" DataTextField="state" CssClass="select-active"
                                                            DataValueField="state" OnSelectedIndexChanged="ddlReceiverState_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select state"
                                                            SetFocusOnError="true" ControlToValidate="ddlReceiverState" InitialValue="Select State" Display="Dynamic"
                                                            ValidationGroup="receiver" CssClass="Validators" ForeColor="Red"> </asp:RequiredFieldValidator></span>
                                                </p>
                                                <p class="form-row address-field validate-required form-row-wide" id="billing_address_1_field" data-priority="50">
                                                    <label for="billing_address_1" class="">
                                                        City&nbsp;<abbr class="required" title="required">*</abbr></label><span class="woocommerce-input-wrapper">

                                                            <asp:DropDownList ID="ddlReceiverCity" runat="server" DataTextField="city_name" CssClass="select-active"
                                                                DataValueField="city_name">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please select city"
                                                                SetFocusOnError="true" ControlToValidate="ddlReceiverCity" InitialValue="Select City" Display="Dynamic"
                                                                ValidationGroup="receiver" CssClass="Validators" ForeColor="Red"> </asp:RequiredFieldValidator>

                                                        </span>
                                                </p>




                                                <p class="form-row address-field form-row-wide" id="billing_address_2_field" data-priority="60">
                                                    <label for="billing_address_2" class="screen-reader-text">Postcode&nbsp;<span class="optional">(optional)</span></label>
                                                    <span class="woocommerce-input-wrapper">

                                                        <asp:TextBox ID="txtReceiverPinCode" runat="server" placeholder="Postcode / ZIP *" CssClass="input-text"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter pincode"
                                                            SetFocusOnError="true" ControlToValidate="txtReceiverPinCode" Display="Dynamic" ValidationGroup="receiver"
                                                            CssClass="Validators" ForeColor="Red"> </asp:RequiredFieldValidator>

                                                    </span>
                                                </p>
                                                <p class="form-row address-field validate-required form-row-wide" id="billing_city_field" data-priority="70" data-o_class="form-row form-row-wide address-field validate-required">
                                                    <label for="billing_city" class="">Phone&nbsp;<abbr class="required" title="required">*</abbr></label><span class="woocommerce-input-wrapper">
                                                        <asp:TextBox ID="txtreceiverphone" runat="server" placeholder="Phone *" CssClass="input-text"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please enter your phone"
                                                            SetFocusOnError="true" ControlToValidate="txtreceiverphone" ForeColor="Red" Display="Dynamic"
                                                            ValidationGroup="receiver" CssClass="Validators"> </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtreceiverphone"
                                                            ErrorMessage="Please enter 10 digits" Display="Dynamic" ValidationGroup="receiver" CssClass="Validators"
                                                            ValidationExpression="[0-9]{10}" ForeColor="Red"> </asp:RegularExpressionValidator>
                                                    </span>
                                                </p>
                                                
                                            </div>
                                            
                                        </div>
                                        
                                    </div>
                                    
                                </div>

                                <div class="jas-col-md-6 jas-col-sm-6 jas-col-xs-12">

                                    <h3 id="order_review_heading">Your order</h3>


                                    <div id="order_review" class="woocommerce-checkout-review-order">
                                        <asp:Label ID="lblCartCount" runat="server" CssClass="d-none"></asp:Label>
                                        <asp:Label ID="lblQtyCount" runat="server" CssClass="d-none"></asp:Label>
                                        <asp:Label ID="lblCustomBagCharge" runat="server" CssClass="d-none"></asp:Label>

                                        <table class="shop_table woocommerce-checkout-review-order-table">
                                            <thead>
                                                <tr>
                                                    <th class="product-name">Product</th>
                                                    <th class="product-total">Total</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater ID="dlCart" runat="server">
                                                    <ItemTemplate>

                                                        <tr class="cart_item">
                                                            <td class="product-name">
                                                                <div class="item-thumb">
                                                                    <%--<img width="570" height="760" />--%>
                                                                    <asp:Image ID="a1" runat="server" ImageUrl='<%# Eval("thumb_image") %>' AlternateText='<%#Eval("product_name")%>' />
                                                                </div>
                                                                <div class="item-info">
                                                                    <%# Eval("product_name")%>&nbsp;<strong class="product-quantity">× <%# Eval("quantity")%></strong>
                                                                    <%--<dl class="variation">
			                        <dt class="variation-Size">Size:</dt>
		                            <dd class="variation-Size"><p>M</p></dd>
	                            </dl>--%>
                                                                </div>
                                                            </td>
                                                            <td class="product-total">
                                                                <span class="woocommerce-Price-amount amount">
                                                                    <bdi>
                                                                        <span class="woocommerce-Price-currencySymbol">Rs.</span><%# Eval("sale_price")%>

                                                                        <%--<%# Eval("quantity")+" x Rs."+(Convert.ToDouble(Eval("sale_price"))).ToString("0.00")%>--%>
                                                                    </bdi></span>						</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>



                                            </tbody>
                                            <tfoot>

                                                <tr class="cart-subtotal">
                                                    <th>Subtotal</th>
                                                    <td><span class="woocommerce-Price-amount amount">
                                                        <bdi><span class="woocommerce-Price-currencySymbol">Rs.</span><asp:Label ID="lblTotalPrice" runat="server">0.00</asp:Label></bdi></span></td>
                                                </tr>
                                                <tr class="cart-subtotal">
                                                    <th>Discount</th>
                                                    <td><span class="woocommerce-Price-amount amount">
                                                        <bdi>
                                                            <span class="woocommerce-Price-currencySymbol">Rs.</span>
                                                            <asp:Label ID="lblDiscount" runat="server">0.00</asp:Label></bdi></span></td>
                                                </tr>




                                                <tr class="woocommerce-shipping-totals shipping">
                                                    <th>Shipping</th>
                                                    <td data-title="Shipping">
                                                        <ul id="shipping_method" class="woocommerce-shipping-methods">
                                                            <li>

                                                                <label for="shipping_method_0_legacy_free_shipping">
                                                                    <asp:Label ID="lblShipping" runat="server">0.00</asp:Label></label>

                                                            </li>
                                                        </ul>


                                                    </td>
                                                </tr>






                                                <tr class="order-total">
                                                    <th>Total</th>
                                                    <td><strong><span class="woocommerce-Price-amount amount">
                                                        <bdi>
                                                            <span class="woocommerce-Price-currencySymbol">Rs.</span><asp:Label ID="lblGrandTotal" runat="server">0.00</asp:Label></bdi></span></strong> </td>
                                                </tr>


                                            </tfoot>
                                        </table>


                                        <div class="payment-area mt-30" style="display: none;">
                                            <div class="single-payment">
                                                <h6 class="mb-10 text-orange">Hey Guys!
                          <asp:Label ID="lblSale" runat="server"></asp:Label>
                                                </h6>
                                                <p class="text-orange">
                                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                                </p>

                                                <span class="text-orange">
                                                    <asp:Label ID="lblOffer" runat="server"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="single-payment mt-20">
                                                <p class="text-orange">Get an EXTRA 5% off on prepaid orders for a contactless transaction. We care for your safety.</p>
                                            </div>
                                        </div>

                                        <div id="payment" class="woocommerce-checkout-payment">
                                            <%--<ul class="wc_payment_methods payment_methods methods">
			<li class="wc_payment_method payment_method_cheque">
	<input id="payment_method_cheque" type="radio" class="input-radio" name="payment_method" value="cheque" checked="checked" data-order_button_text="">

	<label for="payment_method_cheque">
		Check payments 	</label>
			<div class="payment_box payment_method_cheque">
			<p>Please send a check to Store Name, Store Street, Store Town, Store State / County, Store Postcode.</p>
		</div>
	</li>
<li class="wc_payment_method payment_method_cod">
	<input id="payment_method_cod" type="radio" class="input-radio" name="payment_method" value="cod" data-order_button_text="">

	<label for="payment_method_cod">
		Cash on delivery 	</label>
			<div class="payment_box payment_method_cod" style="display:none;">
			<p>Pay with cash upon delivery.</p>
		</div>
	</li>
<li class="wc_payment_method payment_method_paypal">
	<input id="payment_method_paypal" type="radio" class="input-radio" name="payment_method" value="paypal" data-order_button_text="Proceed to PayPal">

	<label for="payment_method_paypal">
		PayPal <img src="#" alt="PayPal acceptance mark">
        <a href="#" class="about_paypal">What is PayPal?</a>	</label>
			<div class="payment_box payment_method_paypal" style="display:none;">
			<p>Pay via PayPal; you can pay with your credit card if you don’t have a PayPal account. SANDBOX ENABLED. You can use sandbox testing accounts only. See the <a href="https://developer.paypal.com/docs/classic/lifecycle/ug_sandbox/">PayPal Sandbox Testing Guide</a> for more details.</p>
		</div>
	</li>
		</ul>--%>
                                            <div class="form-row place-order">
                                                <%--<noscript>
			Since your browser does not support JavaScript, or it is disabled, please ensure you click the <em>Update Totals</em> button before placing your order. You may be charged more than the amount stated above if you fail to do so.			<br/><button type="submit" class="button alt" name="woocommerce_checkout_update_totals" value="Update totals">Update totals</button>
		</noscript>

			<div class="woocommerce-terms-and-conditions-wrapper">
		<div class="woocommerce-privacy-policy-text"></div>
                
					<p class="form-row validate-required">&nbsp;&nbsp;
				<label class="woocommerce-form__label woocommerce-form__label-for-checkbox checkbox">
				<input type="checkbox" class="woocommerce-form__input woocommerce-form__input-checkbox input-checkbox" name="terms" id="terms">
					<span class="woocommerce-terms-and-conditions-checkbox-text">I’ve read and accept the <a href="http://janstudio.net/gecko/fashion/terms-conditions/" target="_blank" class="woocommerce-terms-conditions-link">terms &amp; conditions</a></span>&nbsp;<span class="required">*</span>
				</label>
				<input type="hidden" name="terms-field" value="1">
			</p>
			</div>--%>
                                                <%--<p>
                                                <asp:LinkButton ID="btnCart" runat="server" class="button alt text-center" Width="100%" PostBackUrl="~/cart.aspx">Return To Cart</asp:LinkButton>
                                                    </p>--%>
                                                <asp:LinkButton ID="btnProceedToPayment" runat="server" Text="Proceed to Payment" ForeColor="White" Width="100%" ValidationGroup="receiver" class="btnproced text-center" OnClick="btnProceedToPayment_Click"></asp:LinkButton>
                                                <asp:Label ID="lblSignupMsg" runat="server" CssClass="ValidatorsMsg"></asp:Label>
                                                <a href="/cart.aspx" style="font-size:12px;">Return to cart</a>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                
                            </div>
                        </div>
                        
                        <div id="collapseThree" runat="server" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                            <div class="panel-h" id="headingThree" runat="server">
                                <div data-toggle="collapse" class="h5" data-target="#collapseThree">2. Payment Methods </div>
                                <span class="text-orange small">EXTRA 5% off on all online payments.</span>
                            </div>
                            <br />
                            <div class="your-order-wrappwer tablet-mt__60 small-mt__60">
                                <%--<h6 class="mb-20">Payment Methods <span class="text-red small">EXTRA 5% off on all online payments. COD not available for orders above INR 5000/-.</span></h6>--%>
                                <div class="your-order-area" style="background-color: white!important">
                                    <div class="your-order-wrap">
                                        <div class="your-order-info-wrap">
                                            <div class="your-order">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <%--<img src="images/COD.png" />--%>
                                                        <asp:RadioButton ID="rbtnCashOnDelivery" runat="server" GroupName="Pay" Checked="true" OnCheckedChanged="rbtnCashOnDelivery_CheckedChanged" AutoPostBack="true" />
                                                        <strong>Cash On Delivery </strong> <span style="color:#b93939; font-size:11px;">
                                                            <asp:Label ID="lblCODMsg" runat="server" Text="(not available for order value over INR 5,000)" Visible="false"></asp:Label></span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="rbtnWallets" runat="server" GroupName="Pay" OnCheckedChanged="rbtnWallets_CheckedChanged" AutoPostBack="true" />
                                                        <strong>Wallets</strong>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="rbtnUpi" runat="server" GroupName="Pay" OnCheckedChanged="rbtnUpi_CheckedChanged" AutoPostBack="true" />
                                                        <strong>UPI</strong> <span></span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="rbtnDebitCreditCard" runat="server" GroupName="Pay" OnCheckedChanged="rbtnDebitCreditCard_CheckedChanged" AutoPostBack="true" />
                                                        <strong>Credit/Debit Card</strong> <span></span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <p class="text-red" style="display: none">Get extra 5% off* on card payments. Apply Coupon Code: EXTRA5 at the time of payment. </p>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%--Added by Hetal Patel on 04-03-2020 --%>
                                                        <div id="divCod" runat="server" visible="false" style="padding-left: 150px;">
                                                            <p class="red "><span class="topbar-child1 text-red">COD is not Available for Customize Products!</span> </p>
                                                        </div>
                                                        <%--Added by Hetal Patel on 04-03-2020 --%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span id="siteseal" class="float-l">
                                                            <script async type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=CUhuxumBKCWGuUMfyeOz2N8aFMngNh1bBV8o0XECy8ruK2I8C6Rhc9doEOJ3"></script>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="btnPlaceOrder" runat="server" ClientIDMode="Static" Text="Pay" ForeColor="White"
                                                            CssClass="btn btn--lg btn--black pull-right mt-30" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnPlaceOrder_Click" />
                                                        <br />
                                                        <asp:Image runat="server" ImageUrl="images/Razorpay.png" Width="115" />
                                                    </div>
                                                    <div class="col-sm-12" id="otpbox" runat="server">
                                                        <asp:Label runat="server" ID="Label2" CssClass="pull-left">OTP Number</asp:Label>
                                                        <asp:TextBox ID="txtOTPno" runat="server" class="form-control"></asp:TextBox>
                                                        <asp:Label runat="server" ID="lblotpno" CssClass="red t-left"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- $classes -->

            </div>
            <!-- .jas-row -->

        </div>

    </div>

    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
    <form action="success.aspx" method="post">
        <asp:Literal ID="litRazor" runat="server"></asp:Literal>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#cph_main_addAddress").click(function () {
                $("#divAddNewAddress").removeClass('hidden');
                $("#divAddNewAddress").show(500);
                e.preventDefault();
                return false;
            });

            $("#cancelAddress").click(function () {
                $("#divAddNewAddress").show(500);
                e.preventDefault();
                return false;
            });
        });
    </script>
    <script>
        $(document).ready(function () {

            $("a").not('#already').click(function () {
                //  alert('1');
                $(window).unbind('beforeunload');

            });

            $(".btn").click(function () {
                //   alert('2');
                $(window).unbind('beforeunload');

            });

            $(".radio").click(function () {
                //    alert('3');
                $(window).unbind('beforeunload');

            });

            $(".select").click(function () {
                //    alert('4');
                $(window).unbind('beforeunload');

            });

            //$(window).bind('beforeunload', function () {
            //    //  $(this).myFunction();
            //    //     Fn();
            //    // PageMethods.SendEmailtoRemind();
            // //   Fn();
            //    return '';


            //});

            //$(window).unload(function () {
            //   // Fn();
            //   // alert('Bye.');
            //});


        });


        //function Fn() {
        //    $.ajax({
        //        url: "/proceedtopayment/SendEmailtoRemind",
        //        type: "post",
        //        data: {},
        //        async: data.async,
        //        success: function () {
        //            console.log('memcache deleted');
        //        }//success
        //    });
        // PageMethods.SendEmailtoRemind();
        //}
    </script>
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
