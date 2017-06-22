<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.Admin.SystemEntityCategory.Controls.SearchFilter" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<asp:Table  runat="server" ID="tblMain" CssClass="searchfilter">
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer">
            <asp:Panel ID="pnlHeader" runat="server" >
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer">
            <asp:Panel ID="pnlCollapsibleContent" runat="server" CssClass="collapsePanel">
                <asp:Table ID="Table1" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
                                <HeaderTemplate>
                                    <table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td >
                                            <b>
                                                <div id="VisibleOnHoverLink">
                                                    <a>
                                                        <asp:LinkButton ID='chkbox' runat="server" AutoPostBack="true" Text='[X]' CommandName='<%# Eval("Name") %>'
                                                            OnClick="chkbox_Click" ForeColor="#C0C0C0" /></a>
                                                    <asp:Label ID="label" runat="server" Text='<%# Eval("FieldConfigurationDisplayName") %>' 
                                                        Style="font-weight: bold; width: auto;"></asp:Label>
                                                :
                                       
                                     
                                            </b></div>
                                        </td>
                                        <td >
                                            <asp:HiddenField ID="hdnfield" Value='<%# Eval("Name") %>' runat="server" />
                                        </td>
                                        <td >
                                            <asp:PlaceHolder ID="plcControlHolder" runat="server"></asp:PlaceHolder>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            <%--<asp:Button runat="server" ID="btnSearchLink" Text="Get Link" OnClick="btnSearchLink_Click" />--%>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<ajaxToolkit:CollapsiblePanelExtender ID="cpExtender" runat="Server" TargetControlID="pnlCollapsibleContent"
    ExpandControlID="pnlHeader" CollapseControlID="pnlHeader" TextLabelID="lblPanelStatus"
    ImageControlID="Image1" ExpandedText="Hide Details" CollapsedText="Show Details"
    ExpandedImage="~/Content/images/collapse_blue.jpg" CollapsedImage="~/Content/images/expand_blue.jpg"
    SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
