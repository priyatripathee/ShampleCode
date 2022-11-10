<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="strutt.Admin.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
<asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
        <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Change Password</h3>
            </div>
        </div>
        <div class="clearfix"></div>
       
        <div id="Form1" method="post"  class="form-horizontal form-label-left input_mask">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                    <div class="x_content">
                        <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">Old Password <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:TextBox ID="txtoldpassword" runat="server" CssClass="form-control col-md-7 col-xs-12" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVName" runat="server" ErrorMessage="Please enter old password" Display="Dynamic"
                            ControlToValidate="txtoldpassword" CssClass="Validators" ValidationGroup="changeP" ForeColor="Red"  ></asp:RequiredFieldValidator>
                        </div>
                      </div>
                         <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">New Password <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:TextBox ID="txtnewpassword" runat="server" CssClass="form-control col-md-7 col-xs-12" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter new password" Display="Dynamic"
                                ControlToValidate="txtnewpassword" CssClass="Validators"  ValidationGroup="changeP" ForeColor="Red" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" CssClass="Validators"
                                ControlToValidate="txtnewpassword" ID="RegularExpressionValidator8" ValidationExpression="^[\s\S]{6,}$"
                                runat="server" ValidationGroup="changeP" ErrorMessage="Minimum 6 characters."></asp:RegularExpressionValidator>
                        </div>
                      </div>

                         <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">Confirm Password <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="txtconformpassword" runat="server" CssClass="form-control col-md-7 col-xs-12" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter confirm password" Display="Dynamic"
                                ControlToValidate="txtconformpassword" CssClass="Validators" ValidationGroup="changeP" ForeColor="Red" ></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CPVConPass" runat="server" ErrorMessage="New Password and confirm new password should be same !!"
                                ControlToCompare="txtnewpassword" ValidationGroup="prct" Display="Dynamic" CssClass="Validators"
                                SetFocusOnError="true" ControlToValidate="txtconformpassword" Operator="Equal"></asp:CompareValidator>
                        </div>
                      </div>
                           
                         <div class="form-group">
                        <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                          <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                CssClass="btn btn-success" ValidationGroup="changeP"  onclick="btnSubmit_Click"/>
                       <%-- <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />--%>
                        </div>
                      </div>
                       <div class="form-group">
                      <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UPnlBanner">
                            <ProgressTemplate>
                                <img src="images/loading.gif" title="Please wait.." alt="Please wait.." />Loading...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
        <%--<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">
                              
                        <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                       <asp:GridView ID="gvchangepassword" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="sub_menu_id,sub_menu_name" OnRowCommand="gvSubMenu_RowCommand"
                            OnRowDataBound="gvSubMenu_RowDataBound" OnRowDeleting="gvSubMenu_RowDeleting">
                           
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
                                <asp:TemplateField HeaderText="Menu Name">
                                    <ItemTemplate>
                                        <%# Eval("menu_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="110px" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Gendertype">
                                    <ItemTemplate>
                                        <%# Eval("gendertype_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="110px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Menu Name">
                                    <ItemTemplate>
                                        <%# Eval("sub_menu_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="110px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu URL">
                                    <ItemTemplate>
                                        <%# Eval("menu_url")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="110px" />
                                </asp:TemplateField>
                                
                                 <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>
                              <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                           <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>' 
                                            CommandName="Active" CommandArgument='<%# Eval("sub_menu_id") %>'/>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("sub_menu_id") %>' ToolTip="Edit" CssClass="btn btn-info btn-xs" ><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("sub_menu_id") %>' ToolTip="Delete"  CssClass="btn btn-danger btn-xs" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader01" />
                        </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

