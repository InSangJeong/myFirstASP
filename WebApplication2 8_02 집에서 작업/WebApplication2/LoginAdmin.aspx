<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="WebApplication2.LoginAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 212px">
    <form id="form1" runat="server">
    <div style="height: 175px">
        <table style="width: 100%; height: 200px;">
            <td>
                
                <asp:Image ID="Image1" runat="server" />
                
            </td>
            <td>
                
                <asp:Image ID="Image2" runat="server" />
                
            </td>
            <td>
                
                <asp:Image ID="Image3" runat="server" />
                
            </td>
            <td>
                
                &nbsp;&nbsp; 관리자 계정입니다.<br />
                <br />
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:Button ID="Button_Movie" runat="server" Text="상영 영화 관리" Width="110px" OnClick="Button_Movie_Click" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button_Theater" runat="server" Text="상영관 관리" Width="110px" OnClick="Button_Theater_Click" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button_Member" runat="server" Text="회원 관리" Width="110px" OnClick="Button_Member_Click" />
                
                <br />
                
            </td>
        </table>
    </div>
    </form>
</body>
</html>
