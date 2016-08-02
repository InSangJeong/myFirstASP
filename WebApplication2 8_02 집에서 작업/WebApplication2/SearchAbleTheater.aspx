<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchAbleTheater.aspx.cs" Inherits="WebApplication2.SearchAbleTheater" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title></title>
    <script lang="JAVASCRIPT" type="text/javascript">
        function GoStr() {
            opener.document.getElementById("TextBox_TheaterNumber").value = document.getElementById("movie").value;
            window.close();
        }
    </script>


</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="Table1" runat="server">
            </asp:Table>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button" runat="server" OnClick="test" Text="확인" />
            <input type="text" id="bunho" runat="server" value="선택하세요." visible="false" />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="취소" />

            <input type="hidden" value="" runat="server" id="movie" />
        </div>
    </form>
</body>
</html>
