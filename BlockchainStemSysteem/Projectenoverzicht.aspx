<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projectenoverzicht.aspx.cs" Inherits="Projectenoverzicht" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 139px">
    <form id='form1' runat='server'>
    <div>
        <h2>Kies hier op wie u wilt stemmen:</h2>
        <%
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Windesheim\Blockchain\C#\BlockchainStemSysteem\App_Data\Database.mdf;Integrated Security=True");
            SqlDataAdapter asd = new SqlDataAdapter("Select Naam From Project", con);
            DataTable dt = new DataTable();
            asd.Fill(dt);
            foreach (DataRow row in dt.Rows) { Response.Write("<button formaction='ResultatenPagina.aspx?GestemdOp=" + row["Naam"] + "' style='width:200px'>" + row["Naam"] + "</button><br />"); }
        %>
        <div>Ingelogd als:</div>
        <asp:Label ID="lbl_IngelogdAls" runat="server" Text=" "></asp:Label>
    </div>
    </form>
</body>
</html>
