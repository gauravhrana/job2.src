<%@ Page Title="Clone" MasterPageFile="~/MasterPages/Schedule/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Clone.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleItem.Clone" %>
<%@ Register TagPrefix="gnrc" TagName="myGenericControl" Src="./Controls/Generic.ascx" %>  
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
  
</asp:Content> 
  
<asp:Content ID="cloneContent" runat="server" ContentPlaceHolderID="MainContent">
    <br /><br />
    <asp:Label Text="Please enter the ScheduleItemId" runat="server" />

    <table style="font-weight:bold;color:Black"  width="400" border="0">   
            <tr>
            <td>
                <gnrc:myGenericControl ID="myGenericControl" runat="server" />   
            </td>
            </tr>
        <tr align="right">
            <td>
               <asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
