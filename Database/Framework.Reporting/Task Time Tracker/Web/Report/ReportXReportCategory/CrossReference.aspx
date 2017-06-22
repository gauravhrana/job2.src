<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CrossReference.aspx.cs" 
    Inherits="ApplicationContainer.UI.Web.ReportXReportCategory.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <table border="1" width="400">
                    <tr>
                        <td width="150">
                            Direction:
                        </td>
                        <td>
                            <asp:DropDownList Width="155" ID="drpSelection" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                                <asp:ListItem Value="ByReportCategory">ReportCategory To Report</asp:ListItem>
                                <asp:ListItem Value="ByReport">Report To ReportCategory</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynReportCategory" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                ReportCategory:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpReportCategory" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpReportCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfReport" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynReport" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                Report:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpReport" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpReport_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfReportCategory" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>

