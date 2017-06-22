<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationMode.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting"  AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="FieldConfigurationModeId" 
                                SortExpression="FieldConfigurationModeId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />
                        
                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "ApplicationId"
                                DataTextField="ApplicationId" 
                                DataNavigateUrlFields="FieldConfigurationModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationMode/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "Name"
                                DataTextField="Name" 
                                DataNavigateUrlFields="FieldConfigurationModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationMode/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="FieldConfigurationModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationMode/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="FieldConfigurationModeId"
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationMode/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="FieldConfigurationModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationMode/Clone.aspx?SetId={0}" />
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