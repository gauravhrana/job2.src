<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="SubMenuSettings.aspx.cs"
    Inherits="Shared.UI.Web.Admin.SubMenuSettings" %>

<%@ Register TagPrefix="sm" TagName="SubMenu" Src="~/Shared/Controls/SubMenu/SubMenu.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr>
            <td valign="top"><br />
                <asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="ralabel">Category:</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtSearchConditionCategory" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:DataList ID="SubMenuSettingsRepeater" runat="server" OnItemCommand="SubMenuSettingsRepeater_ItemCommand"
                    OnItemDataBound="SubMenuSettingsRepeater_ItemDataBound">
                    <HeaderTemplate>
                        <table class="DetailControlBorder" width="500">
                            <tr>
                                <td>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td width="350px">
                                    <asp:Label ID="lblCategory" runat="server"
                                        Text='<%# Eval("Name")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="KeyValueList" runat="server">
                                        <ItemTemplate>
                                            <table>
                                                <tr style="width: 300px;">
                                                    <td style="text-align: right; width: 150px;">
                                                        <asp:Label ID="lblKey" runat="server" Text='<%# Eval("UserPreferenceKey")%>' />
                                                    </td>
                                                    <td style="width: 110px;">
                                                        <asp:TextBox ID="txtValue" runat="server" Width="100px" Columns="6" Text='<%# Eval("Value")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td>
                                    <asp:Button ID="btnGo" runat="server" Text="GO" UseSubmitBehavior="False" CommandName="GO" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        </td> </tr> </table>
                    </FooterTemplate>
                </asp:DataList>
                <asp:LinkButton ID="btnReturn" Text="Return" OnClick="btnReturn_Click" Style="padding-right: 10px;"
                    runat="server" />
                <asp:LinkButton ID="btnSave" Text="Save" OnClick="btnSave_Click" Style="padding-right: 10px;"
                    runat="server" />
            </td>
            <td valign="top">
                <sm:SubMenu ID="oSubMenu" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
