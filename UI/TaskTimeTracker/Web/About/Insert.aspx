<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Insert.aspx.cs" Inherits="ApplicationContainer.UI.Web.About.Insert" %>
<%@ Register TagName="GenericControl" TagPrefix="uc" Src="~/About/Controls/Generic.ascx" %>
<%@ Reference Control="./Controls/Generic.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Release Log : Insert
</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">

        <table style="font-weight:bold;color:Black"  width="400" border="0">   
            <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">                 
                    <tr>
                        <td colspan="2" align="center" class="style3">Release Log : Insert</td>
                    </tr>
                    <tr>
                        <td class="style2"></td>
                        <td class="style1">
                          <uc:GenericControl id="myGenericControl" runat="server"  />  
                                                                                  
                        </td>
                    </tr>
                </table>
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


