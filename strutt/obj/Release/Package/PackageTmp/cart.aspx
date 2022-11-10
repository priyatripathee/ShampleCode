<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="strutt.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        // WRITE THE VALIDATION SCRIPT IN THE HEAD TAG.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            /// Hide Header/footer  chandni 6-April-2021
            //$('#hideHeader').hide();
            $('#hideFooter').hide();
        });
    </script>
    <style type="text/css">
        @media only screen and (max-width: 767px) {
   .btn--black {
    width:100%;
  }
}
        .xpressbtn {
            /*margin: 20px;*/
            border-radius: 3px;
        }
        /*  below required code  */
        .xpressbtn {
            /* width: 220px;*/
            display: inline-block;
            font-weight: 500;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            padding: .375rem .75rem;
            font-size: 0.95rem;
            line-height: 1.7;
            font-family: 'Montserrat', sans-serif;
            -webkit-font-smoothing: antialiased;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .xpressbtn-primary {
            color: #fff;
            background-color: #404040;
            border-color: #404040;
        }

            .xpressbtn-primary:hover {
                color: #fff;
                background-color: #404040;
                border-color: #404040;
            }

            .xpressbtn-primary.focus, .xpressbtn-primary:focus {
                box-shadow: 0 0 0 .2rem rgba(0,123,255,.5);
            }

            .xpressbtn-primary.disabled, .xpressbtn-primary:disabled {
                color: #fff;
                background-color: #404040;
                border-color: #404040;
            }

            .xpressbtn-primary:not(:disabled):not(.disabled).active, .xpressbtn-primary:not(:disabled):not(.disabled):active, .show > .xpressbtn-primary.dropdown-toggle {
                color: #fff;
                background-color: #404040;
                border-color: #404040;
            }

                .xpressbtn-primary:not(:disabled):not(.disabled).active:focus, .xpressbtn-primary:not(:disabled):not(.disabled):active:focus, .show > .xpressbtn-primary.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .2rem rgba(0,123,255,.5);
                }

        svg {
            width: 86px;
            vertical-align: middle;
        }

        .xpressbtn:focus, .xpressbtn:hover {
            text-decoration: none;
        }

        .xpressbtn.focus, .xpressbtn:focus {
            outline: 0;
            box-shadow: 0 0 0 .2rem rgba(0,123,255,.25);
        }

        .xpressbtn.disabled, .xpressbtn:disabled {
            opacity: .65;
        }

        .xpressbtn:not(:disabled):not(.disabled) {
            cursor: pointer;
        }

            .xpressbtn:not(:disabled):not(.disabled).active, .xpressbtn:not(:disabled):not(.disabled):active {
                background-image: none;
            }

        .cart-plus-minus-box {
            height: 45px !important;
            line-height: 45px !important;
            width: 20px !important;
            font-weight: 400 !important;
            background: transparent none repeat scroll 0 0;
            border: medium none;
            color: #262626;
            font-size: 14px;
            margin: 0;
            padding: 0px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">

    <asp:Label ID="lblLoginName" runat="server"></asp:Label>
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Cart</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Cart</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <!-- cart start -->
        <asp:UpdatePanel ID="upCart" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="cart-main-area  section-space--ptb_90">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-content table-responsive cart-table-content header-color-gray">
                                    <asp:Label ID="lblError" runat="server" CssClass="text-red" Visible="false"></asp:Label>
                                    <asp:Label ID="lblQtyMsg" runat="server" CssClass="text-red" Visible="false"></asp:Label>
                                    <table>
                                        <thead>
                                            <tr class="bg-gray">
                                                <th></th>
                                                <th></th>
                                                <th class="product-name">Product</th>
                                                <th class="product-price">Price</th>
                                                <th></th>
                                                <th style="width: 20%">Quantity</th>
                                                <th style="width: 20%">Total</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="rptCartPc" runat="server" OnItemCommand="dlCart_ItemDataCommond" OnItemDataBound="dlCart_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td></td>
                                                        <td class="product-img">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="images/<%# Eval('thumb_image') %>" Width="100" AlternateText='<%#Eval("product_name")%>' />
                                                            <asp:HiddenField Value='<%# Eval("thumb_image") %>' ID="hfImgUrl" runat="server" />
                                                        </td>
                                                        <td class="product-name"><a href='../productdetails.aspx?proid=<%# Eval("product_id")%>' target="_blank"><%# Eval("product_name")%>
                                                            <asp:HiddenField ID="HFieldProductName" Value='<%#Eval("product_name") %>' runat="server" />
                                                        </a>
                                                        </td>

                                                        <td class="product-price"><span class="amount">Rs.<%#(Convert.ToDouble(Eval("Total"))).ToString("0.00") %>  </span>
                                                            <br />
                                                            <span><strike><%# Convert.ToInt16(Eval("discount")) == 0 ? "" : "Rs." + (Convert.ToDouble(Eval("sale_price"))).ToString("0.00")%></strike></span>
                                                        </td>
                                                        <%-- <td><%#Eval("quantity") %></td>--%>
                                                        <td class="hidden">
                                                            <%# Convert.ToInt16(Eval("custom_bag_price")) == 0 ? "" : "Custom Bag Charge: " + (Convert.ToDouble(Eval("custom_bag_price"))).ToString("0.00")%>
                                                            <%#Eval("custom_bag_param")%>
                                                            <%#Eval("x_point")%>
                                                            <%#Eval("y_point")%>
                                                        </td>
                                                        <td class="cart-quality">
                                                            <div class="quickview-quality quality-height-dec2">

                                                                <asp:LinkButton ID="btnMin" runat="server" CssClass="decrease icon-minus" CommandName="minus" CommandArgument='<%# Eval("product_id") %>'></asp:LinkButton>
                                                                <asp:TextBox ID="txtquantity" CssClass="cart-plus-minus-box" runat="server" onkeypress="javascript:return isNumber(event)" MaxLength="2" Text='<%#Bind("quantity") %>' ReadOnly="true"></asp:TextBox>
                                                                <asp:LinkButton ID="btnAdd" runat="server" CssClass="increase icon-plus" CommandName="add" CommandArgument='<%# Eval("product_id") %>'></asp:LinkButton>

                                                            </div>
                                                        </td>

                                                        <%--<td class="cart-quality">
                                                            <div class="quickview-quality quality-height-dec2">
                                                                <div class="cart-plus-minus">
                                                                    <div class="dec qtybutton">
                                                                         <asp:LinkButton ID="btnMin"  runat="server" CommandName="minus" CommandArgument='<%# Eval("product_id") %>' ><i class="decrease icon-minus"></i></asp:LinkButton>
                                                                       </div>
                                                                  <asp:TextBox ID="txtquantity" CssClass="cart-plus-minus" runat="server" onkeypress="javascript:return isNumber(event)" MaxLength="2" Text='<%#Bind("quantity") %>' ReadOnly="true"></asp:TextBox>
                                                                    <div class="inc qtybutton">
                                                                         <asp:LinkButton ID="btnAdd" runat="server" CommandName="add" CommandArgument='<%# Eval("product_id") %>' ><i class="increase icon-plus"></i></asp:LinkButton>
                                                                       </div>
                                                                </div>
                                                            </div>
                                                        </td>--%>
                                                        <%-- <td class="cart-quality">
                                                            <div class="quickview-quality quality-height-dec2">
                                                                <asp:Button ID="btnMin" runat="server" Text="-"  CommandName="minus" CommandArgument='<%# Eval("product_id") %>' UseSubmitBehavior="false"></asp:Button>
                                                                <asp:TextBox ID="txtquantity" runat="server" onkeypress="javascript:return isNumber(event)" MaxLength="2" Text='<%#Bind("quantity") %>' ReadOnly="true" Width="50"></asp:TextBox>
                                                                <asp:Button ID="btnAdd"  runat="server" Text="+"  CommandName="add" CommandArgument='<%# Eval("product_id") %>' UseSubmitBehavior="false"></asp:Button>
                                                            </div>
                                                        </td>--%>
                                                        <td class="price-total">
                                                            <span class="amount">Rs.<%#(Convert.ToDouble(Eval("Total"))).ToString("0.00") %></span>
                                                        </td>
                                                        <td class="product-remove">
                                                            <asp:LinkButton ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("product_id") %>' UseSubmitBehavior="false" ToolTip="Remove Item"><i class="icon-cross2"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="shoping-update-area pull-right">
                                    <div class="continue-shopping-butotn mt-30">
                                        <asp:LinkButton ID="btnContinueShopping" runat="server" PostBackUrl="~/category.aspx" CssClass="btn btn--lg btn--black"> <i class="icon-arrow-left"></i>Continue Shopping</asp:LinkButton>&nbsp
                                        <asp:LinkButton ID="btnCheckout" runat="server" PostBackUrl="~/proceedtopayment.aspx" CssClass="btn btn--lg btn--black">Proceed to checkout</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="cart-buttom-area">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="discount-code section-space--mt_60">
                                                <h6 class="mb-30">Coupon Discount</h6>
                                                <asp:Label ID="lblMsgCoupon" runat="server"></asp:Label>
                                                <asp:Label ID="lblDis25" runat="server" CssClass="lblcolor"></asp:Label>
                                                <asp:Label ID="lblDis20" runat="server" CssClass="lblcolor"></asp:Label>
                                                <p>Enter your coupon code if you have one.</p>
                                                <asp:TextBox ID="txtCouponCode" runat="server" placeholder="Coupon code"></asp:TextBox>
                                                <asp:Button ID="lbtnApply" runat="server" Text="Apply Coupon" OnClick="lbtnApply_Click" CssClass="coupon-btn btn btn--lg btn--border_1" />
                                            </div>
                                            <div class="discount-code">
                                                <p class="mb-30 text-orange">
                                                    Hey Guys!<br />
                                                    <asp:Label ID="lblSale" runat="server"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOffer" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="cart_totals section-space--mt_60 ml-md-auto">
                                                <div class="grand-total-wrap">
                                                    <div class="grand-total-content">
                                                        <ul>
                                                            <asp:Label ID="lblCartCount" runat="server" CssClass="d-none">0</asp:Label>
                                                            <li>Subtotal <span>Rs.<asp:Label ID="lblTotalPrice" runat="server">0.00</asp:Label></span></li>
                                                            <li>Discount <span>Rs.<asp:Label ID="lblDiscount" runat="server">0.00</asp:Label></span></li>
                                                            <li>Shipping Charge <span>Rs.<asp:Label ID="lblShipping" runat="server">0.00</asp:Label></span></li>
                                                            <li>Total <span>Rs.<asp:Label ID="lblGrandTotal" runat="server">0.00</asp:Label></span></li>
                                                        </ul>
                                                    </div>
                                                    <%--<p class="mb-30 text-orange">Please proceed to the payment page to avail discount coupon.</p>
                                                    <p class="mb-30 text-orange">Get an EXTRA 5% off on prepaid orders for a contactless transaction. We care for your safety.</p>--%>
                                                </div>
                                                <div class="grand-btn mt-30">

                                                    <%--Start: Added by Hetal Patel : 25-06-2020 for Xpresslane Payment Integration--%>
                                                    <%if (HttpContext.Current.Session["CustomerEmailID"] != null)
                                                        {
                                                            if (Session["CustomerEmailID"].ToString() == "kalpesh013@gmail.com" || Session["CustomerEmailID"].ToString() == "visheshkhosla83@gmail.com")
                                                            {%>
                                                    <div class="col-12-12">
                                                        <button type="button" id="btnXpresslane" runat="server" class="xpressbtn xpressbtn-primary col-12-12" onserverclick="btnXpresslane_Click">
                                                            Checkout on
     
                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 169 28">

                                                    <defs>
                                                        <path id="a" d="M.028 22.18h19.18V.115H.028z" />
                                                    </defs>
                                                    <g fill="none" fill-rule="evenodd">
                                                        <g transform="translate(27.477 5.82)">
                                                            <mask id="b" fill="#fff">
                                                                <use xlink:href="#a" />
                                                            </mask>
                                                            <path fill="#FFFFFF" d="M14.885 7.462c0-2.243-1.463-3.673-3.479-3.673-1.43 0-2.73.812-3.576 1.885l-1.235 5.43c.683 1.072 2.015 1.82 3.673 1.82 2.731 0 4.617-2.569 4.617-5.462m-8.94 6.664l-1.788 8.062H.028L4.84.505h4.129l-.455 1.95c1.333-1.56 2.86-2.34 4.746-2.34 3.544 0 5.95 2.34 5.95 6.502 0 4.714-2.927 9.98-8.258 9.98-2.113 0-3.966-.91-5.006-2.47"
                                                                mask="url(#b)" />
                                                        </g>
                                                        <path fill="#FFFFFF" d="M47.79 22.027L51.27 6.325h4.129l-.456 2.049c1.366-1.496 3.056-2.439 5.56-2.439l-.911 4.096a6.385 6.385 0 0 0-1.495-.195c-1.528 0-2.991.846-3.901 1.886l-2.276 10.305H47.79M72.529 12.665c.032-.065.032-.26.032-.326 0-1.592-1.17-3.023-3.64-3.023-2.244 0-3.901 1.626-4.227 3.349h7.835zM60.11 15.363c0-5.104 3.836-9.428 9.07-9.428 4.03 0 7.054 2.601 7.054 6.99 0 .942-.195 2.113-.325 2.6H64.24v.39c0 1.333 1.397 3.121 4.323 3.121 1.398 0 2.99-.39 3.966-1.138l1.268 2.894c-1.528 1.04-3.609 1.625-5.56 1.625-4.94 0-8.126-2.665-8.126-7.054zM76.917 19.589l2.308-2.893c.943 1.105 3.219 2.47 5.494 2.47 1.398 0 2.276-.747 2.276-1.625 0-2.113-7.997-1.593-7.997-6.307 0-2.73 2.275-5.299 6.469-5.299 2.666 0 5.169 1.008 6.664 2.536l-2.145 2.763c-.748-.975-2.699-2.048-4.617-2.048-1.462 0-2.275.65-2.275 1.463 0 1.918 7.997 1.495 7.997 6.274 0 2.99-2.503 5.494-6.794 5.494-2.861 0-5.624-.975-7.38-2.828M92.81 19.589l2.309-2.893c.942 1.105 3.218 2.47 5.493 2.47 1.398 0 2.276-.747 2.276-1.625 0-2.113-7.997-1.593-7.997-6.307 0-2.73 2.276-5.299 6.469-5.299 2.666 0 5.169 1.008 6.664 2.536l-2.145 2.763c-.748-.975-2.698-2.048-4.616-2.048-1.463 0-2.276.65-2.276 1.463 0 1.918 7.997 1.495 7.997 6.274 0 2.99-2.503 5.494-6.794 5.494-2.861 0-5.624-.975-7.38-2.828" />
                                                        <path fill="#FFFFFF" d="M109.455 22.027L114.233.344h1.69l-4.778 21.683h-1.69M118.85 15.655c0 3.219 1.884 5.234 4.745 5.234 2.21 0 4.259-1.398 5.331-3.023l1.658-7.542c-.78-1.56-2.665-2.86-5.201-2.86-3.933 0-6.534 4.063-6.534 8.191m12.06-6.664l.554-2.666h1.722l-3.478 15.702h-1.723l.552-2.373c-1.3 1.658-3.153 2.763-5.396 2.763-3.608 0-6.144-2.406-6.144-6.437 0-5.298 3.283-10.045 8.257-10.045 2.568 0 4.584 1.268 5.656 3.056M145.57 22.027l2.342-10.663c.064-.292.194-.747.194-1.138 0-1.95-1.495-2.763-3.51-2.763-1.951 0-3.966 1.333-5.299 2.73l-2.633 11.834h-1.691l3.479-15.702h1.69l-.52 2.374c1.397-1.366 3.446-2.764 5.655-2.764 2.667 0 4.585 1.268 4.585 3.836 0 .293-.098.976-.195 1.333l-2.405 10.923h-1.692M166.863 13.38c.033-.163.033-.52.033-.715 0-3.056-1.853-5.267-5.234-5.267-3.153 0-5.948 2.763-6.502 5.982h11.703zm-13.718 2.113c0-5.234 3.835-9.558 8.712-9.558 4.194 0 6.664 2.959 6.664 6.795 0 .682-.163 1.593-.26 1.983h-13.263a7.07 7.07 0 0 0-.065.91c0 2.893 1.918 5.331 5.754 5.331 1.593 0 3.38-.552 4.746-1.69l.65 1.235c-1.397 1.073-3.414 1.918-5.46 1.918-4.715 0-7.478-2.828-7.478-6.924z" />
                                                        <path fill="#F0B9A1" d="M7.757 1.133H5.175l5.003 9.882L.108 21.99h2.583L12.76 11.015 7.757 1.133" />
                                                        <path fill="#EA8F6B" d="M13.617 8.044l-3.408-6.91H7.757l5.003 9.88L2.69 21.99h2.505l9.998-10.898-1.577-3.048" />
                                                        <path fill="#DF582E" d="M19.775 11.292l-1.68-3.248-3.408-6.91h-2.505l3.408 6.91 1.68 3.248-2.93 3.288-6.692 7.41h2.505l6.691-7.41 2.93-3.288" />
                                                        <path fill="#DF582E" d="M17.557 11.292l-1.68-3.248-3.408-6.91h-2.53l5.003 9.88-.07.078.103.2-2.93 3.288-6.693 7.41h2.582l6.692-7.41 2.93-3.288" />
                                                        <path fill="#DF582E" d="M15.13 11.217l-.104-.201L4.958 21.99h.482l6.74-7.463 2.95-3.31" />
                                                        <path fill="#FFFFFF" d="M29.555 1.117H24l-5.212 6.007 1.931 3.761 8.837-9.768M17.276 15.659l3.268 6.386h4.985l-5.007-9.936-3.246 3.55" />
                                                    </g>
                                                </svg>
                                                        </button>
                                                        <%--<asp:ImageButton ID="btnXpresslane" runat="server" OnClick="btnXpresslane_Click" CssClass="col-12-12" ImageUrl="~/images/XpressLane.png"/>--%>

                                                        <p style="color: #F00; margin-top: 10px;">* Online / Quick Checkout</p>
                                                        <%--<asp:LinkButton ID="btnXpresslane" runat="server" OnClick="btnXpresslane_Click" CssClass="btn btn-primary col-12-12">Checkout via Xpresslane</asp:LinkButton>--%>
                                                    </div>
                                                    <%}
                                                        } %>
                                                    <%--End: Added by Hetal Patel : 25-06-2020 for Xpresslane Payment Integration--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- cart end -->
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


