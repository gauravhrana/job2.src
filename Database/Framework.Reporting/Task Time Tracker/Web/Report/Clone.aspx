<%@ Page Title="Clone" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="Clone.aspx.cs" Inherits="ApplicationContainer.UI.Web.Report.Clone" %>
<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="~/Report/Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   <!--<a href="Default.aspx">Report</a> : Clone--> 
</asp:Content>


    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <br /><br />
        <asp:Label Text="Please enter the ReportId" runat="server"/>

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
   