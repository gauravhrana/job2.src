<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs"
     Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityXWorkTicketIdText" runat="server">EntityXWorkTicketId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityXWorkTicketId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynEntityXWorkTicketId" runat="server" />
            </td>
        </tr>
       
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityText" runat="server">Entity :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblWorkTicketText" runat="server">WorkTicket :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWorkTicketId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblMemoText" runat="server">Memo :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMemo" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblAcknowledgedByText" runat="server">AcknowledgedBy :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAcknowledgedBy" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblKnowledgeDateText" runat="server"><span>KnowledgeDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblKnowledgeDate" runat="server"></asp:Label>
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
