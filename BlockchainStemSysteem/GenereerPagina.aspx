<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenereerPagina.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Komt dat zien, komt dat zien. Genereer hier uw unieke code!</h1>
        <asp:TextBox ID="Txtbx_StemmingsNaam" runat="server" Width="300px"></asp:TextBox>
        <asp:TextBox ID="txtbx_Nummer" runat="server" MaxLength="4" TextMode="Number" Width="50px"></asp:TextBox>
        <asp:Button ID="btn_Genereer" runat="server" OnClick="btn_Genereer_Click" Text="Genereer" />
    
    </div>
        <asp:Label ID="lbl_Info" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
