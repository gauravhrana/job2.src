<%@ Page Title="Default" Language="C#" Trace="false" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Default" %>

<%@ Register Src="~/Shared/Controls/List.ascx" TagName="DefaultWindowControl" TagPrefix="dc" %>
<%@ Register Src="~/Shared/Configuration/ApplicationEntityFieldLabel/Controls/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="sr" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <link herf="<%= Page.ResolveUrl("~")%>Styles/ExportMenu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~")%>Scripts/jquery.fixedMenu.js"></script>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    ApplicationEntityFieldLabel
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight: bold; color: Black"  class="maintable"  border="0">
        <tr>
            <td>
                <sr:SearchFilter ID="oSearchFilter" runat="server" />
            </td>
        </tr>
      <tr>
            <td>
                <dc:DefaultWindowControl ID="oDefaultWindowsControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="lnkInsert" Text="Insert" OnClick="btnInsert_Click" runat="server" />
                <asp:LinkButton ID="lnkHome" Text="Home" OnClick="btnHome_Click" runat="server" />
            </td>
        </tr>
    </table>    
</asp:Content>
