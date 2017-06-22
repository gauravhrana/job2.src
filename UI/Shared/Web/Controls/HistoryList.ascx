<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryList.ascx.cs"
    Inherits="Shared.UI.Web.Controls.HistoryList" %>
<table cellpadding="5" style="font-weight: bold; color: Black" width="850px" border="0">
    <tr>
        <td colspan="2">
            Audit History:
        </td>
        <%--<td align="right">
            <asp:CheckBox ID="chkVisible" runat="server" Text="Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                AutoPostBack="true" />
        </td>--%>
    </tr>
    <tr>
        <td align="right" valign="top" colspan="2">
            <asp:PlaceHolder ID="dynAdvancedMode" runat="server" Visible="true">
                <asp:PlaceHolder ID="dynIntervalMode" runat="server" Visible="false">
                    <asp:Label ID="lblInterval" runat="server" Text="Interval: "></asp:Label>
                    <asp:TextBox ID="txtInterval" Width="50" AutoPostBack="true" runat="server" OnTextChanged="txtInterval_TextChanged"></asp:TextBox>
                    &nbsp;
                    <asp:DropDownList ID="drpIntervalUnit" runat="server" OnSelectedIndexChanged="drpdrpIntervalUnit_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="second">Seconds</asp:ListItem>
                        <asp:ListItem Value="minute" Selected="True">Minutes</asp:ListItem>
                        <asp:ListItem Value="hour">Hours</asp:ListItem>
                        <asp:ListItem Value="day">Hours</asp:ListItem>
                        <asp:ListItem Value="week">Weeks</asp:ListItem>
                        <asp:ListItem Value="month">Months</asp:ListItem>
                        <asp:ListItem Value="quarter">Quarters</asp:ListItem>
                        <asp:ListItem Value="year">Years</asp:ListItem>
                    </asp:DropDownList>
                </asp:PlaceHolder>
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="Grouping: "></asp:Label>
                <asp:DropDownList ID="drpAdvancedModeGrouping" runat="server" OnSelectedIndexChanged="drpAdvancedModeGrouping_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="timeinterval">TimeInterval</asp:ListItem>
                    <asp:ListItem Value="auditaction">AuditAction</asp:ListItem>
                    <asp:ListItem Value="actionby" Selected="True">ActionBy And AuditAction</asp:ListItem>
                </asp:DropDownList>
            </asp:PlaceHolder>
            &nbsp;
            <asp:PlaceHolder ID="dynModeHolder" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Grid Mode: "></asp:Label>
                <asp:DropDownList ID="drpGridViewMode" runat="server" OnSelectedIndexChanged="drpGridViewMode_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="classic">Classic</asp:ListItem>
                    <asp:ListItem Value="advanced" Selected="True">Advanced</asp:ListItem>
                </asp:DropDownList>
            </asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="MainGridView" AllowPaging="true" PageSize="100" Width="850px" AllowSorting="true"
                AutoGenerateColumns="false" runat="server" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"
                OnPageIndexChanging="GridView_PageIndexChanging" OnSelectedIndexChanged="MainGridView_SelectedIndexChanged">
                <Columns>
                </Columns>
            </asp:GridView>
            <asp:PlaceHolder ID="plcPaging" runat="server" />
            <asp:Label ID="litPagingSummary" runat="server" />
            <asp:Label ID="lblCacheStatus" runat="server" />
        </td>
    </tr>
</table>
