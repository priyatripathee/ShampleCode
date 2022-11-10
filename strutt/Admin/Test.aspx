<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="strutt.Admin.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Product Details</h3>
            </div>
        </div>
        <div class="clearfix"></div>
       
        <div id="Form1" method="post"  class="form-horizontal form-label-left input_mask">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <asp:Label ID="lblMsg" runat="server" CssClass="green"></asp:Label>
                    <div class="x_content">
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">Category <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                         <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="menu_name"
                           DataValueField="menu_id" AutoPostBack="true" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                         <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                          <asp:RequiredFieldValidator ID="RFVct" runat="server" ErrorMessage="Required" InitialValue="0"
                          ControlToValidate="ddlCategory" ForeColor="Red" SetFocusOnError="true" ValidationGroup="prd"></asp:RequiredFieldValidator>
                        </div>
                      </div>
                         <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name"> Sub Category<span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="ddlsubCategory" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="sub_menu_name"
                         DataValueField="sub_menu_id" AutoPostBack="true" 
                          onselectedindexchanged="ddlsubCategory_SelectedIndexChanged">
                           <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                     </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                          <asp:RequiredFieldValidator ID="RFVsct" runat="server" ErrorMessage="Required" InitialValue="0"
                           ControlToValidate="ddlsubCategory" ForeColor="Red" SetFocusOnError="true" ValidationGroup="prd">
                              </asp:RequiredFieldValidator>
                        </div>
                      </div>
                        <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">Child Category <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                       <asp:DropDownList ID="ddlChildCategory" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="child_name"
                          DataValueField="child_menu_id" AutoPostBack="true" 
                          onselectedindexchanged="ddlChildCategory_SelectedIndexChanged">
                           <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                          </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVcct" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                        InitialValue="0" ControlToValidate="ddlChildCategory" ForeColor="Red" ValidationGroup="prd">
                           </asp:RequiredFieldValidator>
                        </div>
                      </div>
                      </div>
                       <div class="col-md-6 col-sm-6 col-xs-12">

                       
                         <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name"> Gender Type <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                       <asp:DropDownList ID="ddlgendertype" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="gendertype"
                         DataValueField="gendertype">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                             <asp:ListItem Text="Men" Value="1"></asp:ListItem>
                               <asp:ListItem Text="Women" Value="2"></asp:ListItem>
                               </asp:DropDownList>
                        </div>
                      </div>
                        <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name"> Product Type <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                     <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="product_type_name"
                          DataValueField="product_type_id">
                        <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                         </asp:DropDownList>
                        </div>
                             <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVpt" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                           InitialValue="0" ControlToValidate="ddlProductType" ForeColor="Red" ValidationGroup="prd">
                            </asp:RequiredFieldValidator>
                             </div>
                      </div>
                       <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">Order By 
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                     <asp:TextBox ID="txtOrderby" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                      </div>



                      </div>
                      </div>
                      </div>
                      </div>



                       <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_content">
                       <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name"> Product Name <span class="required"></span>
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                             <div class="col-md-3 col-sm-3 col-xs-12">
                        <asp:RequiredFieldValidator ID="RFVpn" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtProductName" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                             </div>
                      </div>
                       <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">  Material  <span class="required"></span>
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                   <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="material_name"
                                                        DataValueField="material_id">
                                                        <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                        </div>
                             <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVmt" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="ddlMaterial" InitialValue="0" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                             </div>
                      </div>
                       <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">  Quantity  <span class="required"></span>
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                   <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control col-md-7 col-xs-12" Width="90px" MaxLength="3"></asp:TextBox>
                        </div>
                             <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtquantity" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtquantity"
                                                    ErrorMessage="please enter only numbers" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                             </div>
                      </div>
                       <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">   Is Best Seller  
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                   <asp:CheckBox ID="chkIsBestSeller" runat="server" />
                        </div>
                      </div>
                         <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">    Is Exclusive  
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                   <asp:CheckBox ID="chkIsExclusive" runat="server" />
                        </div>
                      </div>
                       <asp:HiddenField  runat="server" ID="hfDis"/>
                       <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">    Full Description    <span class="required"></span>
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                            <asp:TextBox id="txtFullDescription" runat="server" cols="225" rows="10" TextMode="MultiLine"></asp:TextBox>
                        </div>
                         <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtFullDescription" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                             </div>
                      </div>
                         <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name">     Features  
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
                            <asp:TextBox id="txtFeatures" runat="server" cols="225" rows="10" TextMode="MultiLine"></asp:TextBox>
                        </div>
                      </div>

                  
                      </div>
                      </div>
                      </div>

                            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_content">
                      <div class="col-md-6 col-sm-6 col-xs-12">
                           <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Meta Keywords 
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                 <asp:TextBox ID="txtMetaKeyword" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                      </div>
                           <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Meta Description 
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                  <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                      </div>
                   
                      </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                                     <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">      Meta Title  
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                   <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                      </div>
                  
                      </div>


                      </div>
                      </div>
                      </div>

                      <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_content">
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Size <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                 <asp:TextBox ID="txtSize" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                         <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVsz" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtSize" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                                                    </div>
                      </div>

                       <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Weight <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                 <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                         <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVwt" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtWeight" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                         </div>
                      </div>

                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Color <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                 <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control col-md-7 col-xs-12" DataTextField="color_name"
                                 DataValueField="color_id">
                                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                        <asp:RequiredFieldValidator ID="RFVcol" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        InitialValue="0" ControlToValidate="ddlColor" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                        </div>

                      </div>
                      </div>

                       <div class="col-md-6 col-sm-6 col-xs-12">
                          <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Price <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                 <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control col-md-7 col-xs-12" onkeyup="Javascript:calculate();"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ErrorMessage="Required"
                                                        ControlToValidate="txtPrice" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                        </div>

                      </div>
                          <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Discount <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:Label ID="lblPaid" runat="server" />
                                                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control col-md-7 col-xs-12" onkeyup="Javascript:calculate();"
                                                        Text="0"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVdprice" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Required" ControlToValidate="txtDiscount" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                        </div>
                      </div>

                          <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">     Sale Price <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:Label ID="Label1" runat="server" />
                                                    <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control col-md-7 col-xs-12"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                         <asp:RequiredFieldValidator ID="RFVsprice" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Required" ControlToValidate="txtSalePrice" ForeColor="Red" ValidationGroup="prd">
                                                    </asp:RequiredFieldValidator>
                        </div>
                      </div>
                      </div>
                      </div>
                      </div>
                      </div>
                      
                      <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_content">
                         <div class="form-group">
                        <label class="control-label col-md-2 col-sm-2 col-xs-12" for="first-name"> Images
                        </label>
                        <div class="col-md-7 col-sm-7 col-xs-12">
               <asp:DataList ID="dlImages" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                        OnItemCommand="dlImages_ItemCommand" DataKeyField="product_image_id">
                                                        <ItemTemplate>
                                                            <div style="text-align: center; width: 120px;">
                                                                <img src='<%# "../images/Product/Thumb/"+ Eval("thumb_image")%>' width="100px" height="100px" />
                                                            </div>
                                                            <div style="text-align: center; width: 120px;">
                                                                <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove"
                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"product_image_id")%>' Font-Underline="false"></asp:LinkButton>
                                                                <asp:ImageButton ID="btnActive" runat="server" ImageUrl='<%#"/Admin/images/"+ DataBinder.Eval(Container.DataItem,"is_default")+".png"%>'
                                                                    CommandName="Active" CommandArgument='<%# Eval("product_image_id") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:DataList>
                      </div>
                         <div class="form-group">
                         <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:FileUpload ID="UploadImages" runat="server" Multiple="true" />
                                                    <asp:Label ID="lblLargeImg" runat="server" Visible="false" Text="noImage.jpg" />
                                                    <asp:Label ID="lblThumbImg" runat="server" Visible="false" Text="noImage.jpg" />
                        </div>
                      </div>
                      </div>
                      </div>
                      </div>
                      </div>
                 <div class="col-md-12 col-sm-12 col-xs-12" id="divExtraImage" runat="server" >
                <div class="x_panel">
                    <div class="x_content">
                          <div class="form-group">
                               <asp:Label ID="lblExmsg" runat="server" CssClass="green"></asp:Label>
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name"> Extra Image
                        </label>
                        <div class="col-md-3 col-sm-3 col-xs-12">
                       <asp:Label ID="Label2" runat="server" Visible="false" Text="noImage.jpg" />
                            <asp:FileUpload AllowMultiple="true"  ID="FileUpload1" runat="server"  Multiple="true" />
                        </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:Button runat="server" ID="uploadedFile" Text="Upload" OnClick="uploadFile_Click" CssClass="btn btn-success"/>  
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
                            <%--  <asp:ImageButton ID="btnActive" runat="server" ImageUrl='<%#"../images/ExtraImage/"+ DataBinder.Eval(Container.DataItem,"is_active")+".png"%>'
                                 CommandName="Active" CommandArgument='<%# Eval("product_image_extra_id") %>' />--%>
                                  </div>
                                </ItemTemplate>
                           </asp:DataList>
                      </div>
                      </div>
                        </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                    <div class="x_content">
                         <div class="form-group">
                        <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
                          <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" ValidationGroup="prd"
                            OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                      </div>
                      </div>
                      </div>
                      </div>
       
                        </div>
                    </div>
</body>
</html>
