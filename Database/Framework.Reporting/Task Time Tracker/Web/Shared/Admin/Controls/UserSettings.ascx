<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.UserSettings" %>
<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>


<style>
    label
    {
        margin-left: 5px;
        vertical-align: middle;
    }
</style>
<table border="0">
    <tr>
        <td colspan="2" align="left"><b>User Settings</b></td>
    </tr>
    <tr>
        <td colspan="2">
            <br />
        </td>
    </tr>
    <tr>
        <td align="center">
            <label class="col-sm-2 control-label">User Preference Key:</label>
        <td align="left">
            <asp:DropDownList ID="ddlUserPreferenceKey" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserPreferenceKey_SelectedIndexChanged" Visible ="true"></asp:DropDownList>
        </td>

    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSubmit_OnClick" Text="Search" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <label class="col-sm-2 control-label">Categories:</label>
    </tr>
    <tr>
        <td colspan="2">
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div style="width: 500px; height: 500px; overflow-y: scroll;">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataField ="UserPreferenceCategory" Visible="true">
                </asp:CheckBoxList>
            </div>
        </td>
    </tr>
    <tr>
        <td></td>
        <td align="left">
            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                CausesValidation="true" />
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_OnClick" />
        </td>
    </tr>
</table>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               