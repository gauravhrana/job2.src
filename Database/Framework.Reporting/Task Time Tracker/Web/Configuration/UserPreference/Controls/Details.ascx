<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.Configuration.UserPreference.Controls.Details" %>
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
        <tr >
            <td class="ralabel">
               <asp:Label ID="lblUserPreferenceIdText" runat="server">UserPreferenceId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserPreferenceId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynUserPreferenceId" runat="server" />
            </td>
        </tr>       
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblApplicationUserIdText" runat="server">ApplicationUserId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationUserId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr >
            <td class="ralabel">
                <asp:Label ID="lblUserPreferenceCategoryText" runat="server">UserPreferenceCategory :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserPreferenceCategory" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblDataTypeIdText" runat="server">DataTypeId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDataTypeId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblUserPreferenceKeyIdText" runat="server">UserPreferenceKeyId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUserPreferenceKeyId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblValueText" runat="server">Value :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValue" runat="server"></asp:Label>
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
                                <asp:Label ID="lblHistory" runat="server" Text="" Visible="false"><b>Record History</b></asp:Label>
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
