<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ApplicationUserTitleId" SortExpression="ApplicationUserTitleId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="ApplicationUserTitleId" 
                            DataNavigateUrlFormatString="~/ApplicationUserTitle/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="ApplicationId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationId" 
                            SortExpression="ApplicationId"
                            DataNavigateUrlFields="ApplicationUserTitleId" 
                            DataNavigateUrlFormatString="~/ApplicationUserTitle/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ApplicationUserTitleId" 
                            DataNavigateUrlFormatString="~/ApplicationUserTitle/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ApplicationUserTitleId"
                            DataNavigateUrlFormatString="~/ApplicationUserTitle/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ApplicationUserTitleId" 
                            DataNavigateUrlFormatString="~/ApplicationUserTitle/Clone.aspx?SetId={0}" />
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