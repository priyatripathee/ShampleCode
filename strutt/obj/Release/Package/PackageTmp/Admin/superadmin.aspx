<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="superadmin.aspx.cs" Inherits="strutt.Admin.superadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
<asp:UpdatePanel ID="UPnlCategory" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="3" cellspacing="0">
                <tr class="orangebg">
                    <td width="1%" height="25">
                        <img style="margin-top: 9px" src="/Admin/images/headbull.gif" alt="t" />
                    </td>
                    <td width="99%" class="myHeader">
                        &nbsp;<asp:Label ID="lblHeading" runat="server" Text="Super Admin Profile"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UPnlCategory">
                <ProgressTemplate>
                    <img src="../images/loading.gif" title="Please wait.." alt="Please wait.." />Loading...
                </ProgressTemplate>
            </asp:UpdateProgress>
            <table width="100%" border="0" cellpadding="2" cellspacing="2" class="newtabbg">
                <tr class="tr">
                    <td align="left" valign="top" style="width: 124px;">
                        <b>Role :</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAdminRole" runat="server" CssClass="ddl" Width="210px">
                            <asp:ListItem>Select Role</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                            <asp:ListItem>SuperAdmin</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" ErrorMessage="Select role"
                            ControlToValidate="ddlAdminRole" ForeColor="Red" InitialValue="Select Role" ValidationGroup="prct"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" valign="top" style="width: 124px;">
                        <b>User Id :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserId" runat="server" CssClass="txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter user Id"
                            ControlToValidate="txtUserId" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tr">
                    <td align="left" valign="top" style="width: 124px;">
                        <b>Password :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Enter password"
                            ControlToValidate="txtPassword" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" valign="top" style="width: 124px;">
                        <b>Confirm Password :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvconPass" runat="server" ErrorMessage="Enter confirm password"
                            ControlToValidate="txtConfirmPassword" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator><br />
                        <asp:CompareValidator ID="comparePasswords" runat="server" ForeColor="Red" ControlToCompare="txtPassword"
                            ControlToValidate="txtConfirmPassword" ErrorMessage="passwords do not match!"
                            ValidationGroup="prct" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px;" colspan="4">
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style1">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                            ValidationGroup="prct" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="height: 20px; text-align:center;" colspan="3">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="4">
                        <asp:Label ID="lbl_Total" runat="server" ForeColor="#CC0000"></asp:Label>
                        <asp:GridView ID="grdAdmin" runat="server" Width="100%" BackColor="White" CssClass="tabgrid"
                            PagerStyle-CssClass="pagerlink" HeaderStyle-CssClass="bluebg" CellPadding="2"
                            AutoGenerateColumns="False" DataKeyNames="admin_id, user_name" OnRowCommand="grdAdmin_RowCommand"
                            OnRowDataBound="grdAdmin_RowDataBound" 
                            OnRowDeleting="grdAdmin_RowDeleting">
                            <PagerStyle CssClass="pagerlink"></PagerStyle>
                            <EmptyDataTemplate>
                                <span>Sorry! No Record Found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%"
                                    ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="hfieldUserName" runat="server" Value='<%#Bind("user_name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="user_name" HeaderText="User Name"></asp:BoundField>
                                <asp:BoundField DataField="permission" HeaderText="permission"></asp:BoundField>
                                <asp:BoundField DataField="created_date" HeaderText="Created Date"></asp:BoundField>
                                <asp:BoundField DataField="created_by" HeaderText="Created By"></asp:BoundField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                        <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                            CommandName="Active" CommandArgument='<%# Eval("admin_id") %>' />&nbsp;
                                        <asp:ImageButton ID="btnImgEdit" runat="server" CssClass="fa fa-edit" ImageUrl="images/edit.jpg"
                                            CommandName="EditRecored" OnClientClick="javascript:return confirm('Are you sure you want to Edit this Record!');"
                                            CommandArgument='<%# Bind("admin_id") %>' ToolTip="Edit" />&nbsp;
                                        <asp:ImageButton ID="btnImgDelete" runat="server" ImageUrl="images/delete.gif" CommandName="Delete"
                                            OnClientClick="javascript:return confirm('Are you sure you want to Remove this Record!');"
                                            CommandArgument='<%# Bind("admin_id") %>' ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UPnlCategory">
                <ProgressTemplate>
                    <img src="../images/loading.gif" title="Please wait.." alt="Please wait.." />Loading...
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- END PAGE CONTENT-->
</asp:Content>