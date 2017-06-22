<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationRoute.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationRouteId" Text="ApplicationRouteId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationRouteId" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationRouteId" runat="server" />
                        </td>
                    </tr>
                    <tr >
                        <td class="ralabel">
                           Route Name:
                        </td>
                        <td width="300">
                            <asp:TextBox ID="txtRouteName" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRouteName" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                           Entity Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityName" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityName" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                           Proposed Route:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProposedRoute" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProposedRoute" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                           Relative Route:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRelativeRoute" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynRelativeRoute" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Description:
                        </td>
                        <td>
                         <asp:TextBox ID="txtDescription" runat="server" Width="250"></asp:TextBox>                            
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
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
