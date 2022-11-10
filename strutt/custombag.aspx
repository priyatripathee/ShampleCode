<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="custombag.aspx.cs" Inherits="strutt.custombag" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="google-site-verification" content="NdGYBoaVysvJyeSLxvbpwfWiraTxlMf43eTQdXyu7w8" />
    <title>The Strutt Store</title>
    <meta name="description" content=""/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="icon" type="image/png" href="../img/favicon.ico"/>
   <link href="../assets/css/vendor/bootstrap.min.css" rel="stylesheet" />
    <!-- Icons Css -->
    <link href="../assets/css/vendor/linearicons.min.css" rel="stylesheet" />
    <link href="../assets/css/vendor/fontawesome-all.min.css" rel="stylesheet" />
    <!-- Animation Css -->
    <link href="../assets/css/plugins/animation.min.css" rel="stylesheet" />
    <!-- Slick Slier Css -->
    <link href="../assets/css/plugins/slick.min.css" rel="stylesheet" />
    <!-- Magnific Popup CSS -->
    <link href="../assets/css/plugins/magnific-popup.css" rel="stylesheet" />
    <!-- Easyzoom CSS -->
    <link href="../assets/css/plugins/easyzoom.css" rel="stylesheet" />
    <!-- Vendor & Plugins CSS (Please remove the comment from below vendor.min.css & plugins.min.css for better website load performance and remove css files from avobe) -->
    <%-- <link href="../assets/css/vendor/vendor.min.css" rel="stylesheet" />
    <link href="../assets/css/plugins/plugins.css" rel="stylesheet" />--%>
    <!-- Main Style CSS -->
    <link href="../assets/css/style.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

    <style type="text/css">
        .form-custum {
            width: 90%;
            padding: 15px 12px;
            border-radius: 2px;
            background-color: #fff;
            border: 1px solid #e4e4e4;
        }

        .font-style {
            width: 90%;
            border-radius: 2px;
            background-color: #fff;
            border: 1px solid #e4e4e4;
        }

        .dot {
            height: 25px;
            width: 25px;
            background-color: #bbb;
            border-radius: 50%;
            display: inline-block;
            border: 1px solid #bbb;
        }

        .div-img {
            /*height: 100%;*/
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 640px;
        }
        .font1 {
            font-family: Arial Black;
        }

        .font2 {
            font-family: Comic sans MS;
        }
        #ball {
                font-size: 18px;
                font-weight:bold;
            cursor: pointer;
            /*width: 36px;*/
            height: 20px;
        }

        .selected {
            border-color: #00ff00;
            border-width: 2px;
        }
    </style>
    <style type="text/css">
        .cust-container {
            width: 1300px !important;
            max-width:none;
            margin-left: 0;
            margin-right: 0;
        }
        .left{width:800px; float:left;margin:10px 30px;}
        .right{width:400px; float:left;}
        .clearfix{clear:both;}
    </style>

        
            <!--************************************
                            Header Start
            *************************************-->
    <div class="modal-backdrop" style="background-color: transparent;"></div>
<div class="cust-container">
  <header class="margin-t-20">
    <div class="flex cross-bottom gutter-sm">
      <div class="col-4-12"></div>
      <div class="col-4-12">
        <div class="text-center"> <asp:LinkButton ID="btnHome" runat="server" PostBackUrl="~/default.aspx"><img src="../../img/logo.png" width="150"/></asp:LinkButton> </div>
      </div>
      <div class="col-4-12"></div>
    </div>
  </header>
