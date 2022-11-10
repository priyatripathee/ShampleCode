<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="searchorderdetails.aspx.cs" Inherits="strutt.Admin.searchorderdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .newbtn {
            float: right;
        }

        .strike {
            text-decoration: line-through;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-6">
                            <!-- Page Title Start -->
                            <h2 class="page--title h5">Order View</h2>
                            <!-- Page Title End -->

                            <ul class="breadcrumb">
                                <li class="breadcrumb-item">Orders Status</a></li>
                                <li class="breadcrumb-item active"><a href="searchorderdetails.aspx"><span>Order View</span></a></li>
                            </ul>
                        </div>

                        <div class="col-lg-6">
                            <!-- Summary Widget Start -->
                             <div class="summary--widget">
                      <div class="summary--item">
                        <p class="summary--title">This Month</p>
                        <p class="summary--stats text-green">₹<asp:Literal  ID="lbl_curentmonth" runat="server" /></p>
                      </div>
                      <div class="summary--item">
                        <p class="summary--title">Last Month</p>
                        <p class="summary--stats text-orange">₹<asp:Literal  ID="lbl_lastmonth" runat="server" /></p>
                      </div>
                </div>
                            <!-- Summary Widget End -->
                        </div>
                    </div>
                </div>
            </section>
    <section class="main--content">
                <div class="panel">
                    <!-- Records Header Start -->
                     <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                    <div class="records--header">
                        <div class="col-md-4">
                        <div class="title fa-shopping-bag">
                            <h3 class="h3">Order View <a href="#" class="btn btn-sm btn-outline-info">View Order Details</a></h3>
                        </div>
                        </div>
                        <div class="col-md-4">
                            <%--<label>
                                STR10001 <span class="required"></span>
                            </label>--%>
                            <div>
                                <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="form-control" placeholder="STR10001-OrderNo"></asp:TextBox>
                            </div>
                            </div>
                             <div class="col-md-2">
                                <div class="title">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-rounded btn-success"
                                        OnClick="btnSearch_Click" ValidationGroup="prct" />
                                </div>
                                <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="* required"
                                ControlToValidate="txtOrderNumber" ForeColor="Red" ValidationGroup="prct">
                            </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtOrderNumber"
                                ForeColor="Red" ValidationExpression="\d+" Display="Static" EnableClientScript="true"
                                ValidationGroup="prct" ErrorMessage="Please enter numbers only" runat="server" />
                             </div>
                            <div class="col-md-2">
                                <div class="title ">
                                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-rounded btn-warning" ValidationGroup="prct"
                                    OnClick="btnback_Click" />
                               </div>
                           </div>
                    </div>
                    <!-- Records Header End -->
                </div>
                
                <div class="panel">

                    <!-- View Order Start -->
                    <div class="records--body">
                        <div class="title">
                            <h6 class="h6">Order STR10001<asp:Label ID="lblOrder" runat="server"></asp:Label><span class="text-lightergray"> - <asp:Label ID="lblOrderDateTime" runat="server"></asp:Label></span></h6>
                        </div>

                        <!-- Tabs Nav Start -->
                        <ul class="nav nav-tabs">
                            <li class="nav-item">
                                <a href="#tab01" data-toggle="tab" class="nav-link active">Overview</a>
                            </li>
                            <li class="nav-item">
                                <a href="#tab02" data-toggle="tab" class="nav-link">Order Details</a>
                            </li>
                        </ul>
                        <!-- Tabs Nav End -->

                        <!-- Tab Content Start -->
                        <div class="tab-content">
                            <!-- Tab Pane Start -->
                            <div class="tab-pane fade show active" id="tab01">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h4 class="subtitle">Order Information</h4>

                                        <table class="table table-simple">
                                            <tbody>
                                                <tr>
                                                    <td> Customer Id :</td>
                                                    <th><a href="#" class="btn-link"><asp:Label ID="lblCustomerId" runat="server"></asp:Label></a></th>
                                                </tr>
                                                <tr>
                                                    <td>Username / Email : </td>
                                                    <th>  <asp:Label ID="lblEmail" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td> Customer Name : </td>
                                                    <th> <asp:Label ID="lblCustomerName" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td> Contact No :</td>
                                                    <th> <asp:Label ID="lblContactNo" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Address :</td>
                                                    <th> <asp:Label ID="lblAddress" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>City : </td>
                                                    <th><asp:Label ID="lblCity" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>State :</td>
                                                    <th> <asp:Label ID="lblState" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Pincode :</td>
                                                    <th>  <asp:Label ID="lblPinCode" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td> Payment Through</td>
                                                    <%--<th> <asp:Label ID="lblpaymentthrough" runat="server"></asp:Label></th>--%>
                                                     <td>
                                                        <asp:DropDownList ID="ddlPayment" runat="server" CssClass="form-control  txt05" style="width:300px;" >
                                                            <asp:ListItem>Prepaid</asp:ListItem>
                                                            <asp:ListItem>COD</asp:ListItem>
                                                            <asp:ListItem>Razorpay</asp:ListItem>
                                                            <asp:ListItem>X-Online</asp:ListItem>
                                                            <asp:ListItem> </asp:ListItem>
                                                       </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"  CssClass="btn btn-rounded btn-primary"> </asp:Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="col-md-6">
                                        <h4 class="subtitle">Other Information</h4>

                                        <table class="table table-simple">
                                            <tbody>
                                                <tr>
                                                    <td>Checkout Options</td>
                                                    <th><asp:Label ID="lblCheckOption" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Coupon Code:</td>
                                                    <th><asp:Label ID="lblCouponCode" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Freight :</td>
                                                    
                                                    <th> <asp:Label ID="lblFreight" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Shipping Charge : </td>
                                                    <th><asp:Label ID="lblshippingprice" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                     <th>Order Id: </th>
                                                    <td>STR10001<asp:Label ID="lblOrderId" runat="server"></asp:Label></td>
                                                   
                                                </tr>
                                                 <tr>
                                                    <td>Order Date :</td>
                                                    <th><asp:Label ID="lblOrderDate" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Order Status :</td>
                                                    <th><asp:Label ID="lblOrdereStatus" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Confirmed Date:</td>
                                                    <th><asp:Label ID="lblConformedDate" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>Dispatched Date:</td>
                                                    <th><asp:Label ID="lblDispatchedDate" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td> Packed Date :</td>
                                                    <th><asp:Label ID="lblPackedDate" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>  Delivered Date:</td>
                                                    <th><asp:Label ID="lblDeliveredDate" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td>  Ship Via:</td>
                                                    <th><asp:Label ID="lblShipVia" runat="server"></asp:Label></th>
                                                </tr>
                                                <tr>
                                                    <td> Track Id:</td>
                                                    <th><asp:Label ID="lblShipId" runat="server"></asp:Label></th>
                                                    </tr>
                                                    <tr>
                                                    <td> Message :</td>
                                                    <th><asp:Label ID="lblMessage" runat="server"></asp:Label></th>

                                                </tr>
                                                <tr>
                                                    <td> Reason for Cancellation :</td>
                                                    <th><asp:Label ID="lblReasonforCancellation" runat="server"></asp:Label></th>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <!-- Tab Pane End -->

                            <!-- Tab Pane Start -->
                            <div class="tab-pane fade" id="tab02">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h4 class="subtitle">Order Information</h4>
                                        <div class="invoice--order">
                                          <div class="table-responsive">
                                            <asp:Panel ID="pneltest" runat="server"  Width="100%" ScrollBars="Horizontal">
                                             <table class="table style--2" width="100%"  cellpadding="1" cellspacing="1">
                                <thead>
                                    <tr>
                                        <th>Product ID</th>
                                        <th>Description</th>
                                        <th>Order Status</th>
                                        <th>Custom Bag Charges Text</th>
                                        <th>Product Id</th>
                                        <th>Quantity</th>
                                        <th>Price</th>
                                        <%--<th>Total</th>--%>
                                    </tr>
                                </thead>
                                 <asp:Repeater ID="rptSearchOrderDetails" runat="server">
                                        <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td><%# Container.ItemIndex +1 %></td>
                                        <td><a href='../productdetails.aspx?proid=<%# Eval("product_id")%>'  target="_blank" class='<%# Eval("order_status").Equals("Cancelled")?"strike":"" %>'>
                                                        <strong><%# Eval("product_name")%></strong></a>
                                                    &nbsp;&nbsp;&nbsp; 
                                                    <a href='../productdetails.aspx?proid=<%# Eval("product_id")%>' target="_blank" >
                                                        <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/Product/Thumb/" + Eval("thumb_image") %>'
                                                            class="img-thumbnail" Width="60" Height="60" />
                                                    </a></td>
                                        <td><%# Eval("order_status")%></td>
                                        <td> <%# Eval("custom_bag_param")%><br /><%# Eval("custom_bag_price")%><br />
                                                     <a href='../custombag.aspx?proid=<%# Eval("product_id")%>&orddetid=<%# Eval("order_detail_id")%>' target="_blank"  class='<%# String.IsNullOrEmpty(Eval("custom_bag_param").ToString())? "hidden":"btn btn-info btn-xs" %>' >
                                                        <strong>Preview</strong></a></td>
                                        <td> <%# Eval("product_id")%></td>
                                        <td> <%# Eval("quantity")%></td>
                                        <td>Rs.<%# Eval("total_price")%></td>
                                    </tr>
                                </tbody>
                                </ItemTemplate>
                                </asp:Repeater>
                                           
                                        <tr>
                                        <th colspan="6" class="text-right">
                                            Total Price:
                                        </th>
                                        <td>
                                        Rs.<asp:Literal ID="lblTotalPrice" runat="server"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="6" class="text-right">
                                            Discount:
                                        </th>
                                        <td>
                                         Rs.<asp:Literal ID="lbldiscount" runat="server"/>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th colspan="6" class="text-right">
                                            Shipping Charge:
                                        </th>
                                        <td>
                                        Rs.<asp:Literal ID="Literal1" runat="server"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="6" class="text-right">
                                            Custom Bag Charge:
                                        </th>
                                        <td>
                                        Rs.<asp:Literal ID="lblCustomeBagChages" runat="server"/>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th colspan="6" class="text-right">
                                           Freight:
                                        </th>
                                        <td>
                                         Rs.<asp:Literal ID="Literal2" runat="server"/>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th colspan="6" class="text-right">
                                           Payable:
                                        </th>
                                        <th>
                                         Rs.<asp:Literal ID="lblPayableAmount" runat="server"/>
                                        </th>
                                    </tr>
                            </table>
                                            </asp:Panel>
                                       </div>
                                    </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Tab Pane End -->
                        </div>
                        <!-- Tab Content End -->
                    </div>
                    <!-- View Order End -->
                </div>
            </section>
</asp:Content>
