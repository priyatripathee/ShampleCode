<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="proceedtopayment.aspx.cs" Inherits="strutt.proceedtopayment" %>
<%@ Import Namespace="BLL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%--Google Login realted--%>
  <script src="https://apis.google.com/js/platform.js" async defer></script>
  <meta name="google-signin-client_id" content="299776664174-93q3859fbfvqv6eis18hsuuu834e0ln8.apps.googleusercontent.com" />
  <meta name="google-signin-scope" content="https://www.googleapis.com/auth/analytics.readonly" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
<%-- <div class="modal-backdrop"></div>--%>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>

<div class="breadcrumb-area">
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="row breadcrumb_box  align-items-center">
          <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
            <h2 class="breadcrumb-title">Checkout</h2>
          </div>
          <div class="col-lg-6  col-md-6 col-sm-6"> 
            <!-- breadcrumb-list start -->
            <ul class="breadcrumb-list text-center text-sm-right">
              <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
              <li class="breadcrumb-item active">Checkout</li>
            </ul>
            <!-- breadcrumb-list end --> 
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<div class="site-wrapper-reveal border-bottom">
  <asp:Label ID="lblCheckMsg" runat="server" CssClass="text-red" Visible="false"></asp:Label>
  <asp:Label ID="lbllogin" runat="server" ForeColor="Red" CssClass="text-red"></asp:Label>
  <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  <!-- checkout start -->
  <div class="checkout-main-area section-space--ptb_90">
    <div class="container">
      <div class="checkout-wrap">
        <div class="row">
          <div class="col-lg-7">
            <div class="accordion" id="accordionExample">
              <div class="panel-h" id="headingOne">
                <div class="h5" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true"> 1.Contact Details
                  <asp:Label ID="lblhead1" runat="server" CssClass="h6"></asp:Label>
                  <asp:LinkButton ID="btnChangeUserDetails" type="button" runat="server"
                                            class="btn btn-sm btn-edit fr change-btn pull-right p-0" OnClick="btnChangeUserDetails_Click">Change</asp:LinkButton>
                </div>
              </div>
              <div id="collapseOne" runat="server" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                <asp:Label ID="lblMsgGuest" runat="server" CssClass="text-danger"></asp:Label>
                <div class="billing-info-wrap mr-100">
                  <p class="cart-page-title text-orange">Returning customer? <a href="login.aspx?url=proceedtopayment.aspx">Click here to login</a> </p>
                  <div class="billing-info mb-10">
                    <asp:TextBox ID="txtguestemail" runat="server" placeholder="Username or email *" AutoPostBack="true" OnTextChanged="txtguestemail_OnTextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvguestemail" runat="server" ErrorMessage="Please enter your email id"
                                                SetFocusOnError="true" ControlToValidate="txtguestemail" Display="Dynamic" ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="retxtguestemail" Display="Dynamic"
                                                ControlToValidate="txtguestemail" Text="Invalid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                runat="server" ValidationGroup="receiver" ForeColor="Red" />
                  </div>
                  <asp:HiddenField ID="hfLoginType" runat="server" />
                  <asp:HiddenField ID="hffbid" runat="server" />
                  <asp:HiddenField ID="hfpersonaName" runat="server" />
                  <asp:HiddenField ID="hfpersonamobile" runat="server" />
                  <asp:HiddenField ID="hfreceiveremail" runat="server" />
                  <asp:HiddenField ID="productinfo" runat="server" Value="productinfo" />
                  <div class="your-order-wrappwer tablet-mt__60 small-mt__60" id="headReceiverAddress" runat="server">
                    <div class="your-order-area" style="background-color: white!important">
                      <div class="your-order-wrap">
                        <div class="your-order-info-wrap">
                          <div class="your-order">
                            <div class="row">
                              <asp:Repeater ID="repReceiverAddress" runat="server" OnItemCommand="repReceiverAddress_ItemCommand">
                                <ItemTemplate>
                                  <div class="col-lg-4 col-md-4">
                                    <div class="h6"> <%# Eval("full_name")%> - <%# Eval("contact_number")%> </div>
                                    <div> <%# Eval("address")%>, <%# Eval("city")%>, <%# Eval("state")%> - <%# Eval("pin_code")%> </div>
                                    <asp:Button ID="btnUseAddress" runat="server" Text="Use this address" CommandName="useThisAddress" CssClass="btn--sm btn--grey btn-md text-center" CommandArgument='<%# Eval("customer_details_id") %>' />
                                    <%--<asp:LinkButton ID="lnkAddressRemove" class="pull-right margin-t-5" runat="server" CommandName="Remove" CommandArgument='<%# Eval("customer_details_id") %>' ToolTip="Delete Address"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>--%>
                                  </div>
                                </ItemTemplate>
                              </asp:Repeater>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <h6 class="mb-20">Billing Details</h6>
                  <div class="row">
                    <div class="col-lg-6 col-md-6">
                      <div class="billing-info mb-10">
                        <asp:TextBox ID="txtreceivername" runat="server" placeholder="First name *"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please enter your name" CssClass="Validators"
                                                        SetFocusOnError="true" ControlToValidate="txtreceivername" Display="Dynamic" ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                      <div class="billing-info mb-10">
                        <asp:TextBox ID="txtreceiverlastname" runat="server" placeholder="Last name *"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-lg-12">
                      <div class="billing-select mb-10">
                        <select name="country_id" data-value="country_id" class="select-active" title="Country *">
                          <option value="101">India</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-lg-12">
                      <div class="billing-info mb-10">
                        <asp:TextBox ID="txtreceiveraddress" runat="server" class="billing-address" placeholder="Street address *"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please enter your Street address"
                                                        SetFocusOnError="true" ControlToValidate="txtreceiveraddress" Display="Dynamic" CssClass="Validators"
                                                        ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtLandMark" runat="server" class="billing-address" placeholder="LandMark"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter your address"
                                                        SetFocusOnError="true" ControlToValidate="txtreceiveraddress" Display="Dynamic" CssClass="Validators"
                                                        ValidationGroup="receiver" ForeColor="Red"> </asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-lg-4">
                      <div class="billing-select mb-10">
                        <asp:DropDownList ID="ddlReceiverState" runat="server"  AutoPostBack="true" DataTextField="state" CssClass="select-active"
                                                        DataValueField="state" OnSelectedIndexChanged="ddlReceiverState_SelectedIndexChanged" > </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select state"
                                                        SetFocusOnError="true" ControlToValidate="ddlReceiverState" InitialValue="Select State" Display="Dynamic"
                                                        ValidationGroup="receiver" CssClass="Validators" ForeColor="Red"> </asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-lg-4">
                      <div class="billing-select mb-10">
                        <asp:DropDownList ID="ddlReceiverCity"  runat="server" DataTextField="city_name" CssClass="select-active"
                                                        DataValueField="city_name"> </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please select city"
                                                        SetFocusOnError="true" ControlToValidate="ddlReceiverCity" InitialValue="Select City" Display="Dynamic"
                                                        ValidationGroup="receiver" CssClass="Validators" ForeColor="Red"> </asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-lg-4 col-md-12">
                      <div class="billing-info mb-10">
                        <asp:TextBox ID="txtReceiverPinCode" runat="server" placeholder="Postcode / ZIP *"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter pincode"
                                                        SetFocusOnError="true" ControlToValidate="txtReceiverPinCode" Display="Dynamic" ValidationGroup="receiver"
                                                        CssClass="Validators" ForeColor="Red"> </asp:RequiredFieldValidator>
                      </div>
                    </div>
                    <div class="col-lg-12 col-md-12">
                      <div class="billing-info mb-10">
                        <asp:TextBox ID="txtreceiverphone" runat="server" placeholder="Phone *"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please enter your phone"
                                                        SetFocusOnError="true" ControlToValidate="txtreceiverphone" ForeColor="Red" Display="Dynamic" ValidationGroup="receiver" CssClass="Validators"> </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtreceiverphone"
                                                        ErrorMessage="Please enter 10 digits" Display="Dynamic" ValidationGroup="receiver" CssClass="Validators"
                                                        ValidationExpression="[0-9]{10}" ForeColor="Red"> </asp:RegularExpressionValidator>
                      </div>
                    </div>
                  </div>
                  <!--<div class="additional-info-wrap">
                                            <h6 class="mb-10">Additional information</h6>
                                            <asp:TextBox ID="txtreceivermessage" runat="server" placeholder="Notes about your order, e.g. special notes for delivery. " TextMode="MultiLine"></asp:TextBox>
                                        </div>-->
                  <div class="your-order-wrappwer tablet-mt__60 small-mt__60">
                    <h6 class="mb-20">Order summary</h6>
                    <div class="your-order-area">
                      <div class="your-order-wrap gray-bg-4">
                        <div class="your-order-info-wrap">
                          <div class="your-order-info">
                            <asp:Repeater ID="dlCart" runat="server">
                              <ItemTemplate>
                                <ul>
                                  <li>
                                    <asp:Image ID="a1" runat="server" Height="115" ImageUrl='<%# Eval("thumb_image") %>' AlternateText='<%#Eval("product_name")%>' />
                                    <span><%# Eval("product_name")%> </span></li>
                                  <li><%# Eval("quantity")+" x Rs."+(Convert.ToDouble(Eval("sale_price"))).ToString("0.00")%></li>
                                </ul>
                              </ItemTemplate>
                            </asp:Repeater>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <br />
                  <div class="col-6-12">
                    <asp:LinkButton ID="btnCart" runat="server" class="btn btn--lg btn--black  bg-secondary m-1" PostBackUrl="~/cart.aspx"><i class="fa fa-angle-left" aria-hidden="true"></i> Return To Cart</asp:LinkButton>
                  </div>
                   
                  <div class="col-6-12">
                    <asp:LinkButton ID="btnProceedToPayment" runat="server" Text="Proceed to Payment" ValidationGroup="receiver" class="btn btn--lg btn--black  m-1" OnClick="btnProceedToPayment_Click"></asp:LinkButton>
                  </div>
                  <asp:Label ID="lblSignupMsg" runat="server" CssClass="ValidatorsMsg"></asp:Label>
                  <br class="clearfix" />
                </div>
              </div>
              <br />
              <div id="collapseThree" runat="server" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                <div class="panel-h" id="headingThree" runat="server">
                  <div data-toggle="collapse" class="h5" data-target="#collapseThree">2. Payment Methods </div>
                  <span class="text-orange small">EXTRA 5% off on all online payments.</span> </div>
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
                              <strong>Cash On Delivery</strong> <span></span> </div>
                            <div class="col-md-6">
                              <asp:RadioButton ID="rbtnWallets" runat="server" GroupName="Pay" OnCheckedChanged="rbtnWallets_CheckedChanged" AutoPostBack="true" />
                              <strong>Wallets</strong> </div>
                            <div class="col-md-6">
                              <asp:RadioButton ID="rbtnUpi" runat="server" GroupName="Pay" OnCheckedChanged="rbtnUpi_CheckedChanged" AutoPostBack="true" />
                              <strong>UPI</strong> <span></span> </div>
                            <div class="col-md-6">
                              <asp:RadioButton ID="rbtnDebitCreditCard" runat="server" GroupName="Pay" OnCheckedChanged="rbtnDebitCreditCard_CheckedChanged" AutoPostBack="true" />
                              <strong>Credit/Debit Card</strong> <span></span> </div>
                            <div class="col-md-6">
                              <p class="text-red" style="display: none"> Get extra 5% off* on card payments. Apply Coupon Code: EXTRA5 at the time of payment. </p>
                            </div>
                            <div class="col-md-6">
                              <%--Added by Hetal Patel on 04-03-2020 --%>
                              <div id="divCod" runat="server" visible="false" style="padding-left: 150px;">
                                <p class="red "> <span class="topbar-child1 text-red">COD is not Available for Customize Products!</span> </p>
                              </div>
                              <%--Added by Hetal Patel on 04-03-2020 --%>
                            </div>
                            <div class="col-md-6"> <span id="siteseal" class="float-l"> 
                              <script async type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=CUhuxumBKCWGuUMfyeOz2N8aFMngNh1bBV8o0XECy8ruK2I8C6Rhc9doEOJ3"></script> 
                              </span> </div>
                          </div>
                          <div class="row">
                            <div class="col-sm-12">
                              <asp:Button ID="btnPlaceOrder" runat="server" ClientIDMode="Static" Text="Pay"
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
          <div class="col-lg-5">
            <div class="customer-zone mb-30">
              <p class="cart-page-title">Have a coupon? </p>
              <div class="checkout-coupon" runat="server">
                <p>If you have a coupon code, please apply it below.</p>
                <p>
                  <asp:Label ID="lblMsgCoupon" runat="server"></asp:Label>
                  <asp:Label ID="lblDis25" runat="server" CssClass="lblcolor"></asp:Label>
                  <asp:Label ID="lblDis20" runat="server" CssClass="lblcolor"></asp:Label>
                </p>
                <div>
                  <asp:TextBox ID="txtCouponCode" runat="server" placeholder="Coupon code" CssClass="txt m-1"></asp:TextBox>
                  
                  <asp:Button ID="lbtnApply" runat="server" OnClick="lbtnApply_Click" Text="Apply Coupon" CssClass="btn-coupon m-1" />
                </div>
              </div>
            </div>
            <div class="your-order-wrappwer tablet-mt__60 small-mt__60">
              <p class="cart-page-title">
              <h6 class="mb-20">Your order</h6>
              </p>
              <div class="your-order-area">
                <div class="your-order-wrap gray-bg-4">
                  <div class="your-order-info-wrap">
                          <asp:Label ID="lblCartCount" runat="server" CssClass="d-none"></asp:Label>
                          <asp:Label ID="lblQtyCount" runat="server" CssClass="d-none"></asp:Label>
                          <asp:Label ID="lblCustomBagCharge" runat="server" CssClass="d-none"></asp:Label>
                    <div class="your-order-info order-subtotal">
                      <ul>
                        <li><strong>Subtotal</strong> <span>Rs.
                          <asp:Label ID="lblTotalPrice" runat="server">0.00</asp:Label>
                          </span></li>
                      </ul>
                    </div>
                    <div class="your-order-middle">
                      <ul>
                        <li><strong>Discount</strong> <span>Rs.
                          <asp:Label ID="lblDiscount" runat="server">0.00</asp:Label>
                          </span></li>
                      </ul>
                    </div>
                    <div class="your-order-middle">
                      <ul>
                        <li><strong>Shipping Charge</strong> <span>Rs.
                          <asp:Label ID="lblShipping" runat="server">0.00</asp:Label>
                          </span></li>
                      </ul>
                    </div>
                    <div class="your-order-info order-total">
                      <ul>
                        <li><strong>Total</strong> <span>Rs.
                          <asp:Label ID="lblGrandTotal" runat="server">0.00</asp:Label>
                          </span></li>
                      </ul>
                    </div>
                    <div class="payment-area mt-30">
                      <div class="single-payment">
                        <h6 class="mb-10 text-orange">Hey Guys!
                          <asp:Label ID="lblSale" runat="server"></asp:Label>
                        </h6>
                        <p class="text-orange">
                          <asp:Label ID="lblCode" runat="server"></asp:Label>
                        </p>
                       
                        <span class="text-orange">
                        <asp:Label ID="lblOffer" runat="server"></asp:Label>
                        </span> </div>
                      <div class="single-payment mt-20">
                        <p class="text-orange">Get an EXTRA 5% off on prepaid orders for a contactless transaction. We care for your safety.</p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="payment-method">
                <p class="mt-30">Your personal data will be used to process your order, support your experience throughout this website, and for other purposes described in our <a href="#">privacy policy</a>.</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- checkout end --> 
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
  <div style="display: inline;"> <img height="1" width="1" style="border-style: none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0" /> </div>
  </noscript>
</asp:Content>
