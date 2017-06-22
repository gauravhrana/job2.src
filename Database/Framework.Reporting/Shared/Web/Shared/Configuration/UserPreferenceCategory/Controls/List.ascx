<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.UserPreferenceCategory.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="UserPreferenceCategoryId" SortExpression="UserPreferenceCategoryId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="UserPreferenceCategoryId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceCategory/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="ApplicationId" 
                                SortExpression="ApplicationId"
                                DataNavigateUrlFields="UserPreferenceCategoryId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceCategory/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="UserPreferenceCategoryId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceCategory/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="UserPreferenceCategoryId"
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceCategory/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="UserPreferenceCategoryId" 
                            DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceCategory/Clone.aspx?SetId={0}" />
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