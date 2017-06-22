<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchConfigurationSettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.SearchConfigurationSettings" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<script type="text/javascript">
    function colorChanged(sender) {
        sender.get_element().style.color = "#" + sender.get_selectedColor();
    }
</script>
<table width="1000" border="0" cellspacing="0" cellpadding="5">
    <tr>
        <td>
            <table style="font-weight: bold; color: Black;" border="0" >
                <tr>
                    <td>
                        BackGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSearchBackgroundColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="BorderSampleColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBorderCPE" runat="server" TargetControlID="txtSearchBackgroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="BorderSampleColor" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ForeGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSearchForegroundColor" runat="server"></asp:TextBox></div>
                        <div>
                            <asp:Panel ID="pnlForeGroundSampleColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="ForeGroundPicker" runat="server" TargetControlID="txtSearchForegroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="pnlForeGroundSampleColor" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Border Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSearchBorderColor" runat="server"></asp:TextBox></div>
                        <div>
                            <asp:Panel ID="pnlBorderColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="BorderColorPicker" runat="server" TargetControlID="txtSearchBorderColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="pnlBorderColor" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Border Style:-
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="drpBorderStyle" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Value="Round">Round</asp:ListItem>
                            <asp:ListItem Value="Box">Box</asp:ListItem>
                            <asp:ListItem Value="None">None</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Font Family:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFontfamily" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Font Size:-
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFontSize" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                            CausesValidation="true" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="400">
            <div id="dvSearchFilter" runat="server">
                <asp:Table  runat="server" ID="tblMain" CssClass="searchfilter" >
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                            <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="ralabel">Name:</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtSearchConditionName" />
                        </asp:TableCell></asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell><asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                            <asp:Button runat="server" ID="btnReset" Text="Reset" />
                            <asp:Button runat="server" ID="btnSearch" Text="Search" />
                        </asp:TableCell></asp:TableRow>
                </asp:Table>
            </div>
        </td>
    </tr>
</table>
