<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="DetailTab.ascx.cs" Inherits="Shared.UI.Web.Controls.DetailTabControl" %>

<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/DetailTab.js")%>"></script>

<asp:HiddenField ID="hidtab" Value="0" runat="server" />
<asp:HiddenField ID="hidTabValue" Value="" runat="server" />
<asp:HiddenField ID="hdnTabOrientation" runat="server" />
<asp:HiddenField ID="hdnAccordionIndex" Value="0" runat="server" />

<div runat="server" id="divTabContainer" style="background-color: transparent; border: 2px;" class="table-bordered">
	<ul class="tabs" runat="server" id="divTabHeaderList" style="background-color: transparent;"/>
    <div id="divTabContentContainer" runat="server" style="border-color: black; border-width: 1px; background-color: transparent; " class="panel" >
	</div>
</div>

<script>

	$(document).ready(function () {

		var tabOrientation = $("#<%=hdnTabOrientation.ClientID%>").val();
    	var divTabContentContainerId = "<%=divTabContentContainer.ClientID%>";

    	var divTabContainerId = "<%=divTabContainer.ClientID%>";
    	var hidTabId = "<%=hidtab.ClientID%>";
    	var hidTabValueId = "<%=hidTabValue.ClientID%>";
    	var hdnAccordionIndexId = "<%=hdnAccordionIndex.ClientID%>";

    	SetupDetailTabs(tabOrientation, divTabContentContainerId, divTabContainerId, hidTabId, hidTabValueId, hdnAccordionIndexId);

	});

</script>
