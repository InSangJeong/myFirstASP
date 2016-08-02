<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TheaterList.aspx.cs" Inherits="WebApplication2.TheaterList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        상영관 관리<br />
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox_Row" runat="server" Width="26px"></asp:TextBox>
        열&nbsp;
        <asp:TextBox ID="TextBox_Number" runat="server" Width="26px"></asp:TextBox>
        번&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button_NewTheater" runat="server" Text="상영관 추가" OnClick="Button_NewTheater_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button_DeleteTheater" runat="server" Text="상영관 삭제" OnClick="Button_DeleteTheater_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="이전페이지" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; * 상영관은 Max열 Max번까지 입력해주세요.<br />
    
    </div>
    </form>
</body>
</html>
