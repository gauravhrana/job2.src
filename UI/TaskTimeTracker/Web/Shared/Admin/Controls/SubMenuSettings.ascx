<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubMenuSettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.SubMenuSettings" %>
<%@ Register TagPrefix="sm" TagName="SubMenu" Src="~/Shared/Controls/SubMenu/SubMenu.ascx" %>
<script type="text/javascript">
    function colorChanged(sender) {
        sender.get_element().style.color = "#" + sender.get_selectedColor();
    }
</script>
<table   >
    <tr>
        <td>
            <table  >
                <tr>
                    <td>
                        SubMenu TopBar BackGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSubMenuTopBarBackgroundColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="TopBarBackgroundColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultTopBackCPE" runat="server" TargetControlID="txtSubMenuTopBarBackgroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="TopBarBackgroundColorSample" />
                    </td>
                </tr>
                 <tr>
                    <td>
                        SubMenu TopBar Border Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSubMenuTopBarBorderColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="TopBarBorderColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultTopBorderCPE" runat="server" TargetControlID="txtSubMenuTopBarBorderColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="TopBarBorderColorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        SubMenu BackGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSubMenuBackgroundColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="BackGroundcolorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBackCPE" runat="server" TargetControlID="txtSubMenuBackgroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="BackGroundcolorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        SubMenu ForeGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSubMenuForegroundColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="ForeGroundColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultForeCPE" runat="server" TargetControlID="txtSubMenuForegroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="ForeGroundColorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Hover Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSubMenuHoverColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="HoverColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultHoverCPE" runat="server" TargetControlID="txtSubMenuHoverColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="HoverColorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Border Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtSubMenuBorderColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="BorderSampleColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBorderCPE" runat="server" TargetControlID="txtSubMenuBorderColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="BorderSampleColor" />
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
                    <td width="100">
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                            CausesValidation="true" />
                    </td>
                </tr>
            </table>
        </td>
        <td align="left" valign="top">
            <sm:SubMenu ID="oSubMenu" runat="server" />
        </td>
    </tr>
</table>
