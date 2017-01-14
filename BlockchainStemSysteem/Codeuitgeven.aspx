<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Codeuitgeven.aspx.cs" Inherits="Codeuitgeven" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Winnovation Expo</title>
    <link rel="stylesheet" type="text/css" href="fonts/style.css" />
    <script type="text/javascript">
      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57)) {
             return false;
         }
         return true;
      }
   </script>
</head>
<body>
    <div class="mobileShow">
    <form id="form1" runat="server">
        <div >
            <img alt="Logo Winnovation" class="auto-style1" src="/fonts/Nummers/logotransparant.png" /><br />
            <br />
            <asp:Label ID="lbl_VoerTelNrIn" runat="server" Text="Voer uw telefoonnummer in" CssClass="invoer"></asp:Label>
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox ID="txtbx_06" runat="server" Enabled="False" Width="22px" CssClass="box">06</asp:TextBox>
                    <asp:TextBox ID="txtbx_telnr" onkeypress="return isNumberKey(event)" input="number" type="tel" min="0" runat="server" MaxLength="8" autocomplete="off" Enabled="False" Width="200px" CssClass="box"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lbl_Info" runat="server" Visible="False" CssClass="foutmelding"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImageButtonOkee" runat="server" ImageUrl="fonts/nummers/checked.png" OnClick="ImageButtonOkee_Click" CssClass="button" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ImageButtonOkee" />
                </Triggers>
            </asp:UpdatePanel>
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
    <div  style="position: relative; width: 600px; height: 150px;">
        <div style="position: absolute; bottom: -91px; left: 3px;">
            Icons made by Pixel Buddha & Freepik 
        <br />
            from www.flaticon.com
        </div>
    </div>
    </div>
</body>
</html>
