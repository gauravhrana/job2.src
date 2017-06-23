<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupList.ascx.cs" Inherits="Shared.UI.Web.Controls.GroupList" %>

<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>

<div class="table-bordered" style="border-color: lightblue;">

	<table class="" style="border-color: black; border-width: 1px; background-color: lightslategray; width: 100%;">
		<tr>
			<td style="width: 375px;">
				&nbsp;
				<asp:LinkButton ID="lnkBtnToggleSearchControl" runat="server" OnClick="lnkBtnToggleSearchControl_Click">[F]</asp:LinkButton>
				&nbsp;
				<input placeholder="Search"/>
				<div class="btn btn-link">Go</div>
			</td>
			<td align="right" class="pull-right">View :&nbsp;
				<asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
				</asp:DropDownList>
				<asp:HiddenField ID="hdnFieldConfigurationModeCategory" runat="server" />
				&nbsp;
				<dc:ExportMenu ID="myExportMenu" runat="server" />
			</td>
			<!--
			<div style="float: right; display: none;">
				<div id="divFontSizeContainer" runat="server">Font Size:
				<asp:LinkButton ID="LinkButton1" runat="server" Style="font-size: 12px; color: Blue; font-weight: bold;"
					OnClick="lnkfontsmall_Click">A</asp:LinkButton>
					<asp:LinkButton ID="LinkButton2" runat="server" Style="font-size: 14px; color: Blue; font-weight: bold;"
						OnClick="lnkfontmedium_Click">A</asp:LinkButton>
					<asp:LinkButton ID="LinkButton3" runat="server" Style="font-size: 16px; color: Blue; font-weight: bold;"
						OnClick="lnkfontlarger_Click">A</asp:LinkButton>
				</div>
			</div>
			-->
		</tr>
	</table>

	<asp:Panel ID="pnlGroupListContainer" runat="server" />

	<%--
	<asp:Panel runat="server">
		OnlyBindActiveTab : <asp:TextBox runat="server" ID="txtOnlyBindActiveTab" Text="False"></asp:TextBox>
	</asp:Panel>
	--%>

</div>
