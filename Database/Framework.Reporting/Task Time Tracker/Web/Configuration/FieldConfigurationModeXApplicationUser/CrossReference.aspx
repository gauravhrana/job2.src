<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CrossReference.aspx.cs"
    Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationUser.CrossReference" %>

<%@ Register src="~/Shared/Controls/BucketFor3.ascx" tagname="BucketFor3" tagprefix="uc1" %>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td width="150">FieldConfigurationMode:
            </td>
            <td>
                <asp:DropDownList ID="drpFieldConfigurationMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFieldConfigurationMode_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">                
                <uc1:BucketFor3 ID="fcModeBucket" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
