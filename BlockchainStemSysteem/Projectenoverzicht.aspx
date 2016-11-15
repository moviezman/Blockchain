<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projectenoverzicht.aspx.cs" Inherits="Projectenoverzicht" %>

<!DOCTYPE html>

<script runat="server">
    Stemming Test = new Stemming("Test", 2);
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 139px">
    <form id="form1" runat="server">
    <div>
        <h2>Kies hier op wie u wilt stemmen:</h2>
        <% foreach(Project proj in Test.Projecten) %>       
        <% { Response.Write("<button type='button'>" + proj.ToString() + "</button></br>"); }%>
        <div>Ingelogd als:</div>
    </div>
        <asp:Label ID="lbl_IngelogdAls" runat="server" Text=" "></asp:Label>
    </form>
</body>
</html>
