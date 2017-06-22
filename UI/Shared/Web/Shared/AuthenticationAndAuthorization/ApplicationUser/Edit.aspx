<%@ Page Title="Edit" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" 
    CodeBehind="Edit.aspx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Edit" %>
    <%@ Register TagName="eList" TagPrefix="dc" Src="~/Shared/AuthenticationAndAuthorization/ApplicationUser/Controls/MultipleEditGrid.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    ApplicationUser : Edit
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td colspan="2" align="center" class="style3">
                            ApplicationUser Details Editable List
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                        </td>
                        <td class="style1">
                           <dc:eList ID="eListApplicationUser" runat="server"></dc:eList>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                
                <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnBack_Click" runat="server" />
                
            </td>
        </tr>
    </table>


</asp:Content>
