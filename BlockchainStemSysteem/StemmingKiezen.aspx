<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StemmingKiezen.aspx.cs" Inherits="StemmingKiezen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Keuze Stemming</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/StyleOverzichtB.css"/>
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
