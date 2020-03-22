<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kitap Ekleme Paneli.aspx.cs" Inherits="yazlab2._1.Kitap_Ekleme_Paneli" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 426px">
    <form id="form1" runat="server">
        <div style="height: 421px">
            <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Resim Seç" />
        </div>
    </form>
</body>
</html>
