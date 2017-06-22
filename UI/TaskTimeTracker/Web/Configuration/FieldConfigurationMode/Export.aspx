<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" MasterPageFile="~/MasterPages/Site.master" Inherits="Shared.UI.Web.Configuration.FieldConfigurationMode.Export" %>

<%@ Register Src="~/Shared/Controls/List/List.ascx" TagName="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="FieldConfigurationModeSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="oList" runat="server" />
</asp:Content> 
