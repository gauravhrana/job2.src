<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="CompetencyXSkillId" SortExpression="CompetencyXSkillId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ApplicationId" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField HeaderText="Skill"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Skill" 
                            SortExpression="Skill"
                            DataNavigateUrlFields="CompetencyXSkillId" 
                            DataNavigateUrlFormatString="~/CompetencyXSkill/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Competency"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Competency" 
                            SortExpression="Competency"
                            DataNavigateUrlFields="CompetencyXSkillId" 
                            DataNavigateUrlFormatString="~/CompetencyXSkill/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="CompetencyXSkillId" 
                            DataNavigateUrlFormatString="~/CompetencyXSkill/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="CompetencyXSkillId"
                            DataNavigateUrlFormatString="~/CompetencyXSkill/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="CompetencyXSkillId" 
                            DataNavigateUrlFormatString="~/CompetencyXSkill/Clone.aspx?SetId={0}" />
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