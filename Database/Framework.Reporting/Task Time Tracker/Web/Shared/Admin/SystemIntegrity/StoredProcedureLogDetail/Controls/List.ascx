<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.SystemIntegrity.StoredProcedureLogStoredProcedureLogDetail.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="StoredProcedureLogDetailId" SortExpression="StoredProcedureLogDetailId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="StoredProcedureLogDetails"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="StoredProcedureLogDetailId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogDetail/StoredProcedureLogDetails.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="StoredProcedureLogDetailId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogDetail/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="StoredProcedureLogDetailId"
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogDetail/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="StoredProcedureLogDetailId" 
                            DataNavigateUrlFormatString="~/SystemIntegrity/StoredProcedureLogDetail/Clone.aspx?SetId={0}" />
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