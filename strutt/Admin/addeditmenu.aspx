<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="addeditmenu.aspx.cs" Inherits="strutt.Admin.addeditmenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Asearch").click(function () {
                $("div#tab0").slideToggle();
            });
        });
    </script>
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
            <h2 class="page--title h5">Menu</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Menu</li>
              <li class="breadcrumb-item active"><a href="addeditmenu.aspx"><span>Main Menu</span></a></li>
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
            <!-- Main Content Start -->
             
            <section class="main--content">
                <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
      <div class="panel nav-tabs"> 
        <!-- Records Header Start -->
        <div class="records--header">
           
          <div class="title fa-shopping-bag">
            <h3 class="h3">Main Menu <a href="#" class="btn btn-sm btn-outline-info">Manage Menu</a></h3>
            <p> <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
          <div class="actions">
            <div  class="search flex-wrap flex-md-nowrap">
                 <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control" placeholder="Menu Name*"></asp:TextBox>
                 
                 <asp:TextBox ID="txtMenuURL" runat="server" CssClass="form-control" placeholder="Menu URL*"></asp:TextBox>
                
                
            </div>
              <div  class="search flex-wrap flex-md-nowrap" style="width:100%;">
                 <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control" placeholder="Meta Title"></asp:TextBox> 
                 <asp:TextBox ID="txtMetaKeyword" runat="server" CssClass="form-control" placeholder="Meta Keywork"></asp:TextBox>
                <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control" placeholder="Meta Description"></asp:TextBox> 
            </div>
              <div>
                  <br />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prct"
                            OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-rounded btn-warning" style="margin-left: 10px;" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            </div>
           
                    <div class="col-md-4">
                        
                    </div>
                    <div class="col-md-8">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" style="margin-left :70px" ErrorMessage="please enter menu name"
                                ControlToValidate="txtMenuName" ForeColor="Red" ValidationGroup="prct" ></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="please enter menu URL"
                                ControlToValidate="txtMenuURL" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
               
            
        </div>
        <!-- Records Header End --> 
      </div>
                 <div class="panel"> 
        <!-- Records List Start -->
           

             <%--<asp:GridView ID="grdMenu" runat="server" Width="100%" CssClass="recordsListView"
                            AutoGenerateColumns="False" DataKeyNames="menu_id,menu_name" OnRowCommand="grdMenu_RowCommand"
                            OnRowDataBound="grdMenu_RowDataBound" OnRowDeleting="grdMenu_RowDeleting">
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
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu URL">
                                    <ItemTemplate>
                                        <%# Eval("menu_url")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
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
                                            CommandName="Active" CommandArgument='<%# Eval("menu_id") %>'/>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("menu_id") %>' ToolTip="Edit" CssClass="btn btn-info btn-xs"><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("menu_id") %>' ToolTip="Delete" CssClass="btn btn-danger btn-xs"  OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader01" />
                        </asp:GridView>--%>
                    
                     <%--<div class="table-responsive">
                         <div>
                         <table id="t1" class="recordsListView dataTable no-footer" aria-describedby="recordsListView_info" role="grid">
            <thead>
              <tr role="row"><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Gender: activate to sort column ascending" style="width: 52px;">Gender</th><th class="not-sortable sorting_disabled" rowspan="1" colspan="1" aria-label="Image" style="width: 80px;">Image</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Product Name: activate to sort column ascending" style="width: 99px;">Product Name</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Category: activate to sort column ascending" style="width: 94px;">Category</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Price: activate to sort column ascending" style="width: 44px;">Price</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Sale Price: activate to sort column ascending" style="width: 67px;">Sale Price</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Quantity: activate to sort column ascending" style="width: 63px;">Quantity</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Created Date: activate to sort column ascending" style="width: 91px;">Created Date</th><th class="sorting" tabindex="0" aria-controls="recordsListView" rowspan="1" colspan="1" aria-label="Status: activate to sort column ascending" style="width: 116px;">Status</th><th class="not-sortable sorting_disabled" rowspan="1" colspan="1" aria-label="Actions" style="width: 53px;">Actions</th></tr>
            </thead>
            <tbody>
              
            <tr role="row" class="odd">
                <td><a href="#" class="btn-link">All</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-success">Approved</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="even">
                <td><a href="#" class="btn-link">All</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-warning">Not Published</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="odd">
                <td><a href="#" class="btn-link">Women</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-danger">Deleted</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="even">
                <td><a href="#" class="btn-link">Women</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-info">Available</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="odd">
                <td><a href="#" class="btn-link">All</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-success">Approved</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="even">
                <td><a href="#" class="btn-link">Men</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-success">Approved</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="odd">
                <td><a href="#" class="btn-link">All</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-warning">Not Published</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="even">
                <td><a href="#" class="btn-link">Men</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-danger">Deleted</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="odd">
                <td><a href="#" class="btn-link">All</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-info">Available</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr><tr role="row" class="even">
                <td><a href="#" class="btn-link">Women</a></td>
                <td><a href="#" class="btn-link"> <img src="assets/img/products/thumb-80x60.jpg" alt=""> </a></td>
                <td><a href="#" class="btn-link">Baby Dress</a></td>
                <td><a href="#" class="btn-link">Baby Products</a></td>
                <td>$12.00</td>
                <td>$12.00</td>
                <td>1</td>
                <td>12 June 2020</td>
                <td><span class="label label-success">Approved</span></td>
                <td><div class="dropleft"> <a href="#" class="btn-link" data-toggle="dropdown"><i class="fa fa-ellipsis-v"></i></a>
                    <div class="dropdown-menu"> <a href="#" class="dropdown-item">Edit</a> <a href="#" class="dropdown-item">Delete</a> <a href="#" class="dropdown-item">Active</a> </div>
                  </div></td>
              </tr></tbody>
          </table>
                             </div>
                     </div>--%>
                       
               <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">
                            <asp:GridView ID="grdMenu" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="menu_id,menu_name" OnRowCommand="grdMenu_RowCommand"
                            OnRowDataBound="grdMenu_RowDataBound" OnRowDeleting="grdMenu_RowDeleting">
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
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu URL">
                                    <ItemTemplate>
                                        <%# Eval("menu_url")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                    <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                           <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>' 
                                            CommandName="Active" CommandArgument='<%# Eval("menu_id") %>'/>
                                        </ItemTemplate>
                                    <ItemStyle Width="30px" HorizontalAlign="Center"  />
                                </asp:TemplateField>
                                
                              <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("menu_id") %>' ToolTip="Edit" CssClass="label label-success"><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete" 
                                            CommandArgument='<%# Bind("menu_id") %>' ToolTip="Delete" CssClass="label label-danger"  OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
              </div>        
    </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
