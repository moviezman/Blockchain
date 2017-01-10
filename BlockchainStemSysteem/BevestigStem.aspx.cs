using System;

//Pagina voor de gebruiker om de keuze op het gekozen project te verifieren
public partial class BevestigStem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checkt of de stemcode bekend is in de sessie (vangt veranderen URL op)
        if (string.IsNullOrEmpty((string)Session["Stemcode"]))
        {
            Response.Redirect("Inlogpagina");
        }
        string Team = Request.QueryString["GestemdOp"];
        Label1.Text = Team;
    }

    protected void ButtonJa_Click(object sender, EventArgs e)
    {
        //Redirect naar de gestemd pagina, waar de stem op het gekozen project in de database wordt gezet
        Session["Team"] = Request.QueryString["GestemdOp"];
        string StemCode = (string)Session["Stemcode"];
        string Team = Request.QueryString["GestemdOp"];
        Response.Redirect("Gestemd.aspx");
    }
    protected void ButtonNee_Click(object sender, EventArgs e)
    {
        //string StemCode = Request.QueryString["StemCode"];
        Response.Redirect("Projectenoverzicht.aspx?Stemmer=" + Session["StemCode"]);
    }
}