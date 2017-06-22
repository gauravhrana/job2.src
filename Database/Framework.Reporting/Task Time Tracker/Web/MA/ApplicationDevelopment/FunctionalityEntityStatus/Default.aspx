<%@ Page Title="Default" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Default" %>
<%@ Register TagPrefix="fc" TagName="FESChart" Src="./Controls/FESSummaryChart.ascx" %>

<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/List/List.ascx" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/SearchFilter.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
    
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
<asp:LinkButton ID="lnkGridSummary" runat="server" OnClick="lnkGridSummary_Click" Text="Show Summary" />
    <dc:GroupList ID="oGroupList" runat="server" />
     <fc:FESChart ID="FESChart1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>
