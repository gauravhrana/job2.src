<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.QuickPaginationRun.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="QuickPaginationRunId" SortExpression="QuickPaginationRunId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="SystemEntityType" 
                            SortExpression="SystemEntityType"
                            DataNavigateUrlFields="QuickPaginationRunId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationUserName" 
                            SortExpression="ApplicationUserName"
                            DataNavigateUrlFields="QuickPaginationRunId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="SortClause" 
                            SortExpression="SortClause"
                            DataNavigateUrlFields="QuickPaginationRunId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="WhereClause" 
                            SortExpression="WhereClause"
                            DataNavigateUrlFields="QuickPaginationRunId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="QuickPaginationRunId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="QuickPaginationRunId"
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="QuickPaginationRunId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/QuickPaginationRun/Clone.aspx?SetId={0}" />
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