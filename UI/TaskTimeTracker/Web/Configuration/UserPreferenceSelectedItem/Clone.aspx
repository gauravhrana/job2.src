<%@ Page Title="Clone" MasterPageFile="~/MasterPages/Site.master" Language="C#" AutoEventWireup="true"
    CodeBehind="Clone.aspx.cs" Inherits="Shared.UI.Web.Configuration.UserPreferenceSelectedItem.Clone" %>

<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Configuration/UserPreferenceSelectedItem/Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="Clone" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label Text="Please enter the UserPreferenceSelectedItemId" runat="server" />
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <gnrc:GenericControl ID="myGenericControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
