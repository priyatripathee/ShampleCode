<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="contact-us.aspx.cs" Inherits="strutt.contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
    <!-- breadcrumb-area start -->
    <div class="breadcrumb-area">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="row breadcrumb_box  align-items-center">
                        <div class="col-lg-6 col-md-6 col-sm-6 text-center text-sm-left">
                            <h2 class="breadcrumb-title">Contact</h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Contact</li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- breadcrumb-area end -->
    <div class="site-wrapper-reveal border-bottom">
        <div class="contact-us-info-area mt-30 section-space--mb_60">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3">
                        <div class="single-contact-info-item">
                            <div class="icon">
                                <i class="icon-clock3"></i>
                            </div>
                            <div class="iconbox-desc">
                                <h6 class="mb-10">Open houses</h6>
                                <p>
                                    Mon – Fri : 8:30 – 18:00
                                    <br>
                                    Sat – Sun : 9:00 – 17:00
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="single-contact-info-item">
                            <div class="icon">
                                <i class="icon-telephone"></i>
                            </div>
                            <div class="iconbox-desc">
                                <h6 class="mb-10">Phone number</h6>
                                <p>
                                    Shout Out :+91-120-425-6583
                                    <br>
                                    Talk to Me : +91-8800400570
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="single-contact-info-item">
                            <div class="icon">
                                <i class="icon-envelope-open"></i>
                            </div>
                            <div class="iconbox-desc">
                                <h6 class="mb-10">Our email</h6>
                                <p>
                                    Shoot us a mail : connect@thestruttstore.com
                                    <br />
                                    Share your feedback: help@thestruttstore.com
                                </p>
                                <p>
                                    Press & Media: vishesh@thestruttstore.com<br />
                                    Investment : vishesh@thestruttstore.com
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="single-contact-info-item">
                            <div class="icon">
                                <i class="icon-map-marker"></i>
                            </div>
                            <div class="iconbox-desc">
                                <h6 class="mb-10">Our location</h6>

                                <h6>Corporate Office</h6>
                                <p>
                                   C 272, 1st Floor, Sector 10, <br />
                                    Noida - 201301, U.P. India
                                </p>
                                <%--<h6>Shop</h6>
                                <p>
                                    Ground Floor, Popup Zone-Woodpecker's,<br />
                                    Mall of India, Noida, U.P. India
                                </p>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="contact-us-page-warpper section-space--pb_120">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="border-top">
                            <div class="row">
                                <div class="col-lg-7">
                                    <div class="contact-form-wrap  section-space--mt_60">
                                        <h5 class="mb-10">Get in touch</h5>
                                        <p>Write us a letter !</p>
                                        <form id="contact-form" class="mt-30" action="https://hasthemes.com/file/mail.php" method="post">
                                            <div class="contact-form">
                                                <div class="contact-input">
                                                    <div class="contact-inner">
                                                        <asp:TextBox ID="txtname" runat="server" placeholder="Enter your Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="Validators" SetFocusOnError="true"
                                                            Display="Dynamic" ControlToValidate="txtname" ForeColor="Red" runat="server" ValidationGroup="contact"
                                                            ErrorMessage="required"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="contact-inner">
                                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter your Mobile"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtMobile"
                                                            runat="server" Display="Dynamic" ValidationGroup="contact" SetFocusOnError="true"
                                                            ErrorMessage="required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="contact-inner">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email address"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="txtEmail"
                                                        runat="server" Display="Dynamic" ValidationGroup="contact" SetFocusOnError="true"
                                                        ErrorMessage="required"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic"
                                                        ForeColor="Red" runat="server" SetFocusOnError="true" ErrorMessage="Invalid Email Id"
                                                        ValidationGroup="contact" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="contact-inner contact-message">
                                                    <asp:TextBox ID="txtQuery" CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Enter Query"></asp:TextBox>
                                                </div>
                                                <div class="submit-btn mt-20">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn--black btn--md"
                                                        ValidationGroup="contact" OnClick="btnSubmit_Click1" />

                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn--black btn--md" OnClick="btnClear_Click1" />

                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                                                    <p class="form-messege"></p>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                                <div class="col-lg-4 ml-auto">
                                    <div class="conatact-info-text section-space--mt_60">
                                        <h5 class="mb-10">Our address</h5>
                                        <h6>Registered Address</h6>
                                        <p>
                                             C 272, 1st Floor, Sector 10, <br />
                                             Noida - 201301, U.P. India
                                        </p>
                                        <p class="mt-30">
                                            Call us :- +91-8800400570
                                            <br />
                                            Mail us :- connect@thestruttstore.com
                                        </p>
                                        <div class="product_socials mt-30">
                                            <span class="label">FOLLOW US:</span>
                                            <ul class="helendo-social-share socials-inline">
                                                <li>
                                                    <a class="share-facebook helendo-facebook" href="https://www.facebook.com/theSTRUTTstore/" target="_blank"><i class="social_facebook"></i></a>
                                                </li>
                                                <li>
                                                    <a class="share-google-plus helendo-google-plus" href="https://www.instagram.com/thestruttstore/" target="_blank"><i class="social_instagram_square"></i></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="map-area">
            <div class="google-map">
                <iframe class="embed-responsive-item googleMap-1" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3502.203190852375!2d77.30698131455941!3d28.62367169123387!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x390ce4b62b9d8ac9%3A0x1d94db1d55cc8f18!2s64%20C%2C%20Pocket%2C%20B-4%2C%20Block%20B%2C%20Mayur%20Vihar%2C%20New%20Delhi%2C%20Delhi%20110091!5e0!3m2!1sen!2sin!4v1612349906902!5m2!1sen!2sin" width="1500" height="450" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe> 
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
