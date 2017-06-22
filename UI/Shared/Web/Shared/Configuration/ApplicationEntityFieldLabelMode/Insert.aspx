<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode.Insert" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Shared/Configuration/ApplicationEntityFieldLabelMode/Controls/Generic.ascx"%>

    <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
        <a href="Default.aspx">ApplicationEntityFieldLabelMode </a> : Insert
    </asp:Content>

    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

         <table cellpadding="5" style="font-weight:bold;color:"Black" width="400" border="0">
            <tr>
                <td>
                    <gnrc:GenericControl ID="myGenericControl" runat="server" />
                </td>
           </tr>
           <tr>
                <td align="right">
                    <asp:LinkButton ID="btnInsert" runat="server" Text="Insert" onclick="btnInsert_Click" />
                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" /> 
                </td>
           </tr>
       </table>
  </asp:Content>
