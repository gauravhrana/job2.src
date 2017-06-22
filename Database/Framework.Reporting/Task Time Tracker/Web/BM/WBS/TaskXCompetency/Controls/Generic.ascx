<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.WBS.TaskXCompetency.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTaskXCompetencyId" Text="TaskXCompetencyId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskXCompetencyId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskXCompetencyId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TaskId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTaskList" runat="server" OnSelectedIndexChanged="drpTaskList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            CompetencyId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpCompetencyList" runat="server" OnSelectedIndexChanged="drpCompetencyList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompetencyId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCompetencyId" runat="server" />
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
