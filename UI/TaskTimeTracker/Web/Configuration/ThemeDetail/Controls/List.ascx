﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.ThemeDetail.Controls.List" %>

<table  >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ThemeDetailId" SortExpression="ThemeDetailId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="ThemeDetailId" 
                            DataNavigateUrlFormatString="~/ThemeDetail/Details.aspx?SetId={0}" />

                    <asp:BoundField DataField="IsAllTab" SortExpression="IsAllTab" HeaderText="IsAllTab" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ThemeDetailId" 
                            DataNavigateUrlFormatString="~/ThemeDetail/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ThemeDetailId"
                            DataNavigateUrlFormatString="~/ThemeDetail/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ThemeDetailId" 
                            DataNavigateUrlFormatString="~/ThemeDetail/Clone.aspx?SetId={0}" />
                </Columns>
            </asp:GridView>                
        </td>
    </tr>
     <tr>
        <td align="center">
            <asp:Label ID="lblCount" runat="server" />
        </td>
    </tr>
</table>