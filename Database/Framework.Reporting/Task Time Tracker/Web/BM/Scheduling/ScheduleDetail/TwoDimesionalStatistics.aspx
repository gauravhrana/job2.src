<%@ Page Title="Two Dimesional Statistics" Language="C#" MasterPageFile="~/MasterPages/Schedule/Default.Master"
    AutoEventWireup="true" CodeBehind="TwoDimesionalStatistics.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.TwoDimesionalStatistics" %>


<%@ Register Src="Controls/2DStatistics.ascx" TagName="Statistics" TagPrefix="uc1" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>


<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <uc1:Statistics ID="oStatistics" runat="server" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
</asp:Content>
