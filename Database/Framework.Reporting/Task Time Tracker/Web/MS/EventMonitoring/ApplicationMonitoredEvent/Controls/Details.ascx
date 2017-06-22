<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEvent.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %> 
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>   
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationMonitoredEventIdText" runat="server" >ApplicationMonitoredEventId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationMonitoredEventId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationMonitoredEventId" runat="server" />
            </td>
        </tr>
       <tr>
            <td class="ralabel">
                 <asp:Label ID="lblApplicationMonitoredEventSourceText" runat="server">ApplicationMonitoredEventSource :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationMonitoredEventSource" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblApplicationMonitoredEventProcessingStateText" runat="server">ApplicationMonitoredEventProcessingState :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationMonitoredEventProcessingState" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblReferenceIdText" runat="server">ReferenceId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReferenceId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblReferenceCodeText" runat="server">ReferenceCode :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReferenceCode" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblCategoryText" runat="server">Category :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCategory" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblMessageText" runat="server">Message :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblIsDuplicateText" runat="server">IsDuplicate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIsDuplicate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblLastModifiedByText" runat="server">LastModifiedBy :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastModifiedBy" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblLastModifiedOnText" runat="server">LastModifiedOn :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastModifiedOn" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
          <tr>
      <td colspan="2">
       <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
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