<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblApplicationUserProfileImageId" Text="ApplicationUserProfileImageId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationUserProfileImageId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUserProfileImageId" runat="server" />
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
                            ApplicationUser:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpApplicationUser" runat="server" OnSelectedIndexChanged="drpApplicationUser_SelectedIndexChanged"
                                Width="155">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtApplicationUser" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationUser" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Image:
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUploadImage" runat="server" />
                            <%--<textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea>--%>
                        </td>
                        <td>
                            <%--<asp:PlaceHolder ID="dynImage" runat="server" />--%>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">
                            ApplicationUserPrfielImageMaster:
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" ID="drpApplicationUserProfileImageMaster" runat="server"
                                OnSelectedIndexChanged="drpApplicationUserProfileImageMaster_SelectedIndexChanged"
                                AppendDataBoundItems="true">
                                <asp:ListItem Text="None" Selected="True">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Image ID="imgApplicationUserProfileImageMaster" Width="300" Height="300" runat="server" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dyndrpApplicationUserProfileImageMaster" runat="server" />
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
