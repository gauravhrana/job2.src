<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CrossReference.aspx.cs" Inherits="ApplicationContainer.UI.Web.Feature.FeatureGroupXFeature.CrossReference" %>

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
                                <asp:ListItem Value="ByFeatureGroup">FeatureGroup To Feature</asp:ListItem>
                                <asp:ListItem Value="ByFeature">Feature To FeatureGroup</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynFeatureGroup" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                FeatureGroup:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFeatureGroup" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpFeatureGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfFeature" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynFeature" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                Feature:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFeature" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpFeature_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfFeatureGroup" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>



