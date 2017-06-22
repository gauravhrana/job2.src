<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CrossReference.aspx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperationRoleMapping.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <table border="1" width="500">
                    <tr>
                        <td width="150">
                            Direction:
                        </td>
                        <td>
                            <asp:DropDownList Width="300" ID="drpSelection" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                                <asp:ListItem Value="ByApplicationOperation">ApplicationOperation To ApplicationRole</asp:ListItem>
                                <asp:ListItem Value="ByApplicationRole">ApplicationRole To ApplicationOperation</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplicationOperation" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                Application Operation:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpApplicationOperation" Width="300" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpApplicationOperation_SelectedIndexChanged">
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
                                Application Role:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpApplicationRole" runat="server" Width="300" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpApplicationRole_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfApplicationOperation" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>

