<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="addcustomerreview.aspx.cs" Inherits="strutt.Admin.addcustomerreview" %>

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
            <h2 class="page--title h5">Customer Review</h2>
            <!-- Page Title End -->
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</li>
              <li class="breadcrumb-item active"><a href="addcustomerreview.aspx"><span>Customer Review</span></a></li>
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
            <h3 class="h3">Customer Review <a href="#" class="btn btn-sm btn-outline-info">Customer Review </a></h3>
            <p>Found Total <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label> Records</p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
            <div class="panel"> 
                        <div class="x_content">
                            <div class="table-responsive">
                                <div id="frm" method="post">
                                    <asp:GridView ID="grdcustomerReview" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                        AutoGenerateColumns="False" DataKeyNames="id,title"   OnRowDataBound="grdcustomerReview_RowDataBound" 
                                        OnRowCommand="grdcustomerReview_RowCommand">
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
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgLarge" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/images/Review/" + Eval("image_name") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="130px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Description">
                                                <ItemTemplate>
                                                    <%# Eval("description_short")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="350px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="createddate" HeaderText="Created Date"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Is Approved" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                        ToolTip="Active"></asp:Label>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                        CommandName="Active" CommandArgument='<%# Eval("id") %>' />&nbsp
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
