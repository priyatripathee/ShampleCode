<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="ImportProduct.aspx.cs" Inherits="strutt.Admin.ImportProduct" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%--<%@ Register Src="~/SgAdmin/UserMenu.ascx" TagPrefix="UC" TagName="UserMenu" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .msg-err {
            color: Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Products</h2>
            <!-- Page Title End -->
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Products</li>
              <li class="breadcrumb-item active"><a href="ImportProduct.aspx"><span>Import/Export Product</span></a>
              </li>
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
            <h3 class="h3">Import/Export Product <a href="#" class="btn btn-sm btn-outline-info">Manage Products</a></h3>
            <!--  <p>Found Total 12 Menu</p>--> 
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
      <!-- Records Header End -->
      <div class="panel"> 
        <!-- Edit Product Start -->
        <div class="records--body"> 
          <!-- Tab Content Start -->
          <div class="tab-content"> 
            <!-- Tab Pane Start -->
            <div>
                <div class="form-group row">
                    
                      <div class="col-md-3">    
                          <div class ="title-text "><strong>Import: -</strong></div>
                                <asp:Button ID="btnimport" runat="server" Text="Import" CssClass="btn btn-rounded btn-success" onclick="btnimport_Click"/> 
                             
                          <div class ="title-text " style="padding-top :30px"><strong>Export: -</strong></div>
                                 <label class="custom-file" style="margin-bottom: 10px;">
                                <asp:FileUpload ID="flFile" runat="server" class="custom-file-input"/><span class="custom-file-label"></span> 
                                </label>
                                 <asp:Button ID="btnSubmit" runat="server" Text="Export" CssClass="btn btn-rounded btn-primary" onclick="btnSubmit_Click"  />
                          </div>

                            <asp:ValidationSummary ID="vs" runat="server" ShowMessageBox="True" ShowSummary="False" />
                        <div class="col-md-3"> 
                                <span class="label-text col-form-label"></span>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" Text="Click here " NavigateUrl="~//Admin//images//ImportProduct.xls" />
                                    to get sample Excel format for upload stock.
                        </div>

                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                               <asp:Label ID="lblMessage" runat="server" CssClass="green"></asp:Label>

                       <%-- <div class="col-md-3"> <span class="label-text col-form-label">Import Product:</span>
                        <label class="custom-file" style="margin-bottom: 10px;">
                         <asp:FileUpload ID="flFile" runat="server" class="custom-file-input"/>
                          <span class="custom-file-label"></span> </label>
                       <asp:Button ID="btnSubmit" runat="server" Text="Export" CssClass="btn btn-rounded btn-primary" onclick="btnSubmit_Click"  />
                      </div>
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                               <asp:Label ID="lblMessage" runat="server" CssClass="green"></asp:Label>
                    --%>
                     
					<%--<div class="col-md-1">&nbsp;</div>--%>
                  <div class="col-md-6 impexppro"> <strong>Rules for Excel file</strong>
                    <ul>
                      <li>File name and sheet name must be same and name must be "ImportProduct".</li>
                      <li>Save as your excel file in Standard excel format after modify record(.xls or .xlsx).</li>
                    </ul>
                    <strong>Compulsory columns in excel sheet:</strong>
                    <ul>
                      <li>Category, Sub Category, Product Name, Material, Quantity,
                        Size, Weight, Color, Price, Discount, Sale Price.</li>
                    </ul>
                    <strong>Columns which are not induced in excel sheet:</strong>
                    <ul>
                      <li>Features,Meta_description,Full_description</li>
                    </ul>
                  </div>

                </div>
            </div>
            <!-- Tab Pane End --> 
          </div>
          <!-- Tab Content End --> 
        </div>
        <!-- Edit Product End --> 
      </div>
     <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_content">
                    <div class="table-responsive">
                      <div id="frm" method="post">
                            <asp:GridView ID="gvdProduct" runat="server" CssClass="table table-striped jambo_table bulk_action" AutoGenerateColumns="False">
                            <Columns>
                            <asp:TemplateField HeaderText="product_id">
                                    <ItemTemplate>
                                        <%# Eval("product_id")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="menu_name">
                                    <ItemTemplate>
                                        <%# Eval("menu_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="sub_menu_name">
                                    <ItemTemplate>
                                        <%# Eval("sub_menu_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="color_name">
                                    <ItemTemplate>
                                        <%# Eval("color_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="material_name">
                                    <ItemTemplate>
                                        <%# Eval("material_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="product_name">
                                    <ItemTemplate>
                                        <%# Eval("product_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="size">
                                    <ItemTemplate>
                                        <%# Eval("size")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="weight">
                                    <ItemTemplate>
                                        <%# Eval("weight")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="price">
                                    <ItemTemplate>
                                        <%# Eval("price")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="discount">
                                    <ItemTemplate>
                                        <%# Eval("discount")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="sale_price">
                                    <ItemTemplate>
                                        <%# Eval("sale_price")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="quantity">
                                    <ItemTemplate>
                                        <%# Eval("quantity")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="is_exclusive">
                                    <ItemTemplate>
                                        <%# Eval("is_exclusive")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="is_latest">
                                    <ItemTemplate>
                                        <%# Eval("is_latest")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="gendertype">
                                    <ItemTemplate>
                                        <%# Eval("gendertype")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="is_best_seller">
                                    <ItemTemplate>
                                        <%# Eval("is_best_seller")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="is_default">
                                    <ItemTemplate>
                                        <%# Eval("is_default")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <%--<asp:TemplateField HeaderText="specification">
                                    <ItemTemplate>
                                        <%# Eval("specification")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="in_stock">
                                    <ItemTemplate>
                                        <%# Eval("in_stock")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="is_deal">
                                    <ItemTemplate>
                                        <%# Eval("is_deal")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="in_wishlist">
                                    <ItemTemplate>
                                        <%# Eval("in_wishlist")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="features">
                                    <ItemTemplate>
                                        <%# Eval("features")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="is_active">
                                    <ItemTemplate>
                                        <%# Eval("is_active")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="orderby">
                                    <ItemTemplate>
                                        <%# Eval("orderby")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <%--<asp:TemplateField HeaderText="short_description">
                                    <ItemTemplate>
                                        <%# Eval("short_description")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="meta_keywords">
                                    <ItemTemplate>
                                        <%# Eval("meta_keywords")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="meta_title">
                                    <ItemTemplate>
                                        <%# Eval("meta_title")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="meta_description">
                                    <ItemTemplate>
                                        <%# Eval("meta_description")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="full_description">
                                    <ItemTemplate>
                                        <%# Eval("full_description")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="created_by">
                                    <ItemTemplate>
                                        <%# Eval("created_by")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="updated_by">
                                    <ItemTemplate>
                                        <%# Eval("updated_by")%>
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
        </div>
            </div>
           </section>
</asp:Content>
