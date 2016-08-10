<%@ Page Language="C#" AutoEventWireup="true" CodeFile="endCodeMD52.aspx.cs" Inherits="Demo_In_Project_endCodeMD52" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtInput" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnEncoer" OnClick="btnEncoer_Click" runat="server" Text="Encoder" />
        <asp:TextBox ID="txtEncoder" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
