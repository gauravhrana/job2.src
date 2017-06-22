<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel.Controls.List" %>
    
<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="SkillXPersonXSkillLevelId" SortExpression="SkillXPersonXSkillLevelId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="Skill"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Skill" 
                            SortExpression="Skill"
                            DataNavigateUrlFields="SkillXPersonXSkillLevelId" 
                            DataNavigateUrlFormatString="~/Aptitude/SkillXPersonXSkillLevel/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="SkillLevel"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="SkillLevel" 
                            SortExpression="SkillLevel"
                            DataNavigateUrlFields="SkillXPersonXSkillLevelId" 
                            DataNavigateUrlFormatString="~/Aptitude/SkillXPersonXSkillLevel/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Person"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Person" 
                            SortExpression="Person"
                            DataNavigateUrlFields="SkillXPersonXSkillLevelId" 
                            DataNavigateUrlFormatString="~/Aptitude/SkillXPersonXSkillLevel/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="SkillXPersonXSkillLevelId" 
                            DataNavigateUrlFormatString="~/Aptitude/SkillXPersonXSkillLevel/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="SkillXPersonXSkillLevelId"
                            DataNavigateUrlFormatString="~/Aptitude/SkillXPersonXSkillLevel/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="SkillXPersonXSkillLevelId" 
                            DataNavigateUrlFormatString="~/Aptitude/SkillXPersonXSkillLevel/Clone.aspx?SetId={0}" />
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