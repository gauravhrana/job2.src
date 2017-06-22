<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CrossReference.aspx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeCategoryXFCMode.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>              
        <tr>
            <td>
                <asp:PlaceHolder ID="dynFieldConfigurationModeCategory" runat="server" Visible="true">
                    <table>
                        <tr>
                            <td width="150">
                                FieldConfigurationModeCategory:
                            </td>
                            <td>
                            <asp:DropDownList ID="drpFieldConfigurationModeCategory" Width="250" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFieldConfigurationModeCategory_SelectedIndexChanged">
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfFieldConfiguration" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
