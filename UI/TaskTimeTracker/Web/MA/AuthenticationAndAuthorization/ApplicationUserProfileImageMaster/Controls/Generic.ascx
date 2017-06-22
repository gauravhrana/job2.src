﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationUserProfileImageMasterId" Text="ApplicationUserProfileImageMasterId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationUserProfileImageMasterId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserProfileImageMasterId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationText" runat="server">Application</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplication" runat="server" OnSelectedIndexChanged="drpApplication_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtApplication" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplication" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Title:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTitle" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel">
                            Image:
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUploadImage" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                     <tr>
                        <td class="ralabel" valign="top">
                            
                        </td>
                        <td>
                            <asp:Image ID="imgApplicationUserImage" runat="server" Height="300" Width="300" BorderWidth="4"
                                BorderColor="#465c71" />
                            <asp:Panel ID="pnlImage" runat="server" Style="display: none">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="PopupHeader">
                                        <div class="TitlebarLeft">
                                        </div>
                                        <div class="TitlebarRight" id="closePopup">
                                        </div>
                                    </div>
                                    <div class="popup_Body">
                                        <asp:Image ID="imgApplicationUserImage1" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                        <td>
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
