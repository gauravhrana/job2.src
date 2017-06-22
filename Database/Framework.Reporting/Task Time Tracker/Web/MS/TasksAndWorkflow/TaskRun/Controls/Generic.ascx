<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTaskRunId" Text="TaskRunId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskRunId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskRunId" runat="server" />
                        </td>
                    </tr>                   
                    <tr>
                        <td class="ralabel"> 
                            TaskEntityId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTaskEntityList" runat="server" OnSelectedIndexChanged="drpTaskEntityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskEntityId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskEntityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            TaskScheduleId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTaskScheduleList" runat="server" OnSelectedIndexChanged="drpTaskScheduleList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskScheduleId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskScheduleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            BusinessDate:
                        </td>
                        <td>
                            <asp:Calendar ID="clnBusinessDate" runat="server" OnSelectionChanged="clnBusinessDate_SelectionChanged">
                            </asp:Calendar>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBusinessDate" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynBusinessDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            StartTime:
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynStartTime" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            EndTime:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEndTime" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            RunBy:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRunBy" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRunBy" runat="server" />
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
