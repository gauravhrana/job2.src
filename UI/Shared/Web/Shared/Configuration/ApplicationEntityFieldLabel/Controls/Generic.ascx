<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table cellpadding="5" style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <table width="300" cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td width="100" class="ralabel">
                            <asp:Label ID="lblApplicationEntityFieldLabelId" Text="ApplicationEntityFieldLabelId:"
                                Font-Bold="true" runat="server"></asp:Label>
                        </td>
                        <td width="200">
                            <asp:TextBox ID="txtApplicationEntityFieldLabelId" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynApplicationEntityFieldLabelId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SystemEntityTypeId:
                        </td>
                        <td>
                            <asp:DropDownList Width="265px" ID="drpSystemEntityTypeList" runat="server" OnSelectedIndexChanged="drpSystemEntityType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Value:
                        </td>
                        <td>
                            <textarea id="txtValue" runat="server" cols="50" rows="3"></textarea>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynValue" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Width:
                        </td>
                        <td>
                            <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynWidth" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Formatting:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFormatting" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynFormatting" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ControlType:
                        </td>
                        <td>
                            <asp:TextBox ID="txtControlType" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynControlType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            HorizontalAlignment:
                        </td>
                        <td>
                            <asp:TextBox ID="txtHorizontalAlignment" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynHorizontalAlignment" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                            GridViewPriority:
                        </td>
                        <td>
                            <asp:TextBox ID="txtGridViewPriority" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynGridViewPriority" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            DetailsViewPriority:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDetailsViewPriority" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynDetailsViewPriority" runat="server" />
                        </td>
                    </tr>
                      <tr>
                        <td class="ralabel">
                            ApplicationEntityFieldLabelMode:
                        </td>
                        <td>
                            <asp:DropDownList Width="155" ID="ddlApplicationEntityFieldLabelMode" runat="server" OnSelectedIndexChanged="ddlApplicationEntityFieldLabelMode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationEntityFieldLabelMode" runat="server"></asp:TextBox>
                        </td>
                        <td width="200">
                            <asp:PlaceHolder ID="dynApplicationEntityFieldLabelMode" runat="server" />
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
