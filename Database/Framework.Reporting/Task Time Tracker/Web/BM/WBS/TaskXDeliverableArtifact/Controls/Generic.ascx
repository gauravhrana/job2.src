<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.TaskXDeliverableArtifact.Controls.Generic" %>

<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>

<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblTaskXDeliverableArtifactId" Text="TaskXDeliverableArtifactId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskXDeliverableArtifactId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTaskXDeliverableArtifactId" runat="server" />
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
                            DeliverableArtifactStatusId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDeliverableArtifactStatusList" runat="server" OnSelectedIndexChanged="drpDeliverableArtifactStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliverableArtifactStatusId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDeliverableArtifactStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            DeliverableArtifactsId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDeliverableArtifactsList" runat="server" OnSelectedIndexChanged="drpDeliverableArtifactsList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliverableArtifactsId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDeliverableArtifactsId" runat="server" />
                        </td>
                    </tr>
                    <tr id="trUpdatedDate" runat="server" visible="false">
                        <td class="ralabel">
                            Updated Date:
                        </td>
                        <td>
                            <asp:Label ID="lblUpdatedDate" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trUpdatedBy" runat="server" visible="false">
                        <td class="ralabel">
                            Updated By:
                        </td>
                        <td>
                            <asp:Label ID="lblUpdatedBy" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trLastAction" runat="server" visible="false">
                        <td class="ralabel">
                            Last Action:
                        </td>
                        <td>
                            <asp:Label ID="lblLastAction" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
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

