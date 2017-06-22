<%@ Page Title="Clone" Language="C#" MasterPageFile="~/MasterPages/Site.Master" 
AutoEventWireup="true" CodeBehind="CommonUpdate.aspx.cs" 
Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityStatus.CommonUpdate" EnableEventValidation="false" %>

<%@ Register TagName="DynamicUpdate" TagPrefix="dyn" Src="~/Shared/Controls/DynamicUpdate.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName"></asp:Content>

<asp:Content ID="UpdateContent" runat="server" ContentPlaceHolderID="MainContent">

<div class="row">
	<div class="col-sm-12">    
		<div style="overflow: auto; height: auto;">
			 <dyn:DynamicUpdate ID="DynamicUpdatePanel" runat="server" />
		</div>
	</div>
	<div class="col-sm-12"> 
		<asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
		 <asp:LinkButton ID="btnBack" runat="server" Text="Return" OnClick="btnBack_Click" />
	</div>
</div>

</asp:Content>
