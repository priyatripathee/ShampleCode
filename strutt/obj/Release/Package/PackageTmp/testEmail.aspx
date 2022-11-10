<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testEmail.aspx.cs" Inherits="strutt.testEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txt_mail" />
        <asp:TextBox runat="server" ID="txt_msg" />
        <asp:Button runat="server" ID="btn_send" Text="Send" OnClick="btn_send_Click" />
    </div>
    </form>
</body>
</html>
