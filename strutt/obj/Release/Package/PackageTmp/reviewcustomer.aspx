<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true" CodeBehind="reviewcustomer.aspx.cs" Inherits="strutt.reviewcustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .m-view {
            position: relative !important;
            padding-right: 15px !important;
            padding-left: 15px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">
                                <asp:Label ID="lblBName" runat="server"></asp:Label>
                                : Places- The Blog</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active"><a href="review.aspx">Places- The Blog</a></li>
                            </ul>
                            <%--<asp:Button ID="btnBlog" runat="server" Text="Add Your Story" OnClick="btnBlog_Click" CssClass="ribbon out-of-stock pull-right" />--%>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <!-- Blog Page Area Start -->
        <div class="blog-page-wrapper section-space--pt_120 section-space--pb_120">
            <div class="container">
                <div class="row">
                    <div class="col-lg-9 col-md-8">
                        <div class="row">
                            <div class="col-lg-12">
                                <!-- Single Blog Item Start -->
                                <div class="single-blog-item">
                                    <div class="blog-thumbnail-box">
                                        <a href="#" class="thumbnail">
                                            <asp:Image ID="lblImage" runat="server" class="img-fluid" alt="Blog Images" />
                                        </a>
                                    </div>
                                    <div class="blog-contents">
                                        <h3 class="blog-title-lg"><a href="#">
                                            <asp:Label ID="lbltitle" runat="server"></asp:Label></a></h3>
                                        <div class="meta-tag-box">
                                            <div class="meta date">
                                                <span>
                                                    <asp:Label ID="lblCreateDate" runat="server"></asp:Label>
                                                    (<asp:Label ID="lblCountView" runat="server"></asp:Label>)</span>
                                            </div>
                                            <div class="meta author">
                                                <span>
                                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label></span>
                                            </div>
                                        </div>

                                        <p class="mt-20 d_text">
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                        </p>
                                        <div class="row align-items-center">
                                            <div class="m-view">
                                                <div class="blog-post-social-networks mt-20">
                                                    <h6 class="title">Share this story on :</h6>
                                                    <ul class="list">
                                                        <li class="item">
                                                            <asp:HiddenField ID="hdreviewId" runat="server" />
                                                            <a href="#" rel="nofollow" onclick="return fbs_click()" target="_blank" aria-label="Facebook"><i class="social social_facebook"></i></a>
                                                            <%--<a href="https://www.facebook.com/theSTRUTTstore/" target="_blank" aria-label="Twitter">
                                                                <i class="social social_facebook"></i>
                                                            </a>--%>
                                                        </li>
                                                        <%--<li class="item">
                                                            <a href="https://www.instagram.com/thestruttstore/" target="_blank" aria-label="Instagram">
                                                                <i class="social social_instagram_square"></i>
                                                            </a>
                                                        </li>--%>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                            </div>
                                        </div>
                                         <div class="row align-items-center">
                                              <div class="col-lg-6">
                                                  Total Comment  (<asp:Literal ID="litTotalComment" runat="server"></asp:Literal>)
                                            </div>
                                             </div>
                                        
                                       <asp:Repeater ID="rpt_customerreview" runat="server">
                                           <ItemTemplate>
                                               <div class="post-author-box clearfix section-space--mt_60">
                                                   <div class="post-author-avatar">
                                                       <img src="../../img/icon-header-01.png" class="photo">
                                                   </div>
                                                   <div class="post-author-info">
                                                       <h6 class="author-name"><%# Eval("customer_name") %> </h6>

                                                       <p class="mt-1"><%# Eval("comment") %></p>
                                                       <span class="comment_date">Posted at <%# Convert.ToDateTime(Eval("created_date")).ToString("dddd, dd MMMM yyyy") %></span>
                                                       <ul class="author-socials">
                                                           <li><a href="https://www.facebook.com/theSTRUTTstore/" target="_blank">Facebook</a></li>
                                                           <li><a href="https://www.instagram.com/thestruttstore/" target="_blank">Instagram</a></li>
                                                       </ul>
                                                   </div>
                                               </div>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                        <div class="comments-area comments-reply-area section-space--mt_60">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="ValidatorsMsg"></asp:Label>
                                                   <%-- <a id="review" visible="false" runat="server" class="thumbnail" href='<%#"reviewcustomer.aspx?id="+Eval("id")%>'>Click here to login</a>--%>
                                                 <a id="review" visible="false" runat="server" href="login.aspx?url=reviewcustomer.aspx">Click here to login</a>
                                                    <asp:Label ID="lblMg" runat="server" ForeColor="Green"></asp:Label>
                                                    <h4 class="mb-30">Leave a Reply </h4>
                                                    <div class="comment-form-area">
                                                        <div class="comment-input-12">
                                                            <asp:TextBox ID="txtComment" runat="server" CssClass="comment-notes" placeholder="Enter Comment" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div class="comment-form-submit">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Post Comment" CssClass="comment-submit btn--md" OnClick="btnSubmit_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Single Blog Item End -->
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4">
                        <h6 class="mb-20">Recent Posts</h6>
                        <div class="blog-widget widget-blog-recent-post mt-20">
                            <asp:Repeater ID="rptTop5" runat="server">
                                <ItemTemplate>
                                    <ul class="widget-nav-list">
                                        <li><a class="link" href='<%#"reviewcustomer.aspx?id="+Eval("id")%>'><%# Eval("title") %> <span class="post-date"><%# Convert.ToDateTime(Eval("createddate")).ToString("dd MMM,yyyy") %></span></a>
                                        <li></li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Blog Page Area End -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
    <script>
        function fbs_click() {
            var shareId = document.getElementById('<%=hdreviewId.ClientID %>').value;
            window.open('http://www.facebook.com/sharer.php?u=' + 'https://thestruttstore.com/reviewcustomer.aspx?id=' + shareId,
                            'sharer',
                            'toolbar=0,status=0,width=626,height=436');
            return false;
        }
    </script>
</asp:Content>