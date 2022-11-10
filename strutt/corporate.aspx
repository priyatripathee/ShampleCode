<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="corporate.aspx.cs" Inherits="strutt.corporate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div id="jas-content">
        <div class="page-head pr tc" style="background: url(images/banner_look.jpg) no-repeat center center / cover;">
            <div class="jas-container pr">
                <h1 class="tu mb__10 cw" itemprop="headline">Corporate</h1>
                <p>Let's see our collections</p>
                <ul class="jas-breadcrumb dib oh">
                    <li class="fl home"><a href="/" title="Home">Home</a></li>
                    <li class="fl separator"><i class="fa fa-angle-right"></i></li>
                    <li class="fl current">Corporate</li>
                </ul>
            </div>
        </div>
        <div class="jas-row jas-page">
            <div class="jas-col-md-12 jas-col-xs-12 mt__60 mb__60" role="main" itemscope="itemscope">
                <asp:Repeater ID="rpt_Corporate" runat="server">
                    <ItemTemplate>
                        <div class="vc_row vc_custom_1461320160882 vc_row-o-full-height vc_row-o-columns-middle vc_row-o-equal-height vc_row-o-content-middle vc_row-flex"
                            style="min-height: 35.68vh;">
                            <div class="wpb_column vc_column_container vc_col-sm-6">
                                <div class="vc_column-inner vc_custom_1461320670857">
                                    <div class="wpb_wrapper">
                                        <div class="wpb_text_column wpb_content_element  vc_custom_1504621192070">
                                            <div class="wpb_wrapper">
                                                <p>
                                                    <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/corp_img/" + Eval("image") %>'
                                                        alt="Blog Images" CssClass="alignnone size-full wp-image-3682" />
                                                </p>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="wpb_column vc_column_container vc_col-sm-6 vc_col-has-fill">
                                <div class="vc_column-inner vc_custom_1461322700534">
                                    <div class="wpb_wrapper">
                                        <div class="wpb_text_column wpb_content_element  vc_custom_1461321207598">
                                            <div class="wpb_wrapper">
                                                <%# Eval("description") %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div class="vc_row vc_row-o-equal-height vc_row-o-content-middle vc_row-flex">
                            <div class="wpb_column vc_column_container vc_col-sm-6 vc_col-has-fill">
                                <div class="vc_column-inner vc_custom_1461322006278">
                                    <div class="wpb_wrapper">
                                        <div class="wpb_text_column wpb_content_element  vc_custom_1461321623597">
                                            <div class="wpb_wrapper" style="float: right; text-align: right;">
                                                <%# Eval("description") %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="wpb_column vc_column_container vc_col-sm-6">
                                <div class="vc_column-inner vc_custom_1461321638913">
                                    <div class="wpb_wrapper">
                                        <div class="wpb_text_column wpb_content_element  vc_custom_1504621211432">
                                            <div class="wpb_wrapper">
                                                <p>
                                                    <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/corp_img/" + Eval("image") %>'
                                                        alt="Blog Images" CssClass="alignnone size-full wp-image-3682" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>