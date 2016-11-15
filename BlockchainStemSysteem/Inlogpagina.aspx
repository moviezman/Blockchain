<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Stemming Test = new Stemming("Test", 2);
        if (Test.Stemmers.Exists(x => x.UniekeCode == TextBox1.Text) && TextBox1.Text != "")
        {
            Response.Redirect("Projectenoverzicht.aspx?Stemmer=" + TextBox1.Text);
        } else
        {
            Label1.Text = "Faal!";
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Welkom bij de Winnovation Stemming</h1>
        Voer uw stemcode in:<br />
        <asp:TextBox ID="TextBox1" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Verstuur" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        &nbsp;<p>
            &nbsp;</p>
    </form>
</body>
</html>
