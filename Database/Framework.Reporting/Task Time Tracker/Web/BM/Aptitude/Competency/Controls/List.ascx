<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.Aptitude.Competency.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="CompetencyId" SortExpression="CompetencyId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="CompetencyId" 
                            DataNavigateUrlFormatString="~/Competency/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="ApplicationId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationId" 
                            SortExpression="ApplicationId"
                            DataNavigateUrlFields="CompetencyId" 
                            DataNavigateUrlFormatString="~/Competency/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="CompetencyId" 
                            DataNavigateUrlFormatString="~/Competency/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="CompetencyId"
                            DataNavigateUrlFormatString="~/Competency/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="CompetencyId" 
                            DataNavigateUrlFormatString="~/Competency/Clone.aspx?SetId={0}" />
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