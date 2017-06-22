<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Export.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Configuration.UserPreferenceKey.Export" %>
<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx"%>

 <asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="UserPreferenceSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="oList" runat="server" />
</asp:Content>
