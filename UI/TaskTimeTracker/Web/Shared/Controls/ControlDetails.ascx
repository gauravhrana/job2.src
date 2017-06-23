<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlDetails.ascx.cs"
    Inherits="Shared.UI.Web.Controls.ControlDetails" %>
<%--<table  width="95%" >
    <tr>
        <td align="right">
            <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td>--%>
            <table   >
                <tr>
                    <td colspan="2" align="center" class="style3">
                        <asp:Label ID="lblEntityName" runat="server" Text="Entity Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:PlaceHolder ID="plcHolderDetails" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        <%--</td>
    </tr>
    <tr>
        <td align="right">
            <asp:LinkButton ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" runat="server" />
            <asp:LinkButton ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnBack_Click" runat="server" />
            <asp:LinkButton ID="btnClone" runat="server" Text="Clone" OnClick="btnClone_Click" />
        </td>
    </tr>
</table>--%>
