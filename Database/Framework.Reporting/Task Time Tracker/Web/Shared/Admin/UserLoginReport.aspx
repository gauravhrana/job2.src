<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="UserLoginReport.aspx.cs" Inherits="Shared.UI.Web.Admin.UserLoginReport" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <b>User Login Report</b>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table class="searchfilter" width="600" border="0">
<tr>
<td>
UserName:
</td>
<td>
<asp:DropDownList ID="drpUserName" runat="server" AppendDataBoundItems="true" 
          AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
            </td>
            </tr>
           
            <tr>
        <td class="ralabel">
            MinDate:
        </td>
        <td>
            <span>
                <asp:TextBox runat="server" ID="txtSearchConditionMinDate" />
            </span>&nbsp;<span> Format:
                <asp:Label ID="lblMinDateFormat" runat="server" Text="DD-MM-YY"></asp:Label></span>
        </td>
    </tr>
    <tr>
        <td class="ralabel">
            MaxDate:
        </td>
        <td>
            <span>
                <asp:TextBox runat="server" ID="txtSearchConditionMaxDate" /></span> &nbsp;<span> Format:
                    <asp:Label ID="lblMaxDateFormat" runat="server" Text="DD-MM-YY"></asp:Label></span>
        </td>
    </tr>
    <tr>
    <td> <asp:Button runat="server" ID="btnSearch" Text="Submit" OnClick="btnSearch_Click" /></td>
    </tr>
    </table>
            <asp:GridView ID="LoginReport" runat="server"></asp:GridView>
</asp:Content>
