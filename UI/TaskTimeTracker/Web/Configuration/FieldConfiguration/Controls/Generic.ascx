<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFieldConfigurationId" Text="FieldConfigurationId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFieldConfigurationId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFieldConfigurationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">ApplicationId:
                        </td>
                        <td>
                            <asp:DropDownList  ID="drpApplicationList" runat="server" OnSelectedIndexChanged="drpApplicationList_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplication" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplication" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">SystemEntityTypeId:
                        </td>
                        <td>
                            <asp:DropDownList Width="265px" ID="drpSystemEntityTypeList" runat="server" OnSelectedIndexChanged="drpSystemEntityType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Value:
                        </td>
                        <td>
                            <textarea id="txtValue" runat="server" cols="50" rows="3"></textarea>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynValue" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Display Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDisplayName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Width:
                        </td>
                        <td>
                            <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynWidth" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Formatting:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFormatting" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFormatting" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">ControlType:
                        </td>
                        <td>
                            <asp:TextBox ID="txtControlType" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynControlType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">HorizontalAlignment:
                        </td>
                        <td>
                            <asp:TextBox ID="txtHorizontalAlignment" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynHorizontalAlignment" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">GridViewPriority:
                        </td>
                        <td>
                            <asp:TextBox ID="txtGridViewPriority" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynGridViewPriority" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">DetailsViewPriority:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDetailsViewPriority" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDetailsViewPriority" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">FieldConfigurationMode:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFieldConfigurationMode" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFieldConfigurationMode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">DisplayColumn:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisplayColumn" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDisplayColumn" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Cell Count:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCellCount" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCellCount" runat="server" />
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

