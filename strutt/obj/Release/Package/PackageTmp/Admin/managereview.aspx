<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true"
    CodeBehind="managereview.aspx.cs" Inherits="strutt.Admin.managereview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <!-- Page Title Start -->
            <h2 class="page--title h5">Review</h2>
            <!-- Page Title End -->
            
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Blog</li>
              <li class="breadcrumb-item active"><a href="#"><span>Manage Review</span></a></li>
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
    <asp:UpdatePanel ID="uPanelReview" runat="server">
        <ContentTemplate>
            <section class="main--content">
                 <div class="panel nav-tabs"> 
        <!-- Records Header Start -->
        <div class="records--header">
          <div class="title fa-shopping-bag">
            <h3 class="h3">Review<a href="#" class="btn btn-sm btn-outline-info">Product Review</a></h3>
            <p>  <asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
        <!-- Records Header End --> 
      </div>
           <div class="panel"> 
                <div class="x_content">
                    <div class="table-responsive">
                        <div id="frm" method="post">
                        <asp:GridView ID="grdProductReview" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="review_id" OnRowCommand="grdProductReview_RowCommand"
                            OnRowDataBound="grdProductReview_RowDataBound" OnRowDeleting="grdProductReview_RowDeleting"
                            OnRowEditing="grdProductReview_RowEditing" OnRowUpdating="grdProductReview_RowUpdating"
                            OnRowCancelingEdit="grdProductReview_RowCancelingEdit">
                            <EmptyDataTemplate>
                                <span>Sorry! No Record Found.</span>
                            </EmptyDataTemplate>
                           <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-CssClass="griditem01" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <%# Eval("product_name")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                    <EditItemTemplate>
                                        <asp:Label ID="lblProName" CssClass="form-control" runat="server" Text='<%# Eval("product_name") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" ItemStyle-CssClass="griditem01" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("user_name")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditUName" CssClass="form-control" Width="150px" runat="server"
                                            Text='<%# Eval("user_name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditUName" runat="server" ErrorMessage="Please enter user name"
                                            ControlToValidate="txtEditUName" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Title" ItemStyle-CssClass="griditem01" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <%# Eval("title")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditTitleName" CssClass="form-control" Width="200px" runat="server"
                                            Text='<%# Eval("title") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditTitleName" runat="server" ErrorMessage="Please enter title"
                                            ControlToValidate="txtEditTitleName" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rating" ItemStyle-CssClass="griditem01" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <%# Eval("Rating")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hfieldRating" Value='<%# Eval("rating")%>' runat="server" />
                                        <asp:DropDownList ID="ddlEditRating" runat="server" Width="50px">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviews" ItemStyle-CssClass="griditem01" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <%# Eval("reviews")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditReviewName" CssClass="form-control" Width="300px" TextMode="MultiLine"
                                            Height="100px" runat="server" Text='<%# Eval("reviews") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVEditReviewName" runat="server" ErrorMessage="Please enter review"
                                            ControlToValidate="txtEditReviewName" ForeColor="Red" ValidationGroup="revSubmit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviews" ItemStyle-CssClass="griditem01">
                                    <ItemTemplate>
                                        <%# Eval("CDate")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblCDate" CssClass="form-control" runat="server" Text='<%# Eval("CDate") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText ="Active" ItemStyle-HorizontalAlign="Center" >
                                   <ItemTemplate>
                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("is_active") %>'
                                            ToolTip="Active"></asp:Label>
                                        <asp:ImageButton ID="imgBtnActive" runat="server" AlternateText='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ToolTip='<%# Eval("is_active").ToString().ToLower()=="true" ? "Active" : "Inactive" %>'
                                            ImageUrl='<%#"/Admin/images/"+DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                            CommandName="Active" CommandArgument='<%# Eval("review_id") %>' />&nbsp;
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                             <%--<asp:LinkButton ID="btnImgEdit" runat="server"   CommandName="EditRecored" 
                                            CommandArgument='<%# Bind("review_id") %>' ToolTip="Edit" CssClass="label label-success" ><i class="fa fa-edit"></i> edit</asp:LinkButton>   --%>                                                        
                                            <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                            CommandArgument='<%# Bind("review_id") %>' ToolTip="Delete"  CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgBtnUpdate" runat="server" AlternateText="Update" ToolTip="Update"
                                            ImageUrl="~/Admin/images/button/icon_update.jpg" OnClientClick="javascript:return confirm('Are you sure you want to update this Record!');"
                                            ValidationGroup="abutUp" CommandName="Update" />
                                        <asp:ImageButton ID="imgBtnCancel" runat="server" AlternateText="Cancel" ToolTip="Cancel"
                                            ImageUrl="~/Admin/images/button/icon_cancel.jpg" CommandName="Cancel" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="200px" />
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
