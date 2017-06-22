﻿<%@ Page Title="Clone" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master"
    Inherits="Shared.UI.Web.ApplicationManagement.ReleasePublishCategory.Clone" %>

<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Shared/ApplicationManagement/ReleasePublishCategory/Controls/Generic.ascx" %>
<%@ MasterType VirtualPath="~/MasterPages/Site.master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label ID="Label1" Text="Please enter the ReleasePublishCategoryId" runat="server" />
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <gnrc:GenericControl ID="myGenericControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>