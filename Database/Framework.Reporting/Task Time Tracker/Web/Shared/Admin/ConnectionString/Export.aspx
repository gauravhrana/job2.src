﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Export.Master"
    CodeBehind="Export.aspx.cs" Inherits="Shared.UI.Web.Admin.ConnectionString.Export" %>

<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx" %>
<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label Text="ConnectionStringSearch" runat="server" ForeColor="Red" />
</asp:Content>
<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="mySearchControl" runat="server" />
</asp:Content>
