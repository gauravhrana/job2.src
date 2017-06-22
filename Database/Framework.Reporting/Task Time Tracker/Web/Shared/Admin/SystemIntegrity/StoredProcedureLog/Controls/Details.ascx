<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.StoredProcedureLog.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStoredProcedureLogIdText" runat="server"><span>StoredProcedureLogId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStoredProcedureLogId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynStoredProcedureLogId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                Name:
            </td>
            <td>
                <asp:Label ID="lblName runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                TimeOfExecution
            </td>
            <td>
                <asp:Label ID="lblTimeOfExecution" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                ExecutedBy
            </td>
            <td>
                <asp:Label ID="lblExecutedBy" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       
        
        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
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
