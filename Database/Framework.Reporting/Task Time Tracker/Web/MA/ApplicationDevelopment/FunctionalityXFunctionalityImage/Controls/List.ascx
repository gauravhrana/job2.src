<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <asp:BoundField DataField="FunctionalityXFunctionalityImageId" SortExpression="FunctionalityXFunctionalityImageId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="FunctionalityXFunctionalityImageId" 
                            SortExpression="FunctionalityXFunctionalityImageId"
                            DataNavigateUrlFields="FunctionalityXFunctionalityImageId" 
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityImage/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="FunctionalityXFunctionalityImageId" 
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityImage/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="FunctionalityXFunctionalityImageId"
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityImage/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="FunctionalityXFunctionalityImageId" 
                            DataNavigateUrlFormatString="~/FunctionalityXFunctionalityImage/Clone.aspx?SetId={0}" />
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