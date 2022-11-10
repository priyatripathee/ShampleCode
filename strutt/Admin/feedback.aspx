<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="strutt.Admin.feedback" %>

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
            <h2 class="page--title h5">Feedback</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Order Status</li>
              <li class="breadcrumb-item active"><a href="feedback.aspx"><span>Feedback</span></a></li>
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
                    <div class="records--header">
                        <div class="title fa-shopping-bag">
                            <h3 class="h3">Feedback<a href="#" class="btn btn-sm btn-outline-info">Feedback</a></h3>
                            <p>  <asp:Label ID="Label1" runat="server" CssClass="msg1"></asp:Label></p>
                        </div>
                         <div class="col-md-2">
                             <label>From Date <span class="required"></span>
                        </label>
                               <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control txt05" autocomplete="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                             </div>
                         <div class="col-md-2">
                             <label>From Date <span class="required"></span>
                        </label>
                              <asp:TextBox ID="txttodate" runat="server" CssClass="form-control txt05" autocomplete="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                             </div>
                        <div class="col-md-2 title">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-rounded  btn-primary" OnClick="btnSearch_Click" ValidationGroup="ord" />
                            </div>
                        
                    </div>
                    <!-- Records Header End -->
                </div>
            <div class="panel">
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">

                            <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                            <asp:GridView ID="grdFeedback" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                               AutoGenerateColumns="False" DataKeyNames="Id,rating">

                                <EmptyDataTemplate>
                                    <span>Sorry! No Record Found.</span>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <%# Eval("customer_name")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <%# Eval("email_id")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <%# Eval("contact_number")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rating">
                                        <ItemTemplate>
                                            <%# Eval("ratingName")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <%# Eval("category")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Suggestion">
                                        <ItemTemplate>
                                            <%# Eval("suggestion")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="createdDate" HeaderText="Created Date"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader01" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
          </section>
</asp:Content>
