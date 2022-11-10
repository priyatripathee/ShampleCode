<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="submenu.aspx.cs" Inherits="strutt.Admin.submenu" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="markitup/sets/default/style.css" />
    <link rel="stylesheet" type="text/css" href="markitup/skins/simple/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
            <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Sub Menu</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Menu</li>
              <li class="breadcrumb-item active"><a href="submenu.aspx"><span>Sub Menu</span></a></li>
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
            <h3 class="h3">Sub Menu <a href="#" class="btn btn-sm btn-outline-info">Manage Menu</a></h3>
            <p> <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
            <div class="panel">
                <!-- Edit Product Start -->
                <div class="records--body">
                    <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                    <div class="tab-content">
                        <!-- Tab Pane Start -->
                        <div>

                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Menu: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="please select menu" Display="Dynamic"
                                        ControlToValidate="ddlMenu" ForeColor="Red" InitialValue="select menu" ValidationGroup="prct"></asp:RequiredFieldValidator>
                                </div>
                                <span class="label-text col-md-2 col-form-label">Gender Type:</span>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlgendertype" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Men" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Women" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="please select gender type"
                                        ControlToValidate="ddlgendertype" ForeColor="Red" InitialValue="select menu" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Sub Menu Name: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSubMenuName" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="please enter sub menu name"
                                        ControlToValidate="txtSubMenuName" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <span class="label-text col-md-2 col-form-label">Sub Menu URL: <span class="requied">* </span></span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSubMenuURL" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVTName" runat="server" ErrorMessage="please enter url name"
                                        ControlToValidate="txtSubMenuURL" ForeColor="Red" ValidationGroup="prct" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Order By:</span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtOrderBy" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Description Header:</span>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDescHeader" runat="server" CssClass="form-control" cols="225" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Description Footer:</span>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDescFooter" runat="server" CssClass="form-control" cols="225" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Is New</span>
                                <div class="col-md-4">
                                    <label class="form-check">
                                        <span class="label-text col-md-2 col-form-label">
                                            <asp:CheckBox ID="chkIsNew" runat="server" class="form-check" style="transform :scale(1.5)" />
                                         </span>
                                        <%--<span class="form-check-label">&nbsp;</span>--%>
                                    </label>
                                </div>
                                <div class="col-md-6">&nbsp;</div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Meta Title:</span>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Meta Keyword:</span>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtMetaKeywork" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="label-text col-md-2 col-form-label">Meta Description:</span>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Tab Pane End -->
                        <div class="row ">
                            <div class="col-md-12" style="text-align: center;">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prct" OnClick="btnSubmit_Click" />
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
                                            <asp:GridView ID="gvSubMenu" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                                AutoGenerateColumns="False" DataKeyNames="sub_menu_id,sub_menu_name" OnRowCommand="gvSubMenu_RowCommand"
                                                OnRowDataBound="gvSubMenu_RowDataBound" OnRowDeleting="gvSubMenu_RowDeleting">
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
                                                        <ItemStyle Width="110px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gendertype">
                                                        <ItemTemplate>
                                                            <%# Eval("gendertype_name")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="110px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Menu Name">
                                                        <ItemTemplate>
                                                            <%# Eval("sub_menu_name")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="110px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Menu URL">
                                                        <ItemTemplate>
                                                            <%# Eval("menu_url")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="110px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order By">
                                                        <ItemTemplate>
                                                            <%# Eval("orderby")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="110px" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Desc Header">
                                                <ItemTemplate>
                                                    <%# Eval("desc_header")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desc Header">
                                                <ItemTemplate>
                                                    <%# Eval("desc_footer")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                                ToolTip="Active"></asp:Label>
                                                            <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                                ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                                ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                                CommandName="Active" CommandArgument='<%# Eval("sub_menu_id") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center"  />

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="created_date" HeaderText="Created Date"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                                                 CommandArgument='<%# Bind("sub_menu_id") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                                CommandArgument='<%# Bind("sub_menu_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
                        
                </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
    <!-- markItUp! -->
    <script type="text/javascript" src="markitup/jquery.markitup.js"></script>
    <!-- markItUp! toolbar settings -->
    <script type="text/javascript" src="markitup/sets/default/set.js"></script>
    <script type="text/javascript">
        $(function () {
            CreateFormatTextbox();
        });
        function CreateFormatTextbox() {
            // Add markItUp! to your textarea in one line
            // $('textarea').markItUp( { Settings }, { OptionalExtraSettings } );
            $('#cphadmin_txtDescHeader').markItUp(mySettings);

            // You can add content from anywhere in your page
            // $.markItUp( { Settings } );	
            $('.add').click(function () {
                $('#cphadmin_txtDescHeader').markItUp('insert',
            {
                openWith: '<opening tag>',
                closeWith: '<\/closing tag>',
                placeHolder: "New content"
            }
        );
                return false;
            });

            // And you can add/remove markItUp! whenever you want
            // $(textarea).markItUpRemove();
            $('.toggle').click(function () {
                if ($("#cphadmin_txtDescHeader.markItUpEditor").length === 1) {
                    $("#cphadmin_txtDescHeader").markItUp('remove');
                    $("span", this).text("get markItUpNew! back");
                } else {
                    $('#cphadmin_txtDescHeader').markItUp(mySettings);
                    $("span", this).text("remove markItUp!");
                }
                return false;
            });

            // Add markItUp! to your textarea in one line
            // $('textarea').markItUp( { Settings }, { OptionalExtraSettings } );
            $('#cphadmin_txtDescFooter').markItUp(mySettings);

            // You can add content from anywhere in your page
            // $.markItUp( { Settings } );	
            $('.add').click(function () {
                $('#cphadmin_txtDescFooter').markItUp('insert',
            {
                openWith: '<opening tag>',
                closeWith: '<\/closing tag>',
                placeHolder: "New content"
            }
        );
                return false;
            });

            // And you can add/remove markItUp! whenever you want
            // $(textarea).markItUpRemove();
            $('.toggle').click(function () {
                if ($("#cphadmin_txtDescFooter.markItUpEditor").length === 1) {
                    $("#cphadmin_txtDescFooter").markItUp('remove');
                    $("span", this).text("get markItUpNew! back");
                } else {
                    $('#cphadmin_txtDescFooter').markItUp(mySettings);
                    $("span", this).text("remove markItUp!");
                }
                return false;
            });
        }

    </script>
</asp:Content>
