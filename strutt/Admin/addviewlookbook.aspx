<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="addviewlookbook.aspx.cs" Inherits="strutt.Admin.addviewlookbook" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <link href="../ckeditor/samples/css/samples.css" rel="stylesheet" />
    <link href="../ckeditor/samples/toolbarconfigurator/lib/codemirror/neo.css" rel="stylesheet" />
    <script type="text/javascript">
        var loadFile = function (event) {
            var output = document.getElementById('cphadmin_imgLarge');
            output.src = URL.createObjectURL(event.target.files[0]);
        };
    </script>
    <asp:UpdatePanel ID="UPnlBlog" runat="server">
        <ContentTemplate>
            <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Lookbook</h2>
            <!-- Page Title End -->
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Lookbook</li>
              <li class="breadcrumb-item active"><a href="manageblog.aspx"><span>Manage Lookbook </span></a></li>
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
            <h3 class="h3">Lookbook <%--<a href="#" class="btn btn-sm btn-outline-info">Manage Lookbook</a>--%></h3>
            <%--<p>Found Total 12 Menu</p>--%>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
      <div class="panel"> 
        <div class="records--body"> 
          <div class="tab-content"> 
            <div>
                 <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                 <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Image (Lookbook): <span class="requied"> * </span></span>
                    <div class="col-md-4">
                    <label class="custom-file">
                         <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                      <span class="custom-file-label">Choose File<asp:FileUpload ID="Upload_LargeImages" runat="server" onchange="loadFile(event)" Visible="true" CssClass="custom-file-input"/></span> </label>
                    <a href="#" class="btn-link" style="margin-top: 15px;">  <asp:Image ID="imgLarge" ImageUrl="assets/css/images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" /> </a> </div>
                      </div>
                    <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Description: <span class="requied"> * </span></span>
                    <div class="col-md-10">
                       <CKEditor:CKEditorControl ID="txtDescription" BasePath="/ckeditor/" runat="server">
                                            </CKEditor:CKEditorControl>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter description"
                            ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="blg" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                </div>
            </div>
            <div class="row ">
              <div class="col-md-12" style="text-align: center;">
                  <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="blg"
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
                                    <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                                    <asp:GridView ID="gvLookbook" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                                        AutoGenerateColumns="False" DataKeyNames="lookbookId" OnRowCommand="gvLookbook_RowCommand"
                                        OnRowDataBound="gvLookbook_RowDataBound" OnRowDeleting="gvLookbook_RowDeleting">
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
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <%# Eval("description")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="350px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgLarge" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/images/BlogImages/" + Eval("image") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="130px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="created_date" HeaderText="Created Date"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                                        ToolTip="Active"></asp:Label>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                                        ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                                        CommandName="Active" CommandArgument='<%# Eval("lookbookId") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnImgEdit" runat="server" CommandName="EditRecored"
                                                        CommandArgument='<%# Bind("lookbookId") %>' ToolTip="Edit" CssClass="label label-success" style="margin-bottom: 5px;"><i class="fa fa-edit"></i> edit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                        CommandArgument='<%# Bind("lookbookId") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../ckeditor/samples/js/sample.js" type="text/javascript"></script>
    <script type="text/javascript">
        initSample();
    </script>
    <script type="text/javascript">
        ClassicEditor
             .create(document.querySelector('#cph_main_txtDescription'), {
                 // toolbar: [ 'heading', '|', 'bold', 'italic', 'link' ]
             })
             .then(editor => {
                 window.editor = editor;
             })
             .catch(err => {
                 console.error(err.stack);
             });
</script>
</asp:Content>