</div>
<hr class="hr hr-light margin-t-10" />

            <!--************************************
                            Header End
            *************************************-->

    <div class="cust-container">
        <div class="margin-t-20">
        <h3 class="h4" style="margin-left:30px;">
            <asp:Label ID="lblProductName" runat="server"></asp:Label>
            <asp:Label ID="lblSalePrice" runat="server"></asp:Label>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </h3>
            <asp:HiddenField ID="hfLetter" runat="server" />
            <asp:HiddenField ID="hfStyle" runat="server" Value="font1" />
            <asp:HiddenField ID="hfColor" runat="server" Value="gray" />
            <asp:HiddenField ID="hfXPoint" runat="server" />
            <asp:HiddenField ID="hfYPoint" runat="server" />
            <div >
                <div class="block-steps div-img left" id="image" runat="server">
                        <div id="ball" class="postion">
                            <span id="spnTag" class="font1">STR</span>
                        </div>
                </div>
                <div class="right">
                    <h1 class="h1 margin-t-10">MAKE IT PERSONAL</h1>
                    <h3 class="h3 margin-t-10">ADD RS.249.00 FOR PERSONALIZATION</h3>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <h3 class="padding-10">LETTERS</h3>
                        <div class="flex gutter-sm">
                            <div class="col-6-12">

                                <asp:TextBox ID="txtLetter" runat="server" CssClass="form-custum" MaxLength="3">STR</asp:TextBox>
                            </div>
                            <div class="col-6-12">
                                <asp:Label ID="lblHeading" runat="server">Add up to three letters—go ahead, express yourself.</asp:Label>
                            </div>
                        </div>
                    </div>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <h3 class="padding-10">PICK YOUR FONT</h3>
                        <div class="flex gutter-sm">
                            <div class="col-6-12">
                                <input type="button" id="btnStyle1" class="font-style font text-center" value="Style 1" />
                            </div>
                            <div class="col-6-12">
                                <input type="button" id="btnStyle2" class="font-style font2 text-center" value="Style 2" />
                            </div>
                        </div>
                    </div>
                    <hr class="hr-light margin-t-40" />
                    <div class="padding-25">
                        <h3 class="padding-10">COLOR</h3>
                        <div class="flex gutter-sm">
                            <div id="divcolor">
                                <span class="dot" id="colBlack" style="background-color: black" title="black"></span>
                                <span class="dot" id="colGray" style="background-color: gray" title="gray"></span>
                                <span class="dot" id="colSandybrown" style="background-color: sandybrown" title="sandybrown"></span>
                                <span class="dot" id="colWhite" style="background-color: white" title="white"></span>
                                <span class="dot" id="colRed" style="background-color:red" title="red"></span>
                                <span class="dot" id="colchartreuse" style="background-color:chartreuse" title="chartreuse"></span>
                                <span class="dot" id="colyellow" style="background-color:yellow" title="yellow"></span>
                                <br />
                                <span class="dot" id="colgold" style="background-color:gold" title="gold"></span>
                                <span class="dot" id="colpink" style="background-color:pink" title="pink"></span>
                                <span class="dot" id="colskyblue" style="background-color:skyblue" title="skyblue"></span>
                                <span class="dot" id="colblueviolet" style="background-color:blueviolet" title="blueviolet"></span>
                                <span class="dot" id="coldodgerblue" style="background-color:dodgerblue" title="dodgerblue"></span>
                            </div>
                        </div>
                    </div>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <p>Show the world who you are. Personalize your bag with up to 3 letters for a custom look. We'll embroider it to order and ship it to you within 1 week.</p>
                        </div>
                     <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <asp:Button ID="btnAddtoCart" runat="server" CssClass="btn btn-primary" Text="ADD TO CART" OnClick="btnAddtoCart_Click" />
                    </div>
                </div>

        <div class="clearfix"></div>

            </div>

        <div class="clearfix"></div>
        </div>
        <div class="clearfix"></div>
    </div>
    
    <script type="text/javascript">

        $(function () {

            $("#spnTag").text($('#txtLetter').val());

            if ($('#hfStyle').val() != '') {
                $("#spnTag").removeClass();
                $("#spnTag").addClass($('#hfStyle').val());

                $("#btnStyle1").removeClass('selected');
                $("#btnStyle2").removeClass('selected');

                if ($('#hfStyle').val() == 'font1')
                    $("#btnStyle1").addClass('selected');
                if ($('#hfStyle').val() == 'font2')
                    $("#btnStyle2").addClass('selected');
            }

            if ($('#hfColor').val() != '')
                $("#spnTag").css('color', $('#hfColor').val());

            $('#divcolor span').each(function () {
                this.className = 'dot';
                if ($(this).attr('title') == $('#hfColor').val()) {
                    this.className = 'dot selected';
                }
            });

            //debugger;
            if ($('#hfXPoint').val() != '') {
                
                $("#ball").css({ 'position': 'absolute', 'left': $('#hfXPoint').val() + 'px', 'top': $('#hfYPoint').val() + 'px' });
                //$("#ball").css({ 'position': 'absolute', 'left': $('#hfXPoint').val(), 'top': $('#hfYPoint').val() });

                //$("#ball").css('left', $('#hfXPoint').val());
                //$("#ball").css('top', $('#hfYPoint').val());
            }
           

            $('#txtLetter').bind('input', function () {
                $("#spnTag").text($('#txtLetter').val());
                //$("#hfLetter").val($('#txtLetter').val());
            });

            $("#btnStyle1").click(function () {
                $("#spnTag").removeClass('font2');
                $("#spnTag").addClass('font1');

                $("#hfStyle").val('font1');

                $("#btnStyle1").addClass('selected');
                $("#btnStyle2").removeClass('selected');
            });

            $("#btnStyle2").click(function () {
                $("#spnTag").removeClass('font1');
                $("#spnTag").addClass('font2');

                $("#hfStyle").val('font2');

                $("#btnStyle1").removeClass('selected');
                $("#btnStyle2").addClass('selected');
            });

            $('#divcolor span').click(function () {
                $("#spnTag").css('color', $(this)[0].title)

                $("#hfColor").val($(this)[0].title);

                $('#divcolor span').each(function () {
                    this.className = 'dot';
                });
                this.className = 'dot selected';
            })

        });
    </script>

    <script type="text/javascript">

        ball.onmousedown = function (event) {

            let shiftX = event.clientX - ball.getBoundingClientRect().left;
            let shiftY = event.clientY - ball.getBoundingClientRect().top;

            ball.style.position = 'absolute';
            ball.style.zIndex = 1000;
            //document.body.append(ball);

            moveAt(event.pageX, event.pageY);

            function moveAt(pageX, pageY) {

                if (pageX == 430) {

                    ball.style.left = pageX - shiftX + 'px';
                }

              
                //ball.style.left = 430 - shiftX + 'px';
                    ball.style.top = pageY - shiftY + 'px';
                console.log(pageX);
                console.log(pageY);

                $("#hfXPoint").val(pageX - shiftX);
                $("#hfYPoint").val(pageY - shiftY);
            }

            function onMouseMove(event) {
                moveAt(event.pageX, event.pageY);

                ball.hidden = true;
                let elemBelow = document.elementFromPoint(event.clientX, event.clientY);
                ball.hidden = false;

            }

            document.addEventListener('mousemove', onMouseMove);

            ball.onmouseup = function () {
                document.removeEventListener('mousemove', onMouseMove);
                ball.onmouseup = null;
            };

            image.onmouseup = function () {
                document.removeEventListener('mousemove', onMouseMove);
                ball.onmouseup = null;
            };

        };

        ball.ondragstart = function () {
            return false;
        };
    </script>

        <div class="clearfix"></div>
        <!--************************************
                            Footer Start
            *************************************-->
          <div class="cust-container">
  <footer>

    <hr class="hr-light margin-t-40" />
    <div class="margin-t-40">
      <div class="flex gutter">
        <div class="col-12-12 col-sm-4-12 text-center">
          <h4 class="h6 bold">Delivery in India only</h4>
        </div>
        <div class="col-12-12 col-sm-4-12 text-center">
          <h4 class="h6 bold"> 7 days return policy </h4>
          <p class="margin-t-10"> <a class="link"  href="../terms-conditions.aspx"> Simply return it within 7 days for a refund.</a> </p>
        </div>
           <div class="col-12-12 col-sm-4-12 text-center">
             <h4 class="h6 bold"> We Accept</h4>
            <p class="margin-t-10">
        		<a href="#">
                <asp:Image runat="server" ImageUrl="../images/card-icon-1.jpg" />
			</a>
            <a href="#">
             <asp:Image ID="Image1" runat="server" ImageUrl="../images/card-icon-2.jpg" />
			</a>
            <a href="#">
             <asp:Image ID="Image2" runat="server" ImageUrl="../images/card-icon-3.jpg" />
			</a>
            <a href="#">
             <asp:Image ID="Image3" runat="server" ImageUrl="../images/card-icon-4.jpg" />
			</a>
            </p>
            </div>
      </div>
    </div>
    </br>
      <hr class="hr-light margin-t-40" />
    <div class="col-12-12 text-center">
    <div class="margin-t-10"> 
   <a  href="https://www.facebook.com/theSTRUTTstore/"> <span class="margin-r-20"><i class="fa fa-facebook" aria-hidden="true"></i></span> </a>
   <a href="https://www.instagram.com/thestruttstore/"> <span><i class="fa fa-instagram" aria-hidden="true"></i></span></a> </div>
      <div class="margin-t-10"> <a class="link"  href="../privacy-policy.aspx">privacy-policy</a> <span>|</span> <a class="link"  href="../terms-conditions.aspx">terms & conditions</a> <span>|</span> <a class="link"  href="../return-policy.aspx">exchanges & returns</a><span> | </span><a class="link"  href="../sitemap.aspx">site map</a> </div>
	  <div class="margin-t-10-linkcolor"> Copyright © Strutt 2018 All rights reserved. | Developed  by: <a href="http://carbonmedia.in" target="_blank">Carbon Media</a></div>
      
    </div>
  </footer>
  </div>
  <script type='text/javascript' data-cfasync='false'>      window.purechatApi = { l: [], t: [], on: function () { this.l.push(arguments); } }; (function () { var done = false; var script = document.createElement('script'); script.async = true; script.type = 'text/javascript'; script.src = 'https://app.purechat.com/VisitorWidget/WidgetScript'; document.getElementsByTagName('HEAD').item(0).appendChild(script); script.onreadystatechange = script.onload = function (e) { if (!done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) { var w = new PCWidget({ c: 'b56ed792-3599-45de-86a1-807b90dd92de', f: true }); done = true; } }; })();</script>

