<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFunctionalityImageInstanceId" Text="FunctionalityImageInstanceId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityImageInstanceId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityImageInstanceId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FunctionalityImage:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityImageList" runat="server" OnSelectedIndexChanged="drpFunctionalityImageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityImageId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityImageId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FunctionalityImageAttribute:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityImageAttributeList" runat="server"
                                OnSelectedIndexChanged="drpFunctionalityImageAttributeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityImageAttributeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityImageAttributeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel" valign="top">
                            Image:
                        </td>
                        <td colspan="2">
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
                                        <asp:Image ID="imgApplicationUserImage1" runat="server" Height="300" Width="300"
                                            BorderWidth="4" BorderColor="#465c71" />
                                    </div>
                                </div>
                            </asp:Panel>
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
