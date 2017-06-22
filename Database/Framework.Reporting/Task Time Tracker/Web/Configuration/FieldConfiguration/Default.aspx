<%@ Page Title="Default" Language="C#" Trace="false" MasterPageFile="~/MasterPages/Default.master" EnableEventValidation="false" AutoEventWireup="true"
 CodeBehind="Default.aspx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Default" %>

<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register Src="~/Configuration/FieldConfiguration/Controls/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="sr" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
        <dc:GroupList ID="oGroupList" runat="server" />
</asp:Content>
<asp:Content ID="ContentActionContent" runat="server" ContentPlaceHolderID="ActionContent">
  
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>