<div class="templates hidden">
  <div class="toast">
    <div class="message"></div>
    <span class="remove"><i class="fa fa-times" aria-hidden="true"></i></span> </div>
</div>
            <!--************************************
                            Footer End
            *************************************-->
    
    
<!--McFee-->
  
  
   <!-- jQuery JS -->
        <script src="../../assets/js/vendor/jquery-3.3.1.min.js" type="text/javascript"></script>
        <!-- Bootstrap JS -->
        <script src="../../assets/js/vendor/bootstrap.min.js" type="text/javascript"></script>

        <!-- Modernizer JS -->
        <script src="../../assets/js/vendor/modernizr-2.8.3.min.js" type="text/javascript"></script>



        <!-- Fullpage JS -->
        <script src="../../assets/js/plugins/fullpage.min.js" type="text/javascript"></script>
        <!-- Slick Slider JS -->
        <script src="../../assets/js/plugins/slick.min.js" type="text/javascript"></script>
        <!-- Countdown JS -->
        <script src="../../assets/js/plugins/countdown.min.js" type="text/javascript"></script>
        <!-- Magnific Popup JS -->
        <script src="../../assets/js/plugins/magnific-popup.js" type="text/javascript"></script>
        <!-- Easyzoom JS -->
        <script src="../../assets/js/plugins/easyzoom.js" type="text/javascript"></script>
        <!-- ImagesLoaded JS -->
        <script src="../../assets/js/plugins/images-loaded.min.js" type="text/javascript"></script>
        <!-- Isotope JS -->
        <script src="../../assets/js/plugins/isotope.min.js" type="text/javascript"></script>
        <!-- YTplayer JS -->
        <script src="../../assets/js/plugins/YTplayer.js" type="text/javascript"></script>

        <!-- Instagramfeed JS -->
        <script src="../../assets/js/plugins/jquery.instagramfeed.min.js" type="text/javascript"></script>
        <!-- Ajax Mail JS -->
        <script src="../../assets/js/plugins/ajax.mail.js" type="text/javascript"></script>
        <!-- wow JS -->
        <script src="../../assets/js/plugins/wow.min.js" type="text/javascript"></script>
        <!-- Plugins JS (Please remove the comment from below plugins.min.js for better website load performance and remove plugin js files from avobe) -->

        <%--<script src="../assets/js/plugins/plugins.js"></script>--%>

        <!-- Main JS -->
        <script src="../../assets/js/main.js" type="text/javascript"></script>

    <%--<script type="text/javascript" src="https://cdn.ywxi.net/js/1.js" async></script>--%> <%-- Comment by chandni--%>
    </form>
</body>
</html>


