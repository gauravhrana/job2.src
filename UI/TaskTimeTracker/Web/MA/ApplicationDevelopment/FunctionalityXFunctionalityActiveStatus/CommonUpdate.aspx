﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CommonUpdate.aspx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.CommonUpdate" %>

<%@ Register TagPrefix="dyn" TagName="DynamicUpdate" Src="~/Shared/Controls/DynamicUpdate.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    <a href="Default.aspx"> FunctionalityXFunctionalityActiveStatus</a> : Common Update
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    <table >
       
        <tr>
            <td>  
                <dyn:DynamicUpdate ID="DynamicUpdatePanel" runat="server" />
                <%--<gnrc:SelectionUpdate ID="SelectionUpdatePanel" runat="server" />--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                
                <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:LinkButton ID="btnBack" runat="server" Text="Return" OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr><td>
            
        </td></tr>
    </table>
</asp:Content>
