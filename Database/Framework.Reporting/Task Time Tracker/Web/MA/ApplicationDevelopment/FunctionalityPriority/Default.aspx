<%@ Page 
    Title="FunctionalityPriority - List" 
    Language="C#"
    MasterPageFile="~/MasterPages/Default.master"
    AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" 
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityPriority.Default"
%>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/SearchFilter.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/List/List.ascx" %>


<asp:Content ID="ContentHead" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    FunctionalityPriority
</asp:Content>


<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <dc:GroupList ID="oGroupList" runat="server" />
</asp:Content>

<asp:Content ID="ContentActionContent" runat="server" ContentPlaceHolderID="ActionContent">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>
