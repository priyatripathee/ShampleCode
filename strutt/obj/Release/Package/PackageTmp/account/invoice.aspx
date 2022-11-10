<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="invoice.aspx.cs" Inherits="strutt.account.invoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice - The Strutt Store</title>
    <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" />
    <style type="text/css">
        table.invoice
        {
            border-collapse: collapse;
            border: 1px solid #bfbfbf;
            font-family: 'Open Sans' , sans-serif;
            font-size: 12px;
        }
        
        table.invoice td
        {
            border: 1px solid #bfbfbf;
            padding: 5px;
            vertical-align: top;
        }
        .txt-r{text-align:right !important;}
    </style>
</head>
<body>
    <table width="900" border="0" style="margin: 0 auto;">
        <tr>
            <td>
                <table width="100%" border="0" class="invoice">
                    <tr>
                        <td style="padding: 35px 0px 0px 60px;">
                            <img src="../images/logo.png" alt="Strutt logo" />
                        </td>
                        <td colspan="3" align="center">
                            <h2>
                                <asp:Literal ID="litCompanyName" runat="server"></asp:Literal></h2>
                            <p>
                                <asp:Literal ID="litAdd1" runat="server"></asp:Literal><br/>
                                <asp:Literal ID="litAdd2" runat="server" Visible="false"></asp:Literal>
                                <asp:Literal ID="litwebsite" runat="server"></asp:Literal> | <asp:Literal ID="litemail" runat="server"></asp:Literal> | <asp:Literal ID="litphn1" runat="server"></asp:Literal></p>
                        </td>
                    </tr>
                    <tr>
                        <td width="35%">
                            <strong>Bill To:</strong><br/>
                            <asp:Literal ID="litBillName" runat="server"></asp:Literal><br/>
                            <asp:Literal ID="litBillEmail" runat="server"></asp:Literal><br/>
                            <asp:Literal ID="litBillPhone" runat="server"></asp:Literal><br/>
                            <strong>Ship To:</strong><br/>
                            <asp:Literal ID="litShipName" runat="server"></asp:Literal><br/>
                            <asp:Literal ID="litShipAddress" runat="server"></asp:Literal><br/>
                            <asp:Literal ID="litShipAddressOther" runat="server"></asp:Literal><br/>
                        </td>
                        <td width="21%">
                            <strong>PAN:</strong> <asp:Literal ID="litPan" runat="server"></asp:Literal><br/>
                            <strong>GST NO:</strong> <asp:Literal ID="litGST" runat="server"></asp:Literal><br/>
                        </td>
                        <td width="23%">
                            <strong>Order ID:</strong> #STR10001<asp:Literal ID="litOrderId" runat="server"></asp:Literal>
                            <strong>Payment Type:</strong> <asp:Literal ID="litPaymentType" runat="server"></asp:Literal>
                        </td>
                        <td width="21%">
                            <strong>INVOICE INVOICE NO :NO :</strong> <asp:Literal ID="litInvoiceNo" runat="server"></asp:Literal><br/>
                            <strong>INVOICE DATE :</strong> <asp:Literal ID="litInvoiceDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <!-- Stert table-2 -->
                    <tr>
                        <td colspan="4" style="padding: 0px;">
                            <table width="100%" border="0" class="invoice">
                                <tr>
                                    <td width="12%" bgcolor="#fabf8f">
                                        <strong>Sr. No.</strong>
                                    </td>
                                    <td width="31%" bgcolor="#fabf8f">
                                        <strong>Description</strong>
                                    </td>
                                    <td width="13%" bgcolor="#fabf8f">
                                        <strong>&nbsp;</strong>
                                    </td>
                                    <td width="14%" bgcolor="#fabf8f" class="txt-r">
                                        <strong>Qty</strong>
                                    </td>
                                    <td width="9%" bgcolor="#fabf8f" class="txt-r">
                                        <strong>Rate (INR)</strong>
                                    </td>
                                    <td width="7%" bgcolor="#fabf8f">
                                        <strong>&nbsp;</strong>
                                    </td>
                                    <td width="14%" bgcolor="#fabf8f" class="txt-r">
                                        <strong>Amount (INR)</strong>
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptOrderDet" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Container.ItemIndex + 1%>
                                        </td>
                                        <td>
                                            <%#Eval("product_name")%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="txt-r">
                                            <%#Eval("quantity")%>
                                        </td>
                                        <td class="txt-r">
                                            <%#Eval("unit_price")%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="txt-r">
                                            <%#(Convert.ToInt32(Eval("quantity")) * Convert.ToDouble(Eval("unit_price"))).ToString("0.00")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" bgcolor="#FABF8F">
                                        <strong>Order Subtotal</strong>
                                    </td>
                                    <td bgcolor="#FABF8F" class="txt-r">
                                        <strong><asp:Literal ID="litOrderQtyTotal" runat="server" Text='<%# Eval("total_price")%>'> </asp:Literal></strong>
                                    </td>
                                    <td bgcolor="#FABF8F">
                                        
                                    </td>
                                    <td bgcolor="#FABF8F">
                                        &nbsp;
                                    </td>
                                    <td bgcolor="#FABF8F" class="txt-r">
                                        <strong><asp:Literal ID="litOrderAmountTotal" runat="server"></asp:Literal></strong>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="2" rowspan="5">
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                        Discount
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td class="txt-r">
                                        <asp:Literal ID="litDiscount" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="2">
                                        Shipping Charge
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                   
                                    <td class="txt-r">
                                       <asp:Literal ID="litShippingCharge" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        CGST 9%
                                    </td>
                                    
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td class="txt-r">
                                        <asp:Literal ID="litCgst9" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        SGST 9%
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td class="txt-r">
                                        <asp:Literal ID="litSgst9" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        IGST 18%
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td class="txt-r">
                                        <asp:Literal ID="litIgst18" runat="server">-</asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <strong>&nbsp;</strong>
                                    </td>
                                    <td>
                                        <strong>Gross Weight:</strong>
                                    </td>
                                    <td>
                                        <strong><asp:Literal ID="litGrossWeight" runat="server"></asp:Literal> gm.</strong>
                                    </td>
                                    <td colspan="2" bgcolor="#FABF8F">
                                        <strong>GRAND TOTAL</strong>
                                    </td>
                                    <td bgcolor="#FABF8F" class="txt-r">
                                        <strong><asp:Literal ID="litGrandTotal" runat="server"></asp:Literal></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <strong>Amount in words:</strong> <asp:Literal ID="litGrandTotalInWord" runat="server"></asp:Literal> only.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7" align="right">
                                        This is a digitally generated invoice, signature not required.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- End table-2 -->
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
