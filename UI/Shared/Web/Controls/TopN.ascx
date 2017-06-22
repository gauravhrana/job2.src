<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopN.ascx.cs" Inherits="Shared.UI.Web.Controls.TopN" %>

<table cellpadding="5" style="font-weight: bold; color: Black" width="850px" border="0">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblHeader" runat="server" Text="Label"></asp:Label>:
        </td>
        <%--<td align="right">
            <asp:CheckBox ID="chkVisible" runat="server" Text="Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                AutoPostBack="true" />
        </td>--%>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="MainGridView" AllowPaging="true" PageSize="100" Width="350px" AllowSorting="true"
                AutoGenerateColumns="false" runat="server" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                OnPageIndexChanging="GridView_PageIndexChanging" OnSelectedIndexChanged="MainGridView_SelectedIndexChanged">
                <Columns>
                </Columns>
            </asp:GridView>
            <asp:PlaceHolder ID="plcPaging" runat="server" />
            <asp:Label ID="litPagingSummary" runat="server" />
            <asp:Label ID="lblCacheStatus" runat="server" />
        </td>
    </tr>
</table>
