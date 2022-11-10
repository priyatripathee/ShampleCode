<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="addeditproductextra.aspx.cs" Inherits="strutt.Admin.addeditproductextra" %>



<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%>  --%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
<%--<ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1"OnUploadComplete="AjaxFileUpload1_UploadComplete" Mode="Auto" runat="server"  />--%>
<asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
                <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Extra Product</h3>
            </div>
        </div>
        <div class="clearfix"></div>
       
        <div id="Form1" method="post"  class="form-horizontal form-label-left input_mask">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                    <div class="x_content">
                         <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name"> Image (Banner)
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                       <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                       <%-- <asp:FileUpload ID="Upload_LargeImages" runat="server" onchange="loadFile(event)" AllowMultiple="true" />
                        <asp:Image ID="imgLarge" ImageUrl="images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" />--%>
                            <asp:FileUpload AllowMultiple="true"  ID="FileUpload1" runat="server" />
                        <%--    <asp:Button runat="server" ID="btnUpload" CssClass="btnStyle" Text="Upload Image"
                             OnClick="btnUpload_Click" />--%>
                              <asp:Image ID="imgLarge" ImageUrl="images/noImage.jpg" Height="80px" Width="100px"
                            AlternateText="" runat="server" ImageAlign="AbsMiddle" />

                        </div>
                      </div>
                      <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name"> Image Order 
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                    <asp:TextBox ID="txtimageorder" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                            <%-- <div class="col-md-3 col-sm-3 col-xs-12">
                        <asp:RequiredFieldValidator ID="RFVpn" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtimageorder" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                             </div>--%>
                      </div>
                          <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">   Is Active  
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                   <asp:CheckBox ID="chkIsactive" runat="server" />
                        </div>
                      </div>
                         <div class="form-group">
                        <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                          <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" ValidationGroup="prct" OnClick="btnSubmit_Click"
                        />
                        </div>
                      </div>
                       <div class="form-group">
                      <asp:UpdateProgress ID="updateprogress2" runat="server" AssociatedUpdatePanelID="UPnlBanner">
                            <ProgressTemplate>
                                <img src="images/loading.gif" title="Please wait.." alt="Please wait.." />Loading...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">
                              
                       
                        <asp:GridView ID="grdextraimage" runat="server" Width="100%"  CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" >
                           
                           
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
                                <asp:TemplateField HeaderText="Url Path">
                                    <ItemTemplate>
                                        <%# Eval("url_path")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="350px" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="created_date" HeaderText="Created Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>
                              <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                           <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>' 
                                            CommandName="Active" CommandArgument='<%# Eval("banner_id") %>'/>
                                            <asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("banner_id") %>' ToolTip="Edit" CssClass="btn btn-info btn-xs" ><i class="fa fa-edit"></i> edit</asp:LinkButton>                                                           
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("banner_id") %>' ToolTip="Delete"  CssClass="btn btn-danger btn-xs" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
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
        </div>
    </div>  
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

