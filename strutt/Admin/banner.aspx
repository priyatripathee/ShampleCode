<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="banner.aspx.cs" Inherits="strutt.Admin.banner" %>

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
            <h2 class="page--title h5">Home Banner</h2>
            <!-- Page Title End -->
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</a></li>
              <li class="breadcrumb-item active"><a href="banner.aspx"><span>Home Banner</span></a></li>
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
            <h3 class="h3">Banner Details <a href="#" class="btn btn-sm btn-outline-info">Manage Banner</a></h3>
            <p>  <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
      </div>
      <div class="panel"> 
        <div class="records--body"> 
              <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
          <div class="tab-content"> 
            <div>
                <div class="form-group row"> 
                   <span class="label-text col-md-2 col-form-label">Type: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Home banner" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Instagan" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                    <span class="label-text col-md-2 col-form-label">Banner Title: <span class="requied"> * </span></span>
                  <div class="col-md-4">
                     <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Title"
                            ControlToValidate="txtTitle" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
					<span class="label-text col-md-2 col-form-label">URL link:<span class="requied"> * </span></span>
                  <div class="col-md-4">
                    <asp:TextBox ID="txtURL" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter url link"
                            ControlToValidate="txtURL" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
                    <span class="label-text col-md-2 col-form-label">Order By :<span class="requied"> * </span></span>
                    <div class="col-md-4">
                    <asp:TextBox ID="txtOrderby" runat="server" CssClass="form-control"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter order by"
                            ControlToValidate="txtOrderby" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
					</div>
                </div>
              <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Image (Banner): <span class="requied"> * </span></span>
                  <div class="col-md-4">
                    <label class="custom-file">
                         <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                      <span class="custom-file-label"> <asp:FileUpload ID="Upload_LargeImages" runat="server" onchange="loadFile(event)" Visible="true" CssClass="custom-file-input"/></span> </label>
                    <a href="#" class="btn-link" style="margin-top: 15px;"> <asp:Image ID="imgLarge" ImageUrl="assets/css/images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" /> </a>
                  </div>
                   <div class="col-md-4" style="align-content:center ">
                       <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prct"
                            OnClick="btnSubmit_Click" />
                         <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-rounded btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                       </div>
            <!-- Tab Pane End -->
            </div>
          </div>
            </div>
             <div class="panel"> 
                        <div class="x_content">
                            <div class="table-responsive">
                                <div id="frm" method="post">
                                    <asp:GridView ID="grdBanner" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                        AutoGenerateColumns="False" DataKeyNames="banner_id,title" OnRowCommand="grdBanner_RowCommand"
                                        OnRowDataBound="grdBanner_RowDataBound" OnRowDeleting="grdBanner_RowDeleting">
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
                                                    <asp:Image ID="imgLarge" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/images/Banner/" + Eval("image") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="130px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <%# Eval("type_name")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="350px" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Order BY">
                                                <ItemTemplate>
                                                    <%# Eval("order_by")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="350px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Url Path">
                                                <ItemTemplate>
                                                    <%# Eval("url_path")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="350px" />
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
                                                        CommandName="Active" CommandArgument='<%# Eval("banner_id") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                                        CommandArgument='<%# Bind("banner_id") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                        CommandArgument='<%# Bind("banner_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');" style="margin-bottom: 5px;"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
