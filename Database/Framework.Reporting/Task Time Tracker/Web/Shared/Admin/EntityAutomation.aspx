<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="EntityAutomation.aspx.cs" Inherits="Shared.UI.Web.Admin.EntityAutomation" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentControlItem" ContentPlaceHolderID="SearchControlItem" runat="server">
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">

        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Entity Name: </asp:TableCell>
            <asp:TableCell>

                <asp:TextBox runat="server" ID="txtEntityName" />
                <asp:RequiredFieldValidator runat="server" ID="val1" ControlToValidate="txtEntityName" ErrorMessage=" *" />

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Connection Key Name: </asp:TableCell>
            <asp:TableCell>

                <asp:TextBox runat="server" ID="txtConnectionKey" />
                <asp:RequiredFieldValidator runat="server" ID="val2" ControlToValidate="txtConnectionKey" ErrorMessage=" *" />

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnAutomate" Text="Create Developer FCMode" OnClick="btnAutomate_Click" />

                <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">
    <table>
        <tr>
            <td colspan="3"></td>
        </tr>
        <tr>
            <td colspan="3"></td>
        </tr>

        <tr>
            <td colspan="3">

                <asp:GridView ID="gvSearchColumns" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Width" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWidth" runat="server" Text='<%# Bind("Width") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HorizontalAlignment" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtHorizontalAlignment" runat="server" Text='<%# Bind("HorizontalAlignment") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="btnCreateSeachFC" Text="Create Search FCMode" Visible="false" OnClick="btnCreateSeachFC_Click" />
            </td>
        </tr>
    </table>
</asp:Content>


