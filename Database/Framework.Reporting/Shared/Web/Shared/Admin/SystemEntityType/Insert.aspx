<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master"  CodeBehind="Insert.aspx.cs" Inherits="Shared.UI.Web.Admin.SystemEntityType.Insert" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Shared/Admin/SystemEntityType/Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
      
</asp:Content>

    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

        <table style="font-weight:bold;color:Black" width="400" border="0">
            <tr>
                <td>
                    <gnrc:GenericControl ID="myGenericControl" runat="server" />
                   <%-- <asp:PlaceHolder ID="PlaceHolderUserControl" runat="server"></asp:PlaceHolder>
               --%> </td>
           </tr>
           <tr>
                <td align="right">
                    <asp:LinkButton ID="btnInsert" runat="server" Text="Insert" onclick="btnInsert_Click" />
                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" /> 
                </td>
           </tr>
       </table>
  </asp:Content>
