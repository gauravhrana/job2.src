<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sample.aspx.cs" Inherits="ApplicationContainer.UI.Web.UnitTest.DayCareTest.Sample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    	<asp:Button ID="Button1" runat="server" OnClick="Insert_Click" Text="Insert" />
    
        <asp:Button ID="Button2" runat="server" OnClick="Update_Click" Text="Update"/>

        <asp:Button ID="Button3" runat="server" OnClick="Search_Click" Text="Search"/>

        <asp:Button ID="Button4" runat="server" OnClick="Delete_Click" Text="Delete"/>

        <asp:GridView runat="server" ID="testing1" />
    </div>
    </form>
</body>
</html>
