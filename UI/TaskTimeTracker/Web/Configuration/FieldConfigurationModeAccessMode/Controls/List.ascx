<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting"  AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="FieldConfigurationModeAccessModeId" 
                                SortExpression="FieldConfigurationModeAccessModeId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />
                        
                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "ApplicationId"
                                DataTextField="ApplicationId" 
                                DataNavigateUrlFields="FieldConfigurationModeAccessModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationModeAccessMode/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "Name"
                                DataTextField="Name" 
                                DataNavigateUrlFields="FieldConfigurationModeAccessModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationModeAccessMode/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="FieldConfigurationModeAccessModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationModeAccessMode/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="FieldConfigurationModeAccessModeId"
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationModeAccessMode/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="FieldConfigurationModeAccessModeId" 
                                DataNavigateUrlFormatString="~/Configuration/FieldConfigurationModeAccessMode/Clone.aspx?SetId={0}" />
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