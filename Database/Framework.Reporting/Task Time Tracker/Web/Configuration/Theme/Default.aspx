<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MasterPages/Default.master" 
    CodeBehind="Default.aspx.cs" 
    Inherits="Shared.UI.Web.Configuration.Theme.Default" 
%>


<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Configuration/Theme/Controls/SearchFilter.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">

    
    <script src="../../../Scripts/ContextTheme/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../../Scripts/ContextTheme/jquery.contextTheme.js" type="text/javascript"></script>
    <script src="../../../Scripts/ContextTheme/ContextTheme.js" type="text/javascript"></script>

    <link href="../../../Styles/ContextTheme/jquery.contextTheme.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">Theme</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <dc:GroupList ID="oGroupList" runat="server" />
</asp:Content>

<asp:Content ID="ContentActionContent" runat="server" ContentPlaceHolderID="ActionContent">
    <vc:VCManager ID="oVC" runat="server" />        
</asp:Content>
