<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projectenoverzicht.aspx.cs" Inherits="Projectenoverzicht" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 139px">
    <form id='form1' runat='server'>
    <div>
        <h2>Kies hier op wie u wilt stemmen:</h2>
        <%
            Response.Write(Team.TeamButtons);
        %>
        <div>Ingelogd als:</div>
        <asp:Label ID="lbl_IngelogdAls" runat="server" Text=" "></asp:Label>
    </div>
    </form>
</body>
</html>
