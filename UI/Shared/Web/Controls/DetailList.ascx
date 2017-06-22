<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailList.ascx.cs"
    Inherits="Shared.UI.Web.Controls.DetailList" %>
<table cellpadding="5" style="font-weight: bold; color: Black" class="maintable"
    border="0">
    <tr>
        <td>
            <asp:GridView ID="MainGridView" AllowPaging="true" PageSize="100" Width="850px" AllowSorting="true"
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
