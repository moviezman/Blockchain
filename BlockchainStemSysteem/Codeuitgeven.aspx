<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Codeuitgeven.aspx.cs" Inherits="Codeuitgeven" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img alt="Logo Winnovation" class="auto-style1" src="/fonts/Nummers/logoplaceholder.png"/><br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Voer uw telefoonnummer in" CssClass="invoer"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server" Enabled="False" Width="22px" CssClass="box">06</asp:TextBox>
            <asp:TextBox ID="TextBox1" runat="server" MaxLength="8" Enabled="False" Width="200px" CssClass="box"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Visible="False" CssClass="foutmelding"></asp:Label>
            <br />
            <br />
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="fonts/nummers/1.png" OnClick="Buttonnr1_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="fonts/nummers/2.png" OnClick="Buttonnr2_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="fonts/nummers/3.png" OnClick="Buttonnr3_Click" CssClass="button" />
            <br />
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="fonts/nummers/4.png" OnClick="Buttonnr4_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="fonts/nummers/5.png" OnClick="Buttonnr5_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="fonts/nummers/6.png" OnClick="Buttonnr6_Click" CssClass="button" />
            <br />
            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="fonts/nummers/7.png" OnClick="Buttonnr7_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="fonts/nummers/8.png" OnClick="Buttonnr8_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="fonts/nummers/9.png" OnClick="Buttonnr9_Click" CssClass="button" />
            <br />
            <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="fonts/nummers/previous.png" OnClick="ButtonnrB_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton0" runat="server" ImageUrl="fonts/nummers/0.png" OnClick="Buttonnr0_Click" CssClass="button" />
            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="fonts/nummers/checked.png" OnClick="Button1_Click" CssClass="button" />
            <br />
            <footer>
            <h1>Disclaimer Winnovation stemdienst</h1>
                <p>
                Het gebruik van deze stemdienst is volledig vrijblijvend en gratis. Het in te voeren telefoonnummer wordt enkel en alleen gebruikt om een sms met stemcode naar te versturen alsmede te verifiëren of er reeds mee gestemd is. 
                Het nummer zal bij het stopzetten van de stemmig/ bekendmaking van de uitslag van Winnovation automatisch verwijderd worden.
                    </p>
                <p>
Windesheim is niet aansprakelijk voor de content op deze website alsmede enige vorm van schade welke is of kan ontstaan door gebruik van deze stemdienst, gebruik van de stemdienst vindt plaats op eigen risico.
                   </p> 
            </footer>
            </div>
    </form>
        <img alt="Logo Windesheim" class="windesheimlogo" src="/fonts/Nummers/windesheimlogodis.png" />
    <div style="position: relative; width: 600px; height: 150px;">    
    <div style="position: absolute; bottom: 5px;">
    Icons made by Pixel Buddha from www.flaticon.com
    </div>
</div>
        </body>
</html>
