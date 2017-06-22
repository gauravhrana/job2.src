<%@ Page Title="Update" MasterPageFile="~/MasterPages/Site.master" EnableEventValidation="false"
    Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Update" %>

<%@ Register TagPrefix="gnrc" TagName="GenericTool" Src="~/Shared/Configuration/FieldConfigurationModeXApplicationRole/Controls/Generic.ascx" %>
<%@ Reference Control="./Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    <table >
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: auto;">
                    <asp:PlaceHolder ID="plcUpdateList" runat="server"></asp:PlaceHolder>
                </div>
                <%--<gnrc:GenericTool ID="myGenericControl" runat="server" />--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:LinkButton ID="btnClone" runat="server" Text="Clone" OnClick="btnClone_Click" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
