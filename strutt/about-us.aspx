<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="about-us.aspx.cs" Inherits="strutt.about_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <!-- breadcrumb-area start -->
<div class="breadcrumb-area">
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="row breadcrumb_box  align-items-center">
          <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
            <h2 class="breadcrumb-title">About Us</h2>
          </div>
          <div class="col-lg-6  col-md-6 col-sm-6"> 
            <!-- breadcrumb-list start -->
            <ul class="breadcrumb-list text-center text-sm-right">
              <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
              <li class="breadcrumb-item active">About Us</li>
            </ul>
            <!-- breadcrumb-list end --> 
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<!-- breadcrumb-area end -->
    <div class="site-wrapper-reveal border-bottom">
  <div class="about-us-pages-area">
    <div class="banner-video-area overflow-hidden section-space--pt_90">
      <div class="container">
        <div class="row">
          <div class="col-lg-12" style="text-align:center;">
              <img src="images/imgabout.jpg" />
           <%-- <p class="text-justify"> Oh Darling, Grab your Coat, leave a note and run away with me! Strutt is here to Fly, Roam, Explore  and Discover with you</p>
            <p class="text-justify"> STRUTT MOLDS BEAUTIFUL, DURABLE, ARTISAN TRAVEL BAGS THAT WILL LAST  A LIFETIME!</p>
            <p class="text-justify"> Strutt has set out to be the purveyor of the finest quality handcrafted travel bags, becoming the first ever one stop
solution catering to the travel needs of each traveler and providing a complete travel story.</p>
            <p class="text-justify"> Starting out with our ever-growing very exclusive range of carryon travel bags - duffel bags, cabin bags,
traveling backpacks and many more, some of which are already here, while some are yet to come.
</p>
            <p class="text-justify"> Most of our designs define the spirit of exploration and adventure. We believe vintage can be Modern, classic can
be edgy, minimalism can still pop color and eco-friendly is fashionable.</p>
            <p class="text-orange"> STRUTT IS HERE TO MAKE  TRAVEL EASY.
            </p>--%>
              <br /><br />
          </div>
        </div>
      </div>
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