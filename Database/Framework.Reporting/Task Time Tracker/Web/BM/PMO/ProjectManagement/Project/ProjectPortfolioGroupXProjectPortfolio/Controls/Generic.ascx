<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroupXProjectPortfolio.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
 
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblProjectPortfolioGroupXProjectPortfolioId" Text="ProjectPortfolioGroupXProjectPortfolioId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProjectPortfolioGroupXProjectPortfolioId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProjectPortfolioGroupXProjectPortfolioId" runat="server" />
                        </td>
                    </tr><tr>
                        <td width="300" class="ralabel" align="left">
                            Project Portfolio Group:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpProjectPortfolioGroup" runat="server" OnSelectedIndexChanged="drpProjectPortfolioGroup_SelectedIndexChanged"
                                AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProjectPortfolioGroupId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProjectPortfolioGroupId" runat="server" />
                        </td>
                    </tr>   
                    <tr>
                        <td width="300" class="ralabel" align="left">
                            Project Portfolio:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpProjectPortfolio" runat="server" OnSelectedIndexChanged="drpProjectPortfolio_SelectedIndexChanged"
                                AppendDataBoundItems="true" >
                                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProjectPortfolioId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynProjectPortfolioId" runat="server" />
                        </td>
                    </tr>                 
                    <tr>
                        <td class="ralabel">
                            Description:
                        </td>
                        <td>
                            <textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="ralabel">
                            <asp:Label ID="lblCreatedDate" Text="CreatedDate:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCreatedDate" runat="server" />
                        </td>
                    </tr>
                    <tr style=" display:none;">
                        <td class="ralabel">
                            <asp:Label ID="lblModifiedDate" Text="ModifiedDate:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModifiedDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynModifiedDate" runat="server" />
                        </td>
                    </tr>
                     <tr style=" display:none;">
                        <td class="ralabel">
                            <asp:Label ID="lblCreatedByAuditId" Text="CreatedByAuditId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedByAuditId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCreatedByAuditId" runat="server" />
                        </td>
                    </tr>
                     <tr style=" display:none;">
                        <td class="ralabel">
                            <asp:Label ID="lblModifiedByAuditId" Text="ModifiedByAuditId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModifiedByAuditId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynModifiedByAuditId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            SortOrder:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dc:List ID="oHistoryList" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
