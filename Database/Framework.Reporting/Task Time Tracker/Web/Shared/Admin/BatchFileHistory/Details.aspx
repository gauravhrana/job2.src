<%@ Page Title="Details" MasterPageFile="~/MasterPages/Site.master" Language="C#" 
    AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Shared.UI.Web.BatchFileHistory.Details" %>
<%@ Register TagName="DetailsControl" TagPrefix="uc" Src="~/Shared/Admin/BatchFileHistory/Controls/Details.ascx" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">

        <table style="font-weight:bold;color:Black"  width="400" border="0">   
            <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">                 
                    <tr>
                        <td colspan="2" align="center" class="style3">BatchFileHistory Details</td>
                    </tr>
                    <tr>
                        <td class="style2"></td>
                        <td class="style1">
                            <asp:PlaceHolder ID="plcDetailsList" runat="server"></asp:PlaceHolder>
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

