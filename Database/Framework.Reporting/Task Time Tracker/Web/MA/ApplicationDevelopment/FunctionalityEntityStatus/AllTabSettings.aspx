<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="AllTabSettings.aspx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.AllTabSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    FunctionalityEntityStatus : All Tab Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table >
        <tr>
            <td>
                <table width="95%" cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td width="450" class="ralabel">
                            Category :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUserPreferenceCategory" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpUserPreferenceCategory_SelectedIndexChanged">
                                <asp:ListItem>FES-DETAILS-ALL</asp:ListItem>
                                <asp:ListItem>FES-UPDATE-ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            All Tab Selected :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAllTabSelected" runat="server">
                                <asp:ListItem>true</asp:ListItem>
                                <asp:ListItem>false</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Paging Enabled :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPaging" runat="server">
                                <asp:ListItem>true</asp:ListItem>
                                <asp:ListItem>false</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            AEFL Mode Enabled :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAEFLMode" runat="server">
                                <asp:ListItem>true</asp:ListItem>
                                <asp:ListItem>false</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Button Panel Enabled :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpButtonPanel" runat="server">
                                <asp:ListItem>true</asp:ListItem>
                                <asp:ListItem>false</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />&nbsp;
                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
