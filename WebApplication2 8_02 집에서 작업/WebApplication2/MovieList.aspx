<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieList.aspx.cs" Inherits="WebApplication2.MovieList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <p>
        <br />
&nbsp; 영화 목록</p>
    <form id="form1" runat="server">
    
    <div>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Table ID="Table1" runat="server" Height="145px" Width="600px" BackColor ="#006699" >
        </asp:Table>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Button ID="Button_NewMovie" runat="server" Text="영화 등록" OnClick="Button_NewMovie_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button_DeleteMovie" runat="server" Text="영화 삭제" OnClick="Button_DeleteMovie_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 21px" Text="돌아가기" />
        </div>
    </form>
</body>
</html>
