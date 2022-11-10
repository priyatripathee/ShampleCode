<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="comingsoon.aspx.cs" Inherits="strutt.comingsoon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/normalize.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .mrg-left{margin-left:135px;}
            .clearfix{clear: both;}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
          <div class="margin-t-50">
 <div class="flex main">
 <div class="container margin-t-50 mrg-left">
        <h1 style="font-size:24px;"><strong>COMING SOON</strong></h1>
        <br />
        <br />
        <br />
        <br />
        <h2>Coming Soon...</h2>
        <img src="images/comingsoon.gif" height="200px" width="350px" />
        <br /><br /><br />
      <p><a class="btn btn-transparent" style="color:Black;" href="/">GO TO HOME PAGE</a></p>
        </div>
        </div>
        
    </div> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
    <script type="text/javascript">
    /* <![CDATA[ */
    var google_conversion_id = 827193669;
    var google_custom_params = window.google_tag_params;
    var google_remarketing_only = true;
    /* ]]> */
    </script>
    <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
    </script>
    <noscript>
    <div style="display:inline;">
    <img height="1" width="1" style="border-style:none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0"/>
    </div>
    </noscript>
</asp:Content>