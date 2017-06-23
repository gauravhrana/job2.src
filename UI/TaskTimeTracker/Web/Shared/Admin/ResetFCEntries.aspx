<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="ResetFCEntries.aspx.cs" Inherits="Shared.UI.Web.Admin.ResetFCEntries" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>



<asp:Content ID="ContentControlItem" ContentPlaceHolderID="SearchControlItem" runat="server">

    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Entity Name:  </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="txtEntityName" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> DataType:  </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlEntity" runat="server"
                    Width="200px">
                    <asp:ListItem Text="Select DataType" Value="Select DataType" Selected="true" />
                    <asp:ListItem Text="Integer" Value="Integer" />
                    <asp:ListItem Text="String" Value="String" />
                    <asp:ListItem Text="DateTime" Value="DateTime" />
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell>
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell>
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">


    <asp:PlaceHolder ID="plcFCGrid" runat="server">
        <table>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>

            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">

                    <asp:GridView ID="gvSearchColumns" runat="server"
                        CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldConfiguration Id" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFieldConfigurationId" runat="server" Text='<%# Bind("FieldConfigurationId") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entity Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEntityName" runat="server" Text='<%# Bind("SystemEntityType") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HorizontalAlignment" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHorizontalAlignment" runat="server" Text='<%# Bind("HorizontalAlignment") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Field Configuration Mode" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFieldConfigurationMode" runat="server" Text='<%# Bind("FieldConfigurationMode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnModifyAlignment"
                        Text="Modify Alignment"
                        OnClick="btnModifyAlignment_Click" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Content>


