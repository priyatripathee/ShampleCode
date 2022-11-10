<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="producttype.aspx.cs" Inherits="strutt.Admin.producttype" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
            <!-- Page Header Start -->
            <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Product Type</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Menu</li>
              <li class="breadcrumb-item active"><a href="producttype.aspx"><span>Product Type</span></a></li>
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
            <h3 class="h3">Product Type <a href="#" class="btn btn-sm btn-outline-info">>Product Type</a></h3>
            <p>  <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
            <div class="panel">
                <!-- Edit Product Start -->
                <div class="records--body">
                    <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                    <div class="tab-content">
                        <!-- Tab Pane Start -->
                        <div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Menu: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="select menu"
                                        ControlToValidate="ddlMenu" ForeColor="Red" InitialValue="select menu" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <span class="label-text col-md-2 col-form-label">Sub Menu: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlSubMenu" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubMenu_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="select sub menu"
                                        ControlToValidate="ddlSubMenu" ForeColor="Red" InitialValue="select sub menu" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Child Menu: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlChildMenu" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="select child menu"
                                        ControlToValidate="ddlChildMenu" ForeColor="Red" InitialValue="select child menu" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <span class="label-text col-md-2 col-form-label">Product Type: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtProductType" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="please enter product type"
                                        ControlToValidate="txtProductType" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Product Type URL: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtProductTypeUrl" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="enter product type url"
                                        ControlToValidate="txtProductTypeUrl" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <!-- Tab Pane End -->
                        <div class="row ">
                            <div class="col-md-12" style="text-align: center;">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prct"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-rounded btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                    </div>
                    </div>
                           <div class="panel">
                                <div class="x_content">
                                    <div class="table-responsive">
                                        <div id="frm" method="post">
                                          
                                            <asp:GridView ID="gvProductType" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                                AutoGenerateColumns="False" DataKeyNames="menu_id,menu_name" OnRowCommand="gvProductType_RowCommand"
                                                OnRowDataBound="gvProductType_RowDataBound" OnRowDeleting="gvProductType_RowDeleting" HeaderStyle-CssClass="headings">
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
                                                    <asp:TemplateField HeaderText="Menu">
                                                        <ItemTemplate>
                                                            <%# Eval("menu_name")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Menu">
                                                        <ItemTemplate>
                                                            <%# Eval("sub_menu_name")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Child Menu">
                                                        <ItemTemplate>
                                                            <%# Eval("child_name")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Type">
                                                        <ItemTemplate>
                                                            <%# Eval("product_type_name")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="URL">
                                                        <ItemTemplate>
                                                            <%# Eval("product_type_url")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="created_date" HeaderText="Created Date"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                                                    <asp:TemplateField HeaderText ="Active" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                                ToolTip="Active"></asp:Label>
                                                            <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                                ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                                ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                                CommandName="Active" CommandArgument='<%# Eval("product_type_id") %>' />
                                                            
                                                        </ItemTemplate>
                                                         <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                                                CommandArgument='<%# Bind("product_type_id") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                                CommandArgument='<%# Bind("product_type_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader01" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
