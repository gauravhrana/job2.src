﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" MasterPageFile="~/MasterPages/Site.master"
 Inherits="Shared.UI.Web.SystemIntegrity.StoredProcedureLogRaw.Details" %>
<%@ Register TagName="DetailsControl" TagPrefix="uc" Src="~/Shared/Admin/SystemIntegrity/StoredProcedureLogRaw/Controls/Details.ascx" %>
<%@ Reference Control="./Controls/Details.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
   
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <table >
        <tr>
            <td align="right">
                <asp:CheckBox ID="chkVisible" runat="server" Text="Audit History Visible" OnCheckedChanged="chkVisible_CheckedChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td colspan="2" align="center" class="style3">
                            StoredProcedureLogRaw Details
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                        </td>
                        <td class="style1">
                            <%--<uc:DetailsControl id="myDetailsControl" runat="server"  />--%>
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
                
                <asp:LinkButton ID="btnBack" Text="Back" OnClick="btnBack_Click" runat="server" />
                
            </td>
        </tr>
    </table>
</asp:Content>

