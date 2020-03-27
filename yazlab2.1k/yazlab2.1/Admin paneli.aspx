<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin Paneli.aspx.cs" Inherits="yazlab2._1.Admin_paneli" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 474px">
    <form id="form1" runat="server">
        <div style="height: 468px">
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Kullanıcıları Listele" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Zaman Atla" />
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" MaxLength="8"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Kitap Ekle" />
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            <br />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Çıkış" />
        </div>
    </form>
</body>
</html>
