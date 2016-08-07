<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebApplication2.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>영화 예매 시스템 메인</title>
    
    <style type="text/css">
        .auto-style1 {            height: 29px;
        }
        .auto-style2 {
            width: 111px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 273px">
            <table style="width: 100%; height: 200px;">
                <tr>
                    <td class="auto-style1" colspan="2" aria-multiline="True">&nbsp;영화 예매 시스템에 오신것을 환영합니다.&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; ID :&nbsp;<br />
                        <br />
&nbsp;Password :&nbsp;
                        <br />
                        <br />
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="TXT_ID" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:TextBox ID="TXT_Pass" runat="server" TextMode="Password"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="BTN_NewMember" runat="server" OnClick="BTN_NewMember_Click" Text="회원가입" />
&nbsp;&nbsp;
                        <asp:Button ID="BTN_LOGIN" runat="server" Text="로그인" OnClick="BTN_LOGIN_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
