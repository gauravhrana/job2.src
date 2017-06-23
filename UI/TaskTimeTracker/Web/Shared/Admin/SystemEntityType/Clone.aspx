<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Clone.aspx.cs" Inherits="Shared.UI.Web.Admin.SystemEntityType.Clone" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Shared/Admin/SystemEntityType/Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
      
    </asp:Content>   
    <asp:Content ID="cloneContent" runat="server" ContentPlaceHolderID="MainContent">
        <br /><br />
        <asp:Label Text="Please enter the SystemEntityTypeId" runat="server" />

        <table style="font-weight:bold;color:Black"  width="400" border="0">   
             <tr>
                <td>
                    <gnrc:GenericControl ID="myGenericControl" runat="server" />
                </td>
             </tr>
            <tr align="right">
                <td>
                    <asp:LinkButton ID="lnkClone" Text="Clone" runat="server" OnClick="lnkClone_Click" />
                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" />
                </td>
            </tr>
       </table>
    </asp:Content>




    


