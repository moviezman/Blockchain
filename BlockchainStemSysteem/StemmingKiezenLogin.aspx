<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StemmingKiezenLogin.aspx.cs" Inherits="StemmingKiezenLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtbx_Login" runat="server" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btn_Login" runat="server" OnClick="btn_Login_Click" Text="Login" />
        </div>
        <asp:Label ID="lbl_Info" runat="server"></asp:Label>
    </form>
</body>
</html>
