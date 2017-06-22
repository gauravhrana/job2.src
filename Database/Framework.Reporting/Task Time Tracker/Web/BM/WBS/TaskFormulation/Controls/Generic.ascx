<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.WBS.TaskFormulation.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTaskFormulationId" Text="TaskFormulationId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskFormulationId" runat="server"></asp:TextBox>
                        </td><td></td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskFormulationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Feature:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeatureList" runat="server" OnSelectedIndexChanged="drpFeatureList_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList></td><td>
                            <asp:TextBox ID="txtFeature" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeature" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Project:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpProjectList" runat="server" OnSelectedIndexChanged="drpProjectList_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList></td><td>
                            <asp:TextBox ID="txtProjectId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProjectId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Task:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTaskList" runat="server" OnSelectedIndexChanged="drpTaskList_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList></td><td>
                            <asp:TextBox ID="txtTask" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTask" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SortOrder:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td><td></td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
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
