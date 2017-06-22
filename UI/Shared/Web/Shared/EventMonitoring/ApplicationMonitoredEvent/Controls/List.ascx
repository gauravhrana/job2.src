<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEvent.Controls.List" %>
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  AutoGenerateColumns="false"
                runat="server" OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ApplicationMonitoredEventId" SortExpression="ApplicationMonitoredEventId"
                        HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId"
                        HeaderText="Application ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="ApplicationMonitoredEventSource" ItemStyle-HorizontalAlign="Center"
                        DataTextField="ApplicationMonitoredEventSource" SortExpression="ApplicationMonitoredEventSource"
                        DataNavigateUrlFields="ApplicationMonitoredEventId" DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="ApplicationMonitoredEventProcessingState" ItemStyle-HorizontalAlign="Center"
                        DataTextField="ApplicationMonitoredEventProcessingState" SortExpression="ApplicationMonitoredEventProcessingState"
                        DataNavigateUrlFields="ApplicationMonitoredEventId" DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="ReferenceId" ItemStyle-HorizontalAlign="Center" DataTextField="ReferenceId"
                        SortExpression="ReferenceId" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="ReferenceCode" ItemStyle-HorizontalAlign="Center"
                        DataTextField="ReferenceCode" SortExpression="ReferenceCode" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="Category" ItemStyle-HorizontalAlign="Center" DataTextField="Category"
                        SortExpression="Category" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="Message" ItemStyle-HorizontalAlign="Center" DataTextField="Message"
                        SortExpression="Message" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="IsDuplicate" ItemStyle-HorizontalAlign="Center" DataTextField="IsDuplicate"
                        SortExpression="IsDuplicate" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="LastModifiedOn" ItemStyle-HorizontalAlign="Center"
                        DataTextField="LastModifiedOn" SortExpression="LastModifiedOn" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="LastModifiedBy" ItemStyle-HorizontalAlign="Center"
                        DataTextField="LastModifiedBy" SortExpression="LastModifiedBy" DataNavigateUrlFields="ApplicationMonitoredEventId"
                        DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Details.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="Update" ItemStyle-HorizontalAlign="Center" Text="Update"
                        DataNavigateUrlFields="ApplicationMonitoredEventId" DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Update.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" Text="Delete"
                        DataNavigateUrlFields="ApplicationMonitoredEventId" DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Delete.aspx?SetId={0}" />
                    <asp:HyperLinkField HeaderText="Clone" Text="Clone" ItemStyle-HorizontalAlign="Center"
                        DataNavigateUrlFields="ApplicationMonitoredEventId" DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEvent/Clone.aspx?SetId={0}" />
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
