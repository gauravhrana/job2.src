<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectUseCaseStatusArchive.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <%-- <asp:BoundField DataField="ProjectUseCaseStatusArchiveId" SortExpression="ProjectUseCaseStatusArchiveId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
--%>
                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ProjectUseCaseStatusArchiveId" 
                            SortExpression="ProjectUseCaseStatusArchiveId"
                            DataNavigateUrlFields="ProjectUseCaseStatusArchiveId" 
                            DataNavigateUrlFormatString="~/ProjectUseCaseStatusArchive/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"  
                            Text="Update" 
                            DataNavigateUrlFields="ProjectUseCaseStatusArchiveId" 
                            DataNavigateUrlFormatString="~/ProjectUseCaseStatusArchive/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ProjectUseCaseStatusArchiveId"
                            DataNavigateUrlFormatString="~/ProjectUseCaseStatusArchive/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ProjectUseCaseStatusArchiveId" 
                            DataNavigateUrlFormatString="~/ProjectUseCaseStatusArchive/Clone.aspx?SetId={0}" />
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