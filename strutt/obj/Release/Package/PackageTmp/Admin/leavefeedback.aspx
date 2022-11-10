<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="leavefeedback.aspx.cs" Inherits="strutt.Admin.leavefeedback" %>

<%@ Import Namespace="BLL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Leave Feedback</h2>
            <!-- Page Title End -->
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Orders Status</a></li>
              <li class="breadcrumb-item active"><a href="leavefeedback.aspx"><span>Leave Feedback</span></a></li>
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
      <div class="panel nav-tabs"> 
        <!-- Records Header Start -->
        <div class="records--header">
          <div class="title fa-shopping-bag">
            <h3 class="h3">Leave Feedback <a href="#" class="btn btn-sm btn-outline-info">Manage Feedback</a></h3>
            <p>Found Total 12 Records</p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
      <div class="panel"> 
        <div class="records--body"> 
          <div class="tab-content"> 
            <div>
                
                    <div class="form-group row">
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlDateRange" CssClass="form-control txt05" AutoPostBack="true" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                            </asp:DropDownList>
                         </div>
                        <asp:Panel runat="server" ID="pnlCustomDate" Visible="false" >
                         <%--<span class="label-text col-md-2 col-form-label" style="text-align: right;">From Date: *</span>--%>
                          
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control txt05" placeholder="From Date" autocomplete="false"></asp:TextBox>
                                   <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate"  runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                          
                            <%--<span class="label-text col-md-2 col-form-label" style="text-align: right;">To Date: *</span>--%>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control txt05"  placeholder="To Date" autocomplete="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                </asp:Panel>

                  <div class="col-md-2">
                       <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-rounded btn-warning" OnClick="btnSearch_Click" ValidationGroup="ord" />
                  </div>
                </div>
            </div>
          </div>
        </div>
      </div>
                 <div class="panel"> 
                    <div class="x_content">
                        <div class="table-responsive">
                            <div id="frm" method="post">
                                <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                                <asp:GridView ID="grdleavefeedback" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                    AutoGenerateColumns="False" DataKeyNames="OrderId,EmailId">
                                    <RowStyle CssClass="griditem01" />
                                    <EmptyDataTemplate>
                                        <span>Sorry! No Record Found.</span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Feedback">
                                            <ItemTemplate>
                                                <%# Eval("LeaveFeedbackId")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                STR10001 <%# Eval("OrderId")%>
                                                <%# Eval("order_date")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <%# Eval("email_id")%>
                                                <%# Eval("contact_number")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State/City">
                                            <ItemTemplate>
                                                <%# Eval("state")%>
                                                <%# Eval("city")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <%# Eval("address")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rating">
                                            <ItemTemplate>
                                                <%# Eval("Rating")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" 
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Item Arrived">
                                            <ItemTemplate>
                                                <%# Eval("ItemArrived")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Described">
                                            <ItemTemplate>
                                                <%# Eval("ItemDescribed")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure OnTime">
                                            <ItemTemplate>
                                                <%# Eval("DepartureOnTime")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment">
                                            <ItemTemplate>
                                                <%# Eval("Comment")%>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Created Date">
                                    <ItemTemplate>
                                        <%# Eval("CreatedDate")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>--%>
                                        <%-- <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>--%>
                                        <%-- <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                           <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>' 
                                            CommandName="Active" CommandArgument='<%# Eval("material_id") %>'/>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("material_id") %>' ToolTip="Edit" CssClass="btn btn-info btn-xs" ><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("material_id") %>' ToolTip="Delete"  CssClass="btn btn-danger btn-xs" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" />
                                </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader01" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
        </section>
</asp:Content>
