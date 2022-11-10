<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="lookbook.aspx.cs" Inherits="strutt.lookbook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div id="jas-content">
        <div class="page-head pr tc" style="background: url(images/banner_look.jpg) no-repeat center center / cover;">
            <div class="jas-container pr">
                <h1 class="tu mb__10 cw" itemprop="headline">Lookbook</h1>
                <p>Let's see our collections</p>
                <ul class="jas-breadcrumb dib oh">
                    <li class="fl home"><a href="http://janstudio.net/gecko/fashion" title="Home">Home</a></li>
                    <li class="fl separator"><i class="fa fa-angle-right"></i></li>
                    <li class="fl current">Lookbook</li>
                </ul>
            </div>
        </div>
        <div class="jas-row jas-page">
            <div class="jas-col-md-12 jas-col-xs-12 mt__60 mb__60" role="main" itemscope="itemscope">
                <asp:Repeater ID="rpt_LookBook" runat="server">
                    <ItemTemplate>
                        <div class="vc_row vc_custom_1461320160882 vc_row-o-full-height vc_row-o-columns-middle vc_row-o-equal-height vc_row-o-content-middle vc_row-flex"
                            style="min-height: 35.68vh;">
                            <div class="wpb_column vc_column_container vc_col-sm-6">
                                <div class="vc_column-inner vc_custom_1461320670857">
                                    <div class="wpb_wrapper">
                                        <div class="wpb_text_column wpb_content_element  vc_custom_1504621192070">
                                            <div class="wpb_wrapper">
                                                <p>
                                                    <%--<img loading="lazy" class="alignnone size-full wp-image-3682" src="http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/1E073DDB85.jpg" alt="1E073DDB85" width="1920" height="1080" srcset="http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/1E073DDB85.jpg 1920w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/1E073DDB85-750x422.jpg 750w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/1E073DDB85-300x169.jpg 300w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/1E073DDB85-768x432.jpg 768w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/1E073DDB85-1024x576.jpg 1024w" sizes="(max-width: 1920px) 100vw, 1920px">--%>

                                                    <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/look_img/" + Eval("image") %>'
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
                                                <%--<h3 class="section-title tl" style="text-align: left;">Spring Women Collection</h3>
                                                <h4 class="fs_14" style="text-align: left;">1. <a class="cd chp" href="http://janstudio.net/gecko/?product=ribbed-bodycon-dress">Ribbed Bodycon Dress</a></h4>
                                                <h4 class="fs_14" style="text-align: left;">2. <a class="cd chp" href="http://janstudio.net/gecko/?product=classic-cotton-leggings">Classic Cotton Leggings</a></h4>
                                                <h4 class="fs_14" style="text-align: left;">3. <a class="cd chp" href="http://janstudio.net/gecko/?product=faux-suede-oxfords-brown">Faux Suede Oxfords Brown</a></h4>--%>
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
                                                <%--<h3 class="section-title tl" style="text-align: right;">Men Collections</h3>
                                                <h4 class="fs_14" style="text-align: right;">1. <a class="cd chp" href="http://janstudio.net/gecko/?product=ribbed-bodycon-dress">Ribbed Bodycon Dress</a></h4>
                                                <h4 class="fs_14" style="text-align: right;">2. <a class="cd chp" href="http://janstudio.net/gecko/?product=classic-cotton-leggings">Classic Cotton Leggings</a></h4>
                                                <h4 class="fs_14" style="text-align: right;">3. <a class="cd chp" href="http://janstudio.net/gecko/?product=faux-suede-oxfords-brown">Faux Suede Oxfords Brown</a></h4>--%>
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
                                                    <%--<img loading="lazy" class="alignnone size-full wp-image-4420" src="http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/B7A6EB72FF.jpg" alt="B7A6EB72FF" width="1920" height="1080" srcset="http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/B7A6EB72FF.jpg 1920w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/B7A6EB72FF-750x422.jpg 750w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/B7A6EB72FF-300x169.jpg 300w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/B7A6EB72FF-768x432.jpg 768w, http://janstudio.net/gecko/fashion/wp-content/uploads/2016/03/B7A6EB72FF-1024x576.jpg 1024w" sizes="(max-width: 1920px) 100vw, 1920px">--%>
                                                    <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/look_img/" + Eval("image") %>'
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
            <!-- $classes -->

        </div>
        <!-- .jas-row -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
