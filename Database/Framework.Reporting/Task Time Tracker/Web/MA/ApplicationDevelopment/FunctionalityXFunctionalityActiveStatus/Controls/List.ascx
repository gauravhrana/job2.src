<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <asp:BoundField DataField="FunctionalityXFunctionalityActiveStatusId" SortExpression="FunctionalityXFunctionalityActiveStatusId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="FunctionalityXFunctionalityActiveStatusId" 
                            SortExpression="FunctionalityXFunctionalityActiveStatusId"
                            DataNavigateUrlFields="FunctionalityXFunctionalityActiveStatusId" 
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityActiveStatus/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="FunctionalityXFunctionalityActiveStatusId" 
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityActiveStatus/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="FunctionalityXFunctionalityActiveStatusId"
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityActiveStatus/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="FunctionalityXFunctionalityActiveStatusId" 
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityActiveStatus/Clone.aspx?SetId={0}" />
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