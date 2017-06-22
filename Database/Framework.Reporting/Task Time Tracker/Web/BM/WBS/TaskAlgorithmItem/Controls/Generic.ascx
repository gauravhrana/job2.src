<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                <asp:Label ID="lblTaskAlgorithmItemId" runat="server"><span>TaskAlgorithmItemId:</span></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTaskAlgorithmItemId" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTaskAlgorithmItemId" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                ActivityId:
            </td>
            <td>
                <asp:DropDownList ID="drpActivityList" runat="server" OnSelectedIndexChanged="drpActivityList_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:TextBox ID="txtActivityId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynActivityId" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="ralabel">
                TaskAlgorithmId:
            </td>
            <td>
                <asp:DropDownList ID="drpTaskAlgorithmList" runat="server" OnSelectedIndexChanged="drpTaskAlgorithmList_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:TextBox ID="txtTaskAlgorithmId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynTaskAlgorithmId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                Description:
            </td>
            <td>
                <textarea id="txtDescription" runat="server" cols="50" rows="3">
            </textarea>
            </td>
            <td>
                <asp:PlaceHolder ID="dynDescription" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                SortOrder:
            </td>
            <td>
                <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynSortOrder" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
            </table>
            <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            <table>
        <tr>
            <td colspan="4">
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
