<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationRouteParameter.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table   >
        <tr>
            <td>
                <table width="400"  >
                    <tr>
                        <td class="ralabel">
                            
                                <asp:Label ID="lblApplicationRouteParameterId" runat="server" Text="ApplicationRouteParameterId:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationRouteParameterId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationRouteParameterId" runat="server" />
                        </td>
                    </tr>
                   
                    <tr>
                        <td width="300" class="ralabel" align="left">
                            ApplicationRoute :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationRoute" runat="server" OnSelectedIndexChanged="drpApplicationRoute_SelectedIndexChanged"
                                AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
                            </asp:DropDownList></td><td>
                            <asp:TextBox ID="txtApplicationRouteId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationRouteId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Parameter Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtParameterName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynParameterName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Parameter Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtParameterValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynParameterValue" runat="server" />
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
