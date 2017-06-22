ï»¿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs" Inherits="Shared.UI.Web.HelpPage.Controls.SearchFilter" %>
<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
<asp:Table  runat="server" ID="tblMain" CssClass="searchfilter">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
            <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
        </asp:TableCell>
    </asp:TableRow>
   
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">SystemEntityType:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionSystemEntityType" runat="server" AppendDataBoundItems="true" 
             OnSelectedIndexChanged="drpSearchConditionSystemEntityType_SelectedIndexChanged" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>   
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">HelpPageContext:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="drpSearchConditionHelpPageContext" runat="server" AppendDataBoundItems="true" 
             OnSelectedIndexChanged="drpSearchConditionHelpPageContext_SelectedIndexChanged" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Name:</asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" ID="txtSearchConditionName" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>    
        <asp:TableCell></asp:TableCell>                    
        <asp:TableCell Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">    
            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />               
            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click"/>
        </asp:TableCell>
    </asp:TableRow>




</asp:Table>
























