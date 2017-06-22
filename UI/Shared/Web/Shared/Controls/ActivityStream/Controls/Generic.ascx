<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ActivityStream.Controls.Generic" %>
<link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://static.jquery.com/ui/css/demo-docs-theme/ui.theme.css"
    type="text/css" media="all" />
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js"
    type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#<%= txtStartDate.ClientID  %>").datepicker();
    });
</script>
<div id="borderdiv" runat="server">
    <table cellpadding="5" style="font-weight: bold; color: Black" width="95%" border="0">
        <tr>
            <td>
                <div class="tabContainer" style="margin-bottom: 10px;">
                    <ul class="toc" runat="server" id="toc">
                        <li runat="server" style="background-color: Yellow;">
                            <asp:LinkButton ID="lnkTabGeneralSettings" runat="server" Text="General Settings"
                                OnClick="lnkTabGeneralSettings_Click"></asp:LinkButton>
                        </li>
                        <li runat="server">
                            <asp:LinkButton ID="lnkTabCosmeticSettings" runat="server" OnClick="lnkTabCosmeticSettings_Click"
                                Text="Cosmetic Settings"></asp:LinkButton>
                        </li>
                        <li runat="server">
                            <asp:LinkButton ID="lnkTabOtherSettings" runat="server" OnClick="lnkTabOtherSettings_Click"
                                Text="System Entities"> </asp:LinkButton>
                        </li>
                        <li runat="server">
                            <asp:LinkButton ID="lnkTabAll" runat="server" OnClick="lnkTabAll_Click" Text="All"> </asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div style="margin-top: 10px; margin-bottom: 10px;" runat="server" id="generalSettings">
                    <div>
                        <table width='100%' cellpadding='0' cellspacing='0' style='padding: 3px; background-color: #F5F5F5;
                            margin-top: 5px; margin-bottom: 5px;'>
                            <tr>
                                <td align='left'>
                                    General Settings
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="DetailControlBorder">
                        <table width="95%" cellpadding="2" cellspacing="4" border="0">
                            <tr>
                                <td class="ralabel" width="300">
                                    Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActivityStreamTitle" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="dynActivityStreamTitle" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    DataType:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpActivityAlertMode" runat="server" Width="200px">
                                        <asp:ListItem Value="Both">Both</asp:ListItem>
                                        <asp:ListItem Value="Real Data">Real Data</asp:ListItem>
                                        <asp:ListItem Value="Test Data">Test Data</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    StartDate:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="dynStartDate" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    Interval:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInterval" runat="server" Width="200px">In days</asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="dynInterval" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    Activity Stream Audit Id:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActivityStreamAuditId" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="dynActivityStreamAuditId" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="margin-top: 10px; margin-bottom: 10px;" runat="server" id="cosmeticSettings"
                    visible="false">
                    <div>
                        <table width='100%' cellpadding='0' cellspacing='0' style='padding: 3px; background-color: #F5F5F5;
                            margin-top: 5px; margin-bottom: 5px;'>
                            <tr>
                                <td align='left'>
                                    Cosmetic Settings
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="DetailControlBorder">
                        <table width="95%" cellpadding="2" cellspacing="4" border="0">
                            <tr>
                                <td class="ralabel">
                                    Background:
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtColor" runat="server" Width="200px"></asp:TextBox></div>
                                    <div>
                                        <asp:Panel ID="colorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                                            margin: 0 3px; float: left" runat="server" />
                                    </div>
                                    <ajaxToolkit:ColorPickerExtender ID="defaultCPE" runat="server" TargetControlID="txtColor"
                                        OnClientColorSelectionChanged="colorChanged" SampleControlID="colorSample" />
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="dynColor" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    Width:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWidth" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    Height:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeight" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    Paging Style:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpPagingStyle" runat="server" Width="200px">
                                        <asp:ListItem Value="ActivityStreamPagingStyle1">ActivityStreamPagingStyle1</asp:ListItem>
                                        <asp:ListItem Value="ActivityStreamPagingStyle2">ActivityStreamPagingStyle2</asp:ListItem>
                                        <asp:ListItem Value="ActivityStreamPagingStyle3">ActivityStreamPagingStyle3</asp:ListItem>
                                        <asp:ListItem Value="ActivityStreamPagingStyle4">ActivityStreamPagingStyle4</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="PlaceHolder4" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="ralabel">
                                    Activity Stream Page Size:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActivityStreamPageSize" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td width="200">
                                    <asp:PlaceHolder ID="dynActivityStreamPageSize" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="margin-top: 10px; margin-bottom: 10px;" runat="server" id="otherSettings"
                    visible="false">
                    <div>
                        <table width='100%' cellpadding='0' cellspacing='0' style='padding: 3px; background-color: #F5F5F5;
                            margin-top: 5px; margin-bottom: 5px;'>
                            <tr>
                                <td align='left'>
                                    System Entities
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="DetailControlBorder">
                        <table width="95%" cellpadding="2" cellspacing="4" border="0">
                            <tr>
                                <td valign="top">
                                    Excluded System Entities:
                                </td>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSearchSource" runat="server" Width="250"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="1" cellpadding="0" cellspacing="5">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblPossibleTitle" runat="server" Text="Possible System Entities:"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblCurrentTitle" runat="server" Text="Currently Excluded System Entities :"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="*">
                                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource" Width="250" Height="250">
                                                </asp:ListBox>
                                            </td>
                                            <td width="*">
                                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTarget" Width="250" Height="250">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button runat="server" Text="-->" ID="btnLeft" OnClick="btnLeft_Click" />
                                            </td>
                                            <td align="center">
                                                <asp:Button runat="server" Text="<--" ID="btnRight" OnClick="btnRight_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Button runat="server" Text="Reset" ID="btnReset" OnClick="btnReset_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    function colorChanged(sender) {
        sender.get_element().style.color = "#" + sender.get_selectedColor();
    }
</script>
