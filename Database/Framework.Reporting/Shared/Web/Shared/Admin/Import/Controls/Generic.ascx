<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.BatchFile.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblBatchFileId" Text="BatchFileId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBatchFileId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynBatchFileId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblSystemEntity" Text="SystemEntityTypeId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntityTypeList" runat="server" OnSelectedIndexChanged="drpSystemEntityTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblBatchFileSetId" Text="BatchFileSetId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpBatchFileSetList" runat="server" OnSelectedIndexChanged="drpBatchFileSetList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBatchFileSetId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynBatchFileSetId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFileType" Text="FileTypeId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFileTypeList" runat="server" OnSelectedIndexChanged="drpFileTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFileTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFileTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblBatchFileStatus" Text="BatchFileStatusId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpBatchFileStatusList" runat="server" OnSelectedIndexChanged="drpBatchFileStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBatchFileStatusId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynBatchFileStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblName" Text="Name:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblDescription" Text="Description:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFile" Text="BatchFile:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFile" runat="server" />
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
    </td> </tr> </table>
</div>