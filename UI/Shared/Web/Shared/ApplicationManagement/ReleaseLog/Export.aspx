<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" MasterPageFile="~/MasterPages/Export.Master"
 Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLog.Export" %>
<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="ClientSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="oList" runat="server" />
</asp:Content>

