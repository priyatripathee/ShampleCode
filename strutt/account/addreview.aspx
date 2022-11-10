<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="addreview.aspx.cs"
    ValidateRequest="false" Inherits="strutt.account.addreview" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- <link href="../ckeditor/samples/css/samples.css" rel="stylesheet" />
    <link href="../ckeditor/samples/toolbarconfigurator/lib/codemirror/neo.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        var loadFile = function (event) {
            var output = document.getElementById('cph_main_imgBlog');
            output.src = URL.createObjectURL(event.target.files[0]);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Add Blog</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Add Blog</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <!-- wishlist start -->
        <div class="wishlist-main-area  section-space--ptb_90">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3">
                        <div class="blog-widget widget-blog-categories mt-40 border p-3">
                            <ul class="widget-nav-list">
                                <li><a href="orderhistory.aspx">Order History</a></li>
                                <li><a href="cancelorder.aspx">Cancel Order</a></li>
                                <li><a href="leavefeedback.aspx">Leave Feedback</a></li>
                                <li><a href="leavecomplaint.aspx">Leave a Complaint</a></li>
                                <li><a href="addresses.aspx">Saved Addresses</a></li>
                                <li><a href="changepassword.aspx">Change Password</a></li>
                                <li><a href="../wishlist.aspx">Wishlist</a></li>
                                <li><a href="../Login.aspx?type=lo">Log out</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <asp:UpdatePanel ID="UPnlBanner" runat="server">
                            <ContentTemplate>
                                <div class="">
                                    <div class="order-tracking-form-box">
                                        <h2><strong>ADD BLOG</strong></h2>
                                         <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="true"></asp:Label>
                                        <div class="billing-info mb-25">
                                            <asp:Label ID="lblTltle" runat="server">Title <span style="color:red">*</span></asp:Label>
                                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="Please enter Title"
                                                ControlToValidate="txtTitle" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="billing-info mb-25">
                                            <asp:Label ID="Label1" runat="server">Image</asp:Label>
                                            <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                                            <asp:FileUpload ID="Upload_Blog" runat="server" onchange="loadFile(event)" />
                                            <asp:Image ID="imgBlog" ImageUrl="../Admin/images/noImage.jpg" Height="80px" Width="100px"
                                                AlternateText="" runat="server" ImageAlign="AbsMiddle" />
                                        </div>
                                        <div class="billing-info mb-25">
                                            <asp:Label ID="Label2" runat="server">Description <span style="color:red">*</span> </asp:Label>
                                            <CKEditor:CKEditorControl ID="txtDescription" BasePath="/ckeditor/" runat="server">
                                            </CKEditor:CKEditorControl>
                                            <%--<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Width="400px" TextMode="MultiLine" Height="100px"></asp:TextBox>--%>
                                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Please enter description"
                                                ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="button-box mt-25">
                                            <asp:LinkButton ID="lbtnSubmit" runat="server" OnClick="lbtnSubmit_Click"  CssClass="btn--lg btn--black font-weight--reguler text-white" ValidationGroup="prct">Submit</asp:LinkButton>
                                        </div>
                                    </div>
                                    <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UPnlBanner">
                                        <ProgressTemplate>
                                            <img src="../images/loading.gif" title="Please wait.." alt="Please wait.." />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lbtnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <div class="col-lg-9">
                        <h2><strong>YOUR PAST BLOG</strong></h2>
                        <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                        <asp:GridView ID="grdcustomerReview" runat="server" Width="130%"
                            AutoGenerateColumns="False" DataKeyNames="id,title" OnRowCommand="grdcustomerReview_RowCommand">
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
                                    <ItemStyle Width="350px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="createddate" HeaderText="Created Date"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader01" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <!-- wishlist end -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
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
