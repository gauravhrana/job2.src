<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.TaskXDeliverableArtifact.Controls.List" %>

<table style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskXDeliverableArtifactId" SortExpression="TaskXDeliverableArtifactId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Task"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Task" 
                            SortExpression="Task"
                            DataNavigateUrlFields="TaskXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/TaskXDeliverableArtifact/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="DeliverableArtifacts"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="DeliverableArtifacts" 
                            SortExpression="DeliverableArtifacts"
                            DataNavigateUrlFields="TaskXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/TaskXDeliverableArtifact/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="DeliverableArtifactStatus"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="DeliverableArtifactStatus" 
                            SortExpression="DeliverableArtifactStatus"
                            DataNavigateUrlFields="TaskXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/TaskXDeliverableArtifact/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/TaskXDeliverableArtifact/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskXDeliverableArtifactId"
                            DataNavigateUrlFormatString="~/TaskXDeliverableArtifact/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskXDeliverableArtifactId" 
                            DataNavigateUrlFormatString="~/TaskXDeliverableArtifact/Clone.aspx?SetId={0}" />
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
