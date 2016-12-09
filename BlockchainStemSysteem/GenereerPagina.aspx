﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenereerPagina.aspx.cs" Inherits="_Default" %>

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
            <asp:Label ID="lbl_Info" runat="server"></asp:Label>
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
                    <asp:Label ID="lbl_Projecten" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_ProjectToevoegen" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
        </div>    
    </form>
</body>
</html>
