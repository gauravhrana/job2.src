<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetailDataManager.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
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
                <asp:Label ID="lblAllEntityDetailIdText" runat="server">AllEntityDetailId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAllEntityDetailId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynAllEntityDetailId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblEntityNameText" runat="server">EntityName :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEntityName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDB_NameText" runat="server">DB_Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDB_Name" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDB_Project_NameText" runat="server">DB_Project_Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDB_Project_Name" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblComponent_Project_NameText" runat="server">Component_Project_Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblComponent_Project_Name" runat="server"></asp:Label>
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
