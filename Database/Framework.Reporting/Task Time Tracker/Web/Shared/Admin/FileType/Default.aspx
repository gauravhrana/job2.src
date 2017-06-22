<%@ Page 
	Title="Default" 
	Language="C#" 
	MasterPageFile="~/MasterPages/Default.master" 
	AutoEventWireup="true" 
	CodeBehind="Default.aspx.cs" 
	Inherits="Shared.UI.Web.FileType.Default" 
%>

<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Shared/Admin/FileType/Controls/SearchFilter.ascx" %>
<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">   
 </asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">FileType</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <dc:GroupList ID="oGroupList" runat="server" />
</asp:Content>

<asp:Content ID="ContentActionContent" runat="server" ContentPlaceHolderID="ActionContent">
</asp:Content>

<asp:Content ID="ContentVisibilityManager" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>
