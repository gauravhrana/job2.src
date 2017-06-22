<%@ Page Language="C#" AutoEventWireup="true" Title="AuditHistorySearch" MasterPageFile="~/MasterPages/Export.Master" 
CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Admin.Audit.AuditHistory.Export" %>
 <%@ Register Src="~/Shared/Admin/Audit/AuditHistory/Controls/List.ascx" TagName="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="AuditHistorySearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="mySearchControl" runat="server" />
</asp:Content> 
    
 

