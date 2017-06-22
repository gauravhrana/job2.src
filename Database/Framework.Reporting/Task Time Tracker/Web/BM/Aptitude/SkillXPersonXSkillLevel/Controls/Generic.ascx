<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblSkillXPersonXSkillLevelId" Text="Skill X Person X Skill Level Id:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSkillXPersonXSkillLevelId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSkillXPersonXSkillLevelId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Skill Id:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSkillList" runat="server" OnSelectedIndexChanged="drpSkillList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSkillId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSkillId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Person Id:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPersonList" runat="server" OnSelectedIndexChanged="drpPersonList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPersonId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPersonId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Skill Level Id:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSkillLevelList" runat="server" OnSelectedIndexChanged="drpSkillLevelList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSkillLevelId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSkillLevelId" runat="server" />
                        </td>
                    </tr>
                    <tr id="trUpdatedDate" runat="server" visible="false">
                        <td class="ralabel">
                            Updated Date:
                        </td>
                        <td>
                            <asp:Label ID="lblUpdatedDate" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trUpdatedBy" runat="server" visible="false">
                        <td class="ralabel">
                            Updated By:
                        </td>
                        <td>
                            <asp:Label ID="lblUpdatedBy" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trLastAction" runat="server" visible="false">
                        <td class="ralabel">
                            Last Action:
                        </td>
                        <td>
                            <asp:Label ID="lblLastAction" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
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
