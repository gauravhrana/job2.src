﻿<%@ Page Title="Insert" Language="C#" MasterPageFile="~/MasterPages/Schedule/Site.master" AutoEventWireup="true"
    CodeBehind="Insert.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.Insert" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="./Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
    <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
       
    </asp:Content>

    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

        <table style="font-weight:bold;color:Black" border="0">
            <tr>
                <td>
                    <gnrc:GenericControl ID="mygenericControl" runat="server" />
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