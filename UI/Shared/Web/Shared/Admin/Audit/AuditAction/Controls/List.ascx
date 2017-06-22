<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Admin.AuditAction.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting"  AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="AuditActionId" 
                                SortExpression="AuditActionId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="Name" 
                                SortExpression="Name" 
                                HeaderText="Name" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="Description" 
                                SortExpression="Description" 
                                HeaderText="Description" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="SortOrder" 
                                SortExpression="SortOrder" 
                                HeaderText="SortOrder" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="AuditActionId" 
                                DataNavigateUrlFormatString="~/Audit/AuditAction/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="AuditActionId"
                                DataNavigateUrlFormatString="~/Audit/AuditAction/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="AuditActionId" 
                                DataNavigateUrlFormatString="~/Audit/AuditAction/Clone.aspx?SetId={0}" />
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
