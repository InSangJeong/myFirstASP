﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargePoint.aspx.cs" Inherits="WebApplication2.RechargePoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        포인트 충전/사용/환불 내역입니다.<br />
        -----------------------------------------------------------------------<br />
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="81px" style="text-align:left;ime-mode:disabled;"
                            onkeyPress="if ((event.keyCode < 48) || (event.keyCode > 57))  event.returnValue=false;"></asp:TextBox>
        원&nbsp;
        <asp:Button ID="Button1" runat="server" Text="충전하기" OnClick="Button1_Click" />
    
    &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="돌아가기" />
    
    </div>
    </form>
</body>
</html>
