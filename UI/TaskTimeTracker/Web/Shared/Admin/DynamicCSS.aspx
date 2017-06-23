<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicCSS.aspx.cs" Inherits="Shared.UI.Web.Admin.DynamicCSS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    .searchtd<br />
    {<br />
        border-color: <asp:Label ID="lblBorderColor" runat="server" Text="Blue"></asp:Label>;<br />
        border-width: <asp:Label ID="lblBorderWidth" runat="server" Text="1px"></asp:Label>;<br />
        border-style: <asp:Label ID="lblBorderStyle" runat="server" Text="Groove"></asp:Label>;<br />
    }
    </div>
    </form>
</body>
</html>
