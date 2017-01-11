<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InlogPaginaBeheerder.aspx.cs" Inherits="InlogPaginaBeheerder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/StyleOverzichtB.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>Beheerderspagina Winnovation Expo</h2>
     <h3>Maak gebruik van uw beheerderswachtwoord om verder te gaan</h3>
    </div>
        <asp:TextBox ID="txtbx_Login" type="password" runat="server" CssClass="InlogBox"> </asp:TextBox>
            <asp:Button ID="btn_Login" runat="server" OnClick="Button_Login_Click" Text="Inloggen" CssClass="inloggen" />
        <p>
            <asp:Label ID="lbl_Info" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
