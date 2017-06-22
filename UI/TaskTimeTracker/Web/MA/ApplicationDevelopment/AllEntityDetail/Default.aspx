<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MasterPages/Default.master"  
    CodeBehind="Default.aspx.cs" 
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetail.Default" 
%>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/SearchFilter.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>


<asp:content id="Content2" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="ContentSectionName" runat="server" contentplaceholderid="SectionName">AllEntityDetail</asp:content>

<asp:content id="ContentControlItem" runat="server" contentplaceholderid="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:content>

<asp:content id="ContentListControlItem" runat="server" contentplaceholderid="ListControlItem">
    <dc:GroupList ID="oGroupList" runat="server" />
</asp:content>

<asp:content id="ContentActionContent" runat="server" contentplaceholderid="ActionContent">
</asp:content>

<asp:content id="Content1" runat="server" contentplaceholderid="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:content>
