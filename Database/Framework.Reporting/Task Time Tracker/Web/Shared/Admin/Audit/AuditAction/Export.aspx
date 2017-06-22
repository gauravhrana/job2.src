<%@ Page Language="C#" AutoEventWireup="true" Title="FoodTypeSearch" MasterPageFile="~/MasterPages/Export.Master" 
CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Admin.Audit.AuditAction.Export" %>
 <%@ Register Src="~/Shared/Admin/Audit/AuditAction/Controls/List.ascx" TagName="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="AuditActionSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="mySearchControl" runat="server" />
</asp:Content> 
    
 

