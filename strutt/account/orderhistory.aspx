<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="orderhistory.aspx.cs" Inherits="strutt.account.orderhistory" %>

<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .shopping-cart-table a.white {
            color: White !important;
        }

        .btnreturnfalse {
            line-height: 44px;
            padding: 0 42px;
            height: 46px;
            width: 100%;
            text-align: center;
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Order History</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="../default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Order History</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <asp:Label ID="lblLoginName" runat="server"></asp:Label>
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
                                <li>
                                    <asp:HyperLink ID="hlAddBlog" runat="server" NavigateUrl="~/account/addreview.aspx">Add Blog</asp:HyperLink>
                                <li><a href="../Login.aspx?type=lo">Log out</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <h2><strong>Order History </strong></h2>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        <div>
                            <asp:Label ID="lblOrderCancel" runat="server"></asp:Label>
                            <asp:Repeater ID="rptLastOrderPlaceStaus" runat="server" OnItemDataBound="rptLastOrderPlaceStaus_ItemDataBound"
                                OnItemCommand="rptLastOrderPlaceStaus_ItemCommand">
                                <ItemTemplate>
                                    <table style="border: 1px solid #999; border-collapse: initial;">
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hfieldorderstatusId" runat="server" Value='<%#Eval("order_detail_id")%>'></asp:HiddenField>
                                                <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                        DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                        DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'>
                                                    <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>'
                                                        class="img-thumbnail" Style="width: 100px;" />
                                                </a>
                                            </td>
                                            <td style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">Order ID:<b style="color: #388E3C; font-size: 14px;"> STR10001<%# Eval("order_id")%> </b>
                                                <strong style="color: #388E3C; margin: 0px; padding: 0px; width: 100px; font-weight: normal;">(<%# Eval("quantity")%> Item)</strong></br>
                                     Order Placed on: <strong style="color: #683415; font-weight: normal;"><%# Eval("OrdDate")%></strong></br>
                                            </td>

                                            <td style="vertical-align: top; width: 165px;">Amount: <strong style="color: #8e471e; font-weight: normal; font-size: 14px;">Rs. <%# Eval("SalePrice")%> x <%# Eval("quantity")%> </strong>
                                                <br />
                                                Shipping: <strong style="color: #8e471e; font-weight: normal; font-size: 14px;">Rs. <%# Eval("shipping_price")%></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <br />
                                                Discount (-) <strong style="color: #8e471e; font-weight: normal; font-size: 14px;">Rs. <%# Eval("discount_price")%></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <br />
                                                You Paid: <strong style="color: #8e471e; font-size: 14px;">Rs. <%# Eval("total_price")%></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td class="text-left">Shipping Information:<br />
                                                <strong style="font-size: 15px; font-weight: normal;">
                                                    <%# Eval("user_name")%></strong>
                                                <br />
                                                <%# Eval("UserNo")%><br />
                                                <%# Eval("address")%><br />
                                                <%# Eval("city")%>&nbsp;<%# Eval("state")%>&nbsp;<%# Eval("pin_code")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">
                                                <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                        DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                        DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'>
                                                    <%#Eval("product_name")%>
                                                </a>
                                            </td>
                                            <td colspan="2" style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">
                                                <strong style="color: #388E3C; font-weight: normal; font-size: 13px;">
                                                    <asp:Label ID="lblOrderStatus" runat="server"></asp:Label>
                                                    <%# Eval("UDate")%></strong> &nbsp;
                                        <a href='orderstatus.aspx?ordid=<%# Eval("order_detail_id")%>'>Status:Detail...</a>
                                            </td>
                                            <td colspan="2" style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">
                                                <asp:HyperLink ID="ibtnInvoice" runat="server" AlternateText="Invoice" Text="VIEW INVOICE"
                                                    NavigateUrl='<%#"~/account/invoice.aspx?id="+ BusinessEntities.security.Encryptdata(Eval("order_id").ToString())%>' Style="margin-bottom: 5px;" onclick="javascript:w= window.open(this.href,'Invoice','left=20,top=20,width=1000,height=700,toolbar=0,resizable=0');return false;">
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--<td>
                                  <strong style="color: #388E3C; font-weight: normal; font-size: 13px;">
                                      <asp:Label ID="lblOrderStatus" runat="server"></asp:Label>
                                      <%# Eval("UDate")%></strong> &nbsp;
                                        <a href='orderstatus.aspx?ordid=<%# Eval("order_detail_id")%>'>Status:Detail...</a>
                              </td>--%>
                                            <td colspan="2" style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">
                                                <asp:LinkButton ID="btntrackorder" CommandName="trackorder" CommandArgument='<%#Eval("ship_via") + ";" +Eval("ship_id")%>' runat="server" Text="TRACK ORDER" Visible='<%# Convert.ToString(Eval("manifest_link")) == "" ? true : false %>' CssClass="btn--lg btn--black font-weight--reguler text-white" />
                                                <asp:LinkButton ID="btnpickertrackorder" CommandName="pickertrackorder" CommandArgument='<%#Eval("ship_via") + ";" +Eval("ship_id")%>' runat="server" Text="Track Order" Visible='<%# Convert.ToString(Eval("manifest_link")) == "" ? false : true %>' CssClass="btn--lg btn--black font-weight--reguler text-white" />
                                            </td>
                                            <td colspan="2" style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">
                                                <asp:HiddenField ID="hfCancelProductId" runat="server" Value='<%#Eval("product_id")%>'></asp:HiddenField>
                                                <asp:Button ID="btncancel" runat="server" Text="CANCEL" OnClientClick="javascript:return confirm('Are you sure you want to cancel this Item!');"
                                                    CommandName="cancel" CommandArgument='<%#Eval("order_id")%>'
                                                    CssClass="btn--lg btn--black font-weight--reguler text-white" ToolTip="The Cancel order." />
                                            </td>
                                            <td colspan="2" style="height: 20px; padding: 5px 7px; text-align: left; vertical-align: top;">
                                                <asp:HiddenField ID="hfilesProductId" runat="server" Value='<%#Eval("product_id")%>'></asp:HiddenField>
                                                <asp:Button ID="btnReturn" runat="server" Text="RETURN / REPLACE" OnClientClick="javascript:return confirm('Are you sure you want to cancel this Item!');"
                                                    CommandName="return" CommandArgument='<%#Eval("order_id")%>'
                                                    CssClass="btn--lg btn--black font-weight--reguler text-white" ToolTip="The Cancel order option will only be active before the order is dispatched." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="padding: 7px; text-align: center;">
                                                <asp:Label ID="lblStatusMsg" runat="server" Font-Size="Larger"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="padding: 7px; text-align: center;">
                                                <asp:Image ID="imgorderStatus" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Label ID="lblLastOrder" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
