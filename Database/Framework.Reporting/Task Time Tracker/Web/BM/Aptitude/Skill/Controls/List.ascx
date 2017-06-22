<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.Aptitude.Skill.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="SkillId" SortExpression="SkillId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Name"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Name" 
                            SortExpression="Name"
                            DataNavigateUrlFields="SkillId" 
                            DataNavigateUrlFormatString="~/Aptitude/Skill/Details.aspx?SetId={0}" />
                    
                    <asp:HyperLinkField HeaderText="ApplicationId"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="ApplicationId" 
                            SortExpression="ApplicationId"
                            DataNavigateUrlFields="SkillId" 
                            DataNavigateUrlFormatString="~/Aptitude/Skill/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="SkillId" 
                            DataNavigateUrlFormatString="~/Aptitude/Skill/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="SkillId"
                            DataNavigateUrlFormatString="~/Aptitude/Skill/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="SkillId" 
                            DataNavigateUrlFormatString="~/Aptitude/Skill/Clone.aspx?SetId={0}" />
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