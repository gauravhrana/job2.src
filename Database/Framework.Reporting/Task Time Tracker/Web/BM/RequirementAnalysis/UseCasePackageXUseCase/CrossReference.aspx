<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="CrossReference.aspx.cs" 
Inherits="ApplicationContainer.UI.Web.RequirementAnalysis.UseCasePackageXUseCase.CrossReference" %>

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
                                <asp:ListItem Value="ByUseCasePackage">UseCasePackage To UseCase</asp:ListItem>
                                <asp:ListItem Value="ByUseCase">UseCase To UseCasePackage</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynUseCasePackage" runat="server">
                    <table>
                        <tr>
                            <td width="150">
                                UseCasePackage:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpUseCasePackage" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpUseCasePackage_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfUseCase" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="dynUseCase" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td width="150">
                                UseCase:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpUseCase" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpUseCase_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfUseCasePackage" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>



