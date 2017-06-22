<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" 
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityImageInstanceIdText"
                    runat="server"><span>FunctionalityImageInstanceId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityImageInstanceId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFunctionalityImageInstanceId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityImageText" runat="server">FunctionalityImage:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityImageId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityImageAttributeIdText" runat="server"><span>FunctionalityImageAttributeId: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityImageAttributeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel" valign="top">
                <asp:Label ID="lblImage" runat="server"><span>Image: </span></asp:Label>
            </td>
            <td>
                <asp:Image ID="imgApplicationUserImage" runat="server" Height="300" Width="300" Visible="false"
                    BorderWidth="4" BorderColor="#465c71" />
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
            <td colspan="2">
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
</div>
