<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.Project.Controls.List" %>

<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" OnSorting="GridView1_Sorting" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server">
                  <Columns>

                        <asp:BoundField DataField="ProjectId" SortExpression="ProjectId" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                DataTextField="Name" 
                                SortExpression="Name"
                                DataNavigateUrlFields="ProjectId" 
                                DataNavigateUrlFormatString="~/Project/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="ProjectId" 
                                DataNavigateUrlFormatString="~/Project/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="ProjectId"
                                DataNavigateUrlFormatString="~/Project/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="ProjectId" 
                                DataNavigateUrlFormatString="~/Project/Clone.aspx?SetId={0}" />
                    </Columns>
             </asp:GridView>
         </td>
    </tr>
</table>