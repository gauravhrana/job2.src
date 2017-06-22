<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Clone.aspx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLog.Clone" %>

<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Shared/ApplicationManagement/ReleaseNote/ReleaseLog/Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label ID="Label1" Text="Please enter the ReleaseLogId" runat="server" />
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
