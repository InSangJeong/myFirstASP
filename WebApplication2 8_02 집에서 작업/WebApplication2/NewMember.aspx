<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMember.aspx.cs" Inherits="WebApplication2.NewMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: 134px;
        }
        .auto-style2 {
            width: 228px;
        }
        .auto-style3 {
            width: 124px;
        }
        #Text1 {
            width: 169px;
        }
        #Text3 {
            width: 111px;
        }
        #Text4 {
            width: 21px;
        }
        #Text5 {
            width: 633px;
        }
        #Text6 {
            width: 181px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
        회원 정보<br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">계정</td>
                <td class="auto-style2">
               <asp:TextBox ID="TXT_ID" runat="server" />
             
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Button1" runat="server" Text="계정확인" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">비밀번호</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TXT_PASS" TextMode="Password" runat="server"></asp:TextBox>
                    <br />
                </td>
                <td class="auto-style3">비밀번호 확인</td>
                <td>
                    <asp:TextBox ID="TXT_PASSCHECK" TextMode="Password" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">이름</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
                     <br />
                </td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
     <tr>
                <td class="auto-style1">주민번호</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TXT_BIRTHDAY" runat="server" Width="138px"  MaxLength="6" style="text-align:left;ime-mode:disabled;"
                            onkeyPress="if ((event.keyCode < 48) || (event.keyCode > 57))  event.returnValue=false;" ></asp:TextBox>
                    -
                    <asp:TextBox ID="TXT_SEX" runat="server" Width="18px"  MaxLength="1" style="text-align:left;ime-mode:disabled;"
                            onkeyPress="if ((event.keyCode < 48) || (event.keyCode > 57))  event.returnValue=false;"></asp:TextBox>
                     <br />
                </td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
   
        </table>
    
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">주소</td>
                <td>
                    <asp:TextBox ID="TXT_ADDRESS" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">전화번호</td>
                <td>
                    <asp:TextBox ID="TXT_PHON" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BTN_Submit" runat="server" OnClick="BTN_Submit_Click" OnClientClick="javascript:confirms()" Text="가입신청" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BTN_Cancel" runat="server" Text="취소" />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
