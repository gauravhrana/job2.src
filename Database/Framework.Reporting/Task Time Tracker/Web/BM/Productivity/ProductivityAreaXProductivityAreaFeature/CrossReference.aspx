﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CrossReference.aspx.cs" 
Inherits="ApplicationContainer.UI.Web.Productivity.ProductivityAreaXProductivityAreaFeature.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 855px; height: 300px; padding-left: 25px;">
        <table border="1" width="400">
            <tr>
                <td width="150">
                    Direction:
                </td>
                <td>
                    <asp:DropDownList Width="155" ID="drpSelection" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="drpSelection_SelectedIndexChanged">
                        <asp:ListItem Value="ByProductivityArea">ProductivityArea To ProductivityAreaFeature</asp:ListItem>
                        <asp:ListItem Value="ByProductivityAreaFeature">ProductivityAreaFeature To ProductivityArea</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:PlaceHolder ID="dynProductivityArea" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td width="150">
                                    ProductivityArea:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpProductivityArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpProductivityArea_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:Bucket ID="BucketOfProductivityAreaFeature" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="dynProductivityAreaFeature" runat="server">
                        <table>
                            <tr>
                                <td width="150">
                                    ProductivityAreaFeature:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpProductivityAreaFeature" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpProductivityAreaFeature_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:Bucket ID="BucketOfProductivityArea" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
