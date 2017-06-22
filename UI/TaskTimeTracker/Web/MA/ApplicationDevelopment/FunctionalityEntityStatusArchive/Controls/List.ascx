<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatusArchive.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <%-- <asp:BoundField DataField="FunctionalityEntityStatusArchiveId" SortExpression="FunctionalityEntityStatusArchiveId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
--%>
                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="FunctionalityEntityStatusArchiveId" 
                            SortExpression="FunctionalityEntityStatusArchiveId"
                            DataNavigateUrlFields="FunctionalityEntityStatusArchiveId" 
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatusArchive/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="FunctionalityEntityStatusArchiveId" 
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatusArchive/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="FunctionalityEntityStatusArchiveId"
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatusArchive/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="FunctionalityEntityStatusArchiveId" 
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatusArchive/Clone.aspx?SetId={0}" />
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