<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.SystemDevNumbers.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblSystemDevNumbersId" Text="SystemDevNumbersId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemDevNumbersId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemDevNumbersId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Person:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPerson" runat="server" OnSelectedIndexChanged="drpPerson_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPersonId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td width="100">
                            <asp:PlaceHolder ID="dynPersonId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            RangeFrom:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRangeFrom" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRangeFrom" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            RangeTo:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRangeTo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRangeTo" runat="server" />
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
            </td>
        </tr>
    </table>
</div>
