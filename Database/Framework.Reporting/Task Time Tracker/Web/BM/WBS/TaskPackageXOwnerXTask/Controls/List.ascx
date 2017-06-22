<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.TaskPackageXOwnerXTask.Controls.List" %>
<table style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskPackageXOwnerXTaskId" SortExpression="TaskPackageXOwnerXTaskId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="TestSuite"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TestSuite" 
                            SortExpression="TestSuite"
                            DataNavigateUrlFields="TaskPackageXOwnerXTaskId" 
                            DataNavigateUrlFormatString="~/TaskPackageXOwnerXTask/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="TestCase"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="TestCase" 
                            SortExpression="TestCase"
                            DataNavigateUrlFields="TaskPackageXOwnerXTaskId" 
                            DataNavigateUrlFormatString="~/TaskPackageXOwnerXTask/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskPackageXOwnerXTaskId" 
                            DataNavigateUrlFormatString="~/TaskPackageXOwnerXTask/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskPackageXOwnerXTaskId"
                            DataNavigateUrlFormatString="~/TaskPackageXOwnerXTask/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskPackageXOwnerXTaskId" 
                            DataNavigateUrlFormatString="~/TaskPackageXOwnerXTask/Clone.aspx?SetId={0}" />
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

