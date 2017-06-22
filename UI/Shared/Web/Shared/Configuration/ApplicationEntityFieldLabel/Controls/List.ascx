<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls.List" %>

<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting" Width="100%" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="ApplicationEntityFieldLabelId" 
                                SortExpression="ApplicationEntityFieldLabelId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />
                        
                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "ApplicationId"
                                DataTextField="ApplicationId" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabel/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "Name"
                                DataTextField="Name" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabel/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabel/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelId"
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabel/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabel/Clone.aspx?SetId={0}" />
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
