<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="customerdetail.aspx.cs" Inherits="strutt.Admin.customerdetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .table {
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Product Feed</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Order Status</li>
              <li class="breadcrumb-item active"><a href="customerdetail.aspx"><span>Product Feed</span></a></li>
          </div>
            </ul>
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
                    <asp:Label ID="lblMsg" runat="server" CssClass="msg1"></asp:Label>
                    <div class="records--header">
                        <div class="title fa-shopping-bag">
                            <h3 class="h3">Customer Detail<a href="#" class="btn btn-sm btn-outline-info">Customer Detail</a></h3>
                            <p>  <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
                        </div>

                        <div class="actions">
                            <div class="text-right">
                                <%--<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"  />--%>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-primary" OnClick="btnDownload_Click" />
                            </div>
                        </div>
                    </div>
                    <!-- Records Header End -->
                </div>
           <div class="panel">
                    <div class="x_content">
                        <div class="table-responsive">
                           
                            <%--<form id="form1" runat="server">--%>
                            <asp:GridView ID="grdcustomerdetails" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                AutoGenerateColumns="False" DataKeyNames="product_name,Price">
                                <RowStyle CssClass="griditem01" />
                                <EmptyDataTemplate>
                                    <span>Sorry! No Record Found.</span>
                                </EmptyDataTemplate>
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Id">
                                        <ItemTemplate>
                                            <%# Eval("product_id")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <%# Eval("product_name")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Link">
                                        <ItemTemplate>
                                            <%# Eval("ProductLink")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Condition">
                                        <ItemTemplate>
                                            New
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <%# Eval("price")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Sale Price">
                                        <ItemTemplate>
                                            <%# Eval("sale_price")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Availability">
                                        <ItemTemplate>
                                            <%# Eval("in_stock")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image_Link">
                                        <ItemTemplate>
                                            <%# Eval("Image1")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image_Link2">
                                        <ItemTemplate>
                                            <%# Eval("Image2")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image_Link3">
                                        <ItemTemplate>
                                            <%# Eval("Image3")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            Strutt
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Product_Type">
                                        <ItemTemplate>
                                            <%# Eval("product_type_name")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <%# Eval("gendertype")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <%# Eval("full_description")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    
                                    <%--<asp:TemplateField HeaderText="Gtin">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mpn">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Google Product category">
                                        <ItemTemplate>
                                            <%# Eval("sub_menu_name")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>--%>

                                    <%--  <asp:BoundField DataField="created_date" HeaderText="Created Date" 
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
                            <%--</form>--%>
                        </div>
                    </div>
                </div>
       </section>
</asp:Content>
