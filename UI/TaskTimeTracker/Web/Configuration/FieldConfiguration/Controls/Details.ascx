<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Controls.Details" %>

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
                <asp:Label ID="lblFieldConfigurationIdText" runat="server"><span>FieldConfigurationId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFieldConfigurationId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFieldConfigurationId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNameText" runat="server"><span>Name: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblValueText" runat="server"><span>Value: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValue" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityTypeIdText" runat="server"><span>SystemEntityTypeId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityTypeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblWidthText" runat="server"><span>Width: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWidth" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFormattingText" runat="server"><span>Formatting: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFormatting" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblControlTypeText" runat="server"><span>ControlType: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblControlType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblHorizontalAlignmentText" runat="server"><span>HorizontalAlignment: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHorizontalAlignment" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDisplayColumnText" runat="server"><span>Display Column: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDisplayColumn" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblCellCountText" runat="server"><span>Cell Count: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCellCount" runat="server"></asp:Label>
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
                                <asp:Label ID="lblHistory" runat="server" Text="" Visible="false"><b>Record History</b></asp:Label>
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
