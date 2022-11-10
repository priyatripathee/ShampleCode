<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="city.aspx.cs" Inherits="strutt.Admin.city" %>

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
            <h2 class="page--title h5">City</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</li>
              <li class="breadcrumb-item active"><a href="city.aspx"><span>City</span></a></li>
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
            <h3 class="h3">City <a href="#" class="btn btn-sm btn-outline-info">Manage City</a></h3>
            <p><asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
      
      <!-- Records Header End -->
      <div class="panel"> 
        <!-- Edit Product Start -->
        <div class="records--body"> 
           <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
          <!-- Tab Content Start -->
          <div class="tab-content"> 
            <!-- Tab Pane Start -->
            <div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">State:</span>
                  <div class="col-md-4">
                    <asp:DropDownList ID="ddlState" runat="server"  CssClass="form-control" ></asp:DropDownList>
                      <asp:Label runat="server" ID="lbldropdown"></asp:Label>
                  </div>
                  <span class="label-text col-md-2 col-form-label">Name:</span>
                  <div class="col-md-4">
                    <asp:TextBox ID="txtcityName" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="please enter City Name" Display="Dynamic" 
                                ControlToValidate="txtcityName" ForeColor="Red" ValidationGroup="prct" ></asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Is New</span>
                  <div class="col-md-4">
                    <label class="form-check">
                        <span class="label-text col-md-2 col-form-label">
                         <asp:CheckBox ID="chkIsActive" runat="server" class="form-check" style="transform :scale(1.5)" />
                            </span>
                        <%--<span class="form-check" >&nbsp;</span> --%>
                    </label>
                  </div>
                  <div class="col-md-6">&nbsp;</div>
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
        </div>
         <div class="panel"> 
                <div class="x_content">
                    <div class="table-responsive">
                        
                       <asp:GridView ID="gvCity" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="city_id,city_name"  OnRowCommand="gvCity_RowCommand"
                            OnRowDataBound="gvCity_RowDataBound">
                            <EmptyDataTemplate>
                                <span>Sorry! No Record Found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <%# Eval("state")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%# Eval("city_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate >
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                        <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                            CommandName="Active" CommandArgument='<%# Eval("city_id") %>' />&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>
                              <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="EditRecored"
                                            CommandArgument='<%# Bind("city_id") %>' ToolTip="Edit" CssClass="label label-success"><i class="fa fa-edit"></i> edit</asp:LinkButton>&nbsp;
                                    </ItemTemplate>
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
