<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin_main.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="addeditproduct.aspx.cs" Inherits="strutt.Admin.addeditproduct" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="markitup/sets/default/style.css" />
    <link rel="stylesheet" type="text/css" href="markitup/skins/simple/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphadmin" runat="server">

    <section class="page--header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-6">
                    <!-- Page Title Start -->
                    <h2 class="page--title h5">Edit Products</h2>
                    <!-- Page Title End -->
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item">Products</li>
                        <li class="breadcrumb-item active"><a href="addeditproduct.aspx"><span>Edit Products</span></a></li>
                    </ul>
                </div>
                <div class="col-lg-6">
                    <!-- Summary Widget Start -->
                    <div class="summary--widget">
                        <div class="summary--item">
                            <p class="summary--title">This Month</p>
                            <p class="summary--stats text-green">₹<asp:Literal ID="lbl_curentmonth" runat="server" /></p>
                        </div>
                        <div class="summary--item">
                            <p class="summary--title">Last Month</p>
                            <p class="summary--stats text-orange">₹<asp:Literal ID="lbl_lastmonth" runat="server" /></p>
                        </div>
                    </div>
                    <!-- Summary Widget End -->
                </div>
            </div>
        </div>
    </section>
    <section class="main--content">
        <div class="panel">
            <!-- Records Header Start -->
            <div class="records--header">
                <div class="title fa-shopping-bag">
                    <h3 class="h3">Products <a href="#" class="btn btn-sm btn-outline-info">Edit Products</a></h3>
                </div>
            </div>
            <!-- Records Header End -->
        </div>
        <div class="panel">
            <!-- Edit Product Start -->
            <div class="records--body">
                <div class="title">
                    <h6 class="h6">Product Details</h6>
                    <a href="#" class="btn btn-rounded btn-danger">Delete Product</a>
                </div>
                <!-- Tabs Nav Start -->
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a href="#tab01" data-toggle="tab" class="nav-link active">Basic</a>
                    </li>
                    <li class="nav-item">
                        <a href="#tab02" data-toggle="tab" class="nav-link">Meta/SEO</a>
                    </li>
                    <li class="nav-item">
                        <a href="#tab03" data-toggle="tab" class="nav-link">Images</a>
                    </li>
                    <li class="nav-item">
                        <a href="#tab04" data-toggle="tab" class="nav-link">Custom Image</a>
                    </li>
                </ul>
                <!-- Tabs Nav End -->
                <!-- Tab Content Start -->

                <div class="tab-content">
                    <!-- Tab Pane Start -->
                    <div class="tab-pane fade show active" id="tab01">
                        <asp:UpdatePanel ID="UPnlBanner" runat="server">
                            <ContentTemplate>

                                <div class="form-group row">
                                    <span class="label-text col-md-2 col-form-label">Category: <span class="requied">* </span></span>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="menu_name" EnableViewState="true" ViewStateMode="Enabled"
                                            DataValueField="menu_id" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                            <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVct" runat="server" ErrorMessage="Required" InitialValue="0"
                                            ControlToValidate="ddlCategory" ForeColor="Red" SetFocusOnError="true" ValidationGroup="prd" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <span class="label-text col-md-2 col-form-label">Sub Category: <span class="requied">* </span></span>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlsubCategory" runat="server" CssClass="form-control" DataTextField="sub_menu_name"
                                            DataValueField="sub_menu_id" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlsubCategory_SelectedIndexChanged">
                                            <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVsct" runat="server" ErrorMessage="Required" InitialValue="0"
                                            ControlToValidate="ddlsubCategory" ForeColor="Red" SetFocusOnError="true" ValidationGroup="prd" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <span class="label-text col-md-2 col-form-label">Child Category: <span class="requied">* </span></span>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlChildCategory" runat="server" CssClass="form-control" DataTextField="child_name"
                                            DataValueField="child_menu_id" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlChildCategory_SelectedIndexChanged">
                                            <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVcct" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                            InitialValue="0" ControlToValidate="ddlChildCategory" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <span class="label-text col-md-2 col-form-label">Gender Type: <span class="requied">* </span></span>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlgendertype" runat="server" CssClass="form-control" DataTextField="gendertype"
                                            DataValueField="gendertype">
                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Men" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Women" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <span class="label-text col-md-2 col-form-label">Product Type: <span class="requied">* </span></span>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" DataTextField="product_type_name"
                                            DataValueField="product_type_id">
                                            <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVpt" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                            InitialValue="0" ControlToValidate="ddlProductType" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <span class="label-text col-md-2 col-form-label">Order By:</span>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOrderby" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Product Name: <span class="requied">* </span></span>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVpn" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="txtProductName" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Material: <span class="requied">* </span></span>

                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control" DataTextField="material_name"
                                    DataValueField="material_id">
                                    <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVmt" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="ddlMaterial" InitialValue="0" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Quantity: <span class="requied">* </span></span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control" Width="90px" MaxLength="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="txtquantity" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtquantity"
                                    ErrorMessage="please enter only numbers" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-6">&nbsp;</div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Is Best Seller</span>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkIsBestSeller" runat="server" />
                            </div>
                            <div class="col-md-6">&nbsp;</div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Is Exclusive</span>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkIsExclusive" runat="server" />
                            </div>
                            <div class="col-md-6">&nbsp;</div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Long Description: <span class="requied">* </span></span>

                            <div class="col-md-10">
                                <asp:TextBox ID="txtFullDescription" runat="server" cols="225" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="txtFullDescription" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Features:</span>

                            <div class="col-md-10">
                                <asp:TextBox ID="txtFeatures" runat="server" cols="225" Rows="10" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!-- Tab Pane End -->
                    <!-- Tab Pane Start -->
                    <div class="tab-pane fade" id="tab02">
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Meta Keywords:</span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtMetaKeyword" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <span class="label-text col-md-2 col-form-label">Meta Title:</span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Meta Description:</span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <!-- Tab Pane End -->
                    <!-- Tab Pane Start -->
                    <div class="tab-pane fade" id="tab03">
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Size: <span class="requied">* </span></span>

                            <div class="col-md-4">
                                <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVsz" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="txtSize" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                            <span class="label-text col-md-2 col-form-label">Price: <span class="requied">* </span></span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" onkeyup="Javascript:calculate();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="txtPrice" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Weight: <span class="requied">* </span></span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVwt" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    ControlToValidate="txtWeight" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                            <span class="label-text col-md-2 col-form-label">Discount: <span class="requied">* </span></span>
                            <div class="col-md-4">
                                <asp:Label ID="lblPaid" runat="server" />
                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" onkeyup="Javascript:calculate();"
                                    Text="0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVdprice" runat="server" SetFocusOnError="true"
                                    ErrorMessage="Required" ControlToValidate="txtDiscount" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Color: <span class="requied">* </span></span>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control" DataTextField="color_name"
                                    DataValueField="color_id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVcol" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                    InitialValue="0" ControlToValidate="ddlColor" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                            <span class="label-text col-md-2 col-form-label">Sale Price: <span class="requied">* </span></span>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVsprice" runat="server" SetFocusOnError="true"
                                    ErrorMessage="Required" ControlToValidate="txtSalePrice" ForeColor="Red" ValidationGroup="prd" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <span class="label-text col-md-2 col-form-label">Images:</span>
                            <div class="col-md-10">
                                <label class="custom-file">
                                    <asp:FileUpload ID="UploadImages" runat="server" AllowMultiple="true" Multiple="true" class="custom-file-input" />
                                    <span class="custom-file-label">Choose File</span>
                                    <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                                    <asp:Label ID="lblThumbImg" runat="server" Visible="false" Text="noImage.jpg" />
                                </label>
                                <asp:Button runat="server" ID="btn_fileclear" Text="Remove All Image" OnClick="Delete" />

                            </div>
                            <div class="col-md-10">
                                <asp:DataList ID="dlImages" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    OnItemCommand="dlImages_ItemCommand" DataKeyField="product_image_id">
                                    <ItemTemplate>
                                        <div style="text-align: center; width: 120px;">
                                            <img id="img1" src='<%# "../images/Product/Thumb/"+ Eval("thumb_image")%>' width="100px" height="100px" />
                                        </div>
                                        <div style="text-align: center; width: 120px;">
                                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove"
                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"product_image_id")%>' Font-Underline="false"></asp:LinkButton>
                                            <asp:ImageButton ID="btnActive" runat="server" ImageUrl='<%#"/Admin/images/"+ DataBinder.Eval(Container.DataItem,"is_default")+".png"%>'
                                                CommandName="Active" CommandArgument='<%# Eval("product_image_id") %>' />
                                        </div>
                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" Text="Remove All" CommandName="RemoveAll"
                                                                    CommandArgument='<%# Eval("product_id") %>' Font-Underline="false"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:DataList>
                            </div>
                            <div class="col-md-10">
                            </div>
                        </div>

                        <div class="col-md-12 col-sm-12 col-xs-12" id="divExtraImage" runat="server">
                            <div class="x_panel">
                                <div class="x_content">
                                    <div class="form-group">
                                        <asp:Label ID="lblExmsg" runat="server" CssClass="green"></asp:Label>
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">
                                            Extra Image
                                        </label>
                                        <br />
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <label class="custom-file">
                                                <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" class="custom-file-input" Multiple="true" />
                                                <span class="custom-file-label">Choose File</span>
                                                <asp:Label ID="Label2" runat="server" Visible="false" Text="noImage.jpg" />
                                            </label>
                                        </div>
                                        <br />
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <asp:Button runat="server" ID="uploadedFile" Text="Upload" OnClick="uploadFile_Click" CssClass="btn btn-rounded btn-success" />
                                            <asp:Label ID="listofuploadedfiles" runat="server" />
                                            <asp:Label ID="lbltext" runat="server" Text="Upload 6 images with size 700px x 467px"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:DataList ID="dlExtraimage" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                        OnItemCommand="dlExtraimage_ItemCommand" DataKeyField="product_image_extra_id">
                                        <ItemTemplate>
                                            <div style="text-align: center; width: 120px;">
                                                <img src='<%# "../images/ExtraImage/"+ Eval("thumb_image")%>' width="100px" height="100px" />
                                            </div>
                                            <div style="text-align: center; width: 120px;">
                                                <asp:LinkButton ID="lnkExRemove" runat="server" Text="Remove" CommandName="Remove"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"product_image_extra_id")%>' Font-Underline="false"></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Tab Pane End -->

                    <%--tab custome images--%>

                    <div class="tab-pane fade" id="tab04">
                        <div class="col-md-12 col-sm-12 col-xs-12" id="div1" runat="server">
                            <div class="x_panel">
                                <div class="x_content">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="green"></asp:Label>
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">
                                            Custome Image
                                        </label>
                                        <br />
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <label class="custom-file">
                                                <asp:FileUpload ID="FU_CustomImg" runat="server" class="custom-file-input" />
                                                <span class="custom-file-label">Choose File</span>
                                                <asp:Label ID="Label6" runat="server" Visible="false" Text="noImage.jpg" />
                                            </label>
                                        </div>
                                        <br />
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <asp:Image ID="imgCustomeImage" ImageUrl="images/noImage.jpg" Height="80px" Width="100px"
                                                AlternateText="" runat="server" ImageAlign="AbsMiddle" />
                                            <asp:Label ID="lblCustomImg" runat="server" Visible="false" Text="noImage.jpg" />
                                        </div>
                                        <br />
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <asp:Button runat="server" ID="btnCustome" Text="Upload" OnClick="btnCustome_Click" CssClass="btn btn-rounded btn-success" />
                                            
                                            <asp:Label ID="Label8" runat="server" Text="Upload image with size 700px x 467px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblMsg_Custome" runat="server" />
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <%--end custom image--%>
                            
                        </div>

                        <!-- Tab Content End -->
                    </div>
                    <!-- Edit Product End -->
                    <hr />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-rounded btn-success" ValidationGroup="prd"
                                OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-rounded btn-primary" Text="Cancel" OnClick="btnCancel_Click" Visible="true" />
                        </div>
                    </div>
                </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpJsScript" runat="server">

    <!-- jQuery -->
    <%--<script type="text/javascript" src="markitup/jquery-1.8.0.min.js"></script>--%>
    <!-- markItUp! -->
    <script type="text/javascript" src="markitup/jquery.markitup.js"></script>
    <!-- markItUp! toolbar settings -->
    <script type="text/javascript" src="markitup/sets/default/set.js"></script>

    <script type="text/javascript">
        function calculate() {
            var price = document.getElementById("cphadmin_txtPrice").value;
            var tax = document.getElementById("cphadmin_txtDiscount").value;
            var totalprice = document.getElementById("cphadmin_txtSalePrice");
            var taxpaied = document.getElementById("cphadmin_lblPaid");

            if (price >= 0) {
                totalprice.value = price;

            }
            else {
                alert('Please enter only numbers like 100 or 100.00')
                document.getElementById("cphadmin_txtPrice").value = '';
            }
            if (tax >= 0) {

            }
            else {
                alert('Please enter only numbers like 100 or 100.00')
                document.getElementById("cphadmin_txtDiscount").value = '';
            }
            if (price && tax) {

                taxpaied.value = (price / 100) * tax;
                totalprice.value = (price * 1) - (taxpaied.value * 1);
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            // Add markItUp! to your textarea in one line
            // $('textarea').markItUp( { Settings }, { OptionalExtraSettings } );
            $('#cphadmin_txtFullDescription').markItUp(mySettings);
            
            // You can add content from anywhere in your page
            // $.markItUp( { Settings } );	
            $('.add').click(function () {
                $('#cphadmin_txtFullDescription').markItUp('insert',
                    {
                        openWith: '<opening tag>',
                        closeWith: '<\/closing tag>',
                        placeHolder: "New content"
                    }
                );
                return false;
            });

            // And you can add/remove markItUp! whenever you want
            // $(textarea).markItUpRemove();
            $('.toggle').click(function () {
                if ($("#cphadmin_txtFullDescription.markItUpEditor").length === 1) {
                    $("#cphadmin_txtFullDescription").markItUp('remove');
                    $("span", this).text("get markItUp! back");
                } else {
                    $('#cphadmin_txtFullDescription').markItUp(mySettings);
                    $("span", this).text("remove markItUp!");
                }
                return false;
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            // Add markItUp! to your textarea in one line
            // $('textarea').markItUp( { Settings }, { OptionalExtraSettings } );
            $('#cphadmin_txtFeatures').markItUp(mySettings);

            // You can add content from anywhere in your page
            // $.markItUp( { Settings } );	
            $('.add').click(function () {
                $('#cphadmin_txtFeatures').markItUp('insert',
                    {
                        openWith: '<opening tag>',
                        closeWith: '<\/closing tag>',
                        placeHolder: "New content"
                    }
                );
                return false;
            });

            // And you can add/remove markItUp! whenever you want
            // $(textarea).markItUpRemove();
            $('.toggle').click(function () {
                if ($("#cphadmin_txtFeatures.markItUpEditor").length === 1) {
                    $("#cphadmin_txtFeatures").markItUp('remove');
                    $("span", this).text("get markItUpNew! back");
                } else {
                    $('#cphadmin_txtFeatures').markItUp(mySettings);
                    $("span", this).text("remove markItUp!");
                }
                return false;
            });
        });
    </script>
</asp:Content>
