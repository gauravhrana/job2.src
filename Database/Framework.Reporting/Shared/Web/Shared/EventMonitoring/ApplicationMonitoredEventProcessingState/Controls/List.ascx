<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEventProcessingState.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ApplicationMonitoredEventProcessingStateId" SortExpression="ApplicationMonitoredEventProcessingStateId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:HyperLinkField HeaderText="Code"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Code" 
                            SortExpression="Code"
                            DataNavigateUrlFields="ApplicationMonitoredEventProcessingStateId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventProcessingState/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Description"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Description" 
                            SortExpression="Description"
                            DataNavigateUrlFields="ApplicationMonitoredEventProcessingStateId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventProcessingState/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ApplicationMonitoredEventProcessingStateId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventProcessingState/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ApplicationMonitoredEventProcessingStateId"
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventProcessingState/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ApplicationMonitoredEventProcessingStateId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventProcessingState/Clone.aspx?SetId={0}" />
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