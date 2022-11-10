<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="blog.aspx.cs" Inherits="strutt.blog1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Life @ Strutt</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Life @ Strutt</li>
                            </ul>
                            <%--<ul class="breadcrumb-list text-center text-sm-right">
                           <asp:Button ID="btnBlog" runat="server" Text="Add Your Story" OnClick="btnBlog_Click" CssClass="ribbon out-of-stock" />
                            </ul>--%>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <!-- Blog Page Area Start -->
        <div class="blog-page-wrapper section-space--pt_90 section-space--pb_120">
            <div class="container">
                <div class="row">
                    <asp:Repeater ID="rpt_blog" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-12">
                                <!-- Single Blog Item Start -->
                                <div class="single-blog-item mt-30">
                                    <div class="blog-thumbnail-box"><a  href='<%#"blogdetail.aspx?id="+Eval("blog_id")%>' class="thumbnail">
                                        <asp:Image ID="imgLarge" runat="server" ImageUrl='<%# "~/images/BlogImages/" + Eval("image") %>'
                                         alt="Blog Images" Height="327" Width="570" CssClass="img-r" />
                                    </a><a href='<%#"blogdetail.aspx?id="+Eval("blog_id")%>' class="btn-blog">Read more </a></div>
                                    <div class="blog-contents">
                                        <h6 class="blog-title"><a href='<%#"blogdetail.aspx?id="+Eval("blog_id")%>'><%# Eval("title") %></a></h6>
                                        <div class="meta-tag-box">
                                            <div class="meta date"><span><%# Convert.ToDateTime(Eval("created_date")).ToString("HH:mm, dd mmmm") %></span></div>
                                            <div class="meta author"><span><%# Eval("name") %></span></div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Single Blog Item End -->
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <!-- Blog Page Area End -->
    </div>
</asp:Content>

