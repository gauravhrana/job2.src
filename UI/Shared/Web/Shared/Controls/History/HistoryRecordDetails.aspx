<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="HistoryRecordDetails.aspx.cs" Inherits="Shared.UI.Web.History.HistoryRecordDetails" %>

<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/List/List.ascx" %>
<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Feature
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="font-weight: bold; color: Black" width="600" border="0">
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
    </table>
</asp:Content>
