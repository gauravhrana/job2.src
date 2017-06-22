<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateInfo.ascx.cs"
    Inherits="Shared.UI.Web.Controls.UpdateInfo" %>
<div id="updateStyle1" runat="server" visible="false">
    <table cellpadding="5">
        <tr>
            <td class="ralabel" width="100">
                <asp:Label ID="lblUpdatedDateText" runat="server" Font-Bold="True" Style="font-weight: bold;">UpdatedDate</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUpdatedDate1" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblUpdatedByText" runat="server" Font-Bold="True" Style="font-weight: bold;">UpdatedBy</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUpdatedBy1" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblLastActionText" runat="server" Font-Bold="True" Style="font-weight: bold;">LastAction</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastAction1" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<div id="updateStyle2" runat="server" visible="false">
    <table cellpadding="5">
        <tr>
            <td class="ralabel" width="100">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Style="font-weight: bold;">Updated</asp:Label>
            </td>
            <td>
                <div style="float: left; width: 50px;">
                    <asp:Label ID="lblLastAction2" runat="server"></asp:Label>
                </div>
                <div style="float: left; width: 150px;">
                    <asp:Label ID="lblUpdatedDate2" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblUpdatedBy2" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="updateStyle3" runat="server" visible="false">
    <table cellpadding="5">
        <tr>
            <td class="ralabel" width="100">
            </td>
            <td>
                <div style="float: left;">
                    <asp:Label ID="lblUpdatedBy3" runat="server"></asp:Label>
                </div>
                <div>
                    &nbsp;<asp:Label ID="lblLastAction3" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="Label1" Text="this record" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblUpdatedDate3" ForeColor="Teal" Font-Size="X-Small" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</div>
