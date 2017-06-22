<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting"  AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="FieldConfigurationId" 
                                SortExpression="FieldConfigurationId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />
                        
                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "ApplicationId"
                                DataTextField="ApplicationId" 
                                DataNavigateUrlFields="FieldConfigurationId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfiguration/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "Name"
                                DataTextField="Name" 
                                DataNavigateUrlFields="FieldConfigurationId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfiguration/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="FieldConfigurationId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfiguration/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="FieldConfigurationId"
                                DataNavigateUrlFormatString="~/Configuration/FieldConfiguration/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="FieldConfigurationId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfiguration/Clone.aspx?SetId={0}" />
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
