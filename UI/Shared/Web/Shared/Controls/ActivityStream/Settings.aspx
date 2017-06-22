<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs"
    Inherits="Shared.UI.Web.ActivityStream.Settings" %>

<%@ Register Src="Controls/Generic.ascx" TagName="Generic" TagPrefix="uc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="/Styles/Tabs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Settings:
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight: bold; color: Black" width="95%" border="0">
        <tr>
            <td>
                <div style="overflow: auto; height: auto;">                    
                    <uc1:Generic ID="oGeneric" runat="server" />
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
