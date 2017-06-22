<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="Default.aspx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Default" %>


<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/SearchFilter.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    FunctionalityXFunctionalityImage
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ListControlItem">
    <dc:GroupList ID="oGroupList" runat="server" />
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ActionContent">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>
