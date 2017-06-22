<%@ Page Language="C#" AutoEventWireup="true" Title="ApplicationEntityFieldLabelSearch" MasterPageFile="~/MasterPages/Export.Master" CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Export" %>
 <%@ Register Src="~/Shared/Controls/List.ascx" TagName="Default" TagPrefix="sr" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="ApplicationEntityFieldLabelSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:Default ID="oList" runat="server" />
</asp:Content> 
    
 

