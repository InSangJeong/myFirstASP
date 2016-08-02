<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMember.aspx.cs" Inherits="WebApplication2.LoginMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 260px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%; height: 200px;">
            <tr>
                <td class="auto-style3">
                    <asp:Image ID="Image1" runat="server" Width ="250" Height ="250"/>
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image2" runat="server" Width ="250" Height ="250"/>
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image3" runat="server" Width ="250" Height ="250"/>
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    &nbsp;
                    환영합니다.<br />
                    <br />
                    &nbsp;
                    <asp:Label ID="LabelName" runat="server" Text="Label"></asp:Label>
                    님, 현재 포인트 : <asp:Label ID="LabelPoint" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    &nbsp;
                    <asp:Button ID="ButtonRechargePoint" runat="server" Text="포인트 충전" OnClick="ButtonRechargePointClick" />
                    <br />
                    <br />
                    &nbsp;
                    <asp:Button ID="Button2" runat="server" Text="예매 하기" OnClick="Button2_Click" />
                    <br />
                    <br />
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" Text="예매 내역 확인" OnClick="Button3_Click" />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
           
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
