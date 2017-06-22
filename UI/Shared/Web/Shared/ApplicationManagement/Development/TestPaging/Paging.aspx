<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Paging.aspx.cs" Inherits="Shared.UI.Web.TestPaging.Paging" %>
<%@ Register assembly="Shared.UI.Web" namespace="Shared.UI.Web.TestPaging.Class" tagPrefix="Paging"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <Paging:PagingGridView ID="PagingGridView1" runat="server" PageSize="5" 
    AllowPaging="true" OnPageIndexChanging="PagingGridView1_PageIndexChanging"  Width="400"></Paging:PagingGridView>
</asp:Content>