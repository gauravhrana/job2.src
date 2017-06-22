<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType.Controls.List" %>
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="NotificationPublisherXEventTypeId" SortExpression="NotificationPublisherXEventTypeId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="NotificationPublisher"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="NotificationPublisher" 
                            SortExpression="NotificationPublisher"
                            DataNavigateUrlFields="NotificationPublisherXEventTypeId" 
                            DataNavigateUrlFormatString="~/EventNotification/NotificationPublisherXEventType/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="NotificationEventType"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="NotificationEventType" 
                            SortExpression="NotificationEventType"
                            DataNavigateUrlFields="NotificationPublisherXEventTypeId" 
                            DataNavigateUrlFormatString="~/EventNotification/NotificationPublisherXEventType/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="NotificationPublisherXEventTypeId" 
                            DataNavigateUrlFormatString="~/EventNotification/NotificationPublisherXEventType/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="NotificationPublisherXEventTypeId"
                            DataNavigateUrlFormatString="~/EventNotification/NotificationPublisherXEventType/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="NotificationPublisherXEventTypeId" 
                            DataNavigateUrlFormatString="~/EventNotification/NotificationPublisherXEventType/Clone.aspx?SetId={0}" />
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
