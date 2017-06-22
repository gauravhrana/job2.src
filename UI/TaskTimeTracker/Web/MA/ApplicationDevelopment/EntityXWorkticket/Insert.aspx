<%@ Page Title="EntityXWorkTicket - Insert" Language="C#" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" 
MasterPageFile="~/MasterPages/Site.master" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket.Insert" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="./Controls/Generic.ascx"%>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    <a href="Default.aspx">EntityXWorkTicket </a> : Insert
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <table style="font-weight:bold;color:Black" width="400" border="0">
        <tr>
            <td>                   
                    <gnrc:GenericControl ID="myGenericControl" runat="server" />
                
            </td>
        </tr>
        <tr>
            <td align="right">
                
                <asp:LinkButton ID="btnInsert" runat="server" Text="Insert" onclick="btnInsert_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" /> 
            </td>                
        </tr>
    </table>
</asp:Content>
