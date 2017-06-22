<%@ Page Language="C#" Title="Export" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Export" %>

<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label Text="NotificationPublisherXEventTypeSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="oList" runat="server" />
</asp:Content>
