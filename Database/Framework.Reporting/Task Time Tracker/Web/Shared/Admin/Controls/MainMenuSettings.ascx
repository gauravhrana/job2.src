<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenuSettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.MainMenuSettings" %>
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
                        Main Menu BackGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtMenuBackgroundColor" runat="server"></asp:TextBox></div>
                        <div>
                            <asp:Panel ID="BackGroundcolorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBackCPE" runat="server" TargetControlID="txtMenuBackgroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="BackGroundcolorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Menu ForeGround Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtMenuForegroundColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="ForeGroundColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultForeCPE" runat="server" TargetControlID="txtMenuForegroundColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="ForeGroundColorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Hover Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtMenuHoverColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="HoverColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultHoverCPE" runat="server" TargetControlID="txtMenuHoverColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="HoverColorSample" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Border Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtMenuBorderColor" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="BorderColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultBorderCPE" runat="server" TargetControlID="txtMenuBorderColor"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="BorderColorSample" />
                    </td>
                </tr>

                <tr>
                    <td>
                        Menu Colored Category Color:-
                    </td>
                    <td align="left">
                        <div style="float: left;">
                            <asp:TextBox ID="txtMenuColoredCategory" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Panel ID="ColoredCategoryColor" Style="width: 18px; height: 18px; border: 1px solid #000;
                                margin: 0 3px; float: left" runat="server" />
                        </div>
                        <ajaxToolkit:ColorPickerExtender ID="defaultColoredCategory" runat="server" TargetControlID="txtMenuColoredCategory"
                            OnClientColorSelectionChanged="colorChanged" SampleControlID="ColoredCategoryColor" />
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
    </tr>
</table>
