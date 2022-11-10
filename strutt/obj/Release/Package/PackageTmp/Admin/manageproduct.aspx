<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="manageproduct.aspx.cs" Inherits="strutt.Admin.manageproduct" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*table {
            border: 1px solid #ccc;
        }*/

            /*table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }*/

        /*.Pager span {
            color: #333;
            background-color: #F7F7F7;
            font-weight: bold;
            text-align: center;
            display: inline-block;
            width: 20px;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #ccc;
        }*/

        /*.Pager a {
            text-align: center;
            width: 20px;
            border: 1px solid #ccc;
            color: #fff;
            color: #333;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }

        .highlight {
            background-color: #FFFFAF;
        }

        .newbtn {
            float: right;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
     <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Manage Products</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item ">Products</li>
              <li class="breadcrumb-item active"><a href ="manageproduct.aspx" ><span>Manage Products</span></a></li>
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
    <!-- Page Header End --> 
    <section class="main--content">
  <div class="panel">

         <div class="records--header"> 
        <!-- Records Header Start -->
             <%--<asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>--%>
          <div class="title fa-shopping-bag">
            <h3 class="h3">Products <a href="#" class="btn btn-sm btn-outline-info">Manage Products</a></h3>
            <p> <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="true"></asp:Label></p>
          </div>
          <div class="actions">
            <div class="search flex-wrap flex-md-nowrap">
                
                 <div class="col-md-3">
                   <asp:DropDownList ID="ddlCategory" runat="server"  CssClass="form-control" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                        DataTextField="menu_name" DataValueField="menu_id" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                 </div>
              
                  <div class="col-md-2">
                    <asp:DropDownList ID="ddlsubCategory" runat="server"  CssClass="form-control" EnableViewState="true" ViewStateMode="Enabled"
                        DataTextField="sub_menu_name"   DataValueField="sub_menu_id">
                        <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                  </div>
                                             
                <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Is Active" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                </asp:DropDownList>

                  <asp:DropDownList ID="ddlStock" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Is Stock" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                  </asp:DropDownList>
                <asp:DropDownList ID="ddlIsLatest" runat="server" CssClass="form-control">
                    <asp:ListItem Text="IsLatest" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-rounded btn-success"></asp:Button>
                &nbsp;&nbsp;&nbsp; <a href="addeditproduct.aspx" class="addProduct btn btn-lg btn-rounded btn-warning">Add Product</a> </div>
            </div>
              <br />
             
        <!-- Records Header End --> 
     </div>
      </div>
    </section>
    <section class="main--content">
    <div class="panel"> 
        <%--<div class="x_content">--%>
            <div class="table-responsive">
                <%--<div id="frm" method="post">--%>
                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%"
                        OnRowDataBound="gvProduct_RowDataBound" OnRowCommand="gvProduct_RowCommand"
                        OnRowDeleting="gvProduct_RowDeleting"  DataKeyNames="product_id" CssClass="table table-striped jambo_table bulk_action">
                        <EmptyDataTemplate>
                    <span>Sorry! No Record Found.</span>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender Type">
                        <ItemTemplate>
                            <%# Eval("gendertype")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Menu name">
                        <ItemTemplate>
                            <%# Eval("menu_name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Menu Name">
                        <ItemTemplate>
                            <%# Eval("sub_menu_name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <%# Eval("quantity")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate>
                            <%# Eval("product_name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <%# Eval("price")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sale Price">
                        <ItemTemplate>
                            <%# Eval("sale_price")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Stock">
                        <ItemTemplate>
                                <asp:Label ID="lblInStock" Visible="false" runat="server" Text='<%# Eval("in_stock") %>'
                                ToolTip="Stock"></asp:Label>
                                <asp:ImageButton ID="imgIsStock" runat="server" AlternateText='<%# Eval("in_stock").ToString().ToLower()=="true" ? "Stock" : "Inactive" %>'
                                ToolTip='<%# Eval("in_stock").ToString().ToLower()=="true" ? "instock" : "outstock" %>'
                                ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"in_stock")+".png"%>'
                                CommandName="InStock" CommandArgument='<%# Eval("product_id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active">
                        <ItemTemplate>
                                <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                ToolTip="Active"></asp:Label>
                                <asp:ImageButton ID="imgIsActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                CommandName="IsActive" CommandArgument='<%# Eval("product_id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="Is Default">
                        <ItemTemplate>
                            <%# Eval("is_default")%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Is Latest">
                        <ItemTemplate>
                                <asp:Label ID="lblIsLatest" Visible="false" runat="server" Text='<%# Eval("is_latest") %>'
                                ToolTip="Latest"></asp:Label>
                                <asp:ImageButton ID="imgIsLatest" runat="server" AlternateText='<%# Eval("is_latest").ToString().ToLower()=="true" ? "Latest" : "notLatesr" %>'
                                ToolTip='<%# Eval("is_latest").ToString().ToLower()=="true" ? "Latest" : "notLatesr" %>'
                                ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_latest")+".png"%>'
                                CommandName="IsLatest" CommandArgument='<%# Eval("product_id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Is Best Seller">
                        <ItemTemplate>
                            <%# Eval("is_best_seller")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Exclusive">
                        <ItemTemplate>
                            <%# Eval("is_exclusive")%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate >
                            <%--<asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                ToolTip="Active"></asp:Label>--%>
                            <%-- <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                CommandName="Active" CommandArgument='<%# Eval("product_id") %>' />&nbsp;--%>
                            <%--<asp:ImageButton ID="btnImgEdit" runat="server"   CssClass="label label-success"
                                CommandName="EditRecored" OnClientClick="javascript:return confirm('Are you sure you want to Edit this Record!');"
                                CommandArgument='<%# Bind("product_id") %>' ToolTip="Edit" />&nbsp;--%>
                            <asp:LinkButton ID="btnImgEdit" runat="server"  ValidationGroup="cE1" CommandName="EditRecored" CssClass="label label-success"
                                CommandArgument='<%# Bind("product_id") %>' ToolTip="Update"  OnClientClick="javascript:return confirm('Are you sure you want to Edit this Product!');" style="display:inline ;"><i class="fa fa-edit">Edit</i></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete" CssClass="label label-danger" 
                                CommandArgument='<%# Bind("product_id") %>' ToolTip="Delete"  OnClientClick="javascript:return confirm('Are you sure you want to Remove this Product!');" style="display:inline "><i class="fa fa-trash">Delete</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gridHeader01" />
                    </asp:GridView>
                    <br />
                <%--</div>--%>
            <%--   <div class="row">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="datatable-responsive_info" role="status" aria-live="polite">
                            Pages: <asp:Label ID="lblPageNo" runat="server" Text="1/10" />
                        </div>                                            
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers">
                            <ul class="pagination">
                                <li class="paginate_button active">
                                    <asp:LinkButton ID="lbFirst" runat="server" ToolTip="First" CausesValidation="false" onclick="lbNavButton_Click">First</asp:LinkButton></li>
                                <li class="paginate_button active">
                                    <asp:LinkButton ID="lbPrev" runat="server" ToolTip="Previus" CausesValidation="false" onclick="lbNavButton_Click">Prev</asp:LinkButton></li>
                                <li class="paginate_button active">
                                    <asp:LinkButton ID="lbNext" runat="server" ToolTip="Next" CausesValidation="false" onclick="lbNavButton_Click">Next</asp:LinkButton></li>
                                <li class="paginate_button active">
                                    <asp:LinkButton ID="lbLast" runat="server" ToolTip="Last" CausesValidation="false" onclick="lbNavButton_Click">Last</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    </div>--%>
            </div>
        <%--</div>--%>
    </div>
    </section>
</asp:Content>

