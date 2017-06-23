<%@ Page Title="Less Css Example" Language="C#" MasterPageFile="~/MasterPages/SA/Site.Master" 
    AutoEventWireup="true" CodeBehind="LessCss.aspx.cs" Inherits="Shared.UI.Web.Admin.LessCss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/styles/LessExample.less" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    LESS CSS PROTOTYPE
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div id="header" class="header">This is a Less Exampel Header DIV</div>

    <div id="footer" class="footer">This is a Less Exampel footer DIV</div>
</asp:Content>
