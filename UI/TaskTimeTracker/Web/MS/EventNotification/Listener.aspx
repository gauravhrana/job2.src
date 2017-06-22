<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listener.aspx.cs" MasterPageFile="~/MasterPages/Default.master" Inherits="ApplicationContainer.UI.Web.EventNotification.Listener" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
  <asp:Table CellSpacing="0" CellPadding="0" runat="server" ID="tblMain" CssClass="searchfilter">
           
                <asp:TableRow> 
               
        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2"> 
         
            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"/>
            </asp:TableCell>
             <asp:TableCell HorizontalAlign="Right" ColumnSpan="2"> 
            <asp:Label ID="lblStatus" runat="server"></asp:Label>

        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
</asp:Content>

          

