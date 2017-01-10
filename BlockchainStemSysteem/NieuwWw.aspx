<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NieuwWw.aspx.cs" Inherits="NieuwWw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            Wachtwoord Verwijderen
        </h1>
        <div>
            <asp:Label ID="lbl_Info" runat="server"></asp:Label>
            <h2>Oude Wachtwoord:</h2>
            <asp:TextBox ID="txtbx_OudWw" runat="server" TextMode="Password"></asp:TextBox><br />
            <h2>Nieuwe Wachtwoord:</h2>
            <asp:TextBox ID="txtbx_NieuwWw1" runat="server" TextMode="Password"></asp:TextBox><br />
            <h2>Nieuwe Wachtwoord Herhalen:</h2>
            <asp:TextBox ID="txtbx_NieuwWw2" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Button ID="btn_Opslaan" runat="server" Text="Opslaan" OnClick="btn_Opslaan_Click" /><br />
            <asp:Button ID="btn_Terug" runat="server" Text="Terug" OnClick="btn_Terug_Click" />
        </div>
    </form>
</body>
</html>
