<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="orderstatus.aspx.cs" Inherits="strutt.Admin.orderstatus" %>

<%@ Import Namespace="BLL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .newbtn {
            float: right;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 3px;
        }

        .greenbtn {
            background-color: #5cb85c;
            border-color: #5cb85c;
        }
    </style>
    
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600&amp;display=swap" rel="stylesheet">
    
    <script type="text/javascript" src="js/custom.min.js"></script>
    
    <script src="js/bundle.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Order Status</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Order Status</li>
              <li class="breadcrumb-item active"><a href="orderstatus.aspx"><span>Order</span></a></li>
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
                        <div>
                          <div class="col-md-12">
                         <%--<div class="title fa-shopping-bag">
                            <h3 class="h3">Orders Status<a href="#" class="btn btn-sm btn-outline-info">Manage Orders</a></h3>
                            
                        </div>--%>
                              <p><asp:Label ID="lbl_total_records" runat="server" CssClass="msg1" ForeColor="Red"></asp:Label></p>
                        </div>
                          <div class="actions">
                            <div class="col-md-4">
                             <%--<asp:Label runat="server"  Text="Date Range" />--%>
                                <asp:UpdatePanel ID="updt" runat="server">
                                <ContentTemplate>
                                <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control txt05"  AutoPostBack="true"  OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" ClientIDMode="Static">
                                </asp:DropDownList>
                                    <asp:Panel ID="pnl_customMonth" runat="server" Visible ="false" >
                                    <%--<label>
                                          From&nbsp;Date <span class="required"></span>
                                    </label>--%>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" txt05" Width="100px" autocomplete="off" Visible="false" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvfromdate" runat="server" ErrorMessage="Please enter from date"
                                            ControlToValidate="txtfromdate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                   <%-- <label>
                                        To Date<span class="required"></span>
                                    </label>--%>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass=" txt05" Width="100px" autocomplete="off" Visible="false" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ErrorMessage="Please enter to date"
                                            ControlToValidate="txttodate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </asp:Panel>
                                 </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                     <%--<label>Customer name </label>--%>
                                   <asp:TextBox ID="txtSearchcustomername" runat="server" CssClass="form-control txt05" placeholder="Customer name " autocomplete="off"></asp:TextBox>
                                </div>
                            <div class="col-md-2">
                                 <%--<label > Phone No </label>--%>
                            <asp:TextBox ID="txtSerachphoneno" runat="server" CssClass="form-control txt05" autocomplete="off" placeholder="Phone No " ></asp:TextBox>
                                

                                 </div>
                            <div class="col-md-2">
                             <%--<label >Status</label>--%>
                                <asp:DropDownList ID="ddlStatusOrder" runat="server" CssClass="form-control txt05" Width="100px">
                                    <asp:ListItem>All Status</asp:ListItem>
                                    <asp:ListItem>inprogress</asp:ListItem>
                                    <asp:ListItem>Confirmed</asp:ListItem>
                                    <asp:ListItem>New Order</asp:ListItem>
                                    <asp:ListItem>Scheduled</asp:ListItem>
                                    <asp:ListItem>Packed</asp:ListItem>
                                    <asp:ListItem>Dispatched</asp:ListItem>
                                    <asp:ListItem>Delivered</asp:ListItem>
                                    <asp:ListItem>Failed</asp:ListItem>
                                    <asp:ListItem>Cancelled</asp:ListItem>
                                    <asp:ListItem>RTO</asp:ListItem>
                                    <asp:ListItem>Returned</asp:ListItem>
                                </asp:DropDownList>
                                  </div>
                            <div class="col-md-2">
                                <%--<label >Deleted Records</label>--%>
                                   <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control txt05" Width="100px">
                                    <asp:ListItem>All Payment</asp:ListItem>
                                    <asp:ListItem>COD</asp:ListItem>
                                    <asp:ListItem>Online</asp:ListItem>
                                </asp:DropDownList>
                               
                                </div>
                         

                        <%--         <div class="col-md-2">
                           
                                    <%--Start: Added By Hetal Patel on 06-03-2020 for Edit and Delete multiple orders 
                                    <asp:HiddenField ID="hdnedit" runat="server" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit Order" BorderColor="#5bc0de" BackColor="#5bc0de" CssClass="btn btn-rounded btn-success" ValidationGroup="ord" OnClick="btnEdit_Click" />
                                </div>
                                      <div class="col-md-2">
                                        <asp:HiddenField ID="hdndelete" runat="server" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete Order" BorderColor="#5bc0de" BackColor="#5bc0de" CssClass="btn btn-rounded btn-warning" ValidationGroup="ord" OnClick="btnDelete_Click" />
                                <%--End: Added By Hetal Patel on 06-03-2020 for Edit and Delete multiple orders 
                                </div>
                            <div class="col-md-2">
                                          <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn btn-rounded btn-dark" OnClick="btnExport_Click" ValidationGroup="ord" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-rounded btn-block" OnClick="btnSearch_Click" ValidationGroup="ord" />
                               </div>
                                <div class="col-md-2">
                                 <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-rounded btn-primary" OnClick="btnReset_Click" ValidationGroup="ord" />   
                                </div>
                        --%>
                        
                        </div>
                        
                            <div class="form-group row" style="padding-top :10px">
                              <div class="col-md-6">
                                    <asp:HiddenField ID="hdnedit" runat="server" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit Order" BorderColor="#5bc0de" BackColor="#5bc0de" CssClass="btn btn-rounded btn-success" ValidationGroup="ord" OnClick="btnEdit_Click" />
                               
                                    <asp:HiddenField ID="hdndelete" runat="server" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete Order" BorderColor="#5bc0de" BackColor="#5bc0de" CssClass="btn btn-rounded btn-warning" ValidationGroup="ord" OnClick="btnDelete_Click" />
                                             
                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn btn-rounded btn-dark" OnClick="btnExport_Click" ValidationGroup="ord" />
                                  
                               </div>
                                 
                              <div class=" col-md-6 " style="text-align: right">     
                                       <asp:CheckBox runat="server" class="btn" ID="chkDeleteRecord" Text="Deleted Records" />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-rounded btn-success" OnClick="btnSearch_Click" ValidationGroup="ord" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-rounded btn-warning" OnClick="btnReset_Click" ValidationGroup="ord" />
                               </div>
                            </div>
                         </div>
                      </div>     
                    <!-- Records Header End -->
                </div>
               
    

    <%--<asp:UpdatePanel ID="uPanelReview" runat="server">
    <ContentTemplate>--%>
    <div class="panel"> 
                    <div class="table-responsive">
                        
                        <asp:GridView ID="grdOrderStatus" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="order_id" OnRowCancelingEdit="grdOrderStatus_RowCancelingEdit"
                            OnRowCommand="grdOrderStatus_RowCommand" OnRowDataBound="grdOrderStatus_RowDataBound"
                            OnRowDeleting="grdOrderStatus_RowDeleting" OnRowEditing="grdOrderStatus_RowEditing"
                            OnRowUpdating="grdOrderStatus_RowUpdating">
                            <RowStyle CssClass="griditem01" />
                            <Columns>
                                <%--Start: Added by Hetal Patel on 06-03-2020 for apply Edit functionality --%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkselect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--End: Added by Hetal Patel on 06-03-2020 for apply Edit functionality --%>
                                <asp:TemplateField HeaderText="Sr. No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="28px" CssClass="griditem02" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ord ID/Order Date">
                                    <ItemTemplate>
                                        <%# Eval("order_id")%><br />
                                        <%# Eval("OrdDate")%>
                                        

