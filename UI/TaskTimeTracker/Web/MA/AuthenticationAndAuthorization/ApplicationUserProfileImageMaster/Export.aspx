<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Export.Master" CodeBehind="Export.aspx.cs"
 Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster.Export" %>
<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label ID="Label1" Text="ApplicationUserProfileImageMasterSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="oList" runat="server" />
</asp:Content>

