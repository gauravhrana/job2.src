<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.NoHistoryControls.List" %>
<table  class="maintable"
    >
    <tr>
        <td>
            <div id="griddiv" runat="server">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Full Table Sort" Selected="True" Value="FTSort"></asp:ListItem>
                    <asp:ListItem Text="View Sort" Value="VSort"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:GridView ID="MainGridView" AllowPaging="true" PageSize="100" Width="1000px"
                    AllowSorting="true" AutoGenerateColumns="false" runat="server" OnSorting="GridView_Sorting"
                    OnRowCreated="GridView_RowCreated" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnSelectedIndexChanged="MainGridView_SelectedIndexChanged" OnRowDataBound="MainGridView_RowDataBound"
                    OnSorted="GridView_Sorted" DataKeyNames="SystemEntityType, EntityKey">
                    <Columns>
                    
                        <%--<asp:TemplateField>
                        <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="All" AutoPostBack="true"
                                OnCheckedChanged="chkSelectAll_CheckedChanged" />     
                        </HeaderTemplate>
                        <ItemTemplate >
                        <div style=" text-align:center;">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="float: left;">
                <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click"
                    Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
                <asp:Button ID="ButtonDetails" runat="server" Text="Details" OnClick="ButtonDetails_Click"
                    Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
                <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click"
                    Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
            </div>
            <div style="float: right;">
                <asp:PlaceHolder ID="plcPaging" runat="server" />
                <asp:Label ID="litPagingSummary" runat="server" />
                <asp:Label ID="lblCacheStatus" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div style="float: left;">
                Font Size:
                <asp:LinkButton ID="lnkfontsmall" runat="server" Style="font-size: 12px; color: Blue;
                    font-weight: bold;" OnClick="lnkfontsmall_Click">A</asp:LinkButton>
                <asp:LinkButton ID="lnkfontmedium" runat="server" Style="font-size: 14px; color: Blue;
                    font-weight: bold;" OnClick="lnkfontmedium_Click">A</asp:LinkButton>
                <asp:LinkButton ID="lnkfontlarger" runat="server" Style="font-size: 16px; color: Blue;
                    font-weight: bold;" OnClick="lnkfontlarger_Click">A</asp:LinkButton>
            </div>
        </td>
    </tr>
</table>
