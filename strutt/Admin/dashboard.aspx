<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="dashboard.aspx.cs" Inherits="strutt.Admin.dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
     <!-- Page Header Start -->
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5" >Dashboard</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item active"><span>Dashboard</span></li>
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
    <!-- Page Header End --> 

     <!-- Main Content Start -->
    <section class="main--content">
      <div class="row gutter-20">
        <div class="col-md-8">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-shopping-cart  text-blue"></i>
                <h3 class="summary--stats text-green h4">Total orders</h3>
                    <div class="col-md-12 pad-5 " >
                         <div class="row ">
                             <div class="col-md-4 pad-5 " >
                                      <p class="miniStats--Caption text-dark ">This Month</p>
                                      <p class="miniStats--num summary--stats  text-green"><asp:Literal  ID="lbl_ord" runat="server" /></p>
                             </div>
                             <div class="col-md-3 pad-5 " >
                                      <p class="miniStats--caption text-dark ">Weekly(M-S)</p>
                                      <p class="miniStats--num summary--stats text-green"><asp:Literal  ID="lbl_ord_weekly" runat="server" /></p>
                                 </div>
                             <div class="col-md-3 pad-5 ">
                                      <p class="miniStats--caption text-dark ">Yesterday</p>
                                      <p class="miniStats--num summary--stats text-green"><asp:Literal  ID="lbl_order_yesterday" runat="server" /></p>
                                 </div>
                             <div class="col-md-2 pad-5 ">
                                      <p class="miniStats--caption text-dark ">Today</p>
                                      <p class="miniStats--num summary--stats text-green"><asp:Literal  ID="lbl_ord_today" runat="server" /></p>
                                 </div>
                         </div>
                    </div>
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
        <div class="col-md-4">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-times-circle-o  text-green"></i>
                <h3 class="summary--stats text-green h4">Total cancellations</h3>
                  <div class="col-md-12 pad-5 ">
                      <div class="row">
                          <div class="col-4 pad-5 ">
                              <p class="miniStats--caption text-dark ">Total</p>
                              <p class="miniStats--num summary--stats text-green"><asp:Literal  ID="lbl_total_cancellation" runat="server"/></p>
                          </div>
                          <div class="col-4 pad-5 ">
                              <p class="miniStats--caption text-dark ">Monthly</p>
                              <p class="miniStats--num summary--stats text-green"><asp:Literal  ID="lbl_cancellation" runat="server" /></p>
                         </div>
                          <div class="col-4 pad-5 ">
                              <p class="miniStats--caption text-dark ">Today</p>
                              <p class="miniStats--num summary--stats text-green"><asp:Literal  ID="lbl_cancellation_today" runat="server" /></p>
                         </div>
                     </div>
                   </div>
                 </div>     
             </div>
           </div>
            <!-- Mini Stats Panel End --> 
         </div>
        <div class="col-md-8">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fas fa-shopping-cart text-orange"></i>
                <h3 class="summary--stats text-orange h4">Total Value of orders(₹)</h3>
                  <div class="col-md-12 pad-5 " >
                         <div class="row ">
                             <div class="col-md-4 pad-5 " >               
                                  <p class="miniStats--caption text-dark ">This Month</p>
                                  <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_monthly_value" runat="server" /></p>
                                 </div>
         
                             <div class="col-md-3 pad-5 " >
                                      <p class="miniStats--caption text-dark ">Weekly(M-S)</p>
                                      <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_weekly_value" runat="server" /></p>
                             </div>
         
                             <div class="col-md-3 pad-5 ">
                                      <p class="miniStats--caption text-dark ">Yesterday</p>
                                  <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_yesterday_value" runat="server" /></p>
                             </div>
                             <div class="col-md-2 pad-5 ">
                                      <p class="miniStats--caption text-dark ">Today</p>
                                  <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_today_value" runat="server" /></p>
                             </div>
                         </div>
                    </div>
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
        <div class="col-md-4">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-undo text-orange"></i>
                <h3 class="summary--stats text-orange h4">Total RTO</h3>
                  <div class="col-md-12 pad-5 ">
                      <div class="row">
                          <div class="col-4 pad-5">
                              <p class="miniStats--caption text-dark ">Total</p>
                              <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_total_rto" runat="server" /></p>
                          </div>
                          <div class="col-4 pad-5">
                              <p class="miniStats--caption text-dark ">Monthly</p>
                              <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_Monthly_rto" runat="server" /></p>
                          </div>
                          <div class="col-4 pad-5">
                              <p class="miniStats--caption text-dark ">Today</p>
                              <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_Today_rto" runat="server" /></p>
                          </div>
                      </div>
                  </div>
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
        <div class="col-md-8">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-money text-green"></i>
                <h3 class="summary--stats text-blue h4">Prepaid Vs COD Payments(₹) </h3>  
                  <%--<div class ="col-md-12">
                      <div class="row">
                          <div class ="col-md-4">
                                <p class="miniStats--caption text-dark "><center>Last 30 Days</center> </p>
                          </div>
                          <div class="col-md-4"></div>
                          <div class ="col-md-4">
                              <p class="miniStats--caption text-green">Today</p>
                          </div>
                      </div>
                  </div>--%>
                  
                  
                  <div class="col-md-12 pad-5 " >
                         <div class="row ">
                             <div class="col-md-4 pad-5" >
                                      <p class="miniStats--caption text-dark ">Prepaid<small>(This Month)</small></p>
                                      <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_prepaid" runat="server" /></p>
                                 </div>
                                 <div class="col-md-3 pad-5 " >
                                      <p class="miniStats--caption text-dark ">COD<small>(This Month)</small></p>
                                      <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_COD" runat="server" /></p>
                                 </div>
                                <div class="col-md-3 pad-5 " >
                                      <p class="miniStats--caption text-dark ">Prepaid<small>(ToDay)</small></p>
                                      <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_today_Prepaid" runat="server" /></p>
                                 </div>
                                <div class="col-md-2 pad-5 " >
                                      <p class="miniStats--caption text-dark ">COD<small>(ToDay)</small></p>
                                      <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_today_COD" runat="server" /></p>
                                 </div>
                            </div>
                    </div>
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
        <div class="col-md-4">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fas fa-truck  text-blue"></i>
                <h3 class="summary--stats text-blue h4">Total Delivered </h3>
                  <div class="col-md-12 pad-5 ">
                      <div class="row">
                          <div class="col-4 pad-5 ">
                              <p class="miniStats--caption text-dark ">Total</p>
                              <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_total_delivered" runat="server" /></p>
                          </div>
                          <div class="col-4 pad-5 ">
                              <p class="miniStats--caption text-dark ">Monthly</p>
                              <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_monthly_delivered" runat="server" /></p>
                          </div>
                          <div class="col-4 pad-5 ">
                              <p class="miniStats--caption text-dark ">Today</p>
                              <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_today_delivered" runat="server" /></p>
                          </div>
                      </div>
                  </div>
                
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div> 
        <div class="col-md-4">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-user-circle-o  text-blue"></i>
                <h3 class="summary--stats text-blue h4">New users</h3>
                  <div class="col-md-12 pad-5 ">
                      <div class="row">
                          <div class="col-md-6 pad-5 " >               
                                  <p class="miniStats--caption text-dark ">Total</p>
                                  <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_total_user" runat="server" /></p>
                          </div>
                          <div class="col-md-6 pad-5 " >               
                                  <p class="miniStats--caption text-dark ">Monthly</p>
                                  <p class="miniStats--num text-blue"><asp:Literal  ID="lbl_totol_user" runat="server" /></p>
                          </div>
                      </div>
                  </div>
                
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
        <div class="col-md-4 pad-5 ">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-check-square-o  text-orange"></i>
                <h3 class="summary--stats text-orange h4">Oldest Confirmed/Pending Order Date </h3>
                  <p class="miniStats--caption text-dark ">Monthly</p>
                <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_oldest_confirmed" runat="server" /></p>
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
        <div class="col-md-4 pad-5">
          <div class="panel"> 
            <!-- Mini Stats Panel Start -->
            <div class="miniStats--panel">
              <div class="miniStats--body"> <i class="miniStats--icon fa fa-truck  text-orange"></i>
                <h3 class="summary--stats text-orange h4">Oldest Dispatched</h3>
                  <p class="miniStats--caption text-dark ">Monthly</p>
                <p class="miniStats--num text-orange"><asp:Literal  ID="lbl_oldestdispach" runat="server" /></p>
              </div>
            </div>
            <!-- Mini Stats Panel End --> 
          </div>
        </div>
          
          
          <div class="col-md-12">
              <asp:UpdatePanel ID="updt" runat="server">
                 <ContentTemplate>
              <div class="panel">
                <div class="panel-heading">
                  <h3 class="panel-title">Total orders and sale value category wise</h3>
                    <div class="dropdown">
                        <%--<button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown" aria-expanded="false"> <i class="fa fa-ellipsis-v"></i> </button>--%>
                        
                            <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="btn-link dropdown-toggle"  AutoPostBack="true"  OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" ClientIDMode="Static">
                            </asp:DropDownList>
                                <asp:Panel ID="pnl_customMonth" runat="server" Visible ="false" >
                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass=" txt05" Width="100px" autocomplete="off" Visible="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvfromdate" runat="server" ErrorMessage="Please enter from date"
                                        ControlToValidate="txtfromdate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                   
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" txt05" Width="100px" autocomplete="off" Visible="false" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ErrorMessage="Please enter to date"
                                        ControlToValidate="txttodate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </asp:Panel>
                           
                      </div>
                  </div>
             <div class="row">
             <asp:Repeater ID="rptCategoryTotal" runat="server">
              <ItemTemplate>
                <div class="col-xl-4 col-md-4">
                    <!-- Mini Stats Panel Start -->
                    <div class="miniStats--panel">
                      <div class="miniStats--body"> <i class="miniStats--icon text-green"></i>
                        <p class="miniStats--caption text-green">Total Quantity/Sales Price of</p>
                  
                  
                        <h3 class="miniStats--title h4">
                             <%# Eval("menu_name")%>

                        </h3>
                        <p class="miniStats--num text-green">
                            <%# Eval("totalCount")%> / ₹<%# Eval("totalPrice")%></p>
                      </div>
                    </div>
                    <!-- Mini Stats Panel End --> 
                </div>
              </ItemTemplate>
            </asp:Repeater>
            </div>
            </div>  
          </ContentTemplate>
         </asp:UpdatePanel>
       </div>

        <div class="col-xl-6 col-md-6">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">Monthly Order Earning </h3>  
            </div>
            <div class="panel-chart"> 
              <!-- Morris Area Chart 01 Start -->
              <div id="morrisAreaChart01" class="chart--body area--chart style--1"></div>
              <%--<div id="chartdiv" class="chart--body area--chart style--1"></div>--%>

              <!-- Morris Area Chart 01 End -->
              
              <div class="chart--stats style--1">
                <ul class="nav">
                  
                  <li data-overlay="1 green">
                    <p class="amount">₹<asp:Literal ID="lbl_chart_amount" runat="server" /></p>
                    <p> <span class="label">Revenue </span></p>
                  </li>
                  <%--<li data-overlay="1 green">
                    <p class="amount">1,340</p>
                    <p> <span class="label">Number Of Orders</span> <span class="text-green"><i class="fa fa-long-arrow-alt-up"></i>2.25%</span> </p>
                  </li>--%>
                </ul>
              </div>
            </div>
          </div>
        </div>
        <div class="col-xl-6 col-md-6">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">Monthly Order Quantity</h3>
            </div>
            <div class="panel-chart"> 
              <!-- Morris Area Chart 01 Start -->
              <div id="morrisAreaChart02" class="chart--body area--chart style--1"></div>
              <!-- Morris Area Chart 01 End -->
              
              <div class="chart--stats style--1">
                <ul class="nav">
                  <li data-overlay="1 orange">
                    <p class="amount"><asp:Literal ID="lbl_chart_Quantity" runat="server" /></p>
                    <p> <span class="label">Quantity</span></p>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
        
      <%--  <div class="col-xl-6 col-md-6">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">Sales Progress</h3>
              <div class="dropdown">
                <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-ellipsis-v"></i> </button>
                <ul class="dropdown-menu">
                  <li><a href="#">This Week</a></li>
                  <li><a href="#">Last Week</a></li>
                </ul>
              </div>
            </div>
            <div class="panel-chart"> 
              <!-- Morris Line Chart 01 Start -->
              <div id="morrisLineChart01" class="chart--body line--chart style--1"></div>
              <!-- Morris Line Chart 01 End --> 
            </div>
          </div>
        </div>
        <div class="col-xl-3 col-md-6">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">Monthly Traffic</h3>
              <div class="dropdown">
                <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-ellipsis-v"></i> </button>
                <ul class="dropdown-menu">
                  <li><a href="#"><i class="fa fa-sync"></i>Update Data</a></li>
                  <li><a href="#"><i class="fa fa-cogs"></i>Settings</a></li>
                  <li><a href="#"><i class="fa fa-times"></i>Remove Panel</a></li>
                </ul>
              </div>
            </div>
            <div class="panel-chart"> 
              <!-- Morris Line Chart 02 Start -->
              <div id="morrisLineChart02" class="chart--body line--chart style--2"></div>
              <!-- Morris Line Chart 02 End -->
              
              <div class="chart--stats style--3">
                <ul class="nav">
                  <li>
                    <p data-trigger="sparkline" data-type="bar" data-width="5" data-height="38" data-color="#2bb3c0">0,5,9,7,12,15,12,5</p>
                    <p><span class="label">Total Visitors</span></p>
                    <p class="amount">12,202</p>
                  </li>
                  <li>
                    <p data-trigger="sparkline" data-type="bar" data-width="5" data-height="38" data-color="#e16123">0,15,12,5,5,9,7,12</p>
                    <p><span class="label">Total Sales</span></p>
                    <p class="amount">25,051</p>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
        <div class="col-xl-3">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">
                <select name="filter" data-trigger="selectmenu" data-minimum-results-for-search="-1">
                  <option value="top-search">Top Search</option>
                  <option value="average-search">Average Search</option>
                </select>
              </h3>
              <div class="dropdown">
                <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-ellipsis-v"></i> </button>
                <ul class="dropdown-menu">
                  <li><a href="#"><i class="fa fa-sync"></i>Update Data</a></li>
                  <li><a href="#"><i class="fa fa-cogs"></i>Settings</a></li>
                  <li><a href="#"><i class="fa fa-times"></i>Remove Panel</a></li>
                </ul>
              </div>
            </div>
            <div class="panel-body"> 
              <!-- Vector Map Start -->
              <div class="vector--map" data-trigger="jvectorMap" data-map-selected='["US", "CA", "MX", "GT", "HN", "BZ", "SV", "NI", "CR", "BS", "CU", "JM", "HT", "DO", "PR", "PA", "CO", "VE", "TT", "GY", "SR", "GL", "EC", "PE", "BR", "BO", "PY", "CL", "AR", "UY", "FK"]'></div>
              <!-- Vector Map End -->
              
              <div class="map--stats">
                <table class="table">
                  <tr>
                    <td>United States</td>
                    <td>65%</td>
                  </tr>
                  <tr>
                    <td>United Kingdom</td>
                    <td>15%</td>
                  </tr>
                  <tr>
                    <td colspan="2"><a href="#" class="btn-link">View All</a></td>
                  </tr>
                </table>
              </div>
            </div>
          </div>
        </div>--%>

        <div class="col-xl-12">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">
                Recent Orders
              </h3>
              <div class="dropdown">
                <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-ellipsis-v"></i> </button>
                <ul class="dropdown-menu">
                  <li><a href="orderstatus.aspx">View More</a></li>
                </ul>
              </div>
            </div>
            <div class="panel-body">
              <div class="table-responsive">
                  <asp:Panel ID="pneltest" runat="server"  Height="300px" ScrollBars="Vertical">
                <table class="table style--2">
                  <asp:Repeater ID="repRecentProduct" runat="server" OnItemDataBound="repRecentProduct_ItemDataBound" >
                      <HeaderTemplate>
                        <thead>
                        <tr>
                          <th>Product Image</th>
                          <th>Order ID</th>
                          <th>Customer Name</th>
                          <th>Price</th>
                          <th>Quantity</th>
                          <th>Tracking No.</th>
                          <th>Status</th>
                        </tr>
                      </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                    <!-- Table Row Start -->
                    <tr>
                      <td><img src='<%# "../images/Product/Thumb/"+ Eval("productImage")%>' alt="" height="50px" width="50px"></td>
                      <td><%# Eval("order_id") %></td>
                      <td><a href='<%#"searchorderdetails.aspx?id="+Eval("order_id")%>' class="btn-link"><%# Eval("customerName") %></a></td>
                      <td><%# Eval("salesprise") %></td>
                      <td><%# Eval("quantity") %></td>
                      <td><span class="text-muted">#<%# Eval("trackingId") %></span></td>
                      <td><asp:label ID="lbl_status" runat="server" Text='<%# Eval("status") %>' /></td>
                    </tr>
                    <!-- Table Row End --> 
                  </tbody>
                    </ItemTemplate>
                  </asp:Repeater>
                </table>    
                      </asp:Panel>
              </div>
            </div>
          </div>
        </div>
        
        <div class="col-xl-6">
          <div class="panel">
            <div class="profile--panel">
              <div class="img-wrapper" data-bg-img="images/banner1.jpg" >
                <div class="img online"><asp:Image ID="img1" runat="server"   alt="" class="rounded-circle"/>  <%--<img src="assets/img/avatars/01_150x150.png" alt="" class="rounded-circle"> --%></div>
              </div>
              <div class="name">
                <h3 class="h3"><asp:Label runat="server" ID ="lbl_uname"/></h3>
              </div>
              <div class="role">
                <p><asp:Label runat="server" ID ="lbl_type"/></p>
              </div>
              <%--<div class="action"> <a href="#" class="btn btn-info">Product</a> </div>
                <div class="action"> <a href="#" class="btn btn-info">Orders</a> </div>--%>
                <div class="action"> <a href="manageproduct.aspx" class="btn btn-sm btn-info">Product</a> <a href="orderstatus.aspx" class="btn btn-sm btn-info">Orders</a> </div>
            </div>
          </div>
        </div>
        <div class="col-xl-3 col-md-6">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">Feeds &amp; Activities</h3>
              <div class="dropdown">
                <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-ellipsis-v"></i> </button>
                <ul class="dropdown-menu">
                  <li><a href="feedback.aspx"><i class="fa fa-sync"></i>More Feeds</a></li>
                </ul>
              </div>
            </div>
            <div class="feeds-panel">
              <ul class="nav">
                  <asp:Panel ID="Panel1" runat="server"  Height="275px" ScrollBars="Vertical">
                  <asp:Repeater ID="repfeedback" runat="server">
                      <ItemTemplate>
                            <li> <span class="time"><%# Eval("createDate","{0:dd/MM/yyyy}") %></span> <i class="fa fa-comment text-white bg-red"></i> <span class="text"><a><%# Eval("customerName") %></a> <a><%# Eval("review") %></a></span> </li>
                       </ItemTemplate>
                      </asp:Repeater>
                       </asp:Panel>
                 </ul>
            </div>
          </div>
        </div>
        <div class="col-xl-3 col-md-6">
          <div class="panel">
            <div class="panel-heading">
              <h3 class="panel-title">Comments</h3>
              <div class="dropdown">
                <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-ellipsis-v"></i> </button>
                <ul class="dropdown-menu">
                  <li><a href="feedback.aspx"><i class="fa fa-sync"></i>More Comments</a></li>
                </ul>
              </div>
            </div>
            <div class="comments-panel">
              <ul>
                  <asp:Panel ID="Panel2" runat="server"  Height="270px" ScrollBars="Vertical">
                    <asp:Repeater ID="repComment" runat="server">
                        <ItemTemplate>
                            <li>
                              <div class="img"> <img src="assets/img/avatars/02_40x40.png" alt="" class="rounded-circle"> </div>
                              <div class="info">
                                <h3 class="h6"><%# Eval("fullName") %></h3>
                                <p><%# Eval("comments") %></p>
                                <%--<div class="action"> <span class="status text-orange">Pending</span> <a href="#" class="btn btn-sm btn-info">Delete</a> </div>--%>
                              </div>
                            </li>
                            </ItemTemplate>
                    </asp:Repeater>
                 </asp:Panel>
              </ul>
              <%--<div class="nav"> <a href="managereview.aspx" class="next btn-link">Older Comments <i class="fa fa-angle-double-right"></i></a> </div>--%>
            </div>
          </div>
        </div>
      </div>
    </section>  
    <!-- Main Content End --> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
     <script type="text/javascript" src="https://www.google.com/jsapi"></script>  
    <script type="text/javascript" language="javascript" >  
        var rowData;
        $(function () {
            $.getJSON("https://canvasjs.com/services/data/datapoints.php?xstart=1&ystart=10&length=100&type=json"),
            //debugger;
            $.ajax({  
                type: 'POST',  
                dataType: 'json',  
                contentType: 'application/json;chartset=utf-8',
                url: 'dashboard.aspx/GetChartData',
                

                success: function (response) {  
                    //alert(response.d);
                    rowData = response.d;
                },
                complete: function (response) {
                    monthlyEarningChart();
                    monthlyEarningChart2();
                },
                error: function (jqXHR, exception) {
                    //debugger;
                    //alert(jqXHR.status);
                    //alert(exception);
                }  
            });  
        })  
  
       
        function monthlyEarningChart() {
            //alert(rowData);
            var result = JSON.parse(rowData);
            new Morris.Area({
                element: "morrisAreaChart01",
                data: result,
                xkey: "monthName",
                ykeys: ["totalSum"],
                labels: ["Total"],
                lineColors: ["#009378"],
                preUnits: "₹",
                parseTime: false,
                pointSize: 0,
                lineWidth: 0,
                gridLineColor: "#eee",
                resize: true,
                hideHover: true,
                behaveLikeLine: true
            });
        }
        
        function monthlyEarningChart2() {
            //alert(rowData);
            var result = JSON.parse(rowData);
            new Morris.Area({
                element: "morrisAreaChart02",
                data: result,
                xkey: "monthName",
                ykeys: ["totalQuantity"],
                labels: ["Quantity"],
                lineColors: ["#e16123"],
                preUnits: "",
                parseTime: false,
                pointSize: 0,
                lineWidth: 0,
                gridLineColor: "#eee",
                resize: true,
                hideHover: true,
                behaveLikeLine: true
            });
        }
    </script>
</asp:Content>
