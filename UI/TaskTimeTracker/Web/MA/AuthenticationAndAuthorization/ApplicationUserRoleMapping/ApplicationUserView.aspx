<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationUserView.aspx.cs"
MasterPageFile="~/MasterPages/Site.master" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserRoleMapping.ApplicationUserView" %>

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
                                <asp:ListItem Value="ByApplicationUser">ApplicationUser To ApplicationRole</asp:ListItem>
                                <asp:ListItem Value="ByApplicationRoles">ApplicationRole To ApplicationUser</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplicationUser" runat="server">
                    <table border="1" width="400">
                        <tr>
                            <td width="150" valign="top">
                                Application
                            </td>
                            <td width="*">
                                <asp:DropDownList Width="155" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpApplication_OnSelectedIndexChanged"
                                    ID="drpApplication">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" valign="top">
                                ApplicationUser
                            </td>
                            <td width="*">
                                <asp:DropDownList Width="155" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpApplicationUser_OnSelectedIndexChanged"
                                    ID="drpApplicationUser">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="1" width="300">
                        <tr>
                            <td>
                                Available ApplicationRoles:
                            </td>
                            <td width="*">
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSourceApplicationRole"></asp:ListBox>
                            </td>
                            <td width="50">
                                <asp:Button runat="server" Text="-->" ID="btnLeftApplicationRole" OnClick="btnLeftApplicationRole_Click" />
                                <asp:Button runat="server" Text="<--" ID="btnRightApplicationRole" OnClick="btnRightApplicationRole_Click" />
                            </td>
                            <td>
                                Current Assigned ApplicationRoles:
                            </td>
                            <td width="*">
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTargetApplicationRole"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="right">
                                <asp:Button runat="server" Text="Save" ID="btnSaveApplicationRoles" OnClick="btnSaveApplicationRole_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplicationRole" runat="server" Visible="false">
                    <table border="1" width="400">
                        <tr>
                            <td width="150" valign="top">
                                ApplicationRole
                            </td>
                            <td width="*">
                                <asp:DropDownList Width="155" AutoPostBack="true" OnSelectedIndexChanged="drpApplicationRole_OnSelectedIndexChanged"
                                    runat="server" ID="drpApplicationRole">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="1" width="300">
                        <tr>
                            <td>
                                All ApplicationUsers:
                            </td>
                            <td width="*">
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSourceApplicationUser"></asp:ListBox>
                            </td>
                            <td width="50">
                                <asp:Button runat="server" Text="-->" ID="btnLeftApplicationUser" OnClick="btnLeftApplicationUser_Click" />
                                <asp:Button runat="server" Text="<--" ID="btnRightApplicationUser" OnClick="btnRightApplicationUser_Click" />
                            </td>
                            <td>
                                Current ApplicationUser Holding the ApplicationRoles:
                            </td>
                            <td width="*">
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTargetApplicationUser"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="right">
                                <asp:Button runat="server" Text="Save" ID="btnSaveApplicationUser" OnClick="btnSaveApplicationUser_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCheckApplicationUser" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>



