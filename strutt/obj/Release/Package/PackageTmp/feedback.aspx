<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="strutt.feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/normalize.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .rate_smiley_container {
            -ms-flex-pack: distribute;
            cursor: pointer;
            display: -ms-flexbox;
            display: flex;
            justify-content: space-around;
            position: relative;
            width: 290px;
            margin: 0 auto;
        }

        .rate_smiley {
            background: url(img/smiley_v1.png);
            background-position-x: 0%;
            background-repeat: repeat;
            background-size: auto;
            background-repeat: no-repeat;
            background-size: 265px;
            float: left;
            height: 49px;
            width: 49px;
        }

            .rate_smiley.rate1 {
                background-position-x: 0;
            }

            .rate_smiley.rate2 {
                background-position-x: -54px;
            }

            .rate_smiley.rate3 {
                background-position-x: -108px;
            }

            .rate_smiley.rate4 {
                background-position-x: -162px;
            }

            .rate_smiley.rate5 {
                background-position-x: -216px;
            }

            .rate_smiley:hover {
                background-position-y: -55px;
            }

        h2.heading {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 20px;
            width: 90px;
            border-bottom: 2px solid #fdd934;
            color: #000;
            padding-bottom: 10px;
            text-align: center;
            margin: 0 auto;
        }

        .order_placed_feedback .rate_smiley.rate1 {
            background-position-x: 0;
        }

        .order_placed_feedback .rate_smiley.active, .order_placed_feedback .rate_smiley:hover {
            background-position-y: -55px;
        }

        .order_placed_feedback {
            max-width: 506px;
        }

        .order_placed_feedback {
            background: #fff;
            color: rgba(0,0,0,.8);
            font-size: 18px;
            line-height: normal;
            margin: 0 auto 10px;
            max-width: 430px;
            min-height: 200px;
            padding: 20px 0;
            text-align: center;
        }

        .order_placed_feedback .rate_smiley {
            background-position: 0% 0%;
            background-repeat: repeat;
            background-size: auto;
            background-repeat: no-repeat;
            background-size: 265px;
            float: left;
            width: 49px;
        }

        .fbUnderline {
            background-color: #fdd835;
            border: none;
            height: 3px;
            margin: 12px auto;
            width: 70px;
        }

        .order_placed_feedback .default_question {
            float: none;
            font-size: 18px;
        }

        .order_placed_feedback .placed_feedback_questions {
            margin: 30px auto;
        }

        .finalSubmitButton {
            background: #51cccc;
            border: none;
            color: #fff;
            float: left;
            margin-left: 20px;
            margin-top: 10px;
            outline: none;
            padding: 10px 20px;
        }
        .order_placed_feedback .placed_feedback_questions .checkbox_button .feedback_question label{
            border: 1px solid #979797;
            border-radius: 22px;
            color: #979797;
            cursor: pointer;
            display: inline-block;
            font-size: 13px;
            font-weight: 400;
            margin: 5px;
            padding: 12px;
        }
        .order_placed_feedback .placed_feedback_questions .checkbox_button .feedback_question input[type=checkbox]:checked + label 
            { background-color:#fdd835; color:#000;}

        label {
            display: inline-block;
            font-weight: 700;
            margin-bottom: 5px;
            max-width: 100%;
        }

        .order_placed_feedback .placed_feedback_questions .checkbox_button input[type="checkbox"] {
            display: none;
        }

        .order_placed_feedback textarea {
            font-size: 16px;
            height: 225px !important;
        }

        .order_placed_feedback textarea {
            font-size: 14px;
            height: 175px !important;
            margin-top: 44px;
            padding: 20px;
            resize: none;
            text-align: left;
        }

        .col-xs-12 {
            width: 100%;
        }

        .order_placed_feedback .rate_smiley.rate1 {
            background-position-x: 0;
        }

        .order_placed_feedback .rate_smiley.active, .order_placed_feedback .rate_smiley:hover {
            background-position-y: -55px;
        }

        .order_placed_feedback .rate_smiley.rate2 {
            background-position-x: -54px;
        }

        .order_placed_feedback .rate_smiley.rate3 {
            background-position-x: -108px;
        }

        .order_placed_feedback .rate_smiley.rate4 {
            background-position-x: -162px;
        }

        .order_placed_feedback .rate_smiley.rate5 {
            background-position-x: -216px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="container">
        <div class="margin-t-40">
            <div class="flex main-center">
                <div class="col-12-12 col-sm-4-12 text-center">
                    <div class="order_placed_feedback">
                            <div>
                                <p class="title"><asp:Literal ID="litRateTitle" runat="server"></asp:Literal></p>
                                <hr class="fbUnderline">
                                <div class="rate_smiley_container">
                                    <asp:LinkButton ID="lbtnRate1" runat="server" CssClass="rate_smiley rate1 active" OnClick="lbtnRate1_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnRate2" runat="server" CssClass="rate_smiley rate2 false" OnClick="lbtnRate2_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnRate3" runat="server" CssClass="rate_smiley rate3 false" OnClick="lbtnRate3_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnRate4" runat="server" CssClass="rate_smiley rate4 false" OnClick="lbtnRate4_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnRate5" runat="server" CssClass="rate_smiley rate5 false" OnClick="lbtnRate5_Click"></asp:LinkButton>
                                </div>
                            </div>
                            <br>
                            <div>
                                <p class="default_question col-xs-12">What can we improve?</p>
                                <div class="placed_feedback_questions">
                                    <div>
                                      <div class="checkbox_button">
                                        <div style="display: inline-block;">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Ease of browsing" CssClass="feedback_question" />
                                        </div>
                                        <div style="display: inline-block;">
                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Our collection" CssClass="feedback_question" />
                                        </div>
                                        <div style="display: inline-block;">
                                            <asp:CheckBox ID="CheckBox3" runat="server" Text="Product information" CssClass="feedback_question" />
                                        </div>
                                        <div style="display: inline-block;">
                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="Ease of payment" CssClass="feedback_question" />
                                        </div>
                                        <div style="display: inline-block;">
                                            <asp:CheckBox ID="CheckBox5" runat="server" Text="Product selection" CssClass="feedback_question" />
                                        </div>
                                      </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="col-xs-12">
                                    <asp:TextBox ID="txtSuggestion" runat="server" placeholder="Write a Suggestion....." MaxLength="255" CssClass="col-xs-12" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSuggestion" runat="server" ControlToValidate="txtSuggestion" CssClass="Validators" SetFocusOnError="true" 
                                        Display="Dynamic" ErrorMessage="Please write Suggestion" ForeColor="Red" ValidationGroup="v1" />
                                </div>
                            </div>
                            <div style="display: inline-block;">
                                <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="finalSubmitButton" OnClick="btnSubmit_Click" ValidationGroup="v1" />
                                <asp:LinkButton ID="btnContinueShopping" runat="server" PostBackUrl="~/category.aspx" CssClass="finalSubmitButton btn btn-primary">Continue Shopping</asp:LinkButton>
                            </div>
                            <div class="clear"></div>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
