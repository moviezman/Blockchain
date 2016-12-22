using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BevestigStem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty((string)Session["Stemcode"]))
        {
            Response.Redirect("Inlogpagina");
        }
        string Team = Request.QueryString["GestemdOp"];
        Label1.Text = Team;
    }

    protected void ButtonJa_Click(object sender, EventArgs e)
    {
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