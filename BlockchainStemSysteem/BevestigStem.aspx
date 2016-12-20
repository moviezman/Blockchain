<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BevestigStem.aspx.cs" Inherits="BevestigStem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Weet u zeker dat u wil stemmen voor
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="ButtonJa_Click" Text="Ja" />
        <asp:Button ID="Button2" runat="server" OnClick="ButtonNee_Click" Text="Nee" />
    
    </div>
    </form>
</body>
</html>
