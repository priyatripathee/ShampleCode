<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="test_customize_product.aspx.cs" Inherits="strutt.test_customize_product" %>

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
            width: 100%;
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
            min-height: 640px;
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
    <style type="text/css">
        .cust-container {
            width: 1300px !important;
            max-width: none;
            margin-left: 0;
            margin-right: 0;
        }

        .left {
            width: 800px;
            float: left;
            margin: 10px 30px;
        }

        .right {
            width: 400px;
            float: left;
        }

        .clearfix {
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="cust-container">
        <div class="margin-t-20">
            <h3 class="h4" style="margin-left: 30px;">
                <asp:Label ID="lblProductName" runat="server"></asp:Label>
                <asp:Label ID="lblSalePrice" runat="server"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </h3>
            <asp:HiddenField ID="hfLetter" runat="server" />
            <asp:HiddenField ID="hfStyle" runat="server" Value="font1" />
            <asp:HiddenField ID="hfColor" runat="server" Value="gray" />
            <asp:HiddenField ID="hfXPoint" runat="server" />
            <asp:HiddenField ID="hfYPoint" runat="server" />
            <div>
                <div class="block-steps div-img left" id="image" runat="server" style="background-color: #f7f0ea;">
                    <div id="ball" class="postion">
                        <span id="spnTag" class="font1">STRUCT</span>
                    </div>
                </div>
                <div class="right">
                    <h3 class="h3 margin-t-10"><%--MAKE IT PERSONAL--%>
                        Make it yours
                    </h3>
                    <%--<h3 class="h3 margin-t-10">ADD RS.249.00 FOR PERSONALIZATION</h3>--%>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <h4 class="padding-10">LETTERS</h4>
                        <div class="flex gutter-sm">
                            <div class="col-6-12">

                                <asp:TextBox ID="txtLetter" runat="server" CssClass="form-custum">STRUCT</asp:TextBox>
                            </div>
                            <%-- <div class="col-6-12">
                                <asp:Label ID="lblHeading" runat="server">Add up to three letters—go ahead, express yourself.</asp:Label>
                            </div>--%>
                        </div>
                    </div>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <h4 class="padding-10">PICK YOUR FONT</h4>
                        <div class="flex gutter-sm">
                            <div class="col-6-12">
                                <input type="button" id="btnStyle1" class="font-style font text-center" value="STR Modern" />
                            </div>
                            <div class="col-6-12">
                                <input type="button" id="btnStyle2" class="font-style font2 text-center pyfnt" value="STR Classic" />
                            </div>

                        </div>
                    </div>
                    <hr class="hr-light margin-t-40" />
                    <div class="padding-25">
                        <h4 class="padding-10">COLOR</h4>
                        <div class="flex gutter-sm">
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
                        </div>
                    </div>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <p>Show the world who you are. Personalize your bag with up to 3 letters for a custom look. We'll embroider it to order and ship it to you within 1 week.</p>
                    </div>
                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <h4 class="padding-10">Your order</h4>
                        <table class="psn__pricing__list">
                            <tbody>
                                <tr>
                                </tr>
                                <tr>
                                    <td>The Backpack</td>
                                    <td class="psn__pricing">$165</td>
                                </tr>
                                <tr>
                                    <td>Embroidery</td>
                                    <td class="psn__pricing">$35</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>

                                </tr>
                                <tr>
                                    <td>Total</td>
                                    <td class="psn__pricing">$200</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


                    <hr class="hr-light margin-t-10" />
                    <div class="padding-25">
                        <asp:Button ID="btnAddtoCart" runat="server" CssClass="btn btn-primary" Text="ADD TO CART $200" />
                    </div>
                    <div class="padding-25">
                        <p style="font-size:11px;">Ships in 1-2 weeks. Since they’re made to order, personalized items cannot be exchanged or returned.</p>
                    </div>
                    <div style="margin:15px 0px;"></div>
                </div>

                <div class="clearfix"></div>

            </div>

            <div class="clearfix"></div>
        </div>
        <div class="clearfix"></div>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
