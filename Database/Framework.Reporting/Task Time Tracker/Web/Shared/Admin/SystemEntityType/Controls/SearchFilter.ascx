﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.Admin.SystemEntityType.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<div class="searchFilterHeaderContainer">

    <asp:Panel ID="pnlHeader" runat="server">
        <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
    </asp:Panel>

    <div id="Div1" class="searchFilterHeaderContainer" runat="server">

        <div id="tabs" style="border-color: greenyellow; border-width: 2px;">

            <ul runat="server" id="divTabHeaderList" />

            <div id="divTabContentContainer" runat="server">

                <div id="divSearchParam" class="form-horizontal">

                    <asp:Repeater ID="SearchParametersRepeater" runat="server" OnItemDataBound="SearchParametersRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div class="form-group">
                                <div runat="server" id="containerRow">
                                    <div class="col-sm-10">
                                        <asp:PlaceHolder ID="plcHoverLinkLabel" runat="server" />
                                        <asp:PlaceHolder ID="plcControlHolder" runat="server" />
                                    </div>
                                    <div class="col-sm-2" style="background: blue;">
                                        <asp:TextBox ID="txtDevBox" runat="server" />
                                        <asp:HiddenField ID="hdnfield" Value='<%# Eval("Name") %>' runat="server" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>

                <div class="form-group">
                    <div class="col-sm-10">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-10">
                            <asp:Button runat="server" ID="Button1" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" />
                            <asp:Button runat="server" ID="Button2" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-default" />
                        </div>
                    </div>
                    <div class="col-sm-2">
                    </div>
                </div>

            </div>

        </div>

    </div>


</div>

<%--<ajaxToolkit:CollapsiblePanelExtender ID="cpExtender" runat="Server" 
	TargetControlID="pnlCollapsibleContent"
    TextLabelID="lblPanelStatus" 
	ImageControlID="Image1" 
	ExpandedText="Hide Details"
    CollapsedText="Show Details" ExpandedImage="~/Content/images/collapse_blue.jpg" CollapsedImage="~/Content/images/expand_blue.jpg"
    SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />--%>

<asp:HiddenField ID="hidtab" Value="0" runat="server" />

<script>
    $(document).ready(function () {
        SetupTabs("<%=hidtab.ClientID%>");
    });
</script>
