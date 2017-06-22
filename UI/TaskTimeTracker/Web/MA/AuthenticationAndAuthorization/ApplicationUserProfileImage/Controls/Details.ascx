<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage.Controls.Details" %>
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
                <asp:Label ID="lblApplicationUserProfileImageIdText" runat="server"><span>ApplicationUserProfileImageId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserProfileImageId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationUserProfileImageId" runat="server" />
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
                <asp:Label ID="lblApplicationUserNameText" runat="server"><span>ApplicationUserName: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserName" runat="server"></asp:Label>
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
