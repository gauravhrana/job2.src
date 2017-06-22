<%@ Page Title="Default" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.Demo.Default" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/List/List.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Layer/Controls/SearchFilter.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Demo
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
            <tr>                        
                <td>
                    <sr:SearchFilter ID="oSearchFilter" runat="server" />                    
                </td>
            </tr>
            <tr>
                <td align="right">         
                    <asp:HyperLink ID="lnkExport" Target="_blank" Text="PopUp" runat="server" /> 
                </td>
            </tr>
            <tr>                        
                <td>            
                    <dc:List ID="oList" runat="server" />                    
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:LinkButton ID="btnInsert" Text="Insert" OnClick="btnInsert_Click" runat="server" />
                    <asp:LinkButton ID="btnHome" Text="Home" OnClick="btnHome_Click" runat="server" /> 
                </td>
            </tr>
    </table>              
</asp:Content>
    