<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverzichtBeheerder.aspx.cs" Inherits="OverzichtBeheerder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Overzicht Beheerder</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/StyleOverzichtB.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btn_WwWijzigen" runat="server" OnClick="btn_WwWijzigen_Click" Text="Wachtwoord Wijzigen" CssClass="WWW"  />
        <br />
        <asp:Button ID="btn_Uitloggen" runat="server" Text="Uitloggen" OnClick="btn_Uitloggen_Click" CssClass="uitloggen" />
        <h2>Lopende stemmingen:</h2>
        <% Response.Write(Overzicht.LopendeStemmingenOphalen()); %>
        <asp:Button ID="btn_GenereerPagina" runat="server" OnClick="btn_GenereerPagina_Click" Text="Nieuwe Stemming" CssClass="NieuweStemming" />
        <h2>Afgelopen stemmingen:</h2>
        <% Response.Write(Overzicht.AfgelopenStemmingenOphalen()); %>
    </div>
    </form>
</body>
</html>
