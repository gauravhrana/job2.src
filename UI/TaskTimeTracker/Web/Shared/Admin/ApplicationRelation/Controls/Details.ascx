<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Admin.ApplicationRelation.Controls.Details" %>
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
               
                    <asp:Label ID="lblApplicationRelationIdText" runat="server" Text="ApplicationRelationId"></asp:Label></td>
            <td>
                <asp:Label ID="lblApplicationRelationId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationRelationId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
            <asp:Label ID="lblPublisherApplicationText" runat="server"><span>Publisher Application: </span></asp:Label>
               
            </td>
            <td>
                <asp:Label ID="lblPublisherApplication" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
            <asp:Label ID="lblSubscriberApplicationText" runat="server"><span>SubscriberApplication: </span></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblSubscriberApplication" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
            <asp:Label ID="lblSystemEntityTypeText" runat="server"><span>System Entity Type: </span></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblSystemEntityType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
            <asp:Label ID="lblSubscriberApplicationRoleText" runat="server"><span>Subscriber Application Role: </span></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblSubscriberApplicationRole" runat="server"></asp:Label>
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
