<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" EnableEventValidation="false"
    MasterPageFile="~/MasterPages/Site.master" Inherits="Shared.UI.Web.Configuration.Menu.Update" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register TagPrefix="gnrc" TagName="GenericTool" Src="~/Shared/Configuration/Menu/Controls/Generic.ascx" %>
<%@ Reference Control="Controls/Details.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    <table cellpadding="0"  style="font-weight: bold; color: Black" border="0">
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
