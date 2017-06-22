<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="FunctionalityView.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.FunctionalityView" %>

<%@ Register TagPrefix="uc2" TagName="ImageControl" Src="~/Shared/Controls/FunctionalityImageControl/FunctionalityImageControl.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:ImageControl ID="ImgControl" runat="server" />
</asp:Content>

