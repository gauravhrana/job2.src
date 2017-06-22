<%@ Page Title="Details" EnableEventValidation="false" MasterPageFile="~/MasterPages/Site.master"
    Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Shared.UI.Web.Configuration.UserPreference.Details" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register Src="~/Shared/Controls/ControlDetails.ascx" TagName="DetailsControl" TagPrefix="dc" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <table >
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
               <dc:DetailsControl ID="oDetailsControl" EntityName="UserPreference" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" runat="server" />
                <asp:LinkButton ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" />
                <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnBack_Click" runat="server" />
                <asp:LinkButton ID="btnClone" runat="server" Text="Clone" OnClick="btnClone_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
