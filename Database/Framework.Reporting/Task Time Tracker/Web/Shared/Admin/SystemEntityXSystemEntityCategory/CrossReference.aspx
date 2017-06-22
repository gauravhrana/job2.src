<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CrossReference.aspx.cs" Inherits="Shared.UI.Web.Admin.SystemEntityXSystemEntityCategory.CrossReference" %>

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
                                <asp:ListItem Value="BySystemEntity">SystemEntity To SystemEntityCategory</asp:ListItem>
                                <asp:ListItem Value="BySystemEntityCategory">SystemEntityCategory To SystemEntity</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynSystemEntity" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                SystemEntity:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSystemEntity" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSystemEntity_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfSystemEntityCategory" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynSystemEntityCategory" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                SystemEntityCategory:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSystemEntityCategory" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSystemEntityCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfSystemEntity" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>



