<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Admin.AuditHistory.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting"  AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="AuditHistoryId" 
                                SortExpression="AuditHistoryId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="SystemEntityId" 
                                SortExpression="SystemEntityId" 
                                HeaderText="SystemEntityId" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="EntityKey" 
                                SortExpression="EntityKey" 
                                HeaderText="EntityKey" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="AuditActionId" 
                                SortExpression="AuditActionId" 
                                HeaderText="AuditActionId" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="CreatedDate" 
                                SortExpression="CreatedDate" 
                                HeaderText="CreatedDate" 
                                ItemStyle-HorizontalAlign="Center"  />

                        <asp:BoundField DataField="CreatedByPersonId" 
                                SortExpression="CreatedByPersonId" 
                                HeaderText="CreatedByPersonId" 
                                ItemStyle-HorizontalAlign="Center"  />
                        
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
