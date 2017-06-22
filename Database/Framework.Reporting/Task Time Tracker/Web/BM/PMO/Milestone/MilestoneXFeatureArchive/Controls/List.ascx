<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.MilestoneXFeatureArchive.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                   <%-- <asp:BoundField DataField="MilestoneXFeatureArchiveId" SortExpression="MilestoneXFeatureArchiveId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
--%>
                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="MilestoneXFeatureArchiveId" 
                            SortExpression="MilestoneXFeatureArchiveId"
                            DataNavigateUrlFields="MilestoneXFeatureArchiveId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeatureArchive/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="MilestoneXFeatureArchiveId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeatureArchive/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="MilestoneXFeatureArchiveId"
                            DataNavigateUrlFormatString="~/MilestoneXFeatureArchive/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="MilestoneXFeatureArchiveId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeatureArchive/Clone.aspx?SetId={0}" />
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