<%@ Page Title="Default - Menu" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/MasterPages/Default.master"
    Inherits="Shared.UI.Web.Configuration.Menu.Default" EnableEventValidation="false" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Configuration/Menu/Controls/SearchFilter.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">

    
    <script src="../../../Scripts/ContextMenu/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../../Scripts/ContextMenu/jquery.contextMenu.js" type="text/javascript"></script>
    <script src="../../../Scripts/ContextMenu/ContextMenu.js" type="text/javascript"></script>

    <link href="../../../Styles/ContextMenu/jquery.contextMenu.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
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
