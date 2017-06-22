<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="UserLoginHistory.aspx.cs"
    Inherits="Shared.UI.Web.Configuration.UserLoginHistory" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<%@ Register TagName="DateRangeControl" TagPrefix="dr" Src="~/Shared/Controls/DateRange.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchControlItem" runat="server">
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Name: </asp:TableCell>
            <asp:TableCell>
                <div>
                    <asp:TextBox runat="server" ID="txtSearchConditionName" />
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearchConditionName"
                        MinimumPrefixLength="1" BehaviorID="AutoCompleteEx" CompletionSetCount="1" ServicePath="~/API/AutoComplete.asmx"
                        UseContextKey="True" CompletionInterval="1" ServiceMethod="GetUserNames">
                    </ajaxToolkit:AutoCompleteExtender>
                </div>
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell ColumnSpan="2" Width="325px">
                <dr:DateRangeControl ID="oDateRange" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </asp:TableCell></asp:TableRow></asp:Table></asp:Content><asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">
    <table >
        <tr>
            <td width="150px">
                <asp:Label ID="Label1" runat="server" Text="Email Resuls To: "></asp:Label></td><td width="150px">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td><td>
                <asp:Button 
                    ID="btnSendEmail" runat="server" Text="Send" onclick="btnSendEmail_Click" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="LoginHistoryGrid" runat="server" >
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
