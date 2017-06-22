<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEventEmail.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"  TagPrefix="db" %>  
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
               <asp:Label ID="lblApplicationMonitoredEventEmailIdText" runat="server">ApplicationMonitoredEventEmailId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationMonitoredEventEmailId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationMonitoredEventEmailId" runat="server" />
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
                 <asp:Label ID="lblUserText" runat="server">User :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUser" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblCorrespondenceLevelText" runat="server">CorrespondenceLevel :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCorrespondenceLevel" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                 <asp:Label ID="lblActiveText" runat="server">Active :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblActive" runat="server"></asp:Label>
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