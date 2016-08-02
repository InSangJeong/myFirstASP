<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMovie.aspx.cs" Inherits="WebApplication2.NewMovie" %>

<%@ Register assembly="obout_Calendar2_Net" namespace="OboutInc.Calendar2" tagprefix="obout" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
        .auto-style2 {
            width: 212px;
        }
        .auto-style3 {
            height: 18px;
            width: 212px;
        }
        .auto-style4 {
            width: 139px;
        }
        .auto-style5 {
            height: 18px;
            width: 139px;
        }
        .auto-style6 {
            width: 166px;
        }
        .auto-style7 {
            height: 18px;
            width: 166px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        영화 정보<br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style6">영화이름</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">&nbsp;</td>
                 <td>*상영일시 yyyy-mm-dd hh:mm:ss</td>
            </tr>
            <tr>
                <td class="auto-style6">상영 시작일시</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox_StartMovie" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">상영종료일시</td>
                 <td>
                     <asp:TextBox ID="TextBox_endMovie" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">상영관</td>
                <td class="auto-style3">
                    <asp:Button ID="Button_Serch_Theater" runat="server" OnClick="Button_Serch_Theater_Click" Text="검색" Width="44px" />
                    <asp:TextBox ID="TextBox_TheaterNumber" runat="server" ReadOnly="True" Width="94px"></asp:TextBox>
                </td>
                <td class="auto-style5"></td>
                 <td class="auto-style1">*상영기간을입력하면 상영관검색이 됩니다.</td>
            </tr>
             <tr>
                <td class="auto-style7">상영이미지</td>
                <td class="auto-style3">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                 </td>
                <td class="auto-style5"></td>
                 <td class="auto-style1"></td>
            </tr>
             <tr>
                <td class="auto-style7">관람등급</td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBox_ViewingClass" runat="server"></asp:TextBox>
                 </td>
                <td class="auto-style5"></td>
                 <td class="auto-style1"></td>
            </tr>
             <tr>
                <td class="auto-style7">RunTime</td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBox_RunningTIme" runat="server"></asp:TextBox>
                 </td>
                <td class="auto-style5"></td>
                 <td class="auto-style1"></td>
            </tr>
             <tr>
                <td class="auto-style7">상영시간</td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBox_MoviePlayList" runat="server" Height="183px" ReadOnly="True" Width="282px" TextMode="MultiLine"></asp:TextBox>
                 </td>
                
                  <td>
                       
                       <asp:TextBox ID="TextBox_playDayTime" runat="server" MaxLength="14" style="text-align:left;ime-mode:disabled;"
                            onkeyPress="if ((event.keyCode < 48) || (event.keyCode > 57))  event.returnValue=false;"></asp:TextBox>

                     
                      </td>
               
                 <td class="auto-style1">
                     <asp:Button ID="Button1" runat="server" Text="상영시간 등록" OnClick="Button1_Click" />
                 </td>
            </tr>
        </table>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button_New" runat="server" Text="등록" OnClick="Button_New_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button_cancel" runat="server" Text="취소" OnClick="Button_cancel_Click" />
        <br />
    
    </div>
    </form>
</body>
</html>
