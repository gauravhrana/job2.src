<%@ Control 
	Language="C#" 
	AutoEventWireup="true" 
	CodeBehind="SearchFilter.ascx.cs" 
	Inherits="ApplicationContainer.UI.Web.ProjectManagement.Project.Controls.SearchFilter" 
%>

<%@ Register Src="~/BaseUI/SearchFilterControl.ascx" TagPrefix="ucSearchActionBar" TagName="SearchFilterControl" %>

<ucSearchActionBar:SearchFilterControl runat="server" id="SearchFilterControl" />
