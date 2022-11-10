<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="preview_cust_img.aspx.cs" Inherits="strutt.preview_cust_img" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-7 col-md-6 col-sm-12 col-xs-12">
                <!-- Product Details Left -->
                <asp:Image ID="imgCustom" runat="server" />
            </div>
            <div class="col-lg-5 col-md-6 col-sm-12 col-xs-12">
                <div class="product-details-content">
                    <h5 class="font-weight--reguler mb-10">
                        <asp:Label ID="lblProductName" runat="server"></asp:Label>
                    </h5>
                    <p>
                    Message : <asp:Label ID="lblCustDetails" runat="server"></asp:Label></p>
                    <p>Quntity: <asp:Label ID="lblQty" runat="server"></asp:Label></p>
                    <p>Sale Price : <asp:Label ID="lblSalePrice" runat="server"></asp:Label></p>
                    <p>Custome Price : <asp:Label ID="lblCustPrice" runat="server"></asp:Label></p>
                    <p>Total Price : <asp:Label ID="lblTotalPrice" runat="server"></asp:Label></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
    
</asp:Content>