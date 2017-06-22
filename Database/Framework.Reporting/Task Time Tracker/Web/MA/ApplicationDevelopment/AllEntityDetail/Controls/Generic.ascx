<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetailDataManager.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblAllEntityDetailId" Text="AllEntityDetailId:" runat="server"
                                Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAllEntityDetailId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynAllEntityDetailId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            EntityName:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            DB_Name:
                        </td>
                        <td>
                            <asp:TextBox id="txtDB_Name" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDB_Name" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            DB_Project_Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDB_Project_Name" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDB_Project_Name" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Component_Project_Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtComponent_Project_Name" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynComponent_Project_Name" runat="server" />
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
