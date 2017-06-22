<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Publisher.aspx.cs"  MasterPageFile="~/MasterPages/Default.master" Inherits="ApplicationContainer.UI.Web.EventNotification.Publisher" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
 <asp:Table CellSpacing="0" CellPadding="0" runat="server" ID="tblMain" CssClass="searchfilter">
   <asp:TableRow>          
                
        <asp:TableCell HorizontalAlign="Left" ColumnSpan="0">  
            <asp:Button runat="server" ID="btnPublish" Text="Publish" OnClick="btnPublish_Click"/>
            <asp:TextBox ID="txtDate" runat="server" Visible="false"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
          
</asp:Content>
