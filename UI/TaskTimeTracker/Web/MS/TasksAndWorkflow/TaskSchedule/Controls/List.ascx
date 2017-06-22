<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskSchedule.Controls.List" %>
    
<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskScheduleId" SortExpression="TaskScheduleId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>
                    <asp:HyperLinkField HeaderText="TaskScheduleType"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TaskScheduleType" 
                            SortExpression="TaskScheduleType"
                            DataNavigateUrlFields="TaskScheduleId" 
                            DataNavigateUrlFormatString="~/TaskSchedule/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="TaskEntity"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TaskEntity" 
                            SortExpression="TaskEntity"
                            DataNavigateUrlFields="TaskScheduleId" 
                            DataNavigateUrlFormatString="~/TaskSchedule/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskScheduleId" 
                            DataNavigateUrlFormatString="~/TaskSchedule/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskScheduleId"
                            DataNavigateUrlFormatString="~/TaskSchedule/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskScheduleId" 
                            DataNavigateUrlFormatString="~/TaskSchedule/Clone.aspx?SetId={0}" />
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