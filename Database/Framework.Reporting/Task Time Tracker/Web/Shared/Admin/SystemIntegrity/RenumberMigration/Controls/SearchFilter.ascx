<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.SystemIntegrity.RenumberMigration.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<asp:Table CellSpacing="0" CellPadding="0" runat="server" ID="tblMain" CssClass="searchfilter">
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer">
            <asp:Panel ID="pnlHeader" runat="server" Width="100%">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer" Style="padding: 0px;">
            <asp:Panel ID="pnlCollapsibleContent" runat="server" CssClass="collapsePanel" Width="100%">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr valign="top">
                                <td style="text-align: right;" class="searchtd" width="240px;">
                                    <b>
                                        <div id="VisibleOnHoverLink">
                                            <a>
                                                <asp:LinkButton ID='chkbox' runat="server" AutoPostBack="true" Text='[X]' CommandName='<%# Eval("Name") %>'
                                                    OnClick="chkbox_Click" ForeColor="#C0C0C0" /></a><asp:Label ID="label" runat="server"
                                                        Text='<%# Eval("FieldConfigurationDisplayName") + ":" %>' Font-Bold="true" Style="font-weight: bold; width: auto;"></asp:Label>
                                    </b></div>
                                </td>
                                <td style="empty-cells: hide; text-align: left;" class="searchtd">
                                    <asp:PlaceHolder ID="plcControlHolder" runat="server"></asp:PlaceHolder>
                                </td>
                                <td style="vertical-align: top; text-align: left;" class="searchtd">
                                    <asp:TextBox ID="txtbox1" runat="server" Columns="10"></asp:TextBox>
                                </td>
                                <td style="text-align: right;" class="searchtd">
                                    <asp:HiddenField ID="hdnfield" Value='<%# Eval("Name") %>' runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer">
            <asp:Table CellSpacing="0" CellPadding="0" runat="server" ID="tblActionButtons" Width="100%">
                <asp:TableRow>
                    <asp:TableCell Width="240px" CssClass="searchtd">
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" CssClass="searchtd" Width="327px">
                        <span style="display: inline-block;">
                            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
                            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                        </span>
                    </asp:TableCell>
                    <asp:TableCell CssClass="searchtd"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<ajaxToolkit:CollapsiblePanelExtender ID="cpExtender" runat="Server" TargetControlID="pnlCollapsibleContent"
    TextLabelID="lblPanelStatus" ImageControlID="Image1" ExpandedText="Hide Details"
    CollapsedText="Show Details" ExpandedImage="~/images/collapse_blue.jpg" CollapsedImage="~/images/expand_blue.jpg"
    SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
