<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageAttribute.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>
<div id="borderdiv" runat="server">
    <ajaxToolkit:ModalPopupExtender ID="mdePopup" runat="server" PopupControlID="pnlImage"
        PopupDragHandleControlID="PopupHeader" TargetControlID="imgApplicationUserImage"
        CancelControlID="closePopup" Drag="true">
    </ajaxToolkit:ModalPopupExtender>
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblFunctionalityImageAttributeId" Text="FunctionalityImageAttributeId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityImageAttributeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityImageAttributeId" runat="server" />
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
                            Name:
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
                            Description:
                        </td>
                        <td>
                            <uc1:KendoEditor runat="server" id="txtDescription" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
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
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel" valign="top">
                            Image:
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
