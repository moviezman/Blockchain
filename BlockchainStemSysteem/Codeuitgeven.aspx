<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Codeuitgeven.aspx.cs" Inherits="Codeuitgeven" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .uil-spin {
            width: 31px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="fonts/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Label ID="Label1" runat="server" Text="Welkom bij de Winnovation" Font-Bold="True" Font-Size="Large"></asp:Label>

            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Op uw telefoon ontvangt u een SMS met een link. Klik op deze link om u stem uit te brengen"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Voer hier u telefoonnummer in:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" Enabled="False" Width="22px">06</asp:TextBox>
            <asp:TextBox ID="TextBox1" runat="server" MaxLength="8" Enabled="False"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Verstuur" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="fonts/nummers/1.jpg" OnClick="Buttonnr1_Click" />
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="fonts/nummers/2.jpg" OnClick="Buttonnr2_Click" />
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="fonts/nummers/3.jpg" OnClick="Buttonnr3_Click" />
            <br />
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="fonts/nummers/4.jpg" OnClick="Buttonnr4_Click" />
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="fonts/nummers/5.jpg" OnClick="Buttonnr5_Click" />
            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="fonts/nummers/6.jpg" OnClick="Buttonnr6_Click" />
            <br />
            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="fonts/nummers/7.jpg" OnClick="Buttonnr7_Click" />
            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="fonts/nummers/8.jpg" OnClick="Buttonnr8_Click" />
            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="fonts/nummers/9.jpg" OnClick="Buttonnr9_Click" />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="ImageButton0" runat="server" ImageUrl="fonts/nummers/0.jpg" OnClick="Buttonnr0_Click" />
            <asp:ImageButton ID="ImageButtonB" runat="server" ImageUrl="fonts/nummers/previous.jpg" OnClick="ButtonnrB_Click" />
        </div>
    </form>
    </body>
</html>
