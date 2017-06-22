<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <%-- <asp:BoundField DataField="FunctionalityEntityStatusId" SortExpression="FunctionalityEntityStatusId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
--%>
                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="FunctionalityEntityStatusId" 
                            SortExpression="FunctionalityEntityStatusId"
                            DataNavigateUrlFields="FunctionalityEntityStatusId" 
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatus/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="FunctionalityEntityStatusId" 
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatus/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="FunctionalityEntityStatusId"
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatus/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="FunctionalityEntityStatusId" 
                            DataNavigateUrlFormatString="~/FunctionalityEntityStatus/Clone.aspx?SetId={0}" />
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