<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Settings.aspx.cs" Inherits="ApplicationContainer.UI.Web.UseCase.Settings" %>

<%@ Register TagPrefix="ec" TagName="eSettingsList" Src="~/Shared/Controls/eSettingsList.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
    <script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/w2jquery.fixedMenu.js"></script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    UseCase Settings
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="font-weight: bold; color: Black"  class="maintable"  border="0">
      
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
