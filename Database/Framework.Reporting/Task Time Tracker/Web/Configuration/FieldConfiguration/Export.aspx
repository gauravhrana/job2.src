<%@ Page Language="C#" AutoEventWireup="true" Title="FieldConfigurationSearch" MasterPageFile="~/MasterPages/Export.Master" CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Export" %>

 <%@ Register Src="~/Shared/Controls/List/List.ascx" TagName="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="FieldConfigurationSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="oList" runat="server" />
</asp:Content>
