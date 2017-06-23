<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="TaskTimeTracker.UI.Web.Report.Controls.List" %>
    
<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ReportId" SortExpression="ReportId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="ReportId" 
                            DataNavigateUrlFormatString="~/Report/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ReportId" 
                            DataNavigateUrlFormatString="~/Report/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ReportId"
                            DataNavigateUrlFormatString="~/Report/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ReportId" 
                            DataNavigateUrlFormatString="~/Report/Clone.aspx?SetId={0}" />
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