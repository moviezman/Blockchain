<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BevestigStem.aspx.cs" Inherits="BevestigStem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/stylebevestig.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h2>Weet u zeker dat u wilt stemmen voor</h2>
        <asp:Label ID="Label1" runat="server" Text="Label" CssClass="projectext"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Ja" runat="server" OnClick="ButtonJa_Click" Text="JA" CssClass="knopJa"/>
        <asp:Button ID="Nee" runat="server" OnClick="ButtonNee_Click" Text="NEE" CssClass="knopNee" />
    
    </div>
    </form>
</body>
</html>
