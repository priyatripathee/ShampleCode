<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="error.aspx.cs" Inherits="strutt.error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <div class="site-wrapper-reveal border-bottom">
        <!-- Blog Page Area Start -->
        <div class="blog-page-wrapper section-space--pt_90 section-space--pb_120">
            <div class="container margin-t-50 t-center">
                <h1 style="color: Red; font-size: 24px;">Payment transactions failed.</h1>
                <br />
                <br />
                <asp:Button ID="btnContinueShopping" runat="server" CssClass="flex-c-m size2 m-text2 bg3 hov1 trans-0-4" Text="Continue Shopping" PostBackUrl="~/category.aspx" />
                <asp:Label ID="lblResponse" runat="server" Style="display: none;"></asp:Label>
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
        <div style="display: inline;">
            <img height="1" width="1" style="border-style: none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/827193669/?guid=ON&amp;script=0" />
        </div>
    </noscript>
</asp:Content>
