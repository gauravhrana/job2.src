<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityStream.ascx.cs"
    Inherits="Shared.UI.Web.Controls.ActivityStream" %>
<%@ Register TagName="userAuditAction" TagPrefix="uaa" Src="~/Shared/Controls/ActivityStream/Controls/AuditActionDisplay.ascx" %>
<table cellpadding="5" id="tblMain" runat="server" style="font-weight: bold; color: Teal"
    border="2">
    <tr class="header" style="color: White; font-family: Arial; height: 30px;">
        <td width="200" valign="top">
            <asp:Label ID="lblActivityStreamTitle" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblAppUser" runat="server" />
            <div style="float: right;" align="right">
                <asp:HyperLink ID="hypSettings" Text="S" NavigateUrl="~/Shared/Controls/ActivityStream/Settings.aspx?CategoryName="
                    runat="server" />
                <a href="javascript:history.go(0)">R</a>
                <asp:HyperLink ID="hypClose" Text="X" runat="server" />
            </div>
        </td>
    </tr>
    <%--<tr class="ProfileName" style="margin: 40px,10px,10px,0px; width: 200px; color: Teal;
        height: 30px;">
        <td valign="top">
            Shared
            <%--<asp:Label ID="lblDataViewMode" runat="server"></asp:Label>--%>
    <%--</td>
    </tr>--%>
    <tr style="height: 30px;">
        <td valign="top">
            <asp:Label ID="lblActivityStreamAuditId" runat="server"></asp:Label>
            <div style="float: right; color; teal" align="right">
                <asp:Label ID="lblDataViewMode" runat="server" Text=""></asp:Label>
            </div>
        </td>
    </tr>
    <tr style="height: 30px;">
        <td valign="top">
            <asp:Label ID="lblCount" runat="server" Visible="false"></asp:Label>
            <div style="float: right;" align="right">
                <asp:Label ID="lblDateRange" runat="server" ForeColor="Teal"></asp:Label>
            </div>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:Repeater ID="repAlerts" runat="server" OnItemDataBound="repFriends_ItemDataBound">
                <ItemTemplate>
                    <uaa:userAuditAction runat="server" ID="auditAction" />
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr style="height: 30px;">
        <td valign="top">
            <div id="pagingStyle1" runat="server" visible="false">
                <asp:Label ID="Label2" runat="server">Show Page: </asp:Label>
                <asp:TextBox ID="txtPagernumber" Width="50" runat="server"></asp:TextBox>
                <asp:Label ID="lblText" runat="server"></asp:Label>
                <asp:Button ID="btnGo" ForeColor="White" BackColor="Teal" BorderColor="White" runat="server"
                    Text="Go" OnClick="btnGo_Click"></asp:Button>
            </div>
            <div id="pagingStyle2" runat="server" visible="false">
                <div style="float: left;">
                    <asp:LinkButton ID="lb_FirstPage" runat="server" CommandName="pgChange" OnClick="lb_FirstPage_Click">First</asp:LinkButton>
                    &nbsp; | &nbsp;
                    <asp:LinkButton ID="lb_PreviousPage" runat="server" CommandName="pgChange" OnClick="lb_PreviousPage_Click">Previous</asp:LinkButton>
                    &nbsp; | &nbsp;
                    <asp:LinkButton ID="lb_NextPage" runat="server" CommandName="pgChange" OnClick="lb_NextPage_Click">Next</asp:LinkButton>
                    &nbsp; | &nbsp;
                    <asp:LinkButton ID="lb_LastPage" runat="server" CommandName="pgChange" OnClick="lb_LastPage_Click">Last</asp:LinkButton>
                </div>
                <div style="text-align: right;">
                    <asp:Label ID="Label1" runat="server" ForeColor="Black" Font-Bold="false" Text="Displaying Records"></asp:Label>
                    &nbsp;<asp:Label ID="lblPagerDescription" runat="server" Font-Bold="true" ForeColor="Black"
                        Text=""></asp:Label></div>
            </div>
            <div id="pagingStyle4" runat="server" visible="false">
                <div style="float: left;">
                    <asp:Label ID="Label3" runat="server" ForeColor="Black" Font-Bold="false" Text="Displaying Records"></asp:Label>
                    &nbsp;<asp:Label ID="lblPagerDescription1" runat="server" Font-Bold="true" ForeColor="Black"
                        Text=""></asp:Label></div>
                <div style="text-align: right;">
                    <asp:PlaceHolder ID="plcPaging" runat="server" />
                </div>
            </div>
        </td>
    </tr>
</table>
