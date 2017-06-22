<%@ Page Title="Default" Language="C#" MasterPageFile="~/MasterPages/Default.master" 
AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
Inherits="Shared.UI.Web.Configuration.Application.Default" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<%@ Register src="./Controls/SearchFilter.ascx" TagPrefix="sr" TagName="SearchFilter" %>
<%@ Register src="~/Shared/Controls/GroupList.ascx" TagPrefix="dc" TagName="GroupList" %>

<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">Application</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
	<sr:SearchFilter ID="oSearchFilter" runat='server' />
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
	<dc:GroupList ID="oGroupList" runat="server" />
</asp:Content>

<asp:Content ID="ContentActionContent" runat="server" ContentPlaceHolderID="ActionContent"></asp:Content>

<asp:Content ID="ContentVisibilityManager" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
	<vc:VCManager ID="oVC" runat="server" />
</asp:Content>
