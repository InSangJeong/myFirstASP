<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="WebApplication2.LoginAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 279px">
    <form id="form1" runat="server">
    <div style="height: 259px">
        <table style="width: 100%; height: 200px;">
            <td>
                
                환영합니다. 관리자님.<br />
                <br />
                현재 관리모드 상태입니다.<br />
                <br />
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:Button ID="Button_Movie" runat="server" Height="27px" Text="상영 영화 관리" Width="110px" OnClick="Button_Movie_Click" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button_Theater" runat="server" Height="27px" Text="상영관 관리" Width="110px" OnClick="Button_Theater_Click" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button_Member" runat="server" Height="27px" Text="회원 관리" Width="110px" OnClick="Button_Member_Click" />
                
                <br />
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Height="27px" OnClick="Button1_Click" Text="로그 아웃" Width="113px" />
                
                <br />
                
            </td>
        </table>
    </div>
    </form>
</body>
</html>
