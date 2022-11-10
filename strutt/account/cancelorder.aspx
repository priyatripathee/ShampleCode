<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="cancelorder.aspx.cs" Inherits="strutt.account.cancelorder" %>

<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Cancel Order</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="../default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Cancel Order</li>
                            </ul>
                            <!-- breadcrumb-list end -->
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
                    <div class="col-lg-9">
                        <h2><strong>CANCEL ORDER </strong></h2>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        <div class="table-content table-responsive cart-table-content">
                            <asp:Label ID="lblOrderCancel" runat="server"></asp:Label>
                            <table>
                                <thead>
                                    <tr>
                                        <th>Order ID:<b style="color: #8e471e; font-size: 14px;"> STR10001<%# Eval("order_id")%> </b></th>
                                        <th>Item Name</th>
                                        <th class="product-name">QTY</th>
                                        <th class="product-price">Sub Total</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptOrderCancel" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'>
                                                        <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>'
                                                            class="img-thumbnail" Width="110" Height="110" />
                                                    </a>
                                                </td>
                                                <td>
                                                    <a href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>'>
                                                        <%#Eval("product_name")%>
                                                    </a>
                                                </td>
                                                <td>
                                                    <%# Eval("quantity")%>
                                                </td>
                                                <td>
                                                    <strong style="color: #8e471e; font-size: 14px;">Rs. <%# Eval("total_price")%></strong>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     </tbody>
                            </table>

                            <table id="tblreason" runat="server" style="border: 1px solid #eeeeee; text-align: center; width: 100%;">
                                <tr>
                                    <td style="width: 170px; text-align: right;">Reason for cancellation <span style="color: Red">*</span> :
                                    </td>
                                    <td style="text-align: left; padding: 10px 0 0 10px;">
                                        <asp:DropDownList ID="ddlreasoncancel" runat="server" CssClass="form-control" Style="width: 513px">
                                            <asp:ListItem>select reason</asp:ListItem>
                                            <asp:ListItem>Order placed by mistake</asp:ListItem>
                                            <asp:ListItem>Need to change shipping address</asp:ListItem>
                                            <asp:ListItem>Item price/shipping cost is high</asp:ListItem>
                                            <asp:ListItem>My reason is not listed</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="plese select reason"
                                            ControlToValidate="ddlreasoncancel" ForeColor="Red" InitialValue="select reason" ValidationGroup="ordcan"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 170px; text-align: right;">Comments :
                                    </td>
                                    <td style="text-align: left; padding: 10px 0 0 10px;">
                                        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" Width="515px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="text-align: left; padding: 10px;">
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn--black btn--md" OnClick="btnClose_Click" />
                                        <asp:Button ID="btnConfirmCancellation" runat="server"
                                            Text="Confirm Cancellation" OnClick="btnConfirmCancellation_Click"
                                            CssClass="btn btn--black btn--md" ValidationGroup="ordcan" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
