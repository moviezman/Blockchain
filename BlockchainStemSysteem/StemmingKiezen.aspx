<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StemmingKiezen.aspx.cs" Inherits="StemmingKiezen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Kies de actieve stemming:<br />
        <% Response.Write(StemButtons); %>
    </div>
    </form>
</body>
</html>
