﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NieuwWw.aspx.cs" Inherits="NieuwWw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/StyleOverzichtB.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            Wachtwoord Wijzigen
        </h1>
        <div>
            <asp:Label ID="lbl_Info" runat="server"></asp:Label>
            <h2>Oud Wachtwoord:</h2>
            <asp:TextBox ID="txtbx_OudWw" runat="server" TextMode="Password" CssClass="InvulBox" MaxLength="20"></asp:TextBox><br />
            <h2>Nieuw Wachtwoord:</h2>
            <asp:TextBox ID="txtbx_NieuwWw1" runat="server" TextMode="Password" CssClass="InvulBox" MaxLength="20"></asp:TextBox><br />
            <h2>Nieuw Wachtwoord Herhalen:</h2>
            <asp:TextBox ID="txtbx_NieuwWw2" runat="server" TextMode="Password" CssClass="InvulBox" MaxLength="20" OnTextChanged="txtbx_NieuwWw2_TextChanged"></asp:TextBox><br />
            <asp:Button ID="btn_Opslaan" runat="server" Text="Opslaan" OnClick="btn_Opslaan_Click" CssClass="Opslaan" /><br />
            <asp:Button ID="btn_Terug" runat="server" Text="Terug" OnClick="btn_Terug_Click" CssClass="TerugKnop" /> 
        </div>
    </form>
</body>
</html>
