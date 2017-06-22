<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="RecordsWithMissingHistory.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.RecordsWithMissingHistory" %>

<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/ApplicationManagement/Development/TestData/NoHistoryControls/List.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Shared/ApplicationManagement/Development/TestData/NoHistoryControls/SearchFilter.ascx" %>
<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Records with Missing History
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table cellpadding="5" style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr>
            <td>
                <sr:SearchFilter ID="oSearchFilter" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <div class="exportmenuContainer">
                    <dc:ExportMenu ID="myExportMenu" runat="server" />
                </div>
                <%--<asp:HyperLink ID="lnkExport" Target="_blank" Text="PopUp" runat="server" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <dc:List ID="oList" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">                
                <asp:LinkButton ID="btnHome" Text="Home" OnClick="btnHome_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
