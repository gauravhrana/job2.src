<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting"  AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="ApplicationRoleId" 
                                SortExpression="ApplicationRoleId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:HyperLinkField HeaderText="Details"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "Name"
                                DataTextField="Name" 
                                DataNavigateUrlFields="ApplicationRoleId" 
                                DataNavigateUrlFormatString="~/ApplicationRole/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="ApplicationRoleId" 
                                DataNavigateUrlFormatString="~/ApplicationRole/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="ApplicationRoleId"
                                DataNavigateUrlFormatString="~/ApplicationRole/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="ApplicationRoleId" 
                                DataNavigateUrlFormatString="~/ApplicationRole/Clone.aspx?SetId={0}" />
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
