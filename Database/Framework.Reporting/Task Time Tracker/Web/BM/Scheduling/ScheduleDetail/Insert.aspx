<%@ Page Title="Insert" Language="C#" MasterPageFile="~/MasterPages/Schedule/Site.master" AutoEventWireup="true"
    CodeBehind="Insert.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Insert" %>

<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="./Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <gnrc:GenericControl ID="myGenericControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
