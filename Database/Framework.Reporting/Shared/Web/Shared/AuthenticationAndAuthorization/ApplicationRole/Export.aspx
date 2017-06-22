<%@ Page Language="C#" AutoEventWireup="true" Title="ApplicationRoleSearch" MasterPageFile="~/MasterPages/Export.Master" CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Export" %>
 <%@ Register Src="~/Shared/Controls/List/List.ascx" TagName="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label Text="ApplicationRoleSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="oList" runat="server" />
</asp:Content> 
    
 