<%--                                        <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" 
                                            CommandArgument='<%# Bind("order_id") %>' ToolTip="View" CssClass="label label-info"><i class="fa fa-eye"></i> view</asp:LinkButton>--%>
                                    </ItemTemplate>

                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="48px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <%# Eval("user_name")%>
                                        <%# Eval("contact_number")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="State, City, Pincode">
                                    <ItemTemplate>
                                        <%# Eval("state")%>
                                        <%# Eval("city")%>
                                        <%# Eval("pin_code")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Left" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <%# Eval("quantity")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Price">
                                    <ItemTemplate>
                                        <%# Eval("price")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                <%--Start: Added by Hetal Patel: 03-03-2020 --%>
                                <asp:TemplateField HeaderText="Coupon Code">
                                    <ItemTemplate>
                                        <%# Eval("coupon_code")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="105px" />
                                </asp:TemplateField>
                                <%--End: Added by Hetal Patel: 03-03-2020 --%>
                                <%--<asp:TemplateField HeaderText="Ord Date">
                                    <ItemTemplate>
                                        <%# Eval("OrdDate")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="85px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Confirmed Date">
                                    <ItemTemplate>
                                        <%# Eval("CnfDate")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="105px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InProgress Date">
                                    <ItemTemplate>
                                        <%# Eval("InprogressDate")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="105px" />
                                </asp:TemplateField>
                                <%--Start: Commented by Hetal Patel: 03-03-2020 --%>
                                <%--<asp:TemplateField HeaderText="Packed Date">
                                    <ItemTemplate>
                                        <%# Eval("PkdDate")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="105px" />
                                </asp:TemplateField>--%>
                                <%--End: Commented by Hetal Patel: 03-03-2020 --%>

                                <asp:TemplateField HeaderText="Dispatched Date">
                                    <ItemTemplate>
                                        <%# Eval("DPDate")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivered Date">
                                    <ItemTemplate>
                                        <%# Eval("DLDate")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="105px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ship Via">
                                      <ItemTemplate>
                                        <asp:PlaceHolder ID="placeholder1" runat="server" Visible='<%# Convert.ToString(Eval("manifest_link")) == "" ? true : false %>'>
                                            <a href='<%#Helpers.GetUrltraking(DataBinder.Eval(Container.DataItem,"ship_via"), DataBinder.Eval(Container.DataItem,"ship_id"),null) %>' target="_blank">
                                                <%# Eval("ship_id")%></a>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="placeholder2" runat="server" Visible='<%# Convert.ToString(Eval("manifest_link")) == "" ? false : true %>'>
                                            <a href='<%#Helpers.GetUrltraking(DataBinder.Eval(Container.DataItem,"ship_via"), DataBinder.Eval(Container.DataItem,"ship_id"),"Picker") %>' target="_blank">
                                                <%# Eval("ship_id")%></a>
                                        </asp:PlaceHolder>
                                        <br />
                                        <%# Eval("ship_via")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditShipId" runat="server" Width="100px" Text='<%# Eval("ship_id")%>' CssClass="form-control"></asp:TextBox>
                                        <br></br>
                                        <asp:HiddenField ID="hfieldshipvia" runat="server" Value='<%#Eval("ship_via") %>' />
                                        <asp:DropDownList ID="ddlshipvia" runat="server" CssClass="form-control" Width="100px"
                                            DataTextField="ship_via" DataValueField="ship_via">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Delhivery</asp:ListItem>
                                            <asp:ListItem>FedEx</asp:ListItem>
                                            <asp:ListItem>DTDC</asp:ListItem>
                                            <asp:ListItem>Ecom Express</asp:ListItem>
                                            <asp:ListItem>Express Bees</asp:ListItem>
                                            <asp:ListItem>Gati</asp:ListItem>
                                            <asp:ListItem>bluedart</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Freight">
                                    <ItemTemplate>
                                        <%# Eval("freight")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditFreight" runat="server" CssClass="txt" Text='<%# Eval("freight")%>' Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Ord Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("order_status")%>' ></asp:Label>
                                        
                                        <asp:Label ID="lblCustom" runat="server" Text='<%# Eval("Custom")%>' ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hfieldOrderStatus" runat="server" Value='<%#Eval("order_status") %>' />
                                        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control" Width="135px"
                                            DataTextField="order_status" DataValueField="order_detail_id">
                                            <asp:ListItem>Select order status</asp:ListItem>
                                            <asp:ListItem>inprogress</asp:ListItem>
                                            <asp:ListItem>Confirmed</asp:ListItem>
                                            <asp:ListItem>Packed</asp:ListItem>
                                            <asp:ListItem>Scheduled</asp:ListItem>
                                            <asp:ListItem>Dispatched</asp:ListItem>
                                            <asp:ListItem>Delivered</asp:ListItem>
                                            <asp:ListItem>Failed</asp:ListItem>
                                            <asp:ListItem>Cancelled</asp:ListItem>
                                            <asp:ListItem>RTO</asp:ListItem>
                                            <asp:ListItem>Returned</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFOrderStatus" runat="server" ErrorMessage="*"
                                            ControlToValidate="ddlOrderStatus" InitialValue="Select order status" ForeColor="Red" ValidationGroup="prct">
                                        </asp:RequiredFieldValidator>

                                    </EditItemTemplate>
                                    <ItemStyle CssClass="griditem02" HorizontalAlign="Center" Width="155px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div class="d-flex">
                                            <div class="dropdown ms-auto">
                                                <a href="#" data-bs-toggle="dropdown" class="btn btn-floating" aria-haspopup="true" aria-expanded="false">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-three-dots" viewBox="0 0 16 16">
                                                      <path d="M3 9.5a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3z"/>
                                                    </svg>
                                                </a>
                                                <div class="dropdown-menu dropdown-menu-end" style="text-align: center;">

                                                    <asp:LinkButton ID="ibtnView" runat="server"  AlternateText="View" ToolTip="View" CssClass="label label-warning"
                                             PostBackUrl='<%#"~/Admin/searchorderdetails.aspx?id="+Eval("order_id")%>' style="margin-bottom: 5px;"><i class="fa fa-eye"></i> view</asp:LinkButton>
                                                    
                                                    <%-- Start: Replace asp:LinkButton with asp:HyperLink by Hetal Patel on 05-03-2020 to open invoice in new window --%>
                                        <%-- <asp:LinkButton ID="ibtnInvoice" runat="server" AlternateText="Invoice" ToolTip="Get Invoice" CssClass="btn btn-info btn-xs" 
                                            PostBackUrl='<%#"~/account/invoice.aspx?id="+Eval("order_id")%>'><i class="fa fa-file-o"></i> invoice</asp:LinkButton>--%>
                                         <asp:HyperLink ID="ibtnInvoice" runat="server" AlternateText="Invoice"  CssClass="label label-info"
                                                    NavigateUrl='<%#"~/account/invoice.aspx?id="+ BusinessEntities.security.Encryptdata(Eval("order_id").ToString())%>' Style="margin-bottom: 5px;" onclick="javascript:w= window.open(this.href,'Invoice','left=20,top=20,width=1000,height=700,toolbar=0,resizable=0');return false;">
                                              <i class="fa fa-file-o"></i>  invoice  </asp:HyperLink>
                                        <%-- End: Replace asp:LinkButton with asp:HyperLink by Hetal Patel on 05-03-2020 to open invoice in new window --%>
                                        <asp:LinkButton ID="ingBtnEdit" runat="server" ValidationGroup="cE1" CommandName="Edit"
                                            OnClientClick="javascript:return confirm('Are you sure you want to change order status this Record!');"
                                            ToolTip="Update" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>&nbsp;
                                    
                                         <asp:LinkButton ID="lnlbtnPicker" runat="server" ValidationGroup="cE1" CommandName="Picker" CommandArgument='<%# Bind("order_id") %>'
                                            OnClientClick="javascript:return confirm('Are you sure you want to place order via Picker!');"
                                            ToolTip="Update" CssClass="label label-picker" Visible='<%# Convert.ToString(Eval("ship_id")) == "" ? true : false %>' style="margin-bottom: 5px;"><i class="fa fa-gift"></i> Via Picker</asp:LinkButton>&nbsp;

                                        <asp:LinkButton ID="lnlbtnprint" runat="server" ValidationGroup="cE1" CommandName="Print" CommandArgument='<%# Eval("manifest_link") %>'
                                            ToolTip="Update" CssClass="label label-print" Visible='<%# Convert.ToString(Eval("manifest_link")) == "" ? false : true %>'>
                                            <i class="fa fa-image" style="margin-bottom: 5px;"></i> Print Label</asp:LinkButton>&nbsp;

                                       
                                        <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("order_id") %>' ToolTip="Delete" CssClass=" label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');" style="margin-bottom: 5px;"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                        <asp:LinkButton ID="btnShow" runat="server" type="button" CommandName="EditCustomer" CommandArgument='<%# Bind("order_id") %>'
                                            class="label label-green"><i class="fa fa-edit"></i> edit customer</asp:LinkButton>&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="imgBtnUpdate" runat="server" AlternateText="Update" ToolTip="Update"
                                            CssClass="label label-info" ValidationGroup="prct" OnClientClick="javascript:return confirm('Are you sure you want to update this Record!');"
                                            CommandName="Update" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> update</asp:LinkButton>
                                        <asp:LinkButton ID="imgBtnCancel" runat="server" AlternateText="Cancel" ToolTip="Cancel"
                                            CssClass="label label-danger" CommandName="Cancel"><i class="fa fa-times-circle-o "></i> cancel</asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader01" />
                        </asp:GridView>
                    </div>
            </div>
        </section>
    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />

    <!-- ModalPopupExtender -->
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none" Width="550px">
        <div class="form-horizontal form-label-left input_mask">
                <div class="modal-dialog modal-md">
            <div class="bs-example-modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel2">Edit Customer Detail</h4>
                        </div>
                        <div class="modal-body">
                                    <div class="form-group row"> <span class="label-text col-md-4 col-form-label">Customer Name:</span>
                                <asp:HiddenField ID="hfeditOrderId" runat="server" />
                                <div class="col-md-8 ">
                                    <div class="form-group">
                                        <%--<label class="control-label col-md-4 col-sm-4" for="first-name">
                                            Customer Name <span class="required"></span>
                                        </label>--%>
                                            <%--  <asp:TextBox ID="txtid" runat="server" CssClass="form-control col-md-6 col-xs-12"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtcustomer" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        </div>
                                </div>
                                    <div class="form-group row"> <span class="control-label col-md-4 col-form-label">Phone No:</span>
                                        <%--<label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                            Phone No <span class="required"></span>
                                        </label>--%>
                                        <div class="col-md-8">
                                            <div class="form-group ">
                                                <asp:TextBox ID="txtpopupphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row"> <span class="control-label col-md-4 col-form-label">Address:</span>
                                        <%--<label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                            Address<span class="required"></span>
                                        </label>--%>
                                        <div class="col-md-8">
                                            <div class="form-group ">
                                                <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control "></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="form-group row"> <span class="control-label col-md-4 col-form-label">City:</span>
                                        <%--<label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                            City <span class="required"></span>
                                        </label>--%>
                                        <div class="col-md-8">
                                            <div class="form-group ">
                                            <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="form-group row"> <span class="control-label col-md-4 col-form-label">State:</span>
                                        <%--<label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                            State <span class="required"></span>
                                        </label>--%>
                                        <div class="col-md-8 ">
                                            <div class="form-group ">
                                            <asp:TextBox ID="txtstate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="form-group row"> <span class="control-label col-md-4 col-form-label">Pincode:</span>
                                        <%--<label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                            Pincode <span class="required"></span>
                                        </label>--%>
                                        <div class="col-md-8 ">
                                            <div class="form-group ">
                                            <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                         </div>
                        <div class="modal-footer">
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />

                        <button type="button" class="btn btn-default">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--Start: Added By Hetal Patel on 06-03-2020 for edit multiple orders --%>

    <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="hdnedit"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none" Width="500px">
        <div class="form-horizontal form-label-left input_mask">
            <div class="modal-dialog modal-md">
                <div class="bs-example-modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel3">Edit Order</h4>
                        </div>
                        <div class="modal-body">
                                <asp:HiddenField ID="hdnOrderId" runat="server" />
                                    <div class="form-group row">
                                        <span class="control-label col-md-3 col-form-label">Order Status </span>
                                        <div class="col-md-6">
                                        <div class="form-group">
                                        <%--<label class="control-label col-md-6 col-sm-6 col-xs-12" for="order-status">
                                            Order Status <span class="required"></span>
                                        </label>--%>
                                            <asp:DropDownList ID="ddorderstatus" runat="server" CssClass="form-control txt05" Width="100px">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>inprogress</asp:ListItem>
                                                <asp:ListItem>Confirmed</asp:ListItem>
                                                <asp:ListItem>Packed</asp:ListItem>
                                                <asp:ListItem>Scheduled</asp:ListItem>
                                                <asp:ListItem>Dispatched</asp:ListItem>
                                                <asp:ListItem>Delivered</asp:ListItem>
                                                <asp:ListItem>Failed</asp:ListItem>
                                                <asp:ListItem>Cancelled</asp:ListItem>
                                                <asp:ListItem>RTO</asp:ListItem>
                                                <asp:ListItem>Returned</asp:ListItem>
                                            </asp:DropDownList>
                                         </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                              <asp:Button runat="server" ID="btn_ordStatus" CssClass="btn btn-primary" Text="Save"  OnClick="btnSave1_Click" />
                                            </div>
                                        </div>
                                    </div>
                                 <div class="form-group row">
                                        <span class="control-label col-md-3 col-form-label">Ship Via</span>  
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hfieldshipvia" runat="server" Value='<%#Eval("ship_via") %>' />
                                            <asp:DropDownList ID="ddlshipvia" runat="server" CssClass="form-control" Width="100px"
                                                DataTextField="ship_via" DataValueField="ship_via">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Delhivery</asp:ListItem>
                                                <asp:ListItem>FedEx</asp:ListItem>
                                                <asp:ListItem>DTDC</asp:ListItem>
                                                <asp:ListItem>Ecom Express</asp:ListItem>
                                                <asp:ListItem>Express Bees</asp:ListItem>
                                                <asp:ListItem>Gati</asp:ListItem>
                                                <asp:ListItem>bluedart</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                         <div class="form-group">
                                            <asp:Button runat="server" ID="btn_shipVia" CssClass ="btn btn-primary" Text="Save"  OnClick="btnshipVia_Click"  />
                                          </div>
                                        </div>
                                </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose1" runat="server" Text="Close" CssClass="btn btn-default" />
                            <%--<asp:Button ID="btnSave1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave1_Click" />--%>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--End: Added By Hetal Patel on 06-03-2020 for edit multiple orders --%>

    <%--Start: Added By Hetal Patel on 07-03-2020 for delete multiple orders --%>

    <cc1:ModalPopupExtender ID="mp3" runat="server" PopupControlID="Panel3" TargetControlID="hdndelete"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div class="form-horizontal form-label-left input_mask">
            <div class="bs-example-modal-md">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel4">Delete Order</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hdndeleteId" runat="server" />
                                <div class="col-md-8 col-sm-8 col-xs-12">
                                    <div class="form-group">
                                        <label class="control-label col-md-15 col-sm-15 col-xs-12" for="delete-msg">
                                            Are you sure you want to Remove this Order! 
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-default" />
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="btnYes_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--End: Added By Hetal Patel on 07-03-2020 for edit multiple orders --%>



    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
