<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <%--<asp:HyperLinkField DataTextField="ApplicationUserId" 
                            SortExpression="ApplicationUserId" 
                            HeaderText="ID" DataNavigateUrlFields="ApplicationUserId" 
                            DataNavigateUrlFormatString="~/ApplicationUser/Details.aspx?SetId={0}"
                            ItemStyle-HorizontalAlign="Center" />--%>

                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="ApplicationUserId" 
                            DataNavigateUrlFormatString="~/ApplicationUser/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="ApplicationId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationId" 
                            SortExpression="ApplicationId"
                            DataNavigateUrlFields="ApplicationUserId" 
                            DataNavigateUrlFormatString="~/ApplicationUser/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ApplicationUserId" 
                            DataNavigateUrlFormatString="~/ApplicationUser/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ApplicationUserId"
                            DataNavigateUrlFormatString="~/ApplicationUser/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ApplicationUserId" 
                            DataNavigateUrlFormatString="~/ApplicationUser/Clone.aspx?SetId={0}" />
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