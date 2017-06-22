<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.SystemForeignRelationship.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblSystemForeignRelationshipId" Text="SystemForeignRelationshipId:"
                                 runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemForeignRelationshipId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemForeignRelationshipId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Primary Database:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPrimaryDatabase" runat="server" OnSelectedIndexChanged="drpPrimaryDatabaseList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPrimaryDatabase" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryDatabase" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            PrimaryEntity:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPrimaryEntityList" runat="server" OnSelectedIndexChanged="drpPrimaryEntityList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPrimaryEntityId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryEntityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Foreign Database:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpForeignDatabase" runat="server" OnSelectedIndexChanged="drpForeignDatabaseList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtForeignDatabase" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynForeignDatabase" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            ForeignEntity:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpForeignEntity" runat="server" OnSelectedIndexChanged="drpForeignEntityList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtForeignEntityId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynForeignEntityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            FieldName:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFieldName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFieldName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Source:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSource" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSource" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SystemForeignRelationshipType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemForeignRelationshipType" runat="server"
                                OnSelectedIndexChanged="drpSystemForeignRelationshipTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSystemForeignRelationshipType" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemForeignRelationshipType" runat="server" />
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
