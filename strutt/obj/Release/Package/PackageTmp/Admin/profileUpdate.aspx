<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" CodeBehind="profileUpdate.aspx.cs" Inherits="strutt.Admin.profileUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">
        <!-- Main Container Start -->
            <!-- Page Header Start -->
            <section class="page--header">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-6">
                            <!-- Page Title Start -->
                            <h2 class="page--title h5">User Profile</h2>
                            <!-- Page Title End -->

                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><span>Extra Pages</span></li>
                                <li class="breadcrumb-item active"><span>Profile</span></li>
                            </ul>
                        </div>

                        <div class="col-lg-6">
                            <!-- Summary Widget Start -->
                            <div class="summary--widget">
                                  <div class="summary--item">
                                    <p class="summary--title">This Month</p>
                                    <p class="summary--stats text-green">₹<asp:Literal  ID="lbl_curentmonth" runat="server" /></p>
                                  </div>
                                  <div class="summary--item">
                                    <p class="summary--title">Last Month</p>
                                    <p class="summary--stats text-orange">₹<asp:Literal  ID="lbl_lastmonth" runat="server" /></p>
                                  </div>
                            </div>
                            <!-- Summary Widget End -->
                        </div>
                    </div>
                </div>
            </section>
            <!-- Page Header End -->

            <!-- Main Content Start -->
            <section class="main--content">
                <div class="row gutter-20">
                    <div class="col-lg-8">
                        <!-- Panel Start -->
                        <div class="panel profile-cover">
                            <div class="profile-cover__img">
                                <img src="assets/img/avatars/01_150x150.png" alt="">
                                <h3 class="h3"><asp:Literal  ID="lbl_name" runat="server" /></h3>
                            </div>

                            <div class="profile-cover__action" data-bg-img="assets/img/covers/01_800x150.jpg" data-overlay="0.3">
                                <button class="btn btn-rounded btn-info">
                                    <i class="fa fa-plus"></i>
                                    <span>Follow</span>
                                </button>

                                <button class="btn btn-rounded btn-info">
                                    <i class="fa fa-comment"></i>
                                    <span>Message</span>
                                </button>
                            </div>

                            <div class="profile-cover__info">
                                <ul class="nav">
                                    <li><strong>26</strong>Projects</li>
                                    <li><strong>33</strong>Followers</li>
                                    <li><strong>136</strong>Following</li>
                                </ul>
                            </div>
                        </div>
                        <!-- Panel End -->

                        <!-- Panel Start -->
                        <div class="panel">
                            <div class="panel-heading">
                                <h3 class="panel-title">Activity Feed</h3>
                            </div>

                            <div class="panel-content panel-activity">
                                <form action="#" class="panel-activity__status">
                                    <textarea name="user_activity" placeholder="Share what you've been up to..." class="form-control"></textarea>

                                    <div class="actions">
                                        <div class="btn-group">
                                            <button type="button" class="btn-link" title="Post an Image" data-toggle="tooltip">
                                                <i class="far fa-image"></i>
                                            </button>

                                            <button type="button" class="btn-link" title="Post an Video" data-toggle="tooltip">
                                                <i class="fas fa-video"></i>
                                            </button>

                                            <button type="button" class="btn-link" title="Post an Idea" data-toggle="tooltip">
                                                <i class="far fa-lightbulb"></i>
                                            </button>

                                            <button type="button" class="btn-link" title="Post an Question" data-toggle="tooltip">
                                                <i class="fas fa-question-circle"></i>
                                            </button>
                                        </div>

                                        <button type="submit" class="btn btn-sm btn-rounded btn-info">Post</button>
                                    </div>
                                </form>

                                <ul class="panel-activity__list">
                                    <li>
                                        <i class="activity__list__icon fas fa-question-circle"></i>

                                        <div class="activity__list__header">
                                            <img src="assets/img/avatars/01_40x40.png" alt="">
                                            <a href="#">John Doe</a> Posted the question: <a href="#">How can I change my annual reports for the better effect?</a>
                                        </div>

                                        <div class="activity__list__body entry-content">
                                            <p><strong>Lorem ipsum dolor sit amet</strong>, consectetur adipisicing elit. Voluptatibus ab a nostrum repudiandae dolorem ut quaerat veniam asperiores, rerum voluptatem magni dolores corporis! <em>Molestiae commodi nesciunt a, repudiandae repellendus ea.</em></p>
                                        </div>

                                        <div class="activity__list__footer">
                                            <a href="#"><i class="far fa-thumbs-up"></i>123</a>
                                            <a href="#"><i class="far fa-comments"></i>23</a>
                                            <span><i class="far fa-clock"></i>2 hours ago</span>
                                        </div>
                                    </li>

                                    <li>
                                        <i class="activity__list__icon fas fa-question-circle"></i>

                                        <div class="activity__list__header">
                                            <img src="assets/img/avatars/01_40x40.png" alt="">
                                            <a href="#">John Doe</a> Posted the question: <a href="#">How can I change my annual reports for the better effect?</a>
                                        </div>

                                        <div class="activity__list__body entry-content">
                                            <blockquote>
                                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus ab a nostrum repudiandae dolorem ut quaerat veniam asperiores, rerum voluptatem magni dolores corporis! Molestiae commodi nesciunt a, repudiandae repellendus ea.</p>
                                            </blockquote>
                                        </div>

                                        <div class="activity__list__footer">
                                            <a href="#"><i class="far fa-thumbs-up"></i>123</a>
                                            <a href="#"><i class="far fa-comments"></i>23</a>
                                            <span><i class="far fa-clock"></i>2 hours ago</span>
                                        </div>
                                    </li>

                                    <li>
                                        <i class="activity__list__icon far fa-image"></i>

                                        <div class="activity__list__header">
                                            <img src="assets/img/avatars/01_40x40.png" alt="">
                                            <a href="#">John Doe</a> Uploaded 4 Image: <a href="#">Office Working Time</a>
                                        </div>

                                        <div class="activity__list__body entry-content">
                                            <ul class="gallery">
                                                <li><img src="assets/img/gallery/01.jpg" alt=""></li>
                                                <li><img src="assets/img/gallery/02.jpg" alt=""></li>
                                                <li><img src="assets/img/gallery/03.jpg" alt=""></li>
                                                <li><img src="assets/img/gallery/04.jpg" alt=""></li>
                                            </ul>
                                        </div>

                                        <div class="activity__list__footer">
                                            <a href="#"><i class="far fa-thumbs-up"></i>123</a>
                                            <a href="#"><i class="far fa-comments"></i>23</a>
                                            <span><i class="far fa-clock"></i>2 hours ago</span>
                                        </div>
                                    </li>

                                    <li>
                                        <i class="activity__list__icon fas fa-question-circle"></i>

                                        <div class="activity__list__header">
                                            <img src="assets/img/avatars/01_40x40.png" alt="">
                                            <a href="#">John Doe</a> Posted the question: <a href="#">How can I change my annual reports for the better effect?</a>
                                        </div>

                                        <div class="activity__list__body entry-content">
                                            <blockquote>
                                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus ab a nostrum repudiandae dolorem ut quaerat veniam asperiores, rerum voluptatem magni dolores corporis! Molestiae commodi nesciunt a, repudiandae repellendus ea.</p>
                                            </blockquote>
                                        </div>

                                        <div class="activity__list__footer">
                                            <a href="#"><i class="far fa-thumbs-up"></i>123</a>
                                            <a href="#"><i class="far fa-comments"></i>23</a>
                                            <span><i class="far fa-clock"></i>2 hours ago</span>
                                        </div>
                                    </li>
                                    
                                    <li>
                                        <i class="activity__list__icon far fa-lightbulb"></i>

                                        <div class="activity__list__header">
                                            <img src="assets/img/avatars/01_40x40.png" alt="">
                                            <a href="#">John Doe</a> bookmarked a page: <a href="#">Awesome Idea</a>
                                        </div>

                                        <div class="activity__list__footer">
                                            <a href="#"><i class="far fa-thumbs-up"></i>123</a>
                                            <a href="#"><i class="far fa-comments"></i>23</a>
                                            <span><i class="far fa-clock"></i>2 hours ago</span>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <!-- Panel End -->
                    </div>

                    <div class="col-lg-4">
                        <!-- Panel Start -->
                        <div class="panel">
                            <div class="panel-heading">
                                <h3 class="panel-title">About Me</h3>
                            </div>

                            <div class="panel-content panel-about">
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatem odit esse quae, et praesentium eligendi, corporis minima repudiandae similique voluptatum dolorem temporibus doloremque.</p>

                                <table>
                                    <tr>
                                        <th><i class="fas fa-briefcase"></i>Occupation</th>
                                        <td>UI/UX Designer</td>
                                    </tr>
                                    <tr>
                                        <th><i class="fas fa-birthday-cake"></i>Date of Birth</th>
                                        <td>13 June 1983</td>
                                    </tr>
                                    <tr>
                                        <th><i class="fas fa-map-marker-alt"></i>Locatoin</th>
                                        <td>123 Lorem Steet, Delhi, India.</td>
                                    </tr>
                                    <tr>
                                        <th><i class="fas fa-mobile-alt"></i>Mobile No.</th>
                                        <td><a href="tel:7328397510" class="btn-link">987-316-0373</a></td>
                                    </tr>
                                    <tr>
                                        <th><i class="fas fa-globe"></i>Website</th>
                                        <td><a href="mailto:example.com" class="btn-link">example.com</a></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="panel-social">
                                <ul class="nav">
                                    <li><a href="#"><i class="fab fa-facebook-f"></i></a></li>
                                    <li><a href="#"><i class="fab fa-twitter"></i></a></li>
                                    <li><a href="#"><i class="fab fa-google-plus-g"></i></a></li>
                                    <li><a href="#"><i class="fab fa-instagram"></i></a></li>
                                    <li><a href="#"><i class="fab fa-behance"></i></a></li>
                                    <li><a href="#"><i class="fab fa-dribbble"></i></a></li>
                                </ul>
                            </div>
                        </div>
                        <!-- Panel End -->

                        <!-- Panel Start -->
                        <div class="panel">
                            <div class="weather--panel text-white bg-blue">
                                <div class="weather--title">
                                    <i class="fa fa-map-marker-alt"></i>
                                    <span>Delhi, India</span>
                                </div>

                                <div class="weather--info">
                                    <i class="wi wi-rain-wind"></i>
                                    <span>37°C</span>
                                </div>
                            </div>
                        </div>
                        <!-- Panel End -->


                        <!-- Panel Start -->
                        <div class="panel">
                            <div class="panel-heading">
                                <h3 class="panel-title">Feeds &amp; Activities</h3>

                                <div class="dropdown">
                                    <button type="button" class="btn-link dropdown-toggle" data-toggle="dropdown">
                                        <i class="fa fa-ellipsis-v"></i>
                                    </button>

                                    <ul class="dropdown-menu">
                                        <li><a href="#"><i class="fa fa-sync"></i>Update Data</a></li>
                                        <li><a href="#"><i class="fa fa-times"></i>Remove Panel</a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="feeds-panel">
                                <ul class="nav">
                                    <li>
                                        <span class="time">2 mins</span>
                                        <i class="fa fa-shopping-cart text-white bg-blue"></i>
                                        <span class="text">New Order Received</span>
                                    </li>
                                    <li>
                                        <span class="time">10 mins</span>
                                        <i class="fa fa-user text-white bg-orange"></i>
                                        <span class="text">Updated Profile Picture</span>
                                    </li>
                                    <li>
                                        <span class="time">20 mins</span>
                                        <i class="fa fa-comment text-white bg-red"></i>
                                        <span class="text"><a href="#">John Doe</a> Commented on <a href="#">News #123</a></span>
                                    </li>
                                    <li>
                                        <span class="time">21 mins</span>
                                        <i class="fa fa-shopping-cart text-white bg-blue"></i>
                                        <span class="text">New Order Received</span>
                                    </li>
                                    <li>
                                        <span class="time">25 mins</span>
                                        <i class="fa fa-user text-white bg-green"></i>
                                        <span class="text">New User Registered</span>
                                    </li>
                                    <li>
                                        <span class="time">25 mins</span>
                                        <i class="fa fa-times text-white bg-dark"></i>
                                        <span class="text">Order <a href="#">#24DP01</a> Rejected</span>
                                    </li>
                                    <li>
                                        <span class="time">2 hours</span>
                                        <i class="fa fa-comment text-white bg-red"></i>
                                        <span class="text"><a href="#">John Doe</a> Commented on <a href="#">News #123</a></span>
                                    </li>
                                    <li>
                                        <span class="time">3 hours</span>
                                        <i class="fa fa-user text-white bg-orange"></i>
                                        <span class="text">You Uploaded A Image</span>
                                    </li>
                                    <li>
                                        <span class="time">4 hours</span>
                                        <i class="fa fa-shopping-cart text-white bg-blue"></i>
                                        <span class="text">New Order Received</span>
                                    </li>
                                    <li>
                                        <span class="time">8 hours</span>
                                        <i class="fa fa-user text-white bg-green"></i>
                                        <span class="text">New User Registered</span>
                                    </li>
                                    <li>
                                        <span class="time">22 hours</span>
                                        <i class="fa fa-shopping-cart text-white bg-blue"></i>
                                        <span class="text">New Order Received</span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <!-- Panel End -->
                    </div>
                </div>
            </section>
            
        <!-- Main Container End -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">
</asp:Content>
