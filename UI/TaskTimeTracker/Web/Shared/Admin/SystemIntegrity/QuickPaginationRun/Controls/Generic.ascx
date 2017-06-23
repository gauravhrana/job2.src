<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.QuickPaginationRun.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblQuickPaginationRunId" runat="server" Text="QuickPaginationRunId:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuickPaginationRunId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynQuickPaginationRunId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SystemEntityType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntityType" runat="server" OnSelectedIndexChanged="drpSystemEntityType_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ApplicationUser:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationUser" runat="server" OnSelectedIndexChanged="drpApplicationUser_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtApplicationUserId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserId" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="ralabel">
                            SortClause:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortClause" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortClause" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            WhereClause:
                        </td>
                        <td>
                            <asp:TextBox ID="txtWhereClause" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynWhereClause" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Expiration Time:
                        </td>
                        <td>
                            <asp:TextBox ID="txtExpirationTime" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynExpirationTime" runat="server" />
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
            </td>
        </tr>
    </table>
</div>

