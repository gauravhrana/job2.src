<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.DatabaseChangeLog.Controls.Details" %>


<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblIdText" runat="server"><span>Id:</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">DatabaseName:
            </td>
            <td>
                <asp:Label ID="lblDatabaseName" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Schema Name
            </td>
            <td>
                <asp:Label ID="lblSchemaName" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Object Name
            </td>
            <td>
                <asp:Label ID="lblObjectName" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Object Type
            </td>
            <td>
                <asp:Label ID="lblObjectType" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Event Type
            </td>
            <td>
                <asp:Label ID="lblEventType" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Record Date
            </td>
            <td>
                <asp:Label ID="lblRecordDate" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">System User
            </td>
            <td>
                <asp:Label ID="lblSystemUser" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Current User
            </td>
            <td>
                <asp:Label ID="lblCurrentUser" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Original User
            </td>
            <td>
                <asp:Label ID="lblOriginalUser" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Host Name
            </td>
            <td>
                <asp:Label ID="lblHostName" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Command Text
            </td>
            <td>
                <asp:Label ID="lblbCommandText" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="ralabel">Event Data
            </td>
            <td>
                <asp:Label ID="lblEventData" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>


    </table>
</div>
