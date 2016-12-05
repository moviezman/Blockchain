<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Codeuitgeven.aspx.cs" Inherits="Codeuitgeven" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Welkom bij de Winnovation" Font-Bold="True" Font-Size="Large"></asp:Label>
    
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Op uw telefoon ontvangt u een SMS met een link. Klik op deze link om u stem uit te brengen"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Voer hier u telefoonnummer in:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Verstuur" OnClick="Button1_Click" />
        <br />
        <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label>
        <br />
    
    </div>
    </form>
</body>
</html>
