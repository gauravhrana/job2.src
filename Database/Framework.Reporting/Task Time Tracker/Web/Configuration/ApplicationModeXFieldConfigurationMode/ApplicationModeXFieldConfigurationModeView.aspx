<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Site.master" EnableEventValidation="false"
CodeBehind="ApplicationModeXFieldConfigurationModeView.aspx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationModeXFieldConfigurationMode.ApplicationModeXFieldConfigurationModeView" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>              
        <tr>
            <td>
                <asp:PlaceHolder ID="dynApplicationMode" runat="server" Visible="true">
                    <table>
                        <tr>
                            <td width="150">
                                ApplicationMode:
                            </td>
                            <td>
                            <asp:DropDownList ID="drpApplicationMode" Width="250" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpApplicationMode_SelectedIndexChanged">
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfApplicationMode" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>

