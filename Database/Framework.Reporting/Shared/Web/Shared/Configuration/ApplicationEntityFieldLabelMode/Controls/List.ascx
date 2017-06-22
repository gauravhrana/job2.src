<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode.Controls.List" %>

<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="True" OnSorting="GridView1_Sorting" Width="100%" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="ApplicationEntityFieldLabelModeId" 
                                SortExpression="ApplicationEntityFieldLabelModeId" 
                                HeaderText="ID" 
                                ItemStyle-HorizontalAlign="Center"  />
                        
                        <asp:HyperLinkField HeaderText="ApplicationId"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "ApplicationId"
                                DataTextField="ApplicationId" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelModeId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabelMode/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Name"
                                ItemStyle-HorizontalAlign="Center" 
                                SortExpression = "Name"
                                DataTextField="Name" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelModeId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabelMode/Details.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText ="Update" 
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Update" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelModeId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabelMode/Update.aspx?SetId={0}" />

                        <asp:HyperLinkField HeaderText="Delete"
                                ItemStyle-HorizontalAlign="Center" 
                                Text="Delete" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelModeId"
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabelMode/Delete.aspx?SetId={0}" />

                        <asp:HyperLinkField 
                                ItemStyle-HorizontalAlign="Center" 
                                HeaderText="Clone" 
                                Text="Clone" 
                                DataNavigateUrlFields="ApplicationEntityFieldLabelModeId" 
                                DataNavigateUrlFormatString="~/Shared/Configuration/ApplicationEntityFieldLabelMode/Clone.aspx?SetId={0}" />
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