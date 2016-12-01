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
        <br />
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" OnLoad="GridView1_Load">
            <Columns>
                <asp:BoundField HeaderText="Projectnaam" ReadOnly="True" />
            </Columns>
        </asp:GridView>
        
    </form>
</body>
</html>
