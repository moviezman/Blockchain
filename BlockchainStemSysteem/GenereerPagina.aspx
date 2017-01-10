﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenereerPagina.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/StyleOverzichtB.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Naam van stemming</h2>
            <asp:TextBox ID="Txtbx_StemmingsNaam" runat="server" Width="300px" CssClass="InvulBox"></asp:TextBox>
            <h2>Max aantal stemcodes</h2>
            <asp:TextBox ID="txtbx_Nummer" runat="server" MaxLength="4" min="0" TextMode="Number" Width="55px" CssClass="StemBox"></asp:TextBox>
            <br />
            
        </div>
        <div>
            <h2>Teamnaam:</h2>
            <asp:TextBox runat="server" MaxLength="30" Width="200px" ID="txtbx_Project" CssClass="InvulBox"></asp:TextBox>
            <br />
            <br />
            <asp:Button runat="server" OnClick="btn_ProjectToevoegen_Click" Text="Toevoegen" ID="btn_ProjectToevoegen" CssClass="ToevoegKnop" />
            <asp:Button ID="btn_Verwijderen" runat="server" OnClick="btn_Verwijderen_Click" Text="Verwijderen" CssClass="VerwijderKnop"/>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Info" runat="server"></asp:Label>
                    <br />
                    <asp:Table ID="Tbl_Projecten" runat="server" BorderWidth="1px" HorizontalAlign="Center"></asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_ProjectToevoegen" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="btn_Genereer" runat="server" OnClick="btn_Genereer_Click" Text="Start stemming" CssClass="StartStemming" />
            <br />
        </div>    
        <p>
            <asp:Button ID="btn_Overzicht" runat="server" OnClick="btn_Overzicht_Click" Text="Terug" CssClass="TerugKnop" />
        </p>
    </form>
</body>
</html>
