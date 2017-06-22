<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeBehind="RecordsWithMissingHistory.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Prototype.TestData.RecordsWithMissingHistory" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <style>
        label
        {
            margin-left: 5px;
            vertical-align: middle;
        }
    </style>
    <table style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr>
            <td align="left">
                <asp:Label ID="Label2" Text="System Entity: " runat="server" class="col-sm-2 control-label"></asp:Label>&nbsp;&nbsp;</td>
            <td align="left">
                <asp:DropDownList ID="drpSystemEntity" runat="server"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class='row'>
                    <div class='col-sm-12'>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                    </div>
                </div>
            </td>
        </tr>

        <%-- <tr>
            <td align="right">
                <div class="exportmenuContainer">--%>
        <%--<dc:ExportMenu ID="myExportMenu" runat="server" />--%>
        <%-- </div>

            </td>
        </tr>--%>
        <%--<tr>
            <td colspan="2">
                <dc:List ID="oList" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class='row'>
                    <div class='col-sm-12'>
                        <asp:GridView ID="TestAndAuditGrid" AllowPaging="false" Width="500px" AutoGenerateColumns="false"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="30" />
                                <asp:BoundField DataField="SystemEntityType" HeaderText="SystemEntityType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="EntityKey" HeaderText="Entity Key" ItemStyle-Width="150" />
                                <asp:BoundField DataField="NoHistoryRecords" HeaderText="NoHistoryRecords" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Reason" HeaderText="Reason" ItemStyle-Width="150" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </td>
        </tr>
        <%--   <tr>
            <td align="right">
                <asp:LinkButton ID="btnHome" Text="Home" OnClick="btnHome_Click" runat="server" />
            </td>
        </tr>--%>
    </table>
</asp:Content>
