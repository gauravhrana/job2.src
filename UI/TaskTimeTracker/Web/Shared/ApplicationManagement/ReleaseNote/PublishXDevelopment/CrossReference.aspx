<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="CrossReference.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.PublishXDevelopment.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="SearchControlItem" runat="server">
    <div style="width: 855px; height: 300px; padding-left: 25px;">
        <table border="1" width="400">
            <tr>
                <td width="150">
                    Direction:
                </td>
                <td>
                    <asp:DropDownList Width="155" ID="drpSelection" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                        <asp:ListItem Value="ByParentReleaseLogDetail">Parent To Child</asp:ListItem>
                        <asp:ListItem Value="ByChildReleaseLogDetail">Child To Parent</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:PlaceHolder ID="dynParentReleaseLogDetail" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td width="150">
                                   Parent Items:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpParentReleaseLogDetail" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpParentReleaseLogDetail_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:Bucket ID="BucketOfChildReleaseLogDetail" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="dynChildReleaseLogDetail" runat="server">
                        <table>
                            <tr>
                                <td width="150">
                                    Child Items:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpChildReleaseLogDetail" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpChildReleaseLogDetail_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:Bucket ID="BucketOfParentReleaseLogDetail" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
