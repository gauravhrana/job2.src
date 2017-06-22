﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner.Controls.Details" %>
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
                <asp:Label ID="lblModuleOwnerIdText" runat="server"><span>ModuleOwnerId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModuleOwnerId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynModuleOwnerId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblModuleText" runat="server"><span>Module: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModuleId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDeveloperRoleText" runat="server"><span>Developer Role: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDevelperRoleId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDeveloperText" runat="server"><span>Developer: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDeveloper" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFeatureOwnerStatusText" runat="server"><span>FeatureOwnerStatus: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeatureOwnerStatusId" runat="server"></asp:Label>
            </td>
            <td>
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
