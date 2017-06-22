<%@ Page Title="Delete" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="Delete.aspx.cs" Inherits="ApplicationContainer.UI.Web.Activity.Delete"
    EnableEventValidation="false" %>

<%@ Register TagName="DetailsControl" TagPrefix="uc" Src="./Controls/Details.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table >
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                    AutoPostBack="true" Enabled="true" Checked="true" />
            </td>
        </tr>
        <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td colspan="2" align="center" class="style3">
                            Activity Details
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                        </td>
                        <td class="style1">
                            <div style="overflow: auto; height: auto;">
                                <asp:PlaceHolder ID="plcDetailsList" runat="server"></asp:PlaceHolder>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" />
                <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnCancel_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
