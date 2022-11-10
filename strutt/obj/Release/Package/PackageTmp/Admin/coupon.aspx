<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="coupon.aspx.cs" Inherits="strutt.Admin.coupon" %>

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
            <h2 class="page--title h5">Coupon</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</li>
              <li class="breadcrumb-item active"><a href="coupon.aspx"><span>Coupon</span></a></li>
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
        <div class="records--header">
          <div class="title fa-shopping-bag">
            <h3 class="h3">Coupon <a href="#" class="btn btn-sm btn-outline-info">Manage Coupon</a></h3>
              <p> <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
      </div>
      <div class="panel"> 
        <div class="records--body"> 
          <div class="tab-content"> 
            <div>
                   <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                <div class="form-group row"> 
					<span class="label-text col-md-2 col-form-label">Menu: <span class="requied"> * </span></span>
                  <div class="col-md-4">
					    <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control" AutoPostBack="true" 
                            onselectedindexchanged="ddlMenu_SelectedIndexChanged">
                        </asp:DropDownList>
                  </div>
					<span class="label-text col-md-2 col-form-label">Sub Menu: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                   <asp:DropDownList ID="ddlSubMenu" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubMenu_SelectIndexChanged">
                        </asp:DropDownList>
                  </div>
                </div>
				  <div class="form-group row"> 
					<span class="label-text col-md-2 col-form-label">Child Menu: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                  <asp:DropDownList ID="ddlChildMenu" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                  </div>
					<span class="label-text col-md-2 col-form-label">Gender Type: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                   <asp:DropDownList ID="ddlgendertype" runat="server" CssClass="form-control">
                         <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Men" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Women" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                  </div>
                </div>
				  <div class="form-group row"> 
					<span class="label-text col-md-2 col-form-label">Coupon Code: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                     <asp:TextBox ID="txtCouponCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="enter coupon code"
                            ControlToValidate="txtCouponCode" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
					<span class="label-text col-md-2 col-form-label">Price(%): <span class="requied"> * </span></span>
                  <div class="col-md-4">
                    <asp:TextBox ID="txtprice" runat="server" CssClass="form-control"></asp:TextBox>
                       
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="enter price"
                            ControlToValidate="txtprice" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
                </div>
            </div>
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
                              
                       
                              <asp:GridView ID="gvCoupon" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="coupon_code_id,coupon_code,menu_id" OnRowCommand="gvCoupon_RowCommand"
                            OnRowDataBound="gvCoupon_RowDataBound" OnRowDeleting="gvCoupon_RowDeleting">
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
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Menu">
                                    <ItemTemplate>
                                        <%# Eval("sub_menu_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Child Menu">
                                    <ItemTemplate>
                                        <%# Eval("child_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Gender Type">
                                    <ItemTemplate>
                                        <%# Eval("gendertype_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coupon Code">
                                    <ItemTemplate>
                                        <%# Eval("coupon_code")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%# Eval("price")%>%
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                                
                                 <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                     <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                           <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>' 
                                            CommandName="Active" CommandArgument='<%# Eval("coupon_code_id") %>'/>
                                    </ItemTemplate>
                                      <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("coupon_code_id") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;" ><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("coupon_code_id") %>' ToolTip="Delete"  CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
      </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>