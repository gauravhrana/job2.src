﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="InlineUpdate.aspx.cs" Inherits="ApplicationContainer.UI.Web.TasksAndWorkflow.TaskSchedule.InlineUpdate" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register TagPrefix="el" TagName="eList" Src="~/Shared/Controls/eList.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <br /><br />
        <asp:Label ID="Label1" Text="" runat="server"/>

        <table style="font-weight:bold;color:Black"  width="400" border="0">   
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

