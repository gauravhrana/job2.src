<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.StoredProcedureLogStoredProcedureLogRaw.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="StoredProcedureLogRawId" SortExpression="StoredProcedureLogRawId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="StoredProcedureLogRaws"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="StoredProcedureLogRawId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogRaw/StoredProcedureLogRaws.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="StoredProcedureLogRawId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogRaw/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="StoredProcedureLogRawId"
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogRaw/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="StoredProcedureLogRawId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogRaw/Clone.aspx?SetId={0}" />
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