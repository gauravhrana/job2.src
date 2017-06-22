﻿<%@ Page Title="FunctionalityXFunctionalityActiveStatus - Details" Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site.master" EnableEventValidation="false" CodeBehind="Details.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.Details" %>


<%@ Register Src="~/Shared/Controls/ControlDetails.ascx" TagName="DetailsControl" TagPrefix="dc" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   <a href="Default.aspx">FunctionalityXFunctionalityActiveStatus </a> : Details
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <table >
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
               <dc:DetailsControl ID="oDetailsControl" EntityName="FunctionalityXFunctionalityActiveStatus" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" runat="server" />
                <asp:LinkButton ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" />
                <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnBack_Click" runat="server" />
                <asp:LinkButton ID="btnClone" runat="server" Text="Clone" OnClick="btnClone_Click" />
            </td>
        </tr>
    </table>
</asp:Content>