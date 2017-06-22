﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.TabChildStructure.Controls.List" %>


<table  >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TabChildStructureId" SortExpression="TabChildStructureId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Details"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="TabChildStructureId" 
                            DataNavigateUrlFormatString="~/TabChildStructure/Details.aspx?SetId={0}" />

                    <asp:BoundField DataField="TabParentStructureId" SortExpression="TabParentStructureId" HeaderText="TabParentStructureId" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TabChildStructureId" 
                            DataNavigateUrlFormatString="~/TabChildStructure/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TabChildStructureId"
                            DataNavigateUrlFormatString="~/TabChildStructure/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TabChildStructureId" 
                            DataNavigateUrlFormatString="~/TabChildStructure/Clone.aspx?SetId={0}" />
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