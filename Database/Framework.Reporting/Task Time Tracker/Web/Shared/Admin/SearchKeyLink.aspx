<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="SearchKeyLink.aspx.cs" Inherits="Shared.UI.Web.Admin.SearchKeyLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Searcg Key Link
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="font-weight: bold; color: Black" width="600" border="0">
        <tr>
            <td>Saved Search Link: 
            </td>
            <td>
                <asp:HyperLink ID="hypSearchLink" runat="server"></asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
