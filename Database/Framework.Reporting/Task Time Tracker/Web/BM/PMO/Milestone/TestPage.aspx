<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="TestPage.aspx.cs" Inherits="ApplicationContainer.UI.Web.Milestone.TestPage" %>


<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table align="right">
        
        <tr>
            <td>
                
                <asp:PlaceHolder ID="plcSubMenuList" runat="server"></asp:PlaceHolder>
                
            </td>
        </tr>
        
    </table>
</asp:Content>

