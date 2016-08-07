<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookingList.aspx.cs" Inherits="WebApplication2.BookingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        예매내역<br />
        <br />
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
        <br />
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="예매취소" OnClick="Button2_Click"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="홈으로" />
    
    </div>
    </form>
</body>
</html>
