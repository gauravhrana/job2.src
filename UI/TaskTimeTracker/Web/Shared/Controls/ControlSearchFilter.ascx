﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlSearchFilter.ascx.cs" 
Inherits="Shared.UI.Web.Controls.ControlSearchFilter" %>

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
        <asp:TableCell CssClass="searchFilterHeaderContainer" Style="padding: 0px;">
            <asp:Panel ID="pnlCollapsibleContent" runat="server" CssClass="collapsePanel" >

                <table cellpadding="0" cellspacing="0" >
                    <asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr valign="top">
                                <td class="searchLabel">
                                    <b>
                                        <div id="VisibleOnHoverLink">
                                            <a>
                                                <asp:LinkButton ID='chkbox' runat="server" 
                                                    AutoPostBack="true" Text='[XX]' 
                                                    CommandName='<%# Eval("Name") %>'
                                                    OnClick="chkbox_Click" ForeColor="#C0C0C0" />
                                            </a>
                                            <asp:Label ID="label" 
                                                runat="server"
                                                Text='<%# Eval("FieldConfigurationDisplayName") + ": " %>' 
                                                Style="width: auto;">                                                
                                            </asp:Label>
                                    </b></div>
                                </td>
                                <td style="empty-cells: hide;text-align: left;" class="searchtd">
                                    <asp:TextBox ID="txtbox" runat="server" Width="168px"></asp:TextBox>
                                </td>
                                <td style="vertical-align: top;text-align: left;" class="searchtd">
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
            <asp:Table  runat="server" ID="tblActionButtons"  >
                <asp:TableRow>
                    <asp:TableCell Width="240px" CssClass="searchtd">
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" CssClass="searchtd" >
                        <span style="display:inline-block;">
                            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" />
                            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-default"/>
                        </span>
                    </asp:TableCell>
                    <asp:TableCell  CssClass="searchtd"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>

<ajaxToolkit:CollapsiblePanelExtender ID="cpExtender" runat="Server" TargetControlID="pnlCollapsibleContent"
    TextLabelID="lblPanelStatus" ImageControlID="Image1" ExpandedText="Hide Details"
    CollapsedText="Show Details" ExpandedImage="~/Content/images/collapse_blue.jpg" CollapsedImage="~/Content/images/expand_blue.jpg"
    SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
