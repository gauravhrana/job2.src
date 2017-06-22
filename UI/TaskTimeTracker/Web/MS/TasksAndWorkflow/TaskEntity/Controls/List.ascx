<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskEntity.Controls.List" %>
    
<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskEntityId" SortExpression="TaskEntityId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="TaskEntityType"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TaskEntityType" 
                            SortExpression="TaskEntityType"
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Description"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Description" 
                            SortExpression="Description"
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Active"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Active" 
                            SortExpression="Active"
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="SortOrder"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="SortOrder" 
                            SortExpression="SortOrder"
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskEntityId"
                            DataNavigateUrlFormatString="~/TaskEntity/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskEntityId" 
                            DataNavigateUrlFormatString="~/TaskEntity/Clone.aspx?SetId={0}" />
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