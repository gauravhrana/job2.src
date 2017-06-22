<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance.Controls.List" %>


<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="FunctionalityImageInstanceId" SortExpression="FunctionalityImageInstanceId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>


                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="FunctionalityImageInstanceId" 
                            DataNavigateUrlFormatString="~/ApplicationDevelopment/FunctionalityImageInstance/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="FunctionalityImageInstanceId" 
                            DataNavigateUrlFormatString="~/ApplicationDevelopment/FunctionalityImageInstance/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="FunctionalityImageInstanceId"
                            DataNavigateUrlFormatString="~/ApplicationDevelopment/FunctionalityImageInstance/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="FunctionalityImageInstanceId" 
                            DataNavigateUrlFormatString="~/ApplicationDevelopment/FunctionalityImageInstance/Clone.aspx?SetId={0}" />
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