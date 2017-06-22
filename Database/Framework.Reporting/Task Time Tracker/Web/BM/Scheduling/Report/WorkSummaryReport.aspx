<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master"
    CodeBehind="WorkSummaryReport.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.Report.WorkSummaryReport" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <%		   
        Response.WriteFile("WorkSummaryReport.html");
    %>
    
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>
