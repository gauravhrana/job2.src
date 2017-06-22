<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailList.ascx.cs" Inherits="Shared.UI.Web.Controls.DetailListControl" %>
<table  class="table" >
    <tr>
        <td>
            <asp:GridView ID="MainGridView" AllowPaging="true" PageSize="100" AllowSorting="true"
                AutoGenerateColumns="false" runat="server" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                OnPageIndexChanging="GridView_PageIndexChanging" >
                <Columns>
                </Columns>
            </asp:GridView>
            <asp:PlaceHolder ID="plcPaging" runat="server" />
            <asp:Label ID="litPagingSummary" runat="server" />
            <asp:Label ID="lblCacheStatus" runat="server" />
        </td>
    </tr>
</table>
