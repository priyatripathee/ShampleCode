<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="strutt.Admin.WebForm1" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
     <asp:UpdatePanel ID="updt" runat="server">
    <ContentTemplate>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Value="1">1</asp:ListItem>
        <asp:ListItem Value="2">2</asp:ListItem>
        <asp:ListItem Value="3">3</asp:ListItem>
    </asp:DropDownList>
        
    <asp:Panel runat="server" ID="pnl1" Visible="false" >
        <p> this is testing panel</p>
    </asp:Panel>
        </ContentTemplate> 
     </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
</asp:Content>
