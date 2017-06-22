<%@ Page Title="Clone" MasterPageFile="~/MasterPages/Schedule/Site.master" Language="C#" AutoEventWireup="true"
    CodeBehind="Clone.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Clone" %>

<%@ Register TagPrefix="gnrc" TagName="GenericControl" Src="./Controls/Generic.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>
<asp:Content ID="cloneContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <asp:Label ID="Label1" Text="Please enter the ScheduleDetailId" runat="server" />

    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <gnrc:genericcontrol id="myGenericControl" runat="server" />
            </td>
        </tr>
        <tr align="right">
            <td>
                <asp:LinkButton ID="lnkClone" Text="Save" runat="server" OnClick="lnkSave_Click" />
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
