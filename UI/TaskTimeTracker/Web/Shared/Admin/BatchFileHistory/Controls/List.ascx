<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.BatchFileHistory.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="BatchFileHistoryId" SortExpression="BatchFileHistoryId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="BatchFileId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BatchFileId" 
                            SortExpression="BatchFileId"
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="BatchFileSet"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BatchFileSet" 
                            SortExpression="BatchFileSet"
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="BatchFileStatus"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BatchFileStatus" 
                            SortExpression="BatchFileStatus"
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="UpdatedDate"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="UpdatedDate" 
                            SortExpression="UpdatedDate"
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="UpdatedByPerson"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="UpdatedByPerson" 
                            SortExpression="UpdatedByPerson"
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="BatchFileHistoryId"
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="BatchFileHistoryId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/BatchFileHistory/Clone.aspx?SetId={0}" />
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