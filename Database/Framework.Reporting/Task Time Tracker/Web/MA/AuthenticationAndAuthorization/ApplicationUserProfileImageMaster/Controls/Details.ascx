<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
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
                <asp:Label ID="lblApplicationUserProfileImageMasterIdText" runat="server"><span>ApplicationUserProfileImageMasterId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserProfileImageMasterId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationUserProfileImageMasterId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationText" runat="server"><span>Application: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplication" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblTitleText" runat="server"><span>Title: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr valign="true">
            <td class="ralabel">
                <asp:Label ID="lblImageText" runat="server"><span>Image: </span></asp:Label>
            </td>
            <td>
                <asp:Image ID="imgApplicationUserImage" Width="300" Height="300" runat="server" />
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
