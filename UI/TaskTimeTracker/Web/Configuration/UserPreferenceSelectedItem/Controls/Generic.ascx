<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.UserPreferenceSelectedItem.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<%@ Register TagPrefix="dc" TagName="TopNList" Src="~/Shared/Controls/TopN.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynTopnHolder" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <dc:TopNList ID="oTopN" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblUserPreferenceSelectedItemId" Text="" runat="server"><span>UserPreferenceSelectedItemId:</span></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserPreferenceSelectedItemId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUserPreferenceSelectedItemId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ApplicationUser:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationUserList" runat="server" OnSelectedIndexChanged="drpApplicationUserList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationUserId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            UserPreferenceKey:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUserPreferenceKeyList" runat="server" OnSelectedIndexChanged="drpUserPreferenceKeyList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserPreferenceKeyId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUserPreferenceKeyId" runat="server" />
                        </td>
                    </tr>
                    <tr id="trDataType" runat="server">
                        <td align="right" class="ralabel">
                            ParentKey:
                        </td>
                        <td>
                            <asp:TextBox ID="txtParentKey" runat="server" ></asp:TextBox>
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynParentKey" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynValue" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Sort Order:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                          </td>
                        <td>
                        </td>
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
