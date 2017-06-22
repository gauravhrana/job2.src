<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Milestone.Controls.Details" %>
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
                <asp:Label ID="lblMilestoneIdText" runat="server">MilestoneId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMilestoneId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynMilestoneId" runat="server" />
            </td>
        </tr>        
         <tr>
            <td class="ralabel">
                 <asp:Label ID="lblNameText" runat="server">Name:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server">Description:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       <tr>
            <td class="ralabel">
                <asp:Label ID="lblSortOrderText" runat="server">SortOrder:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblProjectIdText" runat="server">ProjectId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProjectId" runat="server"></asp:Label>
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
