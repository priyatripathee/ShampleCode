<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="strutt.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
<script src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php"
           type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
   
    <div style="text-align:center;font-weight:300">
        <a class="btn-floating btn btn-tw" type="button" role="button" title="Share on facebook"
   href="https://www.facebook.com/sharer/sharer.php?u=https://www.facebook.com/theSTRUTTstore?app_id=902327343653081" target="_blank"
   rel="noopener">
  <i class="fab fa-2x fa-facebook-square">fb2</i>
</a>
        <a href="http://www.facebook.com/sharer.php?u=https://thestruttstore.com/">Facebook</a>

        <a class="btn-floating btn btn-tw" type="button" role="button" title="Share on facebook"
   href="https://www.facebook.com/dialog/share?
  app_id=902327343653081
  &href=http://localhost:57233/blogdetail.aspx
  &redirect_uri=https://thestruttstore.com/" target="_blank"
   rel="noopener">
  <i class="fab fa-2x fa-facebook-square">share today</i>
</a>

        <a href="https://www.facebook.com/dialog/feed?&app_id=100578651692043&link=https://business.facebook.com/latest/home?asset_id=100578651692043&display=popup&quote=TEXT&hashtag=#HASHTAG" target="_blank">Share1</a>
        <a name='fb_share' type='button_count' href='http://www.facebook.com/sharer.php?appId=100578651692043&link=<?php the_permalink() ?>' rel='nofollow'>Fb |</a><script src='http://static.ak.fbcdn.net/connect.php/js/FB.Share' type='text/javascript'></script>
    <a name="sharebutton" type="button" href="http://www.facebook.com/sharer.php" >Share</a>  
        <asp:Label ID="lblShare" runat="server" Text=""></asp:Label>
    </div>
    <a name="sharebutton" type="button" href="http://www.facebook.com/sharer.php">Share 25 May</a>  

    <a id="facebook-Link"
    href="http://www.facebook.com/sharer.php?
    s=100
    &p[url]=https://thestruttstore.com/blogdetail.aspx?id=10020"
   
    target="_blank">
    test 2
</a>
<script>
$('#facebook-Link').click(function () {
    window.open($(this).attr('href'), 'sharer', 'width=626,height=436');
    return false;
});


    </script>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
