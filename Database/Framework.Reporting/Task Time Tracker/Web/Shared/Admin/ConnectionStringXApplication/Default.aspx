<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="Shared.UI.Web.Admin.ConnectionStringXApplication.Default" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <table border="1" width="400">
                    <tr>
                        <td width="150">
                            Direction:
                        </td>
                        <td>
                            <asp:DropDownList Width="155" ID="drpSelection" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                                <asp:ListItem Value="ByConnectionString">ConnectionString To Application</asp:ListItem>
                                <asp:ListItem Value="ByApplication">Application To ConnectionString</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynConnectionString" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                ConnectionString:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpConnectionString" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpConnectionString_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfApplication" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplication" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                Application:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpApplication" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpApplication_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfConnectionString" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>

