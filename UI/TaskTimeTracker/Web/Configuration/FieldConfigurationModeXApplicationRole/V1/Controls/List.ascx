<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="FieldConfigurationModeXApplicationRoleId" SortExpression="FieldConfigurationModeXApplicationRoleId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="ApplicationRole"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationRole" 
                            SortExpression="ApplicationRole"
                            DataNavigateUrlFields="FieldConfigurationModeXApplicationRoleId" 
                            DataNavigateUrlFormatString="~/FieldConfigurationModeXApplicationRole/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="FieldConfigurationMode"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="FieldConfigurationMode" 
                            SortExpression="FieldConfigurationMode"
                            DataNavigateUrlFields="FieldConfigurationModeXApplicationRoleId" 
                            DataNavigateUrlFormatString="~/FieldConfigurationModeXApplicationRole/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="FieldConfigurationModeXApplicationRoleId" 
                            DataNavigateUrlFormatString="~/FieldConfigurationModeXApplicationRole/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="FieldConfigurationModeXApplicationRoleId"
                            DataNavigateUrlFormatString="~/FieldConfigurationModeXApplicationRole/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="FieldConfigurationModeXApplicationRoleId" 
                            DataNavigateUrlFormatString="~/FieldConfigurationModeXApplicationRole/Clone.aspx?SetId={0}" />
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