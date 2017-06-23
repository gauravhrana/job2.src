<%@ Page Title="List Control Settings" Language="C#" MasterPageFile="~/MasterPages/Site.master"
    AutoEventWireup="true" CodeBehind="ListSettings.aspx.cs" Inherits="Shared.UI.Web.Admin.ListSettings" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="/Styles/Carousel.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <b>
        <asp:Label ID="lblEntityName" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp; Settings </b>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="plcSettings" runat="server"></asp:PlaceHolder>
</asp:Content>
