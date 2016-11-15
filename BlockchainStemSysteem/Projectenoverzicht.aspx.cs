using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projectenoverzicht : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Stemming Test = new Stemming("Test", 2);
        if (Test.Stemmers.Exists(x => x.UniekeCode == Request.QueryString["Stemmer"]))
        {
            
        } else
        {
            //Redirect naar de inlogpagina als de unieke code van de stemmer niet bestaat
            Response.Redirect("Inlogpagina");
        }
        this.lbl_IngelogdAls.Text = Request.QueryString["Stemmer"];
        

    }
}