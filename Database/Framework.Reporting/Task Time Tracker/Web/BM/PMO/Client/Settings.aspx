<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ApplicationContainer.UI.Web.Client.Settings" %>
<%@ Register TagPrefix="ec" TagName="eSettingsList" Src="~/Shared/Controls/eSettingsList.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">      
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    ApplicationOperation Settings
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="font-weight: bold; color: Black" class="maintable" border="0">      
       <tr>
            <td>
                <ec:eSettingsList ID="eSettingsList" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnInsert" Text="Back" OnClick="btnBack_Click" runat="server" CssClass="btn" />
                <asp:LinkButton ID="btnHome" Text="Home" OnClick="btnHome_Click" runat="server" CssClass="btn"/>
            </td>
        </tr>
    </table>
</asp:Content>
