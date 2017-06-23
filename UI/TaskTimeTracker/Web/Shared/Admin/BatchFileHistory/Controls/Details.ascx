<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.BatchFileHistory.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblBatchFileHistoryIdText" runat="server" >BatchFileHistoryId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileHistoryId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynBatchFileHistoryId" runat="server" />
            </td>
        </tr>
       <tr>
            <td class="ralabel">
                 <asp:Label ID="lblBatchFileIdText" runat="server">BatchFileId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblBatchFileSetText" runat="server">BatchFileSet :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileSet" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblBatchFileStatusText" runat="server">BatchFileStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
    <table>
        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblHistory" runat="server" Text="" Visible="false"><b>Record History</b></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dc:List ID="oHistoryList" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</div>