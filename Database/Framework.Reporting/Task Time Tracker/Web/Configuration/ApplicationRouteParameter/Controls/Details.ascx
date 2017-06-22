<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationRouteParameter.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
               
                    <asp:Label ID="lblApplicationRouteParameterIdText" runat="server" Text="ApplicationRouteParameterId"></asp:Label></td>
            <td>
                <asp:Label ID="lblApplicationRouteParameterId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationRouteParameterId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
            <asp:Label ID="lblApplicationRouteText" runat="server"><span>Application Route: </span></asp:Label>
               
            </td>
            <td>
                <asp:Label ID="lblApplicationRoute" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
            <asp:Label ID="lblParameterNameText" runat="server"><span>Parameter Name: </span></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblParameterName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
            <asp:Label ID="lblParameterValueText" runat="server"><span>Parameter Value: </span></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblParameterValue" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       
    </table>
    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
    <table>
        <tr>
            <td colspan="2">
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
</div>
