<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InlineUpdate.aspx.cs" MasterPageFile="~/MasterPages/Site.Master"
    Inherits="Shared.UI.Web.SystemIntegrity.QuickPaginationRun.InlineUpdate" %>

<%@ Register TagPrefix="el" TagName="eList" Src="~/Shared/Controls/eList.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label ID="Label1" Text="" runat="server" />
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <el:eList ID="InlineEditingList" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <%--<asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
