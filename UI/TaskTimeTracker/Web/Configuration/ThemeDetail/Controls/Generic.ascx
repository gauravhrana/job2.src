<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.ThemeDetail.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server"> 
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblThemeDetailId" Text="ThemeDetailId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtThemeDetailId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynThemeDetailId" runat="server" />
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="ralabel">
                            ThemeId:
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlThemeId" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="txtThemeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynThemeId" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                            ThemeKeyId:
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlThemeKeyId" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="txtThemeKeyId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynThemeKeyId" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                            ThemeCategoryId:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlThemeCategoryId" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="txtThemeCategoryId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynThemeCategoryId" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                            Value:
                        </td>
                        <td>
                            <textarea cols="50" rows="3" id="txtValue" runat="server"></textarea>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynValue" runat="server" />
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