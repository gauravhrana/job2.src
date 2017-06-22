<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Admin.UserLoginHistory.Controls.List" %>
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <%-- <asp:BoundField DataField="UserLoginHistoryId" SortExpression="UserLoginHistoryId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
--%>
                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="UserLoginHistoryId" 
                            SortExpression="UserLoginHistoryId"
                            DataNavigateUrlFields="UserLoginHistoryId" 
                            DataNavigateUrlFormatString="~/UserLoginHistory/Details.aspx?SetId={0}" />

                    
                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="UserLoginHistoryId" 
                            DataNavigateUrlFormatString="~/UserLoginHistory/Clone.aspx?SetId={0}" />
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