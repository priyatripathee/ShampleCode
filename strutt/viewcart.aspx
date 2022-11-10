<%@ Page Title="" Language="C#" MasterPageFile="~/master/main.Master" AutoEventWireup="true"
    CodeBehind="viewcart.aspx.cs" Inherits="strutt.viewcart" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
@media screen and (max-width: 767px){
    .table-responsive>.table>tbody>tr>td, .table-responsive>.table>tbody>tr>th, .table-responsive>.table>tfoot>tr>td, .table-responsive>.table>tfoot>tr>th, .table-responsive>.table>thead>tr>td, .table-responsive>.table>thead>tr>th {
        white-space: normal;
    }
    .table>tbody>tr>td, .table>tbody>tr>th, .table>tfoot>tr>td, .table>tfoot>tr>th, .table>thead>tr>td, .table>thead>tr>th {
        padding:2px;
    }
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_main" runat="server">
<script>
    // WRITE THE VALIDATION SCRIPT IN THE HEAD TAG.
function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
        return false;

    return true;
}
</script>

    <!-- ========== CONTENT START ========== -->
<section id="content">
<div class="clearfix"></div>
<asp:UpdatePanel ID="UPnlcom" runat="server">
    <ContentTemplate>
    <div class="container">
        <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UPnlcom">
            <ProgressTemplate>
                <img src="../images/loading.gif" title="Please wait.." alt="Please wait.." />Loading...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <h1 class="page-title">Shopping Cart(<asp:Label ID="lblCartCount" runat="server" Text="0"></asp:Label>)</h1>
    
        <div id="divCart" runat="server" class="table-responsive shopping-cart-table">
			<table class="table table-bordered shop_table">
            <div style="text-align:center; background-color:#ffdfe8; border:1px solid #fff; margin-bottom:3px; color:#5a5858; font-family:Open Sans,sans-serif; font-size:12px;">
            <asp:Label ID="lblQtyMsg" runat="server"></asp:Label>
            </div>
            <asp:Repeater ID="dlCart" runat="server" OnItemCommand="dlCart_ItemDataCommond">
                <HeaderTemplate>
				<thead>
					<tr>
						<td class="text-center">
							Image
						</td>
						<td class="text-center">
							Product Name 
						</td>							
						
						<td class="text-center">
							Weight 
						</td>
                        <td class="text-center" style="width: 75px;">
							Quantity
						</td>
						<td class="text-center">
							Unit Price
						</td>
                        
                        <td class="text-center">
							Total
						</td>
						<td class="text-center">
							Remove
						</td>
					</tr>
				</thead>
                </HeaderTemplate>
                <ItemTemplate>
				<tbody>
					<tr>
						<td class="text-center">
                            <a id="A1" href='<%#Helpers.GetUrlProduct(DataBinder.Eval(Container.DataItem,"menu_name"),
                                DataBinder.Eval(Container.DataItem,"sub_menu_name"),
                                DataBinder.Eval(Container.DataItem,"product_name"),DataBinder.Eval(Container.DataItem,"product_id"))%>' runat="server">
                                <asp:Image ID="imgThumb" runat="server" Width="75" ImageUrl='<%# Eval("thumb_image") %>' AlternateText='<%#Eval("product_name")%>' />
                            </a>
						</td>
						<td class="text-center">
						<b>	<%# Eval("product_name")%> </b><br />
                            Color ( <%# Eval("color_name")%> )&nbsp;&nbsp;
                            Size ( <%# Eval("size")%> )
						</td>
						
						<td class="text-center">
							<%# Eval("weight")%> 
						</td>
                        <td class="text-center">
							<div class="input-group btn-block">
                                 <asp:TextBox ID="txtquantity" runat="server" onkeypress="javascript:return isNumber(event)" CssClass="form-control" Width="45px" MaxLength="3" Text='<%#Bind("quantity") %>'></asp:TextBox>
                                 <asp:Button ID="ibtnUpdate" runat="server" Text="Apply" CommandName="Update" CommandArgument='<%# Eval("product_id") %>'></asp:Button>
                                 <%--<div style="float:right; margin:4px 0px 0 -15px;">
                                    <asp:ImageButton ID="ibtnUpdate" runat="server" ToolTip="Update" CommandName="Update" CommandArgument='<%# Eval("product_id") %>' ImageUrl="~/Admin/images/update.png" BorderWidth="0"></asp:ImageButton>
                                 </div>--%>
							</div>								
						</td>
                        
						<td class="text-center">
							<i class="fa fa-inr"></i> 
                            <asp:Label ID="lblSalePrice" runat="server" Text='<%# Eval("sale_price")%>' CssClass="text"></asp:Label><br />
                            <%--<asp:Label ID="lblDiscountPrice" runat="server" Text='<%# GetDiscountDetails(Convert.ToDecimal(Eval("discount")))%>' ForeColor="green" CssClass="text"></asp:Label>--%>
						</td>
                        <td class="text-center">
							<i class="fa fa-inr"></i> 
                            <asp:Label ID="lblTotal" runat="server" CssClass="topx2" Font-Bold="true" Text='<%# (Convert.ToDouble(Eval("quantity")) * Convert.ToDouble(Eval("sale_price"))).ToString("0.00") %>'></asp:Label>
						</td>
						<td class="text-center">
                            <asp:ImageButton ID="ibtnRemove" runat="server" ToolTip="Remove" CommandName="Remove" CommandArgument='<%# Eval("product_id") %>' ImageUrl="~/Admin/images/delete.gif" BorderWidth="0"></asp:ImageButton>
						</td>
					</tr>
											
				</tbody>
                </ItemTemplate>
            </asp:Repeater>
				<tfoot>    
                    <tr>
          <td colspan="7" class="actions">
            <div class="coupon col-md-6" style="padding-bottom:20px;">
              <h3>Have Coupons?</h3>
              <asp:TextBox ID="txtCouponCode" runat="server" AutoCompleteType="Disabled" value="" placeholder="Coupon code"></asp:TextBox>&nbsp;
              <asp:Button ID="lbtnApply" runat="server" OnClick="lbtnApply_Click" Text="Apply"></asp:Button>
              &nbsp;&nbsp;&nbsp;
              <label for="shipping_estimate">Estimate Shipping :</label>&nbsp;<asp:Label ID="lblShippingEstimate" runat="server"></asp:Label>
             <div style="float:left; font-size:10px; padding:4px; position:absolute;"><asp:Label ID="lblMsgCoupon" runat="server"></asp:Label></div>  
            </div>
            <div class="pull-right">
            <asp:Button ID="btnCheckout" runat="server" class="btnBuyNow" name="proceed" Text="Proceed to Checkout" OnClick="btnCheckout_Click" /><br />
          </div>
          </td>
        </tr>    
					<tr>
                    <td class="text-right" colspan="5">
                    <p style="float:left"><strong style="color:#FB641B">Note :- </strong>Free Shipping over order value of <i class="fa"></i> 750.00</p>
                    <strong>Total :</strong></td>
                      <td class="text-left" colspan="2">Rs. <asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
					</tr>
                    
                    <tr>
					  <%--<td class="text-right" colspan="5"><strong>Total Taxes (14%):</strong></td>--%>
                      <td class="text-right" colspan="5"><strong>GST:</strong></td>
                      <td class="text-left" colspan="2"> inclusive <div style="display:none"> Rs. <asp:Label ID="lblTaxAmount" runat="server"></asp:Label></div></td>
					</tr>
                    
                    <tr style="display:none;">
					  <td class="text-right" colspan="5">
						<strong>Sub-Total :</strong>
					  </td>
                      
                      <td class="text-left" colspan="2">
						Rs. <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
					  </td>
					</tr>
                    
                    <tr>
					  <td class="text-right" colspan="5">
						<strong>Discount :</strong>
					  </td>
                      
                      <td class="text-left" colspan="2">
						Rs. <asp:Label ID="lblDiscount" runat="server" Text="0.00"></asp:Label>
					  </td>
					</tr>
                    
                    <tr>
					  <td class="text-right" colspan="5">
						<strong>Shipping :</strong>
					  </td>
                      
                      <td class="text-left" colspan="2">
						Rs. <asp:Label ID="lblShipping" runat="server"></asp:Label>
					  </td>
					</tr>
                    
                    <tr>
					  <td class="text-right" colspan="5">
                      <asp:Button ID="btnContinueShopping" runat="server" style="float:left" class="btnBuyNow" Text="Continue Shopping"
                                                OnClick="btnContinueShopping_Click" />
						<strong>Grand Total :</strong>
					  </td>
                      
                      <td class="text-left" colspan="2" style="font-weight:bold;">
						Rs. <asp:Label ID="lblGrandTotal" runat="server"></asp:Label> <br />
                        <%--<asp:Label ID="lblCouponCode" runat="server" Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblCouponApply" style="text-decoration:line-through; color:#dd8a00" runat="server"></asp:Label>
					  </td>
					</tr>
				</tfoot>
                
			</table>				
		</div>
        <div id="divCartContinue" runat="server" style="text-align:center;">
            <p>There are no items in this cart.</p>
            <asp:Button ID="btnContinue" runat="server" class="btnBuyNow" Text="Continue Shopping" PostBackUrl="~/default.aspx" /></div>
          
    </div>
    

    </ContentTemplate>
</asp:UpdatePanel>
<!--Offer Popup--->
<div id="offer" runat="server" class="loginForm" style="display:none;">
    <div class="modal1" id="myModal" role="dialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="ribbon"></div>
            <div class="modal-header">
                <a href="javascript:void(0)" onclick="javascript:document.getElementById('cph_main_offer').style.display='none';" class="close">&times;</a>
                <h4 class="modal-title">OFFER</h4>
            </div>
          <div class="modal-body">
          <br />
          <br />
          <br />
          <br />
            Sign up now! Use code "EARLYBIRD15" to avail 15% discount on your first purchase. 
          </div>
            <div class="modal-footer">
                <a href="javascript:void(0)" onclick="javascript:document.getElementById('cph_main_offer').style.display='none';" class="btn btn-primary">Close</a>
            </div>
      
        </div>
      </div>
    </div>
</div>
<!--Offer Popup--->
</section>
    <!-- ========== CONTENT END ========== -->  
</asp:Content>