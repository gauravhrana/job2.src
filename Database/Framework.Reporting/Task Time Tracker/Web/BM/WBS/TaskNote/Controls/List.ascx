<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.TaskNote.Controls.List" %>
    
<table style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"  Width="100%" AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="TaskNoteId" SortExpression="TaskNoteId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="TaskNoteId" 
                            DataNavigateUrlFormatString="~/WBS/TaskNote/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Project"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Project" 
                            SortExpression="Project"
                            DataNavigateUrlFields="TaskNoteId" 
                            DataNavigateUrlFormatString="~/WBS/TaskNote/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Description"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Description" 
                            SortExpression="Description"
                            DataNavigateUrlFields="TaskNoteId" 
                            DataNavigateUrlFormatString="~/WBS/TaskNote/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="TaskNoteId" 
                            DataNavigateUrlFormatString="~/WBS/TaskNote/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="TaskNoteId"
                            DataNavigateUrlFormatString="~/WBS/TaskNote/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="TaskNoteId" 
                            DataNavigateUrlFormatString="~/WBS/TaskNote/Clone.aspx?SetId={0}" />
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