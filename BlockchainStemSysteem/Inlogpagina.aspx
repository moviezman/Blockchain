<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand CheckUniekeCode = new SqlCommand("Select COUNT(*) From UC WHERE ([UniekeCode] = @UniekeCode)", sqlConnection);
        CheckUniekeCode.Parameters.AddWithValue("@UniekeCode", TextBox1.Text);
        int CodeBestaat = (int)CheckUniekeCode.ExecuteScalar();

        if(CodeBestaat > 0)
        {
            Response.Redirect("Projectenoverzicht.aspx?Stemmer=" + TextBox1.Text);
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Faal!";
        }
        sqlConnection.Close();
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Welkom bij de Winnovation Stempagina</h1>
        <div>Voer uw stemcode in:</div><br />
        <asp:TextBox ID="TextBox1" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Verstuur" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        &nbsp;<p>
            &nbsp;</p>
    </form>
</body>
</html>
