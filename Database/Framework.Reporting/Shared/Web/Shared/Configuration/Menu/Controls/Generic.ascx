<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.Menu.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%--<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>--%>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table   >
        <tr>
            <td>
                <table width="400"  >
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblMenuId" runat="server" Text="MenuId:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMenuId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMenuId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                        <label class="col-sm-2 control-label" for="<%=txtName.ClientID%>">Name:</label>
                         </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblDisplayName" runat="server" Text="DisplayName:"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDisplayName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="300" class="ralabel" align="left">
                            Parent Menu:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpParentMenu" runat="server" OnSelectedIndexChanged="drpParentMenu_SelectedIndexChanged"
                                AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtParentMenuId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynParentMenuId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Primary Developer:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrimaryDeveloper" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryDeveloper" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">
                            Description:
                        </td>
                        

                        <td>
                            <%--<CKEditor:CKEditorControl ID="txtDescription" Width="750" runat="server" Toolbar="Basic"
                                ToolbarBasic="Bold|Italic|-|NumberedList|BulletedList|-|Link|Unlink|Anchor|-|Outdent|Indent" />--%>
                            <textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea>
                            
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Sort Order:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Is Checked:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsVisible" runat="server" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynIsVisible" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Is Visible:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsChecked" runat="server" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynIsChecked" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Navigate URL:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNavigateURL" runat="server" Width="408px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNavigateURL" runat="server" />
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
