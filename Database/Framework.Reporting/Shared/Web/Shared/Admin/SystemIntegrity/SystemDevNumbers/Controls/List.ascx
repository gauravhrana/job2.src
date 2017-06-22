<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.SystemDevNumbers.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="SystemDevNumbersId" SortExpression="SystemDevNumbersId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="ApplicationId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationId" 
                            SortExpression="ApplicationId"
                            DataNavigateUrlFields="SystemDevNumbersId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Person"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Person" 
                            SortExpression="Person"
                            DataNavigateUrlFields="SystemDevNumbersId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="RangeFrom"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="RangeFrom" 
                            SortExpression="RangeFrom"
                            DataNavigateUrlFields="SystemDevNumbersId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="RangeTo"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="RangeTo" 
                            SortExpression="RangeTo"
                            DataNavigateUrlFields="SystemDevNumbersId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="SystemDevNumbersId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="SystemDevNumbersId"
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="SystemDevNumbersId" 
                            DataNavigateUrlFormatString="~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Clone.aspx?SetId={0}" />
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