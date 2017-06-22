<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="Settings.aspx.cs" Inherits="TaskTimeTracker.UI.Web.Settings" %>

<%@ Register TagPrefix="eg" TagName="eSettingsGrid" Src="~/Shared/Controls/eSettingsGrid.ascx" %>
<%@ Register TagPrefix="er" TagName="eSettingsRepeater" Src="~/Shared/Controls/eSettingsRepeater.ascx" %>
<%@ Register TagPrefix="gnrc" TagName="GenericList" Src="~/Shared/Configuration/FieldConfiguration/Controls/Generic.ascx" %>
<%@ Register TagPrefix="dc" TagName="DetailsView" Src="~/Shared/Configuration/FieldConfiguration/Controls/Details.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
<b><asp:Label ID="lblEntityName" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp; Settings
  </b><br /><br />

  <asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">Search</asp:TableCell>

    </asp:TableRow>
    <asp:TableRow ID="ApplicationRow" runat="server">
        <asp:TableCell CssClass="ralabel">Application:</asp:TableCell>
        <asp:TableCell>
            <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack="true"
        onselectedindexchanged="ddlApplication_SelectedIndexChanged"></asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Entity:</asp:TableCell>
        <asp:TableCell>
          <asp:DropDownList ID="ddlSystemEntityType" runat="server" AutoPostBack="true"
        onselectedindexchanged="ddlSystemEntityType_SelectedIndexChanged"></asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
     <asp:TableRow>
        <asp:TableCell CssClass="ralabel">Mode:</asp:TableCell>
        <asp:TableCell>
         <asp:DropDownList ID="ddlAEFLMode" runat="server" AutoPostBack="true"
        onselectedindexchanged="ddlAEFLMode_SelectedIndexChanged"></asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
            <asp:Button ID="btnGetColumns" runat="server" OnClick="btnGetColumns_Click" Text="Get Columns" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>


   
  
  

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight: bold; color: Black"  class="maintable"  border="0">
        <tr>
    <td>
    <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal" 
    OnSelectedIndexChanged="rbtnList_SelectedIndexChanged" AutoPostBack="true">
    <asp:ListItem Text="Table View" Value="GridView" ></asp:ListItem>
    <asp:ListItem Text="Panel View" Value="Repeater" Selected="True" ></asp:ListItem>
    </asp:RadioButtonList>    </td>
    </tr>
       <tr>

            <td>
            <asp:LinkButton ID="lnkbtnAddRow" Text="Add Row" OnClick="lnkbtnAddRow_Click" runat="server" />
            <br />
           <eg:eSettingsGrid ID="eSettingsGrid" runat="server" />
           <er:eSettingsRepeater ID="eSettingsRepeater" runat="server" />
            </td>
        </tr>
        <tr>
        <td>
        <asp:Panel ID="AddRowPanel" runat="server" Visible="false">
            <gnrc:GenericList ID="genericList" runat="server"  />
            <asp:LinkButton ID="lnkbtnAdd" Text="Save" OnClick="lnkbtnAdd_Click" runat="server" />
            <asp:LinkButton ID="lnkbtnCancel" Text="Cancel" OnClick="lnkbtnCancel_Click" runat="server" />
            </asp:Panel>
        </td>
        </tr>
        <tr>
        <td>
        
        </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnReturn" Text="Return" OnClick="btnReturn_Click" style=" padding-right:10px;" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>