<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="ApplicationSettings.aspx.cs" Inherits="Shared.UI.Web.Admin.ApplicationSettings" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td width="250px">
                            Field Configuration Mode Category:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFCModeCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFCModeCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <uc1:Bucket ID="BucketOfControlsCategory" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
