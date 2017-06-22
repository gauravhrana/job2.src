<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Export.Master" CodeBehind="Export.aspx.cs"
 Inherits="Shared.UI.Web.ApplicationMonitoredEventEmail.Export" %>
<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label Text="ApplicationMonitoredEventEmailSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="oList" runat="server" />
</asp:Content>
