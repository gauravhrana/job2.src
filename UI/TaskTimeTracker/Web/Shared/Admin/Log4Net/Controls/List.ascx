<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Admin.Log4Net.Controls.List" %>
<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<table cellpadding="0" cellspacing="0"  class="maintable"
    >
    <tr style="background: lightblue;">
        <td align="right">
            <span class="exportmenuContainer">
                <asp:HiddenField ID="hdnAEFLModeCategory" runat="server" />
                <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
                </asp:DropDownList>
                <dc:exportmenu id="myExportMenu" runat="server" />
            </span>
        </td>
    </tr>
    <tr>
        <td>
            <div id="griddiv" runat="server">
                <div id="gridContainer">
                    <asp:GridView ID="dgvRecords" runat="server" Width="1000px" AllowPaging="false" AutoGenerateColumns="false"
                        CellPadding="2" CellSpacing="2" AllowSorting="true" OnSorting="dgvRecords_Sorting">
                    </asp:GridView>
                </div>
            </div>
            <asp:Panel ID="pnlPaging" runat="server">
                <div style="float: right;">
                    <asp:PlaceHolder ID="plcPaging" runat="server">
                        <asp:LinkButton ID="lb_FirstPage" runat="server" CommandName="pgChange" OnClick="lb_FirstPage_Click">First</asp:LinkButton>
                        &nbsp; | &nbsp;
                        <asp:LinkButton ID="lb_PreviousPage" runat="server" CommandName="pgChange" OnClick="lb_PreviousPage_Click">Previous</asp:LinkButton>
                        &nbsp; | &nbsp;
                        <asp:LinkButton ID="lb_NextPage" runat="server" CommandName="pgChange" OnClick="lb_NextPage_Click">Next</asp:LinkButton>
                        &nbsp; | &nbsp;
                        <asp:LinkButton ID="lb_LastPage" runat="server" CommandName="pgChange" OnClick="lb_LastPage_Click">Last</asp:LinkButton>&nbsp;
                    </asp:PlaceHolder>
                    <asp:Label ID="litPagingSummary" runat="server" />
                    <asp:Label ID="lblCacheStatus" runat="server" />
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
