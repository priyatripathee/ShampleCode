<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="material.aspx.cs" Inherits="strutt.Admin.material" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Material Details</h2>
            <!-- Page Title End -->
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</a></li>
              <li class="breadcrumb-item active"><a href="material.aspx"><span>Material</span></a></li>
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
            <h3 class="h3">Material <a href="#" class="btn btn-sm btn-outline-info">Manage Material</a></h3>
            <p><asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
      </div>
      <div class="panel"> 
        <div class="records--body"> 
          <div class="tab-content"> 
            <div>
                   <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Material Name: <span class="requied"> * </span></span>
                  <div class="col-md-10">
                    <asp:TextBox ID="txtMaterialName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="Please enter material name"
                            ControlToValidate="txtMaterialName" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
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
          <!-- Tab Content End --> 
        </div>
        <!-- Edit Product End --> 
      </div>
                
           <div class="panel"> 
                        <div class="x_content">
                            <div class="table-responsive">
                                <div id="frm" method="post">
                                    
                                    <asp:GridView ID="grdMaterial" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                        AutoGenerateColumns="False" DataKeyNames="material_id,material_name" OnRowCommand="grdMaterial_RowCommand"
                                        OnRowDataBound="grdMaterial_RowDataBound" OnRowDeleting="grdMaterial_RowDeleting">
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
                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <%# Eval("material_name")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="created_date" HeaderText="Created Date"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>

                                            <asp:TemplateField HeaderText ="Active" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                        ToolTip="Active"></asp:Label>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                        CommandName="Active" CommandArgument='<%# Eval("material_id") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                                        CommandArgument='<%# Bind("material_id") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                        CommandArgument='<%# Bind("material_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="120px" />
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
