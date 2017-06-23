<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="CheckDefaultView.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.CheckDefaultView" %>

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

               <asp:DropDownList ID="ddlApplication" runat="server" ></asp:DropDownList>

            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> View: </asp:TableCell>
            <asp:TableCell>

               <asp:DropDownList ID="ddlView" runat="server" >
                     <asp:ListItem Value="Developer" Selected="True">Default</asp:ListItem>                   
                     <asp:ListItem Value="Standard">Standard</asp:ListItem>                   
                     <asp:ListItem Value="InlineUpdateColumns">Inline</asp:ListItem>                   
                     <asp:ListItem Value="CommonEditable">Common</asp:ListItem>                   
               </asp:DropDownList>

            </asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell CssClass="ralabel"> Group By: </asp:TableCell><asp:TableCell>
                <asp:DropDownList runat="server" ID="drpGroupBy"
                    AppendDataBoundItems="true">                    
                    <asp:ListItem Value="HasView">HasView</asp:ListItem>                   
                    

                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow> <asp:TableRow>
            <asp:TableCell> </asp:TableCell><asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </asp:TableCell></asp:TableRow></asp:Table>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ListControlItem" runat="server">

    <table>
        <tr>
            <td colspan="3">
                <div id="tabs">
                 <ul>
                    <li><a href="#tabs-1"><asp:Label ID="lblView" runat="server"></asp:Label></a></li>
                    <li><a href="#tabs-2"><asp:Label ID="lblWithView" runat="server"></asp:Label></a></li>
                </ul>

                 <div id="tabs-1">
                    <asp:CheckBoxList ID="chkListData" runat="server"></asp:CheckBoxList>
                </div>

                <div id="tabs-2">
                    <asp:CheckBoxList ID="chkListDataWithview" runat="server"></asp:CheckBoxList>
                </div> 
                </div> 
            </td>
        </tr>
    </table>
    <table >    
         <tr>
            <td Style="padding-right: 155px;" HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" ID="btnDefault" Text="Create Default View" OnClick="btnDefault_Click" />           
                 <asp:Button runat="server" ID="btnStandard" Text="Create Standard View" OnClick="btnStandard_Click" />           
            </td>
        </tr>
    </table>
     <script>

         $(function () {

             $("#tabs").tabs();

         });

  </script>


</asp:Content>
