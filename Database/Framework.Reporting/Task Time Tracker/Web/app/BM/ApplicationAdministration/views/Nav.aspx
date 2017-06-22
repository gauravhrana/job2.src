<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Nav.aspx.cs" 
    Inherits="ApplicationContainer.Core.UI.Web.ApplicationAdministration.Nav" %>

<form id="form1" runat="server" >

    <div class="row" style="background-color: #4b6c9e;">
	    <nav id="nav" role="navigation">
			<div class="col-sm-12">
				<asp:Menu ID="NavigationMenu" runat="server"
					CssClass="app-main-menu"
					EnableViewState="false"
					IncludeStyleBlock="false" Orientation="Horizontal"
					RenderingMode="List">

					<StaticMenuItemStyle CssClass="stdMenuItem" />
					<DynamicMenuItemStyle CssClass="stdMenuItem" />

					<StaticSelectedStyle CssClass="MenuDefaultSelectedStyle" />
					<DynamicSelectedStyle CssClass="MenuDefaultSelectedStyle" />

					<StaticHoverStyle CssClass="MenuDefaultHoverStyle" />
					<DynamicHoverStyle CssClass="MenuDefaultHoverStyle" />

				</asp:Menu>
			</div>
		</nav>
    </div>

</form>

