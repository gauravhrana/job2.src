﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="ApplicationContainer.UI.Web.UseCaseActorXUseCase.Export" %>

<%@ Register TagName="List" TagPrefix="sr" Src="~/Shared/Controls/List/List.ascx" %>

<asp:Content ID="exportHeading" ContentPlaceHolderID="SectionName" runat="server">
    <asp:Label Text="UseCaseActorXUseCaseSearch" runat="server" ForeColor="Red" />
</asp:Content> 

<asp:Content ID="exportDetails" ContentPlaceHolderID="MainContent" runat="server">
    <sr:List ID="oList" runat="server" />
</asp:Content>
