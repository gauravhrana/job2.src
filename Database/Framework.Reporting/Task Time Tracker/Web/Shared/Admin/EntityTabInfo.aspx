<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntityTabInfo.aspx.cs" MasterPageFile="~/MasterPages/Site.master" Inherits="Shared.UI.Web.Admin.EntityTabInfo" %>
<%@ Import namespace="System.Data" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <b>Entity Tab Info</b>
    <br /><br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<asp:Repeater id="TabResults" 
  OnItemDataBound="TabResults_ItemDataBound" 
  runat="server">
    <headerTemplate>
      <table border="1" width="400px" cellpadding="0" cellspacing="0">
      <tr>
      <td style=" width:40%;"><b>Entity</b></td>
      <td style=" width:60%;"><b>Tabs Associated</b></td>
      </tr>
    </headerTemplate>
    <ItemTemplate>
      <tr>
        <td valign="top" style=" width:40%;">
           <asp:Label ID="lblParentStructureId" Text='<%# Eval("TabParentStructureId")%>' Visible="false" runat="server" >
            </asp:Label>
          <asp:Label ID="lblParentTab" Text='<%# Eval("Name")%>' runat="server" >
            </asp:Label>
        </td>
        <td style=" width:60%;">
          
         <%-- <%# Eval("Name")%>
          <%# Eval("Description")%>--%>
             <asp:Repeater id="ChildTabResults" runat="server">
                <headerTemplate>
                     <table border="0" cellpadding="0" cellspacing="0">
                </headerTemplate>
                <ItemTemplate>
                  <tr>
                    <td>
                    <asp:Label ID="lblChildTab" Text='<%# Eval("EntityName")%>' runat="server" >
            </asp:Label>
                    </td>
                  </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                      <td colspan="3" height="10"></td>
                    </tr>
                   </table>
                </FooterTemplate>
             </asp:Repeater>
             </td>
             </tr>
             </ItemTemplate>
             <FooterTemplate></table></FooterTemplate>
             </asp:Repeater>
</asp:Content>
