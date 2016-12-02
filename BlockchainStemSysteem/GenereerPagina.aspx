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
        <asp:Label ID="lbl_Info" runat="server"></asp:Label>
        <h2>Projecten:</h2>
        <asp:TextBox runat="server" MaxLength="4" Width="200px" ID="txtbx_Project"></asp:TextBox>
        <asp:Button runat="server" OnClick="btn_ProjectToevoegen_Click" Text="Toevoegen" ID="btn_ProjectToevoegen" />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnLoad="GridView1_Load">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        
    </form>
</body>
</html>
