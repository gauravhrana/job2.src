﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls.Details" %>
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
                <asp:Label ID="lblApplicationUserIdText" runat="server">ApplicationUserId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationUserId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationText" runat="server">Application :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplication" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationUserTitleText" runat="server">Application User Title :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationUserNameText" runat="server">Application User Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEmailAddressText" runat="server">Email Address :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmailAddress" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblLastNameText" runat="server">LastName :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblMiddleNameText" runat="server">MiddleName :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMiddleName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFirstNameText" runat="server">FirstName :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFirstName" runat="server"></asp:Label>
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
