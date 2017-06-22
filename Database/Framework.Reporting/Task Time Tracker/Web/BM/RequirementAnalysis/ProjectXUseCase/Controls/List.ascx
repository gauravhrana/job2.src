<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectXUseCase.Controls.List" %>

<table   >
    <tr>
        <td>
            <asp:GridView ID="GridView1" AllowSorting="true"   AutoGenerateColumns="false" runat="server" 
                OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:BoundField DataField="ProjectXUseCaseId" SortExpression="ProjectXUseCaseId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>                    
                     <asp:BoundField DataField="ApplicationId" SortExpression="ApplicationId" HeaderText="ID" ItemStyle-HorizontalAlign="Center"/>

                    <asp:HyperLinkField HeaderText="UseCase"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="UseCase" 
                            SortExpression="UseCase"
                            DataNavigateUrlFields="ProjectXUseCaseId" 
                            DataNavigateUrlFormatString="~/RequirementAnalysis/ProjectXUseCase/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Project"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="Project" 
                            SortExpression="Project"
                            DataNavigateUrlFields="ProjectXUseCaseId" 
                            DataNavigateUrlFormatString="~/RequirementAnalysis/ProjectXUseCase/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="UseCaseProjectStatus"
                            ItemStyle-HorizontalAlign="Center"
                            DataTextField="UseCaseProjectStatus" 
                            SortExpression="UseCaseProjectStatus"
                            DataNavigateUrlFields="ProjectXUseCaseId" 
                            DataNavigateUrlFormatString="~/RequirementAnalysis/ProjectXUseCase/Details.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText ="Update" 
                            ItemStyle-HorizontalAlign="Center"
                            Text="Update" 
                            DataNavigateUrlFields="ProjectXUseCaseId" 
                            DataNavigateUrlFormatString="~/RequirementAnalysis/ProjectXUseCase/Update.aspx?SetId={0}" />

                    <asp:HyperLinkField HeaderText="Delete"
                            ItemStyle-HorizontalAlign="Center"
                            Text="Delete" 
                            DataNavigateUrlFields="ProjectXUseCaseId"
                            DataNavigateUrlFormatString="~/RequirementAnalysis/ProjectXUseCase/Delete.aspx?SetId={0}" />

                    <asp:HyperLinkField 
                            HeaderText="Clone" 
                            Text="Clone" 
                            ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFields="ProjectXUseCaseId" 
                            DataNavigateUrlFormatString="~/RequirementAnalysis/ProjectXUseCase/Clone.aspx?SetId={0}" />
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
