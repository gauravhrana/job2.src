<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.ApplicationMonitoredEventEmail.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ApplicationMonitoredEventEmailId" SortExpression="ApplicationMonitoredEventEmailId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:HyperLinkField HeaderText="ApplicationMonitoredEventSource"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationMonitoredEventSource" 
                            SortExpression="ApplicationMonitoredEventSource"
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="User"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="User" 
                            SortExpression="User"
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="CorrespondenceLevel"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="CorrespondenceLevel" 
                            SortExpression="CorrespondenceLevel"
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Active"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Active" 
                            SortExpression="Active"
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId"
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ApplicationMonitoredEventEmailId" 
                            DataNavigateUrlFormatString="~/Shared/EventMonitoring/ApplicationMonitoredEventEmail/Clone.aspx?SetId={0}" />
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