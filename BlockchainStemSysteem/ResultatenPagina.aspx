<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultatenPagina.aspx.cs" Inherits="ResultatenPagina" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>U heeft gestemd op:</h3>
    <div>
    
        <asp:Label ID="lbl_GestemdOp" runat="server" Text="Label"></asp:Label>
    
    </div>
        <asp:Button ID="btn_redirect_naar_home" runat="server" OnClick="Button1_Click" Text="Home" />
    </form>
</body>
</html>
