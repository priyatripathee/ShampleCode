<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="success.aspx.cs" Inherits="strutt.success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style type="text/css">
   .rate_smiley_container {
    -ms-flex-pack: distribute;
    cursor: pointer;
    display: -ms-flexbox;
    display: flex;
    justify-content: space-around;
    position: relative;
    width: 290px; margin:0 auto;}
	.rate_smiley {
    background: url(img/smiley_v1.png);
    background-position-x: 0%;
    background-repeat: repeat;
    background-size: auto;
    background-repeat: no-repeat;
    background-size: 265px;
    float: left;
    height: 49px;
    width: 49px;}
	
	.rate_smiley.rate1 {
    background-position-x: 0;}
	
   .rate_smiley.rate2 {
    background-position-x: -54px;}
	
	.rate_smiley.rate3 {
    background-position-x: -108px;}
	
	.rate_smiley.rate4 {
    background-position-x: -162px;}
	
	.rate_smiley.rate5 {
    background-position-x: -216px;}
	
	.rate_smiley:hover {
    background-position-y: -55px;}
  

   
   
   </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
<asp:HiddenField ID="hfLoginType" runat="server" />

    <div class="container" style="padding:10px;">
               <div class="col-12-12 col-sm-12-12 text-center">
            <p><img style="width:200px;" src="img/order_success_v1.png"/></p>
                <h3 id="lblSuccessHead" runat="server" style="color:Green;">Successful Transactions</h3>
            <p>STR10001<asp:Label ID="lblOrderNo" runat="server"></asp:Label> Your order has been placed Successful!</p>
            <br>
            <p>Now sit back and relax while the good folks at our warehouse work their magic!</p>
            <asp:Label ID="lblsuccessMsg" runat="server">YOUR ORDER IS CONFIRMED.</asp:Label>
            <br>
            <br>
            <hr>
            <p style="color:#000; font-weight:bold;">Rate Your Shopping Experience</p>
            <br>
              <div class="rate_smiley_container">
               <a href="feedback.aspx?rate=1" class="rate_smiley rate1"></a>
               <a href="feedback.aspx?rate=2" class="rate_smiley rate2"></a>
               <a href="feedback.aspx?rate=3" class="rate_smiley rate3"></a>
               <a href="feedback.aspx?rate=4" class="rate_smiley rate4"></a>
               <a href="feedback.aspx?rate=5" class="rate_smiley rate5"></a>
              </div>
                 <%--<asp:Button ID="btnContinueShopping" runat="server" CssClass="btn btn-primary"
                Text="Continue Shopping" onclick="btnContinueShopping_Click" />--%>
            <asp:Label ID="lblResponse" runat="server" style="display:none;"></asp:Label>
         </div>
        </div>
<%--<div class="margin-t-50">
    <div class="flex main">
        <div class="container margin-t-50 mrg-left">
            <h1 id="lblSuccessHead" runat="server" style="color:Green;">Successful Transactions</h1>
            <br /><br />
            Your order <strong style="color:#8e471e;"> STR10001<asp:Label ID="" runat="server"></asp:Label></strong> has been placed.
            <br />
            <asp:Label ID="lblsuccessMsg" runat="server">YOUR ORDER IS CONFIRMED.</asp:Label>
            <br /><br />
            Please do check your spam folder incase you are receiving email from us.
            <br /><br />
            <asp:Button ID="btnContinueShopping" runat="server" CssClass="btn btn-primary btn-lg"
                Text="Continue Shopping" onclick="btnContinueShopping_Click" /><br /><br />
            <asp:Label ID="lblResponse" runat="server" style="display:none;"></asp:Label>
        </div>
    </div>
</div>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
    <script type="text/javascript">
    /* <![CDATA[ */
    var google_conversion_id = 827193669;
    var google_conversion_label = "Nc28CNOn8YoBEMXyt4oD";
    var google_remarketing_only = false;
    /* ]]> */
    </script>
    <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
    </script>


    <noscript>
    <div style="display:inline;">
    <img height="1" width="1" style="border-style:none;" alt="" src="//www.googleadservices.com/pagead/conversion/827193669/?label=Nc28CNOn8YoBEMXyt4oD&amp;guid=ON&amp;script=0"/>
    </div>
    </noscript>

</asp:Content>
