<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultatenPagina.aspx.cs" Inherits="ResultatenPagina" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
   <title>Winnaar</title>
     <style type="text/css">
          

.check{
    width:200px;
    height:200px;
}</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <!-- Haalt de Uitslagen op uit de Uitslagen klasse -->
       <% Response.Write(Uitslagen.UitslagStemming(Request.QueryString["Stemming"])); %>
         <img alt="Kroon" class="crown" src="fonts/nummers/crown.png"/><br />
        <h2>Gefeliciteerd!!</h2>
    </div>
    </form>
</body>
</html>
