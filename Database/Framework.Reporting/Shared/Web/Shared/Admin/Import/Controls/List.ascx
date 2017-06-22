<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.BatchFile.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="BatchFileId" SortExpression="BatchFileId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    
                    <asp:HyperLinkField HeaderText="BatchFile"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BatchFile" 
                            SortExpression="BatchFile"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />                    

                    <asp:HyperLinkField HeaderText="BatchFileSet"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BatchFileSet" 
                            SortExpression="BatchFileSet"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />                    

                    <asp:HyperLinkField HeaderText="FileType"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="FileType" 
                            SortExpression="FileType"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />                    

                    <asp:HyperLinkField HeaderText="SystemEntityType"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="SystemEntityType" 
                            SortExpression="SystemEntityType"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />                    

                    <asp:HyperLinkField HeaderText="BatchFileStatus"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="BatchFileStatus" 
                            SortExpression="BatchFileStatus"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />                    

                    <asp:HyperLinkField HeaderText="CreatedDate"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="CreatedDate" 
                            SortExpression="CreatedDate"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />                    

                    <asp:HyperLinkField HeaderText="CreatedByPerson"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="CreatedByPerson" 
                            SortExpression="CreatedByPerson"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="BatchFileId"
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="BatchFileId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/Import/Clone.aspx?SetId={0}" />
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