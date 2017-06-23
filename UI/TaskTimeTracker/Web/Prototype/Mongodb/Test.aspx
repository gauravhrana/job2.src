<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Prototype/Default.master" CodeBehind="Test.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.Test" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>

<asp:Content ID="ContentControlItem" ContentPlaceHolderID="SearchControlItem" runat="server">
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel">
                 <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            </asp:TableCell>
           
        </asp:TableRow>
        
        
          
    </asp:Table>
</asp:Content>

        
   