<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEventSource.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ApplicationMonitoredEventSourceId" SortExpression="ApplicationMonitoredEventSourceId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Code"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Code" 
                            SortExpression="Code"
                            DataNavigateUrlFields="ApplicationMonitoredEventSourceId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventSource/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Description"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Description" 
                            SortExpression="Description"
                            DataNavigateUrlFields="ApplicationMonitoredEventSourceId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventSource/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ApplicationMonitoredEventSourceId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventSource/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ApplicationMonitoredEventSourceId"
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventSource/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ApplicationMonitoredEventSourceId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventSource/Clone.aspx?SetId={0}" />
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