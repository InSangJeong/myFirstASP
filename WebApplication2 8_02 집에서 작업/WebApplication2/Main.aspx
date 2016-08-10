<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebApplication2.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>영화 예매 시스템 메인</title>
    
    <style type="text/css">
        .auto-style1 {
            width: 200px;
        }
        .auto-style2 {
            width: 193px;
        }
        .auto-style3 {
            width: 84px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 273px">
            <table style="width: 100%; height: 200px;">
                <tr>
                    <td class="auto-style1">
                        <asp:Image ID="IMG_MOVIE_1" runat="server" Width="200px" />
                    </td>
                    <td class="auto-style2">
                        <asp:Image ID="Image1" runat="server" />
                    </td>
                    <td class="auto-style3">
                        <asp:Image ID="Image2" runat="server" />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ID :&nbsp;
                        <asp:TextBox ID="TXT_ID" runat="server"></asp:TextBox>
                        <br />
                        <br />
&nbsp;Password :&nbsp;
                        <asp:TextBox ID="TXT_Pass" runat="server"></asp:TextBox>
                        <br />
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;
                        <asp:Button ID="BTN_NewMember" runat="server" OnClick="BTN_NewMember_Click" Text="회원가입" />
&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BTN_LOGIN" runat="server" Text="로그인" OnClick="BTN_LOGIN_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
