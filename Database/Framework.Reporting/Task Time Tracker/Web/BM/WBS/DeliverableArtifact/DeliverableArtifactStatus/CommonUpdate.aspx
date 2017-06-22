﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonUpdate.aspx.cs" MasterPageFile="~/MasterPages/Site.master" Inherits="ApplicationContainer.UI.Web.DeliverableArtifactStatus.CommonUpdate" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register TagPrefix="dyn" TagName="DynamicUpdate" Src="~/Shared/Controls/DynamicUpdate.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">    
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    <table >
       
        <tr>
            <td>  
                <dyn:DynamicUpdate ID="DynamicUpdatePanel" runat="server" />               
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

