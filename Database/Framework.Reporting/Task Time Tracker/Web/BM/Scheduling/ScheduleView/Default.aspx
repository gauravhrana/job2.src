<%@ Page Title="Default" Language="C#" MasterPageFile="~/MasterPages/Schedule/Default.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleView.Default" %>

<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/ScheduleNewSearchControl.ascx" %>
<%@ Register TagPrefix="sc" TagName="StatChart" Src="./Controls/ScheduleStatChart.ascx" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>
<%--  --%>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <asp:Panel ID="dynGridContainer" runat="server" />
    <%--<dc:GroupList ID="oGroupList" runat="server" />--%>
    <div id="Div1">
        <asp:PlaceHolder ID="plcGroupByHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="plcTabHolder" runat="server"></asp:PlaceHolder>

        <div style="background: lightblue; height: 50px;">
            <div class="exportmenuContainer" style="background: lightblue; float: left;">
                <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <table border="1">
            <tr>
                <td align="left">
                    <asp:Label ID="lblSearchStatus" Text="" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                    <div id="maindiv" runat="server">

                        <asp:Repeater ID="GrdParentGrid" runat="server" OnItemDataBound="GrdParentGrid_RowDataBound">

                            <ItemTemplate>

                                <div id="itemdiv" runat="server">

                                    <table>
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <a href="Details/<%# DataBinder.Eval(Container.DataItem,"PersonId")%>"><%# DataBinder.Eval(Container.DataItem,"Person")%></a>
                                                    <asp:HiddenField ID="hdnScheduleId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PersonId")%>' />
                                                </b>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnInsert" runat="server" Text="Insert" PostBackUrl="~/Schedule/Insert/" />

                                            </td>
                                            <td align="right">
                                                <span>
                                                    <asp:LinkButton ID='chkbox' runat="server" AutoPostBack="true" Text='[X]' CommandName='<%# Eval("PersonId") %>' OnClick="chkbox_Click" ForeColor="GrayText" />
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                    <dt>
                                        <div id="detailsGridContainer" runat="server"></div>
                                    </dt>
                                    <%--<dt>
                                        <dc:List ID="oList" runat="server" />
                                        <div id="divStat" class="divReleaseNotes" runat="server">
                                            <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                            <asp:Label ID="lblCount" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblAverage" Text="Average:" runat="server"></asp:Label>
                                            <asp:Label ID="lblAverageValue" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblMedian" Text="Median:" runat="server"></asp:Label>
                                            <asp:Label ID="lblMedianValue" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblRecordCountText" Text="Count:" runat="server"></asp:Label>
                                            <asp:Label ID="lblRecordCount" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblMaxText" Text="Max:" runat="server"></asp:Label>
                                            <asp:Label ID="lblMax" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblMinText" Text="Min:" runat="server"></asp:Label>
                                            <asp:Label ID="lblMin" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                        </div>
                                    </dt>--%>
                                </div>
                                <ajaxToolkit:RoundedCornersExtender ID="Round1" runat="server" TargetControlID="itemdiv" Radius="6" Corners="All" BorderColor="Gray" />
                                <br />
                            </ItemTemplate>

                        </asp:Repeater>
                    </div>
                </td>
            </tr>
        </table>

        <asp:PlaceHolder ID="TableReportContent" runat="server"></asp:PlaceHolder>
        
		<asp:PlaceHolder ID="dynChart" runat="server">
            <table>
                <tr>
                    <td>
                        <sc:StatChart ID="oSC" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>

    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>

