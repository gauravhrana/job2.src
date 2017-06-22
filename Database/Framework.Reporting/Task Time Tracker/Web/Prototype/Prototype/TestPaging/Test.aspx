<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeBehind="Test.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestPaging.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchControlItem" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">
    <asp:Menu ID="NavigationMenu" StaticDisplayLevels="2" StaticSubMenuIndent="10" Orientation="Vertical"
        OnMenuItemClick="NavigationMenu_MenuItemClick" runat="server">
        <Items>
            <asp:MenuItem Text="Home" ToolTip="Home">
                <asp:MenuItem Text="Music" ToolTip="Music">
                    <asp:MenuItem Text="Classical" ToolTip="Classical" />
                    <asp:MenuItem Text="Rock" ToolTip="Rock" />
                    <asp:MenuItem Text="Jazz" ToolTip="Jazz" />
                </asp:MenuItem>
                <asp:MenuItem Text="Movies" ToolTip="Movies">
                    <asp:MenuItem Text="Action" ToolTip="Action" />
                    <asp:MenuItem Text="Drama" ToolTip="Drama" />
                    <asp:MenuItem Text="Musical" ToolTip="Musical" />
                </asp:MenuItem>
            </asp:MenuItem>
        </Items>
    </asp:Menu>
    <asp:GridView ID="dgvTest" runat="server" AutoGenerateColumns="false" AllowPaging="true"
        OnPageIndexChanging="dgvTest_PageIndexChanging" OnRowDataBound="dgvTest_RowDataBound"
        OnDataBound="dgvTest_DataBound" OnRowCommand="dgvTest_RowCommand">
        <Columns>
            <asp:BoundField DataField="MenuId" HeaderText="MenuId" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="PrimaryDeveloper" HeaderText="PrimaryDeveloper" />
            <asp:BoundField DataField="ParentMenu" HeaderText="ParentMenu" />
        </Columns>
        <PagerStyle HorizontalAlign="Right"  />
        <%--<PagerTemplate>
            <asp:Button ID="Button1" runat="server" Text="First" CommandName="First" Height="35"
                ForeColor="SaddleBrown" />
            <asp:Button ID="Button2" runat="server" Text="Next" CommandName="Next" Height="35"
                ForeColor="SaddleBrown" /></PagerTemplate>--%>
    </asp:GridView>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ActionContent" runat="server">
</asp:Content>
