
<%@ Page Language="C#" Title="Home Page" AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
    MasterPageFile="~/MasterPages/DayCare/Site.master" 
    Inherits="ApplicationContainer.UI.Web.BM.DayCare.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"/>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

	<%--
		<div id="ProductSample"></div>
		<script type="text/jsx" src="/react_components/ProductList.jsx"></script>
	--%>

	<div id="example"></div>
    <script type="text/jsx" src="/react_components/helloworld.jsx"></script>

	<div id="example2"></div>
    <script type="text/jsx" src="/react_components/HelloWorld2.jsx"></script>

	<div id="example3"></div>
    <script type="text/jsx" src="/react_components/HelloWorld3.jsx"></script>

</asp:Content>
