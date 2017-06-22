<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FESSummaryChart.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls.FESSummaryChart" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
<asp:Table runat="server" ID="tblMain" CssClass="searchfilter">
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Functionality:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionFunctionality" runat="server" AppendDataBoundItems="true"
                OnSelectedIndexChanged="drpSearchConditionFunctionality_SelectedIndexChanged" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionFunctionality" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Group By:</asp:TableCell><asp:TableCell>
            <asp:DropDownList ID="ddlGroupBy" runat="server" AppendDataBoundItems="true"
                OnSelectedIndexChanged="ddlGroupBy_SelectedIndexChanged" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionGroupBy" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">SubGroup By:</asp:TableCell><asp:TableCell>
            <asp:DropDownList ID="ddlSubGroupBy" runat="server" AppendDataBoundItems="true"
                OnSelectedIndexChanged="ddlSubGroupBy_SelectedIndexChanged" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionSubGroupBy" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Date:</asp:TableCell>
        <asp:TableCell Style="width: 300px;">
            <span>
                <asp:TextBox runat="server" ID="txtSearchConditionToDate1" Columns="15" />
                &nbsp;
                <asp:TextBox runat="server" ID="txtSearchConditionToDate2" Columns="15" />
                &nbsp; </span>

        </asp:TableCell>
        <asp:TableCell>
            <asp:Label ID="lblToDateFormat" runat="server" Text="dd-MM-yy"></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>



        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<asp:LinkButton ID="lnkGridSummary" runat="server" OnClick="lnkGridSummary_Click" Text="Show Pie Chart" />
<table>
    <tr>
        <td colspan="2">
            <asp:PlaceHolder ID="dynChart" runat="server">
                <table>

                    <tr>
                        <td>
                            <asp:Chart ID="Chart1" ImageStorageMode="UseImageLocation" ImageLocation="~/MA/ApplicationDevelopment/FunctionalityEntityStatus/ChartPic_#SEQ(300,3)"
                                runat="server" OnLoad="Chart1_Load" Height="400px" Width="600px">
                                <ChartAreas>
                                    <asp:ChartArea
                                        Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend></asp:Legend>
                                </Legends>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
        </td>
    </tr>
</table>



