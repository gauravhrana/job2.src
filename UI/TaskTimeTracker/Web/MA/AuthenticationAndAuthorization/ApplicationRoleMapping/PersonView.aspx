<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="PersonView.aspx.cs"
    Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRoleMapping.PersonView" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <table border="1" width="450">
                    <tr>
                        <td width="150">
                            Direction:
                        </td>
                        <td>
                            <asp:DropDownList Width="250" ID="drpSelection" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                                <asp:ListItem Value="ByApplicationUser">ApplicationUser To ApplicationRole</asp:ListItem>
                                <asp:ListItem Value="ByApplicationRole">ApplicationRole To ApplicationUser</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplicationUser" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                ApplicationUser:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpApplicationUser" Width="250" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpApplicationUser_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfApplicationRole" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplicationRole" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                ApplicationRole:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpApplicationRole" Width="250" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpApplicationRole_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfApplicationUser" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
