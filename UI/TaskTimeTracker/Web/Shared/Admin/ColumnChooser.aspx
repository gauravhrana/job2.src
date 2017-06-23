<%@ Page Title="Column Chooser" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="ColumnChooser.aspx.cs" Inherits="Shared.UI.Web.Admin.ColumnChooser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Application Entity Field Mode : Column Chooser
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table >
        <tr>
            <td>
                <table cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td width="200px">
                            Application Entity Field Mode:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAEFLModeName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="griddiv" runat="server">
                                <asp:GridView ID="dgvAEFL" AllowSorting="true" PageSize="100" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" AutoGenerateEditButton="false" ShowFooter="True" Style="table-layout: fixed;"
                                    Width="1000px">
                                    <RowStyle Height="30px" />
                                    <AlternatingRowStyle Height="30px" />
                                    <HeaderStyle Height="40px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server" Checked="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Columns="12"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Value") %>' Columns="12"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Width">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWidth" runat="server" Text='<%# Bind("Width") %>' Columns="6"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Formatting">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFormatting" runat="server" Text='<%# Bind("Formatting") %>' Columns="6"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Control Type">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtControlType" runat="server" Text='<%# Bind("ControlType") %>'
                                                    Columns="12"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HorizontalAlignment">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHorizontalAlignment" runat="server" Text='<%# Bind("HorizontalAlignment") %>'
                                                    Columns="6"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GridViewPriority">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGridViewPriority" runat="server" Text='<%# Bind("GridViewPriority") %>'
                                                    Columns="6"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DetailsViewPriority">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDetailsViewPriority" runat="server" Text='<%# Bind("DetailsViewPriority") %>'
                                                    Columns="6"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
