<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="SearchFilter.ascx.cs"  
    Inherits="ApplicationContainer.UI.Web.WBS.TaskXActivityInstance.Controls.SearchFilter" %>

<%@ Register Src="~/BaseUI/SearchFilterControl.ascx" TagPrefix="ucSearchActionBar" TagName="SearchFilterControl" %>

<ucSearchActionBar:SearchFilterControl runat="server" ID="SearchFilterControl" />
