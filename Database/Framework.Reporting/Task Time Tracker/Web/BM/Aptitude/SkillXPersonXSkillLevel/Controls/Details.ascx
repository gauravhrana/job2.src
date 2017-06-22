<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %> 
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
                <asp:Label ID="lblSkillXPersonXSkillLevelIdText" runat="server" >SkillXPersonXSkillLevelId:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSkillXPersonXSkillLevelId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynSkillXPersonXSkillLevelId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblSkillText" runat="server">Skill:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSkill" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblPersonText" runat="server">Person:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPerson" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel"> 
                <asp:Label ID="lblSkillLevelText" runat="server">SkillLevel:</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSkillLevel" runat="server"></asp:Label>
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
