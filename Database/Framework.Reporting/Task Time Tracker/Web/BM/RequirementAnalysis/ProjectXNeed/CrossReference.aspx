<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CrossReference.aspx.cs" Inherits="ApplicationContainer.UI.Web.ProjectXNeed.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
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
                                <asp:ListItem Value="ByProject">Project To Need</asp:ListItem>
                                <asp:ListItem Value="ByNeed">Need To Project</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynProject" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                Project:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpProject" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpProject_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfNeed" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynNeed" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                Need:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpNeed" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpNeed_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfProject" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
