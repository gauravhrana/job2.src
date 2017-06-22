<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.UseCaseActorXUseCase.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblUseCaseActorXUseCaseId" Text="Use Case Actor X Use Case Id:"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUseCaseActorXUseCaseId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUseCaseActorXUseCaseId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            UseCaseId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUseCaseList" runat="server" OnSelectedIndexChanged="drpUseCaseList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUseCaseId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUseCaseId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            UseCaseRelationshipId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUseCaseRelationshipList" runat="server" OnSelectedIndexChanged="drpUseCaseRelationshipList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUseCaseRelationshipId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUseCaseRelationshipId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            UseCaseActorId:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUseCaseActorList" runat="server" OnSelectedIndexChanged="drpUseCaseActorList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUseCaseActorId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynUseCaseActorId" runat="server" />
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
