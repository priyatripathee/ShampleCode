<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="addresses.aspx.cs" Inherits="strutt.account.addresses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-controlnew {
            width: 100%;
            border-radius: 2px;
            background-color: #fff;
            border: 1px solid #e4e4e4;
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
                            <h2 class="breadcrumb-title">Addresses </h2>
                        </div>
                        <div class="col-lg-6  col-md-6 col-sm-6">
                            <!-- breadcrumb-list start -->
                            <ul class="breadcrumb-list text-center text-sm-right">
                                <li class="breadcrumb-item"><a href="../default.aspx">Home</a></li>
                                <li class="breadcrumb-item active">Addresses </li>
                            </ul>
                            <!-- breadcrumb-list end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-wrapper-reveal border-bottom">
        <div class="wishlist-main-area  section-space--ptb_90">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3">
                        <div class="blog-widget widget-blog-categories mt-40 border p-3">
                            <ul class="widget-nav-list">
                                <li><a href="orderhistory.aspx">Order History</a></li>
                                <li><a href="cancelorder.aspx">Cancel Order</a></li>
                                <li><a href="leavefeedback.aspx">Leave Feedback</a></li>
                                <li><a href="leavecomplaint.aspx">Leave a Complaint</a></li>
                                <li><a href="addresses.aspx">Saved Addresses</a></li>
                                <li><a href="changepassword.aspx">Change Password</a></li>
                                <li><a href="../wishlist.aspx">Wishlist</a></li>
                                 <li><asp:HyperLink ID="hlAddBlog" runat="server" NavigateUrl="~/account/addreview.aspx">Add Blog</asp:HyperLink>
                                <li><a href="../Login.aspx?type=lo">Log out</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <table class="form" style="width: 70%; margin-left: 20px;">
                            <tbody>
                                <tr>
                                    <td colspan="4">
                                        <h4>Add a New Address</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Name: <span class="text-red"> *</span></td>
                                    <td>
                                        <asp:HiddenField ID="hfcustomeremailid" runat="server" />
                                        <asp:HiddenField ID="hfcustomerloginid" runat="server" />
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-controlnew"></asp:TextBox>
                                    </td>
                                    <td>Landmark: <span class="text-red"> *</span></td>
                                    <td>
                                        <asp:TextBox ID="txtlandmark" runat="server" CssClass="form-controlnew"></asp:TextBox>
                                    </td>
                                </tr>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                <tr>
                                    <td>State:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlReceiverState" runat="server" AutoPostBack="true" DataTextField="state" CssClass="form-controlnew"
                                            DataValueField="state" OnSelectedIndexChanged="ddlReceiverState_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>City:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlReceiverCity" runat="server" AutoPostBack="true" DataTextField="city_name" CssClass="form-controlnew"
                                            DataValueField="state">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                <tr>
                                    <td>Address: <span class="text-red"> *</span></td>
                                    <td>
                                        <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="form-controlnew"></asp:TextBox>
                                    </td>
                                    <td>Pincode: <span class="text-red"> *</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-controlnew"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Phone Number: <span class="text-red"> *</span></td>
                                    <td>
                                        <asp:TextBox ID="txtphonenumner" runat="server" CssClass="form-controlnew"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Add New Address" CssClass="btn--lg btn--black font-weight--reguler text-white"
                                            ValidationGroup="prct" OnClientClick="insertcustomerdetails(); return false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center; height: 50px;">
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="ValidatorsMsg"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <h4>Saved Addresses</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:DataList ID="dlistcustomeraddress" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <table style="border: 1px solid #d4d4d4;" width="200px">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="lblCustomerId" runat="server" Value='<%# Eval("customer_details_id") %>' />
                                                            <strong><%# Eval("full_name") %></strong>
                                                            <hr style="margin: 3px; padding: 0px;" />
                                                            <%# Eval("address") %>
                                                            <hr style="margin: 3px; padding: 0px;" />
                                                            <%# Eval("city") %>,&nbsp;<%# Eval("state") %>
                                                            <hr style="margin: 3px; padding: 0px;" />
                                                            <%# Eval("pin_code") %>
                                                            <hr style="margin: 3px; padding: 0px;" />
                                                            <%# Eval("contact_number")%>
                                                            <hr style="margin: 3px; padding: 0px;" />
                                                            <p style="text-align: center;">
                                                                <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" Width="100%" BackColor="#3f3e3e" ForeColor="#ffffff" />
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
    <script type="text/javascript">
        function insertcustomerdetails() {
            var customeremailid = $.trim($('#<%=hfcustomeremailid.ClientID %>').val());
            var customerloginid = $.trim($('#<%=hfcustomerloginid.ClientID %>').val());
            var fullname = $.trim($('#<%=txtname.ClientID %>').val());
            var contactnumber = $.trim($('#<%=txtphonenumner.ClientID %>').val());
            var address = $.trim($('#<%=txtaddress.ClientID %>').val());
            var landmark = $.trim($('#<%=txtlandmark.ClientID %>').val());
            var city = $.trim($('#<%=ddlReceiverCity.ClientID %>').val());
            var state = $.trim($('#<%=ddlReceiverState.ClientID %>').val());
            var pincode = $.trim($('#<%=txtPincode.ClientID %>').val());
            var Messege = "";
            if (fullname == '') {
                alert("Please enter full name.");
                document.getElementById("cph_main_txtname").focus();
            }
            else if (address == '') {
                alert("Please enter address.");
                document.getElementById("cph_main_txtaddress").focus();
            }
            else if (landmark == '') {
                alert("Please enter land mark.");
                document.getElementById("cph_main_txtlandmark").focus();
            }
            else if (city == '') {
                alert("Please enter city.");
                document.getElementById("cph_main_ddlReceiverCity").focus();
            }
            else if (state == '') {
                alert("Please enter state.");
                document.getElementById("cph_main_ddlReceiverState").focus();
            }
            else if (pincode == '') {
                alert("Please enter pin code.");
                document.getElementById("cph_main_txtPincode").focus();
            }
            else if (contactnumber == '') {
                alert("Please enter phone number.");
                document.getElementById("cph_main_txtphonenumner").focus();
            }

            else {

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "addresses.aspx/Insertcustomerdetails",
                    data: "{'customerId':'" + customerloginid + "','fullName':'" + fullname + "', 'emailId':'" + customeremailid + "', 'contactNumber':'" + contactnumber + "', 'address':'" + address + "', 'landMark':'" + landmark + "', 'city':'" + city + "', 'state':'" + state + "', 'pinCode':'" + pincode + "'}",
                    success: function (Record) {

                        $('#txtname').val();
                        $('#txtphonenumner').val();
                        $('#txtaddress').val();
                        $('#txtlandmark').val();
                        $('#ddlReceiverCity').val();
                        $('#ddlReceiverState').val();
                        $('#txtPincode').val();

                        if (Record.d == true) {
                            //$('#Result').text("Your Record inserted successfully.");
                            alert("new address has been added successfully.");
                            window.location.href = "addresses.aspx";
                            $("#cph_main_txtname").val('');
                            $("#cph_main_txtphonenumner").val('');
                            $("#cph_main_txtaddress").val('');
                            $("#cph_main_txtlandmark").val('');
                            $("#cph_main_ddlReceiverCity").val('');
                            $("#cph_main_ddlReceiverState").val('');
                            $("#cph_main_txtPincode").val('');
                        }
                        else {
                            //$('#Result').text("Your Record Not Insert");
                            alert("faild.");
                        }

                    },
                    Error: function (textMsg) {

                        $('#Result').text("Error: " + Error);
                    }
                });
            }
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=dlistcustomeraddress] [id*=lnkDelete]").click(function () {
                if (confirm("Do you want to delete this Address details?")) {
                    var row = $(this).closest("tr");
                    var customerId = parseInt(row.find("[id*=lblCustomerId]").val());
                    $.ajax({
                        type: "POST",
                        url: "addresses.aspx/DeleteCustomer",
                        data: '{customerId: ' + customerId + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            if (r.d) {
                                row.remove();
                                if ($("[id*=dlistcustomeraddress] td").length == 0) {
                                    $("[id*=dlistcustomeraddress] tbody").append("<tr><td colspan = '4' align = 'center'>No records found.</td></tr>")
                                }


                                alert("This Address record has been deleted.");
                            }
                        }
                    });
                }
                return false;
            });
        });
    </script>
</asp:Content>
