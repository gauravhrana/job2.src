<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Admin.ConnectionString.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>

<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel"><b>
                            <asp:Label ID="lblConnectionStringId" runat="server" Text="ConnectionStringId: "></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConnectionStringId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynConnectionStringId" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Description:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Data Source:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataSource" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDataSource" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Initial Catalog:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInitialCatalog" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynInitialCatalog" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">UserName:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUserName" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPassword" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Provider Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProviderName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProviderName" runat="server"></asp:PlaceHolder>
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

