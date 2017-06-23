<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="TestFESsummaryChart.aspx.cs" Inherits="ApplicationContainer.UI.Web.TestFESsummaryChart" %>

<%@ Register TagPrefix="fc" TagName="FESChart" Src="~/MA/ApplicationDevelopment/FunctionalityEntityStatus/Controls/FESSummaryChart.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <fc:feschart id="oSearchFilter" runat="server" />
</asp:Content>
