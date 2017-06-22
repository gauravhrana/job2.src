<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblEntityOwnerId" Text="EntityOwnerId:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityOwnerId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityOwnerId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Entity:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpEntityList" runat="server" OnSelectedIndexChanged="drpEntityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntity" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Developer Role:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDeveloperRoleList" runat="server" OnSelectedIndexChanged="drpDeveloperRoleList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeveloperRole" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDeveloperRoleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Developer:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeveloper" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDeveloper" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FeatureOwnerStatus: 
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFeatureOwnerStatusList" runat="server" OnSelectedIndexChanged="drpFeatureOwnerStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFeatureOwnerStatus" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeatureOwnerStatusId" runat="server" />
                        </td>
                    </tr>
                    <td>
                <asp:DropDownList ID="drpApplication" runat="server" OnSelectedIndexChanged="drpApplication_SelectedIndexChanged"
                    Width="155">
                </asp:DropDownList>
                <asp:TextBox ID="txtApplication" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplication" runat="server" />
            </td>
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
