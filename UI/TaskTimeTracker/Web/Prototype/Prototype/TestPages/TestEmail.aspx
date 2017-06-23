<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="TestEmail.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestPages.TestEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                Email Adress:
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" 
                    onclick="btnSendEmail_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
