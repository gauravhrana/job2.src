<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table width="100%" cellpadding="5" border="0" runat="server" id="tblMain1">
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="100" class="ralabel">
                <asp:Label ID="lblApplicationEntityFieldLabelIdText" Font-Bold="True" runat="server"><span style="font-weight:bold;">ApplicationEntityFieldLabelId :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationEntityFieldLabelId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationEntityFieldLabelId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblNameText" Font-Bold="True" runat="server"><span style="font-weight:bold;">Name :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblValueText" Font-Bold="True" runat="server"><span style="font-weight:bold;">Value :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValue" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSystemEntityTypeIdText" Font-Bold="True" runat="server"><span style="font-weight:bold;">SystemEntityTypeId :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityTypeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblWidthText" Font-Bold="True" runat="server"><span style="font-weight:bold;">Width :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblWidth" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFormattingText" Font-Bold="True" runat="server"><span style="font-weight:bold;">Formatting :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFormatting" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblControlTypeText" Font-Bold="True" runat="server"><span style="font-weight:bold;">ControlType :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblControlType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblHorizontalAlignmentText" Font-Bold="True" runat="server"><span style="font-weight:bold;">HorizontalAlignment :</span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHorizontalAlignment" runat="server"></asp:Label>
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
