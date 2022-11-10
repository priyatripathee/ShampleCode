<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="customerblog.aspx.cs" Inherits="strutt.Admin.customerblog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Customer Blog</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Blog</li>
              <li class="breadcrumb-item active"><a href="#"><span>Customer Blog</span></a></li>
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
    <asp:UpdatePanel ID="uPanelReview" runat="server">
        <ContentTemplate>
            <section class="main--content">
                 <div class="panel nav-tabs"> 
        <!-- Records Header Start -->
        <div class="records--header">
          <div class="title fa-shopping-bag">
            <h3 class="h3">Blog<a href="#" class="btn btn-sm btn-outline-info">Customer Blog</a></h3>
            <p>  <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
              <p><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
           <div class="panel"> 
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">
                       <asp:GridView ID="grdcustomerReview" runat="server" Width="130%"
                            AutoGenerateColumns="False" DataKeyNames="id,title" OnRowDeleting="grdcustomerReview_RowDeleting"
                           OnRowDataBound="grdcustomerReview_RowDataBound" OnRowCommand="grdcustomerReview_RowCommand">
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
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <%# Eval("title")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:Image ID="imgLarge" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/images/Review/" + Eval("image_name") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <%# Eval("description_short")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="createddate" HeaderText="Created Date"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                            <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                        ToolTip="Active"></asp:Label>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                        CommandName="Active" CommandArgument='<%# Eval("id") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                        CommandArgument='<%# Bind("id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
</asp:Content>
