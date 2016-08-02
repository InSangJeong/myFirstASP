<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargePoint.aspx.cs" Inherits="WebApplication2.RechargePoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        포인트 사용내역<br />
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="81px"></asp:TextBox>
        원&nbsp;
        <asp:Button ID="Button1" runat="server" Text="충전하기" OnClick="Button1_Click" />
    
    &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="돌아가기" />
    
    </div>
    </form>
</body>
</html>
