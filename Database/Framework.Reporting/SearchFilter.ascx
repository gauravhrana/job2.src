<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs"
    Inherits="Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<asp:Table CellSpacing="0" CellPadding="0" runat="server" ID="tblMain" CssClass="searchfilter">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
            <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TaskEntity:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionTaskEntity" runat="server" AppendDataBoundItems="true"
                AutoPostBack="false" OnSelectedIndexChanged="drpSearchConditionTaskEntity_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
             </asp:TableCell>
            <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTaskEntity"/> 
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">TaskScheduleId:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionTaskSchedule" runat="server" AppendDataBoundItems="true"
                AutoPostBack="false" OnSelectedIndexChanged="drpSearchConditionTaskSchedule_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionTaskSchedule"/>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">RunBy:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionRunBy" />
        </asp:TableCell>
    </asp:TableRow>
     <asp:TableRow>  
        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2"> 
             <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click"/>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>