<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" MasterPageFile="~/MasterPages/Site.master" Inherits="ApplicationContainer.UI.Web.Aptitude.Competency.Settings" %>

<%@ Register TagPrefix="ec" TagName="eSettingsList" Src="~/Shared/Controls/eSettingsList.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Competency Settings
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
