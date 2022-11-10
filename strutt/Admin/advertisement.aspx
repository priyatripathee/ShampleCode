<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="advertisement.aspx.cs" Inherits="strutt.Admin.advertisement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <script>
        var loadFile = function (event) {
            var output = document.getElementById('cphadmin_imgLarge');
            output.src = URL.createObjectURL(event.target.files[0]);
        };
    </script>
    <asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
            <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Manage Advertisement</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</li>
              <li class="breadcrumb-item active"><a href="#"><span>Advertisement</span></a></li>
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
            <h3 class="h3">Advertisement <a href="#" class="btn btn-sm btn-outline-info">Manage Advertisement</a></h3>
            <p><asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
      <div class="panel"> 
        <!-- Edit Product Start -->
        <div class="records--body"> 
         <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
          <div class="tab-content"> 
            <div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Banner Title: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                     <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="Please enter Title"
                            ControlToValidate="txtTitle" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
					<span class="label-text col-md-2 col-form-label">URL link: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                       <asp:TextBox ID="txtURL" runat="server"  CssClass="form-control"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter url link"
                            ControlToValidate="txtURL" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Image (Banner): <span class="requied"> * </span></span>
                  <div class="col-md-4">
                    <label class="custom-file">
                         <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                      <span class="custom-file-label">  <asp:FileUpload ID="Upload_LargeImages" runat="server" onchange="loadFile(event)" Visible="true" CssClass="custom-file-input"/></span> </label>
                    <a href="#" class="btn-link" style="margin-top: 15px;"> 
                          <asp:Image ID="imgLarge" ImageUrl="assets/css/images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" />
                    </a>
                  </div>
                  <span class="label-text col-md-2 col-form-label">&nbsp;</span>
                  <div class="col-md-4">
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
                                    <asp:GridView ID="grdAdvertisement" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                        AutoGenerateColumns="False" DataKeyNames="category_link_id,title" OnRowCommand="grdAdvertisement_RowCommand"
                                        OnRowDataBound="grdAdvertisement_RowDataBound" OnRowDeleting="grdAdvertisement_RowDeleting">
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
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%# Eval("title")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="180px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Banner" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgLarge" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/images/Banner/" + Eval("image") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="130px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="URL Link">
                                                <ItemTemplate>
                                                    <%# Eval("category_link_url")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="300px" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="created_date" HeaderText="Created Date"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>

                                            <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                        ToolTip="Active"></asp:Label>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                        CommandName="Active" CommandArgument='<%# Eval("category_link_id") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                                        CommandArgument='<%# Bind("category_link_id") %>' ToolTip="Edit" style="margin-bottom: 5px;" CssClass="label label-success"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                        CommandArgument='<%# Bind("category_link_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
