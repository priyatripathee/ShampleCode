<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestXpressLean.aspx.cs" Inherits="strutt.TestXpressLean" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript">
        function Post() {
            $(document).ready(function () {
                $("#btnsubmit").click();
            });
        }

    </script>
</head>
<body>
    <form id="form" runat="server" method="post" name="form" action="https://www.xpresslane.in/order/api/v1/checkout">
        <div>
            <input type="hidden" id="merchantid" name="merchantid" runat="server" />
            <input type="hidden" id="checksum" name="checksum" runat="server" />
            <button type="submit" id="btnsubmit" runat="server" hidden="hidden">submit </button>
            <%--<asp:Button ID="btnsubmit" runat="server" Text="PostData" OnClick="btnsubmit_Click"/>--%>
        </div>
    </form>
</body>
</html>
