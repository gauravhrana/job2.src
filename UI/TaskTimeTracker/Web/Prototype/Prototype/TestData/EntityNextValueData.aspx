<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeBehind="EntityNextValueData.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.EntityNextValueData" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    Entity Test Data
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <table style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr>
            <td align="center">
                <label id="Label1" Text="Application :-" runat="server" class="col-sm-2 control-label"/></td>
            <td align="left">
                <asp:DropDownList ID="ddlApplication" runat="server"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="DataGrid" AllowPaging="false" Width="500px" AutoGenerateColumns="false"
                    runat="server">

                    <Columns>
                        <asp:BoundField DataField="Entity Id" HeaderText="Entity Id" ItemStyle-Width="30" />
                        <asp:BoundField DataField="Application Id" HeaderText="Application Id" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Entity Name" HeaderText="Entity Name" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Next PrimaryKey Value" HeaderText="Next PrimaryKey Value" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Next Value" HeaderText="Incorrect Next Value - Configuration" ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
    </table>
</asp:Content>
