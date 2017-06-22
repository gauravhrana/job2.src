<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="SearchFilter.ascx.cs" 
    Inherits="Shared.UI.Web.Configuration.UserPreferenceDataType.Controls.SearchFilter" 
    %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar" TagPrefix="ucSearchActionBar" %>

<%@ Register Src='~/BaseUI/SearchFilterControl.ascx' TagPrefix='ucSearchActionBar' TagName='SearchFilterControl' %>

<ucSearchActionBar:SearchFilterControl runat='server' id='SearchFilterControl' />