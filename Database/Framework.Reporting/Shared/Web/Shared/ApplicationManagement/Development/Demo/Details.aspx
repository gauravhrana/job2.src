<%@ Page Title="Details" MasterPageFile="~/MasterPages/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.Demo.Details" %>
<%@ Register TagName="DetailsControl" TagPrefix="uc2" Src="~/Shared/Controls/Demo/Details.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Demo : Details
</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">

        <table cellpadding="5" style="font-weight:bold;color:Black"  width="400" border="0">   
            <tr>
            <td>
                <table width="100%" cellpadding="2" cellspacing="4" border="0">                 
                    <tr>
                        <td colspan="2" align="center" class="style3">Demo Details</td>
                    </tr>
                    <tr>
                        <td class="style2"></td>
                        <td class="style1">                                                        
                            <uc2:DetailsControl runat="server" ID="xyz" BackGroundColor="Orange"/>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnBack_Click" runat="server" />
                <asp:LinkButton ID="btnClone" runat="server" Text="Clone" onclick="btnClone_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

