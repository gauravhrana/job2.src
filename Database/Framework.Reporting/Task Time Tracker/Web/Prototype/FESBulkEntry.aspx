<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="FESBulkEntry.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.FESBulkEntry" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>
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
            <asp:TableCell CssClass="ralabel"> Application: </asp:TableCell>
            <asp:TableCell>

               <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" ></asp:DropDownList>

            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Functionality: </asp:TableCell>
            <asp:TableCell>

               <asp:DropDownList ID="ddlFunctionality" runat="server"></asp:DropDownList>

            </asp:TableCell>
        </asp:TableRow>
        
      
                 <asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnCreate" Text="Create FES entries" OnClick="btnCreate_Click" />
            </asp:TableCell></asp:TableRow></asp:Table>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ListControlItem" runat="server">
    <table>
        <tr>
            <td colspan="3">
            </td>
        </tr>
    </table>
    <table >
     
       <%-- <tr>
            <td colspan="3">
               <div style="width: 500px; height: 500px; overflow-y: scroll;">
                    <asp:CheckBoxList ID="chkList"  RepeatColumns="2"  RepeatDirection="Vertical" RepeatLayout="Flow" runat="server">
                    </asp:CheckBoxList>
                </div>
                
            </td>
        </tr>--%>
         <tr>
            <td colspan="2">
              
            </td>
        </tr>
    </table>
</asp:Content>
