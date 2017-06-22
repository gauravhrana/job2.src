<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Test.aspx.cs" Inherits="Shared.UI.Web.MenuDemo.Test" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Test Menu
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />

<div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                    </asp:Menu>
                    </div>
</asp:Content>
