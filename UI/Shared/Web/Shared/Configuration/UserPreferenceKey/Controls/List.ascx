<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.UserPreferenceKey.Controls.List" %>
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" AllowSorting="true"   AutoGenerateColumns="false" runat="server">
                  <Columns>

                        <asp:BoundField DataField="UserPreferenceKeyId" SortExpression="UserPreferenceKeyId" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="Name" 
                                SortExpression="Name"
                                DataNavigateUrlFields="UserPreferenceKeyId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="ApplicationId" 
                                SortExpression="ApplicationId"
                                DataNavigateUrlFields="UserPreferenceKeyId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Value"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="Value" 
                                SortExpression="Value"
                                DataNavigateUrlFields="UserPreferenceKeyId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Data Type"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="DataTpeId" 
                                SortExpression="DataTpeId"
                                DataNavigateUrlFields="UserPreferenceKeyId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="UserPreferenceKeyId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="UserPreferenceKeyId"
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="UserPreferenceKeyId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/UserPreferenceKey/Clone.aspx?SetId={0}" />
                    </Columns>
             </asp:GridView>
         </td>
    </tr>
</table>