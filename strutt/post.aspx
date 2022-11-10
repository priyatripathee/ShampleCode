<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="strutt.post" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%--<form id="form1" runat="server">--%>
    <div>
        
        
    <h1>Razorpay Payment Tesing</h1>
    <form action="success.aspx" method="post">
        <input type="hidden" value="Hidden Element" name="hidden" />
        <asp:Literal ID="litRazor" runat="server"></asp:Literal>
    </form>

    </div>
    <%--</form>--%>
</body>
</html>
