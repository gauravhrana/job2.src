<%@ Page Title="Delete" MasterPageFile="~/MasterPages/Site.master" EnableEventValidation="false"
    Language="C#" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Delete" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<%@ Register TagName="DetailsControl" TagPrefix="uc" Src="~/Configuration/FieldConfiguration/Controls/Details.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
</asp:Content>
<asp:Content ID="deleteContent" ContentPlaceHolderID="MainContent" runat="server">
    <table >
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                <table  cellpadding="2" cellspacing="2" border="0">
                    <tr>
                        <td colspan="2" align="center" class="style3">
                            FieldConfiguration Details
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
                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

