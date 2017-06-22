<%@ Page Title="EntityXWorkTicket - Update" Language="C#" AutoEventWireup="true"
    CodeBehind="Update.aspx.cs" EnableEventValidation="false" MasterPageFile="~/MasterPages/Site.master"
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket.Update" %>

<%@ Register TagPrefix="gnrc" TagName="GenericTool" Src="./Controls/Generic.ascx" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
  <a href="Default.aspx">EntityXWorkTicket </a> : Update
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
