<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ActivityXDeliverableArtifact.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ActivityXDeliverableArtifactId" SortExpression="ActivityXDeliverableArtifactId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Activity"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Activity" 
                            SortExpression="Activity"
                            DataNavigateUrlFields="ActivityXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/ActivityXDeliverableArtifact/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="DeliverableArtifacts"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="DeliverableArtifacts" 
                            SortExpression="DeliverableArtifacts"
                            DataNavigateUrlFields="ActivityXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/ActivityXDeliverableArtifact/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="DeliverableArtifactsStatus"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="DeliverableArtifactsStatus" 
                            SortExpression="DeliverableArtifactsStatus"
                            DataNavigateUrlFields="ActivityXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/ActivityXDeliverableArtifact/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ActivityXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/ActivityXDeliverableArtifact/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ActivityXDeliverableArtifactId"
                            DataNavigateUrlFormatString="~/ActivityXDeliverableArtifact/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ActivityXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/ActivityXDeliverableArtifact/Clone.aspx?SetId={0}" />
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