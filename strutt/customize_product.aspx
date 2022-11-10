
<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="customize_product.aspx.cs" Inherits="strutt.customize_product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
        .form-custum {
            width: 100%;
            padding: 15px 12px;
            border-radius: 2px;
            background-color: #fff;
            border: 1px solid #e4e4e4;
            text-transform: uppercase;
        }

        .font-style {
            border-radius: 2px;
            background-color: #fff;
            border: 1px solid #e4e4e4;
        }

        .dot {
            height: 35px;
            width: 35px;
            background-color: #bbb;
            border-radius: 50%;
            display: inline-block;
            border: 1px solid #bbb;
            margin: 3px;
        }

        .div-img {
            /*height: 100%;*/
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 515px;
        }
       @media only screen and (max-width: 768px) {
        .div-img {
            /*height: 100%;*/
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 400px;
        }
        #ball {
           font-size: 14px;
           font-weight: bold;
           cursor: pointer;
           /* width: 36px; */
           height: 15px;
           text-transform: uppercase;
       }
       }

        .font1 {
            font-family: Arial;
            text-transform: uppercase;
        }

        .font2 {
            font-family: Helvetica Neue;
            text-transform: uppercase;
        }

        #ball {
            font-size: 18px;
            font-weight: bold;
            cursor: pointer;
            /*width: 36px;*/
            height: 20px;
            text-transform: uppercase;
        }

        .selected {
            border-color: #001E3D;
            border-width: 2px;
        }

        .psn__pricing__list {
            color: #727373;
            font-family: "Graphik Web","Helvetica Neue","Arial","sans-serif";
            font-size: 14px;
            line-height: 17px;
            width: 100%;
        }
    </style>
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script type="text/javascript">
        function ConvertToImage(btnAddtoCart) {
            html2canvas($('[id*=image]')[0], {
                onrendered: function (canvas) {
                    var base64 = canvas.toDataURL();
                    $("[id*=hfImageData]").val(base64);
                    __doPostBack(btnAddtoCart.name, "");
                }
            });
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">

    <div class="container">
        <div class="row">
        <div class="col-lg-7 col-md-6 col-sm-12 col-xs-12"> 
          <!-- Product Details Left -->
            <asp:HiddenField ID="hfImageData" runat="server" />

          <div class="block-steps div-img" id="image" runat="server" style="background-color: #f7f0ea;">
            <div id="ball" class="postion">
                <span id="spnTag" class="font1">STRUCT</span>
            </div>
        </div>
        </div>
        <div class="col-lg-5 col-md-6 col-sm-12 col-xs-12">
          <div class="product-details-content">
            <h5 class="font-weight--reguler mb-10">
               <asp:Label ID="lblProductName" runat="server"></asp:Label>
                <asp:HiddenField ID="hfLetter" runat="server" />
            <asp:HiddenField ID="hfStyle" runat="server" Value="font1" />
            <asp:HiddenField ID="hfColor" runat="server" Value="gray" />
            <asp:HiddenField ID="hfXPoint" runat="server" />
            <asp:HiddenField ID="hfYPoint" runat="server" />
            </h5>
            <h3 class="price">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
              <strike>
              <asp:Label ID="lblDiscPrice" runat="server"></asp:Label>
              </strike> </h3>
            <asp:HiddenField ID="hfProId" runat="server"></asp:HiddenField>

              <h3 class="h3 margin-t-10"><%--MAKE IT PERSONAL--%>
                        Make it yours
                    </h3>
              <h4 class="padding-10">LETTERS</h4>

            <div class="quickview-peragraph">
                <asp:TextBox ID="txtLetter" runat="server" MaxLength="20" CssClass="form-custum">STRUCT</asp:TextBox>
                <hr class="hr-light margin-t-10" />
                <h4 class="padding-10">PICK YOUR FONT</h4>

                <input type="button" id="btnStyle1" class="font-style font text-center" value="STR Modern" />
                <input type="button" id="btnStyle2" class="font-style font2 text-center pyfnt" value="STR Classic" />
                <hr class="hr-light margin-t-10" />
                <h4 class="padding-10">COLOR</h4>
                <div id="divcolor">
                    <span class="dot" id="colBlack" style="background-color: black" title="black"></span>
                    <span class="dot" id="colGray" style="background-color: gray" title="gray"></span>
                    <span class="dot" id="colSandybrown" style="background-color: sandybrown" title="sandybrown"></span>
                    <span class="dot" id="colWhite" style="background-color: white" title="white"></span>
                    <span class="dot" id="colRed" style="background-color: red" title="red"></span>
                    <span class="dot" id="colGold" style="background-color: #CB9131" title="#CB9131"></span>
                    <br />
                    <span class="dot" id="colgold" style="background-color: gold" title="gold"></span>
                    <span class="dot" id="colpink" style="background-color: pink" title="pink"></span>
                    <span class="dot" id="colskyblue" style="background-color: skyblue" title="skyblue"></span>
                    <span class="dot" id="coldodgerblue" style="background-color: dodgerblue" title="dodgerblue"></span>
                    <span class="dot" id="colgreen" style="background-color: green" title="green"></span>
                    <span class="dot" id="colnavy" style="background-color: #001E3D" title="#001E3D"></span>
                </div>
                <hr class="hr-light margin-t-10" />
                <div class="padding-25">
                    <p>Show the world who you are. Personalize your bag with up to 3 letters for a custom look. We'll embroider it to order and ship it to you within 1 week.</p>
                </div>
                <hr class="hr-light margin-t-10" />
                <h4 class="padding-10">Your order</h4>
                <table class="psn__pricing__list">
                    <tbody>
                        <tr>
                        </tr>
                        <tr>
                            <td>Subtotal</td>
                            <td class="psn__pricing" style="text-align: right">Rs.
                                <asp:Label ID="lblSalePrice" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Embroidery</td>
                            <td class="psn__pricing" style="text-align: right">Rs.250.00</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>

                        </tr>
                        <tr>
                            <td>Total</td>
                            <td class="psn__pricing" style="text-align: right">Rs.<asp:Label ID="lblTotalPrice" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>

                <div class="quickview-action-wrap mt-30">
                  <asp:Button ID="btnAddtoCart" runat="server" CssClass="btn--lg btn--black font-weight--reguler text-white" Text="ADD TO CART" 
                      OnClick="btnAddtoCart_ExportToImage" OnClientClick="return ConvertToImage(this)" />
                </div>
                <div class="padding-25">
                    <p style="font-size: 11px;">Ships in 1-2 weeks. Since they’re made to order, personalized items cannot be exchanged or returned.</p>
                </div>

            </div>
            
          </div>
        </div>
      </div>
    </div>


    <script type="text/javascript">

        $(function () {

            $("#spnTag").text($('#cph_main_txtLetter').val());

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


            $('#cph_main_txtLetter').bind('input', function () {
                $("#spnTag").text($('#cph_main_txtLetter').val());
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

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
