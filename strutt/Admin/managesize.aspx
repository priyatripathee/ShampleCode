<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="managesize.aspx.cs" Inherits="strutt.Admin.managesize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
        .newbtn{float:right;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
<asp:UpdatePanel ID="uPanelReview" runat="server">
        <ContentTemplate>
         <div class="page-title">
            <div class="title_left">
                <h3>Product Size</h3>
            </div>
             <div class="title_right" style="float:right">
            <asp:Button ID="btnAddNew" runat="server" Text="+ Add Product Size" CssClass="btn btn-primary newbtn" PostBackUrl="~/Admin/size.aspx" />
        </div>
        </div>
        <div class="clearfix"></div>
            <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">
                        <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                        <asp:GridView ID="grdProductSize" runat="server" Width="100%"  CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="size_id" OnRowCommand="grdProductSize_RowCommand"
                            OnRowDataBound="grdProductSize_RowDataBound" OnRowDeleting="grdProductSize_RowDeleting"
                            OnRowEditing="grdProductSize_RowEditing" OnRowUpdating="grdProductSize_RowUpdating"
                            OnRowCancelingEdit="grdProductSize_RowCancelingEdit">
                             <RowStyle CssClass="griditem01" />
                            <EmptyDataTemplate>
                                <span>Sorry! No Record Found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-CssClass="griditem01" ItemStyle-Width="350px">
                                    <ItemTemplate>
                                        <%# Eval("product_name")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hfieldproductId" Value='<%# Eval("product_id")%>' runat="server" />
                                        <asp:Label ID="lblProName" CssClass="form-control" runat="server" Text='<%# Eval("product_name") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size" ItemStyle-CssClass="griditem01" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <%# Eval("size")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditSize" CssClass="form-control" Width="70px" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "size") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditSize" runat="server" ErrorMessage="Please enter size"
                                            ControlToValidate="txtEditSize" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="price" ItemStyle-CssClass="griditem01" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <%# Eval("price")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditPrice" CssClass="form-control" Width="90px" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "price") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditPrice" runat="server" ErrorMessage="Please enter price"
                                            ControlToValidate="txtEditPrice" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount" ItemStyle-CssClass="griditem01" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("discount")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblPaid" runat="server" />
                                        <asp:TextBox ID="txtEditDiscount" CssClass="form-control" Width="100px" runat="server"
                                            Text='<%# Eval("discount") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditDiscont" runat="server" ErrorMessage="Please enter discount"
                                            ControlToValidate="txtEditDiscount" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Price" ItemStyle-CssClass="griditem01" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("sale_price")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditSalePrice" CssClass="form-control" Width="100px" runat="server"
                                            Text='<%# Eval("sale_price") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditSalePrice" runat="server" ErrorMessage="Please enter sale price"
                                            ControlToValidate="txtEditSalePrice" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                       <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>' 
                                            CommandName="Active" CommandArgument='<%# Eval("size_id") %>'/>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("size_id") %>' ToolTip="Edit" CssClass="btn btn-info btn-xs" ><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("size_id") %>' ToolTip="Delete"  CssClass="btn btn-danger btn-xs" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
                                 <EditItemTemplate>
                                        <asp:ImageButton ID="imgBtnUpdate" runat="server" AlternateText="Update" ToolTip="Update"
                                            ImageUrl="~/Admin/images/button/icon_update.jpg" OnClientClick="javascript:return confirm('Are you sure you want to update this Record!');"
                                            ValidationGroup="abutUp" CommandName="Update" />
                                        <asp:ImageButton ID="imgBtnCancel" runat="server" AlternateText="Cancel" ToolTip="Cancel"
                                            ImageUrl="~/Admin/images/button/icon_cancel.jpg" CommandName="Cancel" />
                                    </EditItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader01" />
                        </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>