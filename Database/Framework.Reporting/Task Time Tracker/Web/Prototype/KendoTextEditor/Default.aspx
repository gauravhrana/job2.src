<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.KendoTextEditor.Default" %>
<%@ Register TagPrefix="ke" TagName="KendoEditor" Src="~/Prototype/KendoTextEditor/Controls/Generic.ascx" %>
<!DOCTYPE html>

<html >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ke:KendoEditor ID="KendoUIEditor" runat="server"></ke:KendoEditor>
    </div>
    </form>
</body>
</html>