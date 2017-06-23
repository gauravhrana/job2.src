<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" MasterPageFile="~/MasterPages/Site.master" Inherits="ApplicationContainer.UI.Web.Prototype.KendoTextEditor.Default2" %>
<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
    
</asp:Content>
<asp:Content ID="UpdateContent" ContentPlaceHolderID="MainContent" runat="server">
   <uc1:KendoEditor runat="server" id="KendoEditor" />
</asp:Content>
