<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="addreviewold.aspx.cs" Inherits="strutt.account.addreviewold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/normalize.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <script type="text/javascript">
        var loadFile = function (event) {
            var output = document.getElementById('cph_main_imgBlog');
            output.src = URL.createObjectURL(event.target.files[0]);
        };
    </script>
    <div class="container">
        <div class="margin-t-50">
            <div class="flex gutter">
                <div id="block-details" class="col-12-12 col-sm-4-12 col-md-3-12">
                    <div class="bd bg-grey-light padding-25">
                        <div class="flex gutter-sm">
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="orderhistory.aspx">Order History</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="cancelorder.aspx">Cancel Order</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="leavefeedback.aspx">Leave Feedback</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="leavecomplaint.aspx">Leave a Complaint</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="addresses.aspx">Saved Addresses</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="changepassword.aspx">Change Password</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="../wishlist.aspx">Wishlist</a>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                  <li><asp:HyperLink ID="hlAddBlog" runat="server" NavigateUrl="~/account/addreview.aspx">Add Blog</asp:HyperLink>
                            </div>
                            <div class="col-sm-12-12 col-xs-4-12">
                                <a href="../Login.aspx?type=lo">Log out</a>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UPnlBanner" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="content">
                                <h2 style="font-size: 24px;"><strong>ADD REVIEW</strong></h2>
                                </br>
                <table class="form">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblTltle" runat="server">Title <span style="color:red">*</span></asp:Label></br>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="400px" Height="30px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Title"
                                    ControlToValidate="txtTitle" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server">Image</asp:Label></br>
                                <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                                <asp:FileUpload ID="Upload_Blog" runat="server" onchange="loadFile(event)" />
                                <asp:Image ID="imgBlog" ImageUrl="../Admin/images/noImage.jpg" Height="80px" Width="100px"
                                    AlternateText="" runat="server" ImageAlign="AbsMiddle" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server">Description <span style="color:red">*</span> </asp:Label></br>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Width="400px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter description"
                                    ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" ValidationGroup="prct"
                                    OnClick="btnSubmit_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="ValidatorsMsg"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>


                            </div>
                        </div>
                        <br />
                        <h2 style="font-size: 20px;"><strong>YOUR PAST REVIEW</strong></h2>
                        <br />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_content">
                                        <div class="table-responsive">
                                            <div id="frm" method="post">
                                                <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label>
                                                <asp:GridView ID="grdcustomerReview" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
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
                                                            <ItemStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgLarge" runat="server" Width="100px" Height="60px" ImageUrl='<%# "~/images/Review/" + Eval("image_name") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="130px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <%# Eval("description")%>
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
                            </div>
                        </div>
                        <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UPnlBanner">
                            <ProgressTemplate>
                                <img src="../images/loading.gif" title="Please wait.." alt="Please wait.." />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

