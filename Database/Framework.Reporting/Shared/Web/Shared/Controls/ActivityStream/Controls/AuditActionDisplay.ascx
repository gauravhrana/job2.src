<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditActionDisplay.ascx.cs"
    Inherits="Shared.UI.Web.ActivityStream.Controls.AuditActionDisplay" %>
<table  runat="server" id="tblTimeGroup" visible="false">
    <tr>
        <td align="left">
            <asp:Label ID="lblTimeGroup" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <%--<hr style="height: 1px; color: #E5E5E5; background: #E5E5E5; border: none;" />--%>
            <hr />
        </td>
    </tr>
</table>
<table >
    <tr id="recordSeperator" runat="server">
        <td colspan="2">
            <hr style="height: 1px; color: #E5E5E5; background: #E5E5E5; border: none;">
        </td>
    </tr>
    <tr id="smallRecordSeperator" runat="server" visible="false">
        <td>
        </td>
        <td>
            <hr style="height: 1px; color: #E5E5E5; background: #E5E5E5; border: none;">
        </td>
    </tr>
    <tr>
        <td width="50">
            <asp:Image ImageAlign="Left" Width="50" Height="50" ID="imgAvatar" ImageUrl="~/Content/images/ProfileImage.aspx"
                runat="server" />
        </td>
        <td>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="childRecords" runat="server" visible="false">
        <td>
        </td>
        <td>
            <asp:Panel ID="pnlHeader" runat="server" Height="30px">
                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                    <div style="float: left; vertical-align: middle;">
                        <div style="float: left;">
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Content/images/expand_blue.jpg" AlternateText="Show Details" />
                        </div>
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblPanelStatus" runat="server">Show Details</asp:Label>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlCollapsibleContent" runat="server" CssClass="collapsePanel" Height="0">
                <div style="margin-left: 25px; color: Black; font-size: small;">
                    <table border="1" cellspacing="2">
                        <asp:Repeater ID="repChildItems" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("ChildText") %>
                                    </td>
                                    <%--<td>
                                    <%# Eval("ChildAction") %>
                                </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pnlCollapsibleContent"
                ExpandControlID="pnlHeader" CollapseControlID="pnlHeader" Collapsed="True" TextLabelID="lblPanelStatus"
                ImageControlID="Image1" ExpandedText="Hide Details" CollapsedText="Show Details"
                ExpandedImage="~/Content/images/collapse_blue.jpg" CollapsedImage="~/Content/images/expand_blue.jpg"
                SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
        </td>
    </tr>
    <tr id="seperateTimeStamp" runat="server" visible="false">
        <td width="50">
        </td>
        <td>
            <asp:Label ID="lblTimeStamp" runat="server"></asp:Label>
        </td>
    </tr>
</table>
