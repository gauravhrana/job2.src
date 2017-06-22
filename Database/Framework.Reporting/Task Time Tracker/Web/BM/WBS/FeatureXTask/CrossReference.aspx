﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="CrossReference.aspx.cs" Inherits="ApplicationContainer.UI.Web.FeatureXTask.CrossReference" %>

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
                        <asp:ListItem Value="ByTask">Task To Feature</asp:ListItem>
                        <asp:ListItem Value="ByFeature">Feature To Task</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:PlaceHolder ID="dynTask" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td width="150">
                                    Task:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpTask" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpTask_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:Bucket ID="BucketOfFeature" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:PlaceHolder ID="dynFeature" runat="server">
                        <table>
                            <tr>
                                <td width="150">
                                    Feature:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpFeature" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFeature_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:Bucket ID="BucketOfTask" runat="server" />
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
