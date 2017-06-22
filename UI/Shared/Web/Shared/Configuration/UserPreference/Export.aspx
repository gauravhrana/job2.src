<%@ Page Language="C#" AutoEventWireup="true" Title="UserPreferenceSearch" MasterPageFile="~/MasterPages/Export.Master" CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Configuration.UserPreference.Export" %>
 <%@ Register Src="~/Shared/Controls/List/List.ascx" Tagname="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label Text="UserPreferenceSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="oList" runat="server" />
</asp:Content> 
    
 

