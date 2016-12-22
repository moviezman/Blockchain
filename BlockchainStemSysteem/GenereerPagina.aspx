<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenereerPagina.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Vul een stemmingsnaam en het aantal stemmen in</h1>
            <h1>Voeg daarna projecten toe aan deze stemming</h1>
            <asp:TextBox ID="Txtbx_StemmingsNaam" runat="server" Width="300px"></asp:TextBox>
            <asp:TextBox ID="txtbx_Nummer" runat="server" MaxLength="4" TextMode="Number" Width="50px"></asp:TextBox>
            <br />
            
        </div>
        <div>
            <h2>Projecten:</h2>
            <asp:TextBox runat="server" MaxLength="30" Width="200px" ID="txtbx_Project"></asp:TextBox>
            <asp:Button runat="server" OnClick="btn_ProjectToevoegen_Click" Text="Toevoegen" ID="btn_ProjectToevoegen" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Table ID="Tbl_Projecten" runat="server" BorderWidth="1px"></asp:Table>
                    <asp:Label ID="lbl_Info" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_ProjectToevoegen" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="btn_Genereer" runat="server" OnClick="btn_Genereer_Click" Text="Genereer" />
            <br />
        </div>    
        <p>
            <asp:Button ID="btn_Overzicht" runat="server" OnClick="btn_Overzicht_Click" Text="Overzicht" />
        </p>
    </form>
</body>
</html>
