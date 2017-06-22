<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.UserPreference.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="UserPreferenceId" SortExpression="UserPreferenceId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationUserId" 
                            SortExpression="ApplicationUserId"
                            DataNavigateUrlFields="UserPreferenceId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreference/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="ApplicationId" 
                                SortExpression="ApplicationId"
                                DataNavigateUrlFields="UserPreferenceId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreference/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="UserPreferenceId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreference/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="UserPreferenceId"
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreference/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="UserPreferenceId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreference/Clone.aspx?SetId={0}" />
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