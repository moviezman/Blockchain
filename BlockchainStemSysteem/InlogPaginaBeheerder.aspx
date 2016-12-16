<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InlogPaginaBeheerder.aspx.cs" Inherits="InlogPaginaBeheerder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Inlogpagina Beheerder:</h1>
    </div>
        <asp:TextBox ID="txtbx_Login" runat="server"></asp:TextBox>
            <asp:Button ID="btn_Login" runat="server" OnClick="Button_Login_Click" Text="Inloggen" />
        <p>
            <asp:Label ID="lbl_Hash" runat="server"></asp:Label>
        </p>
        <p>
        <asp:Button ID="btn_Genereer" runat="server" Text="Genereer" OnClick="btn_Genereer_Click" Visible="False" />
        </p>
    </form>
</body>
</html>
