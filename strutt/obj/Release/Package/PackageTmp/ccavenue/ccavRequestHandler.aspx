﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="SubmitData" Codebehind="ccavRequestHandler.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="1.10.2jquery.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
           debugger
               $("#nonseamless").submit();
           });
       </script>
    <title>
    </title>
</head>
<body>
    <form id="nonseamless" method="post" name="redirect" action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction"> 
        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>"/>
    </form>
</body>
</html>
