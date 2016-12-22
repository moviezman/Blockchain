<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gestemd.aspx.cs" Inherits="Gestemd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/stylebevestig.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <img alt="Logo Winnovation" class="check" src="fonts/nummers/checked.png"/><br />
        <h2>U heeft gestemd op:</h2>
            <asp:Label ID="Label1" runat="server" CssClass="projectext">></asp:Label>
    <div>
        <br />
        <asp:Label ID="lbl_GestemdOp" runat="server" Text="Label"></asp:Label>
    </div>
        <br />
        <asp:Button ID="btn_redirect_naar_home" runat="server" OnClick="Button1_Click" Text="Home" CssClass="homeknop" />
    </form>
</body>
</html>
