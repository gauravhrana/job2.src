<%@ Page Title="Clone" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="Clone.aspx.cs" Inherits="ApplicationContainer.UI.Web.UseCaseWorkFlowCategory.Clone" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="./Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <br /><br />
        <asp:Label ID="Label1" Text="Please enter the UseCaseWorkFlowCategoryId" runat="server"/>

        <table style="font-weight:bold;color:Black"  width="400" border="0">   
             <tr>
                <td>
                    <gnrc:GenericControl ID="myGenericControl" runat="server" />
                </td>
             </tr>
            <tr>
                <td align="right">
                    <asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" />
                </td>
            </tr>
       </table>
    </asp:Content>
   