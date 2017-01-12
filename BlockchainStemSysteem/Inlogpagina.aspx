<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    //Op deze pagina kan een gebruiker inloggen na het invullen van zijn/haar unieke code
    protected void Page_Load(object sender, EventArgs e)
    {
        //Maakt de sessie leeg
        Session["Team"] = string.Empty;
        Session["Stemcode"] = string.Empty;
    }

    //Controleert de unieke code en logt de gebruiker in als deze klopt
    protected void btn_Verstuur_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        //Checkt of de unieke code voorkomt in de database
        SqlCommand CheckUniekeCode = new SqlCommand("Select COUNT(*) From UC WHERE ([UniekeCode] = @UniekeCode)", sqlConnection);
        CheckUniekeCode.Parameters.AddWithValue("@UniekeCode", TextBox1.Text);
        int CodeBestaat = (int)CheckUniekeCode.ExecuteScalar();

        //Als de unieke code een keer voorkomt in de database
        if(CodeBestaat > 0)
        {
            //Redirect naar het projectenoverzicht
            Session["Stemcode"] = TextBox1.Text;
            Response.Redirect("Projectenoverzicht.aspx?Stemmer=" + TextBox1.Text);
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Uw code is ongeldig";
        }
        sqlConnection.Close();
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
    <link rel="stylesheet" type="text/css" href="fonts/StyleOverzichtB.css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Welkom bij de Winnovation Stempagina</h1>
        <div>Voer uw stemcode in:</div><br />
        <asp:TextBox ID="TextBox1" runat="server" style="margin-bottom: 0px" autocomplete="off" CssClass="InlogBox" MaxLength="10"></asp:TextBox>
        <asp:Button ID="btn_Verstuur" runat="server" Text="Verstuur" OnClick="btn_Verstuur_Click" CssClass="Inloggen" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        &nbsp;<p>
            &nbsp;</p>
    </form>
</body>
</html>
