<%@ Control 
	Language="C#" 
	AutoEventWireup="true" 
	CodeBehind="ScheduleNewSearchControl.ascx.cs"  
	Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.Controls.ScheduleNewSearchControl" 
%>

<%@ Register Src="~/BaseUI/SearchFilterControl.ascx" TagPrefix="ucSearchActionBar" TagName="SearchFilterControl" %>

<ucSearchActionBar:SearchFilterControl runat="server" ID="SearchFilterControl" />

