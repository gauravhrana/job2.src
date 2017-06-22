<%@ Page Title="Clone" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="Clone.aspx.cs" Inherits="Shared.UI.Web.Configuration.SystemForeignRelationshipType.Clone" %>

<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Configuration/SystemForeignRelationshipType/Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label ID="Label1" Text="Please enter the SystemForeignRelationshipTypeId" runat="server" />
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"
                    Style="color: Red;"></asp:Label>
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
