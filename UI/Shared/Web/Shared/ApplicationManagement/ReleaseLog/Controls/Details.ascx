<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  class="table table-striped">
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblReleaseLogIdText" runat="server">Release Log Id: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReleaseLogId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynReleaseLogId" runat="server" />
            </td>
        </tr>
         <tr>
            <td >
                <asp:Label ID="lblApplicationIdText" runat="server">Application : </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td >
                <asp:Label ID="lblReleaseLogStatusText" runat="server">Release Log Status : </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReleaseLogStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblNameText" runat="server">Name: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td >
               <asp:Label ID="lblVersionNoText" runat="server">Version No: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVersionNo" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td >
               <asp:Label ID="lblReleaseDateText" runat="server">Release Date: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReleaseDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td >
               <asp:Label ID="lblDescriptionText" runat="server">Description: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td >
               <asp:Label ID="lblSortOrderText" runat="server">Sort Order: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
    <table class="table table-striped">
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
        <%--<tr>
            <td colspan="2">
                <asp:PlaceHolder ID="dynReleaseLogDetail" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text=""><b>Release Log Details</b></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dl:DetailList ID="oDetailList" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>--%>
    </table>
</div>
