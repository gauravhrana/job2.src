<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ClientXProject.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ClientXProjectId" SortExpression="ClientXProjectId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="Project"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Project" 
                            SortExpression="Project"
                            DataNavigateUrlFields="ClientXProjectId" 
                            DataNavigateUrlFormatString="~/ClientXProject/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Client"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Client" 
                            SortExpression="Client"
                            DataNavigateUrlFields="ClientXProjectId" 
                            DataNavigateUrlFormatString="~/ClientXProject/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ClientXProjectId" 
                            DataNavigateUrlFormatString="~/ClientXProject/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ClientXProjectId"
                            DataNavigateUrlFormatString="~/ClientXProject/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ClientXProjectId" 
                            DataNavigateUrlFormatString="~/ClientXProject/Clone.aspx?SetId={0}" />
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