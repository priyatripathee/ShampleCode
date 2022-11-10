<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="pagelabel.aspx.cs" Inherits="strutt.Admin.pagelabel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
    <asp:UpdatePanel ID="UPnlBanner" runat="server">
        <ContentTemplate>
            <section class="page--header">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6"> 
            <h2 class="page--title h5"> Page Label</h2>
            <ul class="breadcrumb">
              <li class="breadcrumb-item">Application Tools</li>
              <li class="breadcrumb-item active"><a href="city.aspx"><span>Page Label</span></a></li>
            </ul>
          </div>
          <div class="col-lg-6"> 
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
          </div>
        </div>
      </div>
    </section>
            <section class="main--content">
      <div class="panel nav-tabs"> 
        <div class="records--header">
          <div class="title fa-shopping-bag">
            <h3 class="h3">Page Label <a href="#" class="btn btn-sm btn-outline-info"> Page Label</a></h3>
            <p><asp:Label ID="lbl_total_records" runat="server" CssClass="msg1"></asp:Label></p>
          </div>
        </div>
      </div>
      <div class="panel"> 
        <div class="records--body"> 
           <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
          <div class="tab-content"> 
            <div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Page Name: <span class="requied">* </span></span>
                  <div class="col-md-4">
                    <asp:TextBox ID="txtpagename" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvpagename" runat="server" ErrorMessage="please enter Page Name" Display="Dynamic"
                                        ControlToValidate="txtpagename" ForeColor="Red"  ValidationGroup="prct"></asp:RequiredFieldValidator>
                  </div>
                <span class="label-text col-md-2 col-form-label">Label Name:</span>
                  <div class="col-md-4">
                   <asp:TextBox ID="txtlabelname" runat="server" CssClass="form-control"></asp:TextBox>
                  </div>
                
                </div>
                <div class="form-group row"> <span class="label-text col-md-2 col-form-label">Label Value: <span class="requied">* </span></span>
                  <div class="col-md-4">
                     <asp:TextBox ID="txtlabelvalue" runat="server" CssClass="form-control"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rfvlabelvalue" runat="server" ErrorMessage="please enter Label Value" Display="Dynamic"
                                        ControlToValidate="txtlabelvalue" ForeColor="Red"  ValidationGroup="prct"></asp:RequiredFieldValidator>
                  </div>
                </div>
            </div>
            <div class="row ">
              <div class="col-md-12" style="text-align: center;">
                   <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prct" OnClick="btnSubmit_Click"/>
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-rounded btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
              </div>
            </div>
          </div>
        </div>
        </div>
         <div class="panel"> 
                <div class="x_content">
                    <div class="table-responsive">
                       <asp:GridView ID="gvPageLabel" runat="server" Width="100%" CssClass="table table-striped jambo_table bulk_action"
                            AutoGenerateColumns="False" DataKeyNames="label_id,page_name"  OnRowCommand="gvPageLabel_RowCommand" OnRowDeleting="gvPageLabel_RowDeleting">
                            <EmptyDataTemplate>
                                <span>Sorry! No Record Found.</span>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Page Name ">
                                    <ItemTemplate>
                                        <%# Eval("page_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Label Name">
                                    <ItemTemplate>
                                        <%# Eval("label_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Label Value">
                                    <ItemTemplate>
                                        <%# Eval("label_value")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="modified_date" HeaderText="Modified Date" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundField>
                              <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="EditRecored"
                                            CommandArgument='<%# Bind("label_id") %>' ToolTip="Edit" CssClass="label label-success"><i class="fa fa-edit"></i> edit</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnImgDelete" runat="server" CommandName="Delete"
                                                                CommandArgument='<%# Bind("label_id") %>' ToolTip="Delete" CssClass="label label-danger" OnClientClick="javascript:return confirm('Are you sure you want to Remove this Banner!');"><i class="fa fa-trash"></i> delete</asp:LinkButton>
                                    </ItemTemplate>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
</asp:Content>

