<%@ Control 
	Language="C#" 
	AutoEventWireup="true" 
	CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.ReleasePublishCategory.Controls.SearchFilter" %>

<%@ Register Src="~/BaseUI/SearchFilterControl.ascx" TagPrefix="ucSearchActionBar" TagName="SearchFilterControl" %>

<ucSearchActionBar:SearchFilterControl runat="server" ID="SearchFilterControl" />
