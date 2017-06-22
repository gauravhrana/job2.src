<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls.List" %>
    
<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskRunId" SortExpression="TaskRunId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="TaskScheduleId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TaskScheduleId" 
                            SortExpression="TaskScheduleId"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="TaskEntity"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TaskEntity" 
                            SortExpression="TaskEntity"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="BusinessDate"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BusinessDate" 
                            SortExpression="BusinessDate"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="StartTime"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="StartTime" 
                            SortExpression="StartTime"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="EndTime"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="EndTime" 
                            SortExpression="EndTime"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="RunBy"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="RunBy" 
                            SortExpression="RunBy"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskRunId"
                            DataNavigateUrlFormatString="~/TaskRun/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskRunId" 
                            DataNavigateUrlFormatString="~/TaskRun/Clone.aspx?SetId={0}" />
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