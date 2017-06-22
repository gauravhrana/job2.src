<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Default.aspx.cs" Inherits="ApplicationContainer.UI.Web.About.Default" %>
<%@ Register TagName="DetailsControl" TagPrefix="uc" Src="~/About/Controls/Details.ascx" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Release Log : Details
</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">

        <table style="font-weight:bold;color:Black"  width="400" border="0">   
            <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">                 
                    <tr>
                        <td colspan="2" align="center" class="style3">Release Log : Details</td>
                    </tr>
                    <tr>
                        <td class="style2"></td>
                        <td class="style1">
                          <uc:DetailsControl id="myDetailsControl" runat="server"  />  
                                                                                  
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
        <tr>
            <td align="right">
            <asp:LinkButton ID="btnInsert" runat="server" Text="Insert New Log" onclick="btnInsert_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

