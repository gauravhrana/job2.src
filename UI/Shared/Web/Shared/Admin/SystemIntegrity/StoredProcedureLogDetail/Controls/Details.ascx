<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.StoredProcedureLogDetail.Controls.Details" %>


<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblStoredProcedureLogDetailIdText" runat="server"><span>StoredProcedureLogDetailId:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStoredProcedureLogDetailId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynStoredProcedureLogDetailId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                StoredProcedureLogId:
            </td>
            <td>
                <asp:Label ID="lblStoredProcedureLogId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                Parameter Name
            </td>
            <td>
                <asp:Label ID="lblParameterName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                Parameter Value
            </td>
            <td>
                <asp:Label ID="lblParameterValue" runat="server"></asp:Label>
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
