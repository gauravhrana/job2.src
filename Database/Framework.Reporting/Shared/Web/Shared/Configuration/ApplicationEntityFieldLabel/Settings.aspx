<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Settings" %>

<%@ Register TagPrefix="ec" TagName="eSettingsList" Src="~/Shared/Controls/eSettingsList.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <link herf="<%= Page.ResolveUrl("~")%>Styles/ExportMenu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/jquery.fixedMenu.js"></script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    ApplicationEntityFieldLabel Settings
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight: bold; color: Black"  class="maintable"  border="0">
      
       <tr>

            <td>
                <ec:eSettingsList ID="eSettingsList" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnInsert" Text="Back" OnClick="btnBack_Click" runat="server" />
                <asp:LinkButton ID="btnHome" Text="Home" OnClick="btnHome_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
