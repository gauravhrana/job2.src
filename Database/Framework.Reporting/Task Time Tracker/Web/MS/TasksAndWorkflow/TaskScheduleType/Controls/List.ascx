<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskScheduleType.Controls.List" %>
    
<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskScheduleTypeId" SortExpression="TaskScheduleTypeId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>
                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="TaskScheduleTypeId" 
                            DataNavigateUrlFormatString="~/TaskScheduleType/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Description"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Description" 
                            SortExpression="Description"
                            DataNavigateUrlFields="TaskScheduleTypeId" 
                            DataNavigateUrlFormatString="~/TaskScheduleType/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Active"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Active" 
                            SortExpression="Active"
                            DataNavigateUrlFields="TaskScheduleTypeId" 
                            DataNavigateUrlFormatString="~/TaskScheduleType/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="SortOrder"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="SortOrder" 
                            SortExpression="SortOrder"
                            DataNavigateUrlFields="TaskScheduleTypeId" 
                            DataNavigateUrlFormatString="~/TaskScheduleType/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskScheduleTypeId" 
                            DataNavigateUrlFormatString="~/TaskScheduleType/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskScheduleTypeId"
                            DataNavigateUrlFormatString="~/TaskScheduleType/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskScheduleTypeId" 
                            DataNavigateUrlFormatString="~/TaskScheduleType/Clone.aspx?SetId={0}" />
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