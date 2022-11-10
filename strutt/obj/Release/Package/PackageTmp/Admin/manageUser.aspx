<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="manageUser.aspx.cs" Inherits="strutt.Admin.manageUser" %>

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
            <!-- Page Header Start -->
            <section class="page--header">
            <div class="container-fluid">
            <div class="row">
              <div class="col-lg-6"> 
                <!-- Page Title Start -->
                <h2 class="page--title h5">Manage user</h2>
                <!-- Page Title End -->
            
                <ul class="breadcrumb">
                  <li class="breadcrumb-item">Extra Page</li>
                  <li class="breadcrumb-item active"><a href="#"><span>Manage user</span></a></li>
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
      <div class="panel "> 
        <!-- Records Header Start -->
        <div class="records--header">
          <div class="title fa-shopping-bag">
            <h3 class="h3">Users <a href="#" class="btn btn-sm btn-outline-info">Manage User</a></h3>
            <p><asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p> <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
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
            <div class="tab-pane fade show active">
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Type:</span>
                  <div class="col-md-4">
                    <asp:DropDownList ID="ddluser" runat="server"  CssClass="form-control" >

                        <asp:ListItem Value="1">Super Admin</asp:ListItem>
                        <asp:ListItem Value="2">Admin</asp:ListItem>
                        <asp:ListItem Value="3">Oparator</asp:ListItem>

                    </asp:DropDownList>
                  </div>
                  <span class="label-text col-md-2 col-form-label">User Name:<span class="requied"> * </span></span>
                  <div class="col-md-4">
                    <asp:TextBox ID="txtuname" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter User Name"
                            ControlToValidate="txtuname" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">First Name:<span class="requied"> * </span></span>
                  <div class="col-md-4">
                         <asp:TextBox ID="txtfname" runat="server" CssClass="form-control"  ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter First Name"
                            ControlToValidate="txtfname" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                        <%--<span class="form-check" >&nbsp;</span> --%>
                    </label>
                  </div>

                    <span class="label-text col-md-2 col-form-label">Last Name:<span class="requied"> * </span></span>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtlname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Last Name"
                            ControlToValidate="txtlname" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                        <%--<span class="form-check" >&nbsp;</span> --%>
                    </label>
                  </div>
                </div>

                <div class="form-group row">
                    <span class="label-text col-md-2 col-form-label">Password:<span class="requied"> * </span></span>
                    <div class="col-md-4">
                          <asp:TextBox ID="txtpass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Password"
                            ControlToValidate="txtpass" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                    </label>
                  </div>

                    <span class="label-text col-md-2 col-form-label">Is Acive</span>
                    <div class="col-md-4">
                    <label class="form-check">
                        <span class="label-text col-md-2 col-form-label">
                         <asp:CheckBox ID="chkIsActive" runat="server" class="form-check" style="transform :scale(1.5)" />
                         <%--<asp:CheckBox ID="chkIsActive" runat="server" class="form-check" style="transform :scale(1.5)" />--%>
                            <%--<asp:Label runat="server" ID="lblisactive"></asp:Label>
                            </span>--%>
                        <%--<span class="form-check" >&nbsp;</span> --%>
                    </label>
                  </div>
                    </div>
                <div class="form-group row">
                    <span class="label-text col-md-2 col-form-label">Image:</span>
                    <div class="col-md-4">
                    <label class="custom-file">
                         <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                      <span class="custom-file-label">Choose File <asp:FileUpload ID="Upload_Images" runat="server"  onchange="loadFile(event)" Visible="true" CssClass="custom-file-input"/></span> </label>
                    <a href="#" class="btn-link" style="margin-top: 15px;"> <asp:Image ID="imgLarge" ImageUrl="assets/css/images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" /> </a>
                  </div>
                  <%--<div class="col-md-4">
                      <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                      <span class="custom-file-label"> 
                          <asp:FileUpload ID="Upload_LargeImages" runat="server" onchange="loadFile(event)" Visible="true" CssClass="custom-file-input"/>
                      </span>
                     </label>
                    <a href="#" class="btn-link" style="margin-top: 15px;"> 
                        <asp:Image ID="imgLarge" ImageUrl="assets/css/images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" />
                    </a>
                         <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Image"
                            ControlToValidate="txtpass" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                  </div>--%>
                </div>
            </div>
              <asp:HiddenField runat="server" ID="lblisActive" Value ='<%# Eval("is_active") %>'/>
            <!-- Tab Pane End -->
            
            <div class="row ">
              <div class="col-md-12" style="text-align: center;">
                   <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prct" OnClick="btnSubmit_Click" />
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
                        
                       <asp:GridView ID="gvUser" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="admin_id,user_name"  OnRowDataBound="gvUser_RowDataBound" OnRowCommand="gvUser_RowCommand" OnRowDeleting="gvUser_RowDeleting">
                            <EmptyDataTemplate>
                                <span>Sorry! No Record Found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Type">
                                    <ItemTemplate>
                                        
                                        <asp:Label runat="server" id="lbltype" Text='<%# Eval("user_type")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <%# Eval("user_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="First Name">
                                    <ItemTemplate>
                                        <%# Eval("first_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Last Name">
                                    <ItemTemplate>
                                        <%# Eval("last_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  
                                <asp:TemplateField HeaderText="Created by"  >
                                    <ItemTemplate>
                                        <%# Eval("created_by") %>
                                        </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>           
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                        <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                            CommandName="Active" CommandArgument='<%# Eval("admin_id") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>


                                <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:Image ID="img1" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/Admin/images/" + Eval("profile_image") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="130px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                            CommandArgument='<%# Bind("admin_id") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                        <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("admin_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                            
                                

                            </Columns>
                            <HeaderStyle CssClass="gridHeader01" />
                        </asp:GridView>
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

