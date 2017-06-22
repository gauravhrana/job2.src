<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"  Inherits="ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar" TagPrefix="ucSearchActionBar" %>
<asp:Table  runat="server" ID="tblMain" CssClass="searchfilter">
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer">
            <asp:Panel ID="pnlHeader" runat="server" >
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:Panel>
        </asp:TableCell> 
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="searchFilterHeaderContainer" Style="padding: 0px;">
            <asp:Panel ID="pnlCollapsibleContent" runat="server" CssClass="collapsePanel" >
                <table class="searchTable">
                    <asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td class="searchLabel">
                                    <asp:PlaceHolder ID="plcHoverLinkLabel" runat="server"></asp:PlaceHolder>                               
                                </td>
                                <td >
                                    <asp:PlaceHolder ID="plcControlHolder" runat="server"></asp:PlaceHolder>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtbox1" runat="server" Columns="10"></asp:TextBox>
                                </td>
                                <td >
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
            <asp:Table  runat="server" ID="tblActionButtons" >
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
    CollapsedText="Show Details" ExpandedImage="~/Content/images/collapse_blue.jpg" CollapsedImage="~/Content/images/expand_blue.jpg"
    SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />