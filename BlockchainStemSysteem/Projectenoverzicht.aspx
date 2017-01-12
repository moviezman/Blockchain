<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projectenoverzicht.aspx.cs" Inherits="Projectenoverzicht" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Winnovation Teams</title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/styleoverzicht.css"/>
</head>
<body style="height: 139px">
    <form id='form1' runat='server'>
    <div>
        <h2>Kies je favoriet!</h2>
        <asp:TextBox ID="txtbx_ZoekResultaat" runat="server"></asp:TextBox>
        <asp:Button ID="btn_ZoekResultaat" runat="server" OnClick="btn_ZoekResultaat_Click" Text="Zoek" />
        <br />
        <br />
        <% Response.Write(Team.TeamButtons); %>
    </div>
    </form>
</body>
</html>
