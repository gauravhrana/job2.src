<%@ Page 
    Title="Default" 
    Language="C#" 
    MasterPageFile="~/MasterPages/Default.master" 
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" 
    Inherits="Shared.UI.Web.Configuration.ApplicationEntityParentalHierarchy.Default" 
%>

<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Shared/Configuration/ApplicationEntityParentalHierarchy/Controls/SearchFilter.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Application Entity Parental Hierachy
</asp:Content>

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
