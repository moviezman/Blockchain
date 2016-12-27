<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultatenPagina.aspx.cs" Inherits="ResultatenPagina" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <% Response.Write(Uitslagen.UitslagStemmingBeheerder(Request.QueryString["Stemming"])); %>
    </div>
    </form>
</body>
</html>
