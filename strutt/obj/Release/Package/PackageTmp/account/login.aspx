<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="strutt.account.login" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>strutt: login</title>

    <!-- ==== Document Meta ==== -->
    <meta name="author" content="" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />

    <!-- ==== Favicon ==== -->
    <link rel="icon" href="favicon.ico" type="image/png" />

    <!-- ==== Google Font ==== -->
    <link href="../Admin/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/fontawesome-all.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/perfect-scrollbar.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/morris.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/select2.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/jquery-jvectormap.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/horizontal-timeline.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/weather-icons.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/dropzone.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/ion.rangeSlider.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/ion.rangeSlider.skinFlat.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/datatables.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="../Admin/assets/css/style.css" rel="stylesheet" />
    <style type="text/css">
        .x {
            display: block;
            line-height: 1.5;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .input-group {
            position: relative;
            display: flex;
            flex-wrap: wrap;
            align-items: stretch;
            width: 100%;
        }

        .m-account--form .form-control {
            color: #fff;
            background-color: #494f50;
            border-color: #494f50;
        }
    </style>


    
    
</head>

<body class="login">
    <form id="form1" runat="server">
        <div class="wrapper">
            <!-- Login Page Start -->
            <div class="m-account-w">
                <div class="m-account">
                    <div class="row no-gutters">
                        <div class="col-md-6">
                            <!-- Login Content Start -->
                            <div class="m-account--content-w" style="padding:0">
                                <div>
                                    <%--<img src="../Admin/images/bag.jpg" height="630px" width="430px"/>--%>
                                    <asp:Image runat="server" ID="img" ImageUrl="~/Admin/images/bag.jpg" height="630px" width="460px" />
                                </div>
                            </div>
                            <!-- Login Content End -->
                            
                        </div>
                        <div class="col-md-6">
                            <!-- Login Form Start -->
                            <div class="m-account--form-w">
                                <div class="m-account--form">
                                    <!-- Logo Start -->
                                    <div class="logo">
                                        <img src="../Admin/assets/img/logo.png" alt="" />
                                    </div>
                                    <!-- Logo End -->
                                    <form action="#" method="post">
                                        <label class="m-account--title">Login to your account</label>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <i class="fas fa-user"></i>
                                                </div>
                                                <asp:DropDownList ID="ddlAdminRole" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1">Super Admin</asp:ListItem>
                                                    <asp:ListItem Value="2">Admin</asp:ListItem>
                                                    <asp:ListItem Value="3">Oparator</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select role"
                                                ControlToValidate="ddlAdminRole" ForeColor="Red" InitialValue="Select Role" ValidationGroup="prct"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <i class="fas fa-key"></i>
                                                </div>
                                                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Username" required=""></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter user Id"
                                                ControlToValidate="txtUserId" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <i class="fas fa-key"></i>
                                                </div>
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" required=""></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter password"
                                                ControlToValidate="txtPassword" ForeColor="Red" ValidationGroup="prct"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="m-account--actions">
                                            <%--<a href="#" class="btn-link">Forgot Password?</a>--%>
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-rounded btn-info" ValidationGroup="prct"
                                                OnClick="btnLogin_Click" />
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </div>
                                        <%--<div class="m-account--alt">
                                            <p><span>OR LOGIN WITH</span></p>

                                            <div class="btn-list">
                                                <a href="#" class="btn btn-rounded btn-warning">Facebook</a>
                                                <a href="#" class="btn btn-rounded btn-warning">Google</a>
                                            </div>
                                        </div>--%>
                                        <div class="m-account--footer">
                                            <p>&copy;The Strutt</p>
                                        </div>
                                    </form>
                                </div>
                            </div>
                            <!-- Login Form End -->
                        </div>
                    </div>
                </div>
            </div>
            <!-- Login Page End -->
        </div>
    </form>
</body>
</html>
