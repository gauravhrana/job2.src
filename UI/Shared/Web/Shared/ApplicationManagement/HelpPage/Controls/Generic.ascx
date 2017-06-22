<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.HelpPage.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="95%" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblHelpPageId" runat="server" Text="HelpPageId:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHelpPageId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynHelpPageId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SystemEntityType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntityType" runat="server" OnSelectedIndexChanged="drpSystemEntityType_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            HelpPageContext:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpHelpPageContext" runat="server" OnSelectedIndexChanged="drpHelpPageContext_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHelpPageContextId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynHelpPageContextId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">
                            Content:
                        </td>
                        <td colspan="2">
                            <CKEditor:CKEditorControl ID="txtHelpContent" Width="750" runat="server" Toolbar="Basic" 
                                ToolbarBasic="Bold|Italic|-|NumberedList|BulletedList|-|Link|Unlink|Anchor|-|Outdent|Indent" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynContent" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SortOrder:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dc:List ID="oHistoryList" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
