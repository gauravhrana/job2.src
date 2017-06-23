<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtReleaseDate.ClientID%>").datepicker({
                dateFormat: '<%= ConvertDateTimeFormat %>'
            });
        });
</script>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table width="95%">
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblReleaseLogId" Text="ReleaseLogId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseLogId" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseLogId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Application :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationIdList" runat="server" OnSelectedIndexChanged="drpApplicationIdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Release Log Status:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpReleaseLogStatusList" runat="server" OnSelectedIndexChanged="drpReleaseLogStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseLogStatusId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseLogStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Version No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersionNo" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynVersionNo" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">Release Date:
                        </td>

                        <td>
                            <asp:TextBox ID="txtReleaseDate" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynReleaseDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Description:
                        </td>
                        <td>
                            <textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Sort Order:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                </table>
                <ui:UpdateInfo ID="UpdateInfo" runat="server" />
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
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
    </table>
</div>
