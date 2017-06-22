<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilterControl.ascx.cs" Inherits="ApplicationContainer.UI.Web.BaseUI.SearchFilterControl" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar" TagPrefix="ucSearchActionBar" %>

<div class="table-bordered" style="border-color: lightblue; background: grey;">

	<ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />

	<div id="Div1" class="searchFilterHeaderContainer show" runat="server">

		<!--<asp:Panel ID="pnlCollapsibleContent" runat="server" >-->

		<div id="tabs" style="border-color: greenyellow; border-width: 2px;">

			<ul runat="server" id="divTabHeaderList" />

			<div id="divTabContentContainer" runat="server" class="k-content">

				<div id="divSearchParam" class="form-horizontal">

					<asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
						<ItemTemplate>
							<div runat="server" id="containerRow">
								<div class="form-group">
									<asp:PlaceHolder ID="plcHoverLinkLabel" runat="server" />
									<asp:PlaceHolder ID="plcControlHolder" runat="server" />
									<asp:TextBox ID="txtDevBox" runat="server" />
									<asp:HiddenField ID="hdnfield" Value='<%# Eval("Name") %>' runat="server" />
								</div>
							</div>
						</ItemTemplate>
					</asp:Repeater>

					<div class="form-group">
						<div class="col-sm-2">
						</div>
						<div class="col-sm-10">
							<asp:Button runat="server" ID="Button1" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" />
							<asp:Button runat="server" ID="Button2" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-default" />
						</div>
					</div>

				</div>

			</div>

		</div>

		<!--</asp:Panel>-->

	</div>

	<%--
	<ajaxToolkit:CollapsiblePanelExtender ID="cpExtender" runat="Server" 
		TargetControlID="pnlCollapsibleContent"
		TextLabelID="lblPanelStatus" 
		ImageControlID="Image1" 
		ExpandedText="Hide Details"
		CollapsedText="Show Details" 
		ExpandedImage="~/content/images/collapse_blue.jpg" 
		CollapsedImage="~/content/images/expand_blue.jpg"
		SuppressPostBack="true" />
	--%>

	<asp:HiddenField ID="hidtab" Value="0" runat="server" />

	<script>
		$(document).ready(function () {
			SetupTabs("<%=hidtab.ClientID%>");
		});
	</script>
	
	<div class="row col-sm-12">
        <br />
    </div>

</div>