<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.MilestoneXFeature.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="MilestoneXFeatureId" SortExpression="MilestoneXFeatureId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Milestone"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Milestone" 
                            SortExpression="Milestone"
                            DataNavigateUrlFields="MilestoneXFeatureId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeature/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Feature"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Feature" 
                            SortExpression="Feature"
                            DataNavigateUrlFields="MilestoneXFeatureId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeature/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="MilestoneFeatureState"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="MilestoneFeatureState" 
                            SortExpression="MilestoneFeatureState"
                            DataNavigateUrlFields="MilestoneXFeatureId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeature/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="MilestoneXFeatureId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeature/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="MilestoneXFeatureId"
                            DataNavigateUrlFormatString="~/MilestoneXFeature/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="MilestoneXFeatureId" 
                            DataNavigateUrlFormatString="~/MilestoneXFeature/Clone.aspx?SetId={0}" />
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
