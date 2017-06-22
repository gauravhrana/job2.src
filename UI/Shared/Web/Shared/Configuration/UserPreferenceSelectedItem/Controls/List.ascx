﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.UserPreferenceSelectedItem.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="UserPreferenceSelectedItemId" SortExpression="UserPreferenceSelectedItemId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationUserId" 
                            SortExpression="ApplicationUserId"
                            DataNavigateUrlFields="UserPreferenceSelectedItemId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceSelectedItem/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="ApplicationId" 
                                SortExpression="ApplicationId"
                                DataNavigateUrlFields="UserPreferenceSelectedItemId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceSelectedItem/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="UserPreferenceSelectedItemId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceSelectedItem/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="UserPreferenceSelectedItemId"
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceSelectedItem/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="UserPreferenceSelectedItemId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceSelectedItem/Clone.aspx?SetId={0}" />
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