﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InlineUpdate.aspx.cs" MasterPageFile="~/MasterPages/Site.master"
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage.InlineUpdate" %>

<%@ Register TagPrefix="el" TagName="eList" Src="~/Shared/Controls/eList.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label ID="Label1" Text="" runat="server" />
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <el:eList ID="InlineEditingList" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
        </tr>
    </table>
</asp:Content>
