﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverzichtBeheerder.aspx.cs" Inherits="OverzichtBeheerder" %>

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
<<<<<<< HEAD
        <asp:Button ID="btn_Uitloggen" runat="server" Text="Uitloggen" OnClick="btn_Uitloggen_Click" />
        <br />
        <h1>Voorbereide stemmingen:</h1><br />
        <h1>Lopende stemmingen:</h1>
=======
        <asp:Button ID="btn_Uitloggen" runat="server" Text="Uitloggen" OnClick="btn_Uitloggen_Click" CssClass="uitloggen" />
        <h2>Lopende stemmingen:</h2>
>>>>>>> a1e998c001df05e685c36c5cbacf6333e83a7738
        <% Response.Write(Overzicht.LopendeStemmingenOphalen()); %>
        <asp:Button ID="btn_GenereerPagina" runat="server" OnClick="btn_GenereerPagina_Click" Text="Nieuwe Stemming" CssClass="NieuweStemming" />
        <h2>Afgelopen stemmingen:</h2>
        <% Response.Write(Overzicht.AfgelopenStemmingenOphalen()); %>
    </div>
    </form>
</body>
</html>
