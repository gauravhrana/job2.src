<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.ActivityStream.Default" %>

<%@ Register TagPrefix="as" TagName="ActivityStream" Src="~/Shared/Controls/ActivityStream.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr valign="top">
            <td>
                <as:ActivityStream runat="server" ID="activityStream4" />
            </td>
            <td>
                <as:ActivityStream runat="server" ID="activityStream2" />
            </td>
        </tr>
    </table>
</asp:Content>
