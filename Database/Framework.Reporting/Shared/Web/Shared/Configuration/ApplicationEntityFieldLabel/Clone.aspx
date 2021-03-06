﻿<%@ Page Title="Clone" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Clone.aspx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Clone" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Shared/Configuration/ApplicationEntityFieldLabel/Controls/Generic.ascx"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
        <a href="Default.aspx">ApplicationEntityFieldLabel </a> : Clone
</asp:Content>

<asp:Content ID="Clone" runat="server" ContentPlaceHolderID="MainContent">
    <br /><br />
    <asp:Label ID="Label1" Text="Please enter the ApplicationEntityFieldLabelId" runat="server" />

    <table cellpadding="5" style="font-weight:bold; color:Black"  width="400" border="0">
        <tr>
            <td>
                <gnrc:GenericControl ID="myGenericControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" />
            </td>
        </tr> 
    </table>    
            
</asp:Content>
