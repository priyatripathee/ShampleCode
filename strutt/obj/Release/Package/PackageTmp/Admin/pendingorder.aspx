<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="pendingorder.aspx.cs" Inherits="strutt.Admin.pendingorder" %>

    <%@ Import Namespace="BLL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">


            <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3> Pending Order</h3>
            </div>
        </div>
        <div class="clearfix"></div>
       
        <div id="Form1" method="post"  class="form-horizontal form-label-left input_mask">
        <div class="row">    
             <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal">
            <div class="x_panel">
                <div class="x_content">
               <asp:Label ID="lblMsg" runat="server" CssClass="msg1"></asp:Label>
                   <div class="col-md-3 col-sm-3 col-xs-12">
                    <div class="form-group">
                        <label class="control-label col-md-6 col-sm-6 col-xs-12" for="first-name"> From Date</label>
                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control txt05" autocomplete="false" Width="100px"></asp:TextBox>
                           <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                     </div>
                    </div>
                    <div class="col-md-3 col-sm-3 col-xs-12">
                    <div class="form-group">
                        <label class="control-label col-md-6 col-sm-6 col-xs-12" for="first-name">To Date</label>  
                       <asp:TextBox ID="txttodate" runat="server" CssClass="form-control txt05" autocomplete="false" Width="100px"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                    </div>
                    </div>
                          <div class="col-md-3 col-sm-3 col-xs-12">
                    <div class="form-group">
                        <label class="control-label col-md-6 col-sm-6 col-xs-12" for="first-name">Email Sent</label>
                          <asp:DropDownList ID="ddlEmailSent" runat="server" CssClass="form-control txt05" Width="100px">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="1">Sent</asp:ListItem>
                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                        </asp:DropDownList>
                     </div>
                    </div>                       
                <div class="col-md-3 col-sm-3 col-xs-12">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" 
                            CssClass="btn btn-primary newbtn" onclick="btnSearch_Click" />
                        </div>


                         <div class="col-md-4 col-sm-4 col-xs-12">
                         </div>
                          <div class="col-md-4 col-sm-4 col-xs-12">
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-12">
                          <asp:Button ID="btnSentEmail" runat="server" Text="Sent Email For All Pending Order" 
                            CssClass="btn btn-primary newbtn" onclick="btnSentEmail_Click"  />
                         </div>


                        </div>
            
                </div>
                </div>
                </div>
            </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_content">
                        <div class="table-responsive">
                            <div id="frm" method="post">
                                <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                                <table width="100%" border="1" style="margin-bottom: 10px;" cellpadding="2" cellspacing="2"
                                    class="table table-striped jambo_table bulk_action">
                                    <tr>
                                        <th>Sr.No</th>
                                        <th>Email</th>
                                        <th>Name</th>
                                        <th>Email Sent</th>
                                        <th>Address</th>
                                        <th>City</th>
                                        <th>State</th>
                                        <th>Country</th>
                                    </tr>
                                    <asp:Repeater ID="rpttemp" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.ItemIndex +1 %>
                                                </td>
                                                
                                                <td>
                                                    <%# Eval("email_id")%>
                                                </td>
                                                <td>
                                                    <%# Eval("name")%>
                                                </td>
                                                <td>
                                                   <%# Eval("email_sent")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("address")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("city")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("state")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("address")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>  
    </div>
</asp:Content>

