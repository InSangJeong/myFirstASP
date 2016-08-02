<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="WebApplication2.Booking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 181px;
        }
        .auto-style4 {
            height: 20px;
        }
        .auto-style5 {
            width: 220px;
        }
        .auto-style7 {
            width: 518px;
            height: 20px;
        }
        .auto-style8 {
            width: 225px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        예약<br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">
                    영화명 :
                    <asp:DropDownList ID="DropDownList_MovieName" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="auto-style5">
                    상영 날짜 :
                    <asp:DropDownList ID="DropDownList_PlayDate" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="auto-style8">
                    상영관 :<asp:DropDownList ID="DropDownList_Theater" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                 <td>
                     예약 인원 :
                     <asp:TextBox ID="TextBox_TicketCount" runat="server" Width="17px"></asp:TextBox>
                     명<asp:Button ID="Button1" runat="server" Text="확인" OnClick="Button1_Click" />
                </td>
            </tr>
           
              </table>
        <table style="width:100%;">
            <tr>
                <td class="auto-style7" >
                    <asp:Table ID="Table1" runat="server" Height="381px" Width="638px" style="margin-top: 0px">
                    </asp:Table>
                </td>
               
                <td class="auto-style4">
                    <asp:Button ID="Button2" runat="server" Text="예약하기" OnClick="Button2_Click" />
                </td>
            </tr>
              </table>
        <br />
    
    </div>
    </form>
</body>
</html>
