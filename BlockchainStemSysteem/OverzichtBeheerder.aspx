﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverzichtBeheerder.aspx.cs" Inherits="OverzichtBeheerder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Lopende stemmingen:</h1>
        <% Response.Write("Test"); %>
        <br />
        <br />
        <br />
        <asp:Button ID="btn_GenereerPagina" runat="server" OnClick="btn_GenereerPagina_Click" Text="Nieuwe Stemming" />
        <h1>Afgelopen stemmingen:</h1>
        <% Response.Write("Test"); %>
    </div>
    </form>
</body>
</html>