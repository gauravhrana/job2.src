<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="InlineUpdate.aspx.cs" Inherits="ApplicationContainer.UI.Web.Feature.RunTimeFeature.InlineUpdate" %>

<%@ Register TagPrefix="el" TagName="eList" Src="~/Shared/Controls/eList.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:content id="Content1" runat="server" contentplaceholderid="SectionName">
    
</asp:content>
<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">
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
    </asp:content>
