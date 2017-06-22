<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportMenu.ascx.cs" Inherits="Shared.UI.Web.Controls.ExportMenu" %>

<link rel="Stylesheet" href="<%= Page.ResolveUrl("~")%>Styles/ExportMenu.css" />

<script type="text/javascript" src="/Scripts/jquery.fixedMenu.js"></script>


<a name="exportLink" href="#exportLink" runat="server" id="RLink">[R]</a>
<asp:LinkButton ID="imgSettings" runat="server" ToolTip="Settings" CssClass="button">[S]</asp:LinkButton>
&nbsp;&nbsp;

<div style="visibility: hidden;">
	<asp:Menu ID="ExportParentMenu" OnMenuItemClick="ExportParentMenu_MenuItemClick"
		runat="server" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal"
		Visible="False"
		>
	</asp:Menu>

	<script type="text/javascript" language="javascript">
		$('document').ready(function () {
			$('.exportmenu').fixedMenu();
		});
	</script>

</div>


