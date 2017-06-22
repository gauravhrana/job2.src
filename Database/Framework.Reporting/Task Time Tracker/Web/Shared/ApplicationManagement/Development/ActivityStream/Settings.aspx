<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.Development.ActivityStream.Settings" ValidateRequest="false" %>

<%@ Register Src="~/Shared/ApplicationManagement/Development/ActivityStream/Controls/Generic.ascx"
    TagName="Generic" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                <div style="overflow: auto; height: auto;">
                    <uc1:generic id="oGeneric" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
