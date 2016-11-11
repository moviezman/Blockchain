<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projectenoverzicht.aspx.cs" Inherits="Projectenoverzicht" %>

<!DOCTYPE html>

<script runat="server">
    Stemming Test = new Stemming("Test", 2);
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Kies hier op wie u wilt stemmen:</h2>
        <% foreach(Project proj in Test.Projecten) %> 
        <% { Response.Write(proj.ToString() + "<br>"); }%>
        <div>Ingelogd als:</div>
    </div>
    </form>
</body>
</html>
