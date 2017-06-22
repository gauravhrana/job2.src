<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAndAuditDetails.aspx.cs"
    MasterPageFile="~/MasterPages/Site.master" Inherits="Shared.UI.Web.Admin.TestAndAuditDetails" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    <b>Test and Audit Details Page:</b>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table >
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table  cellpadding="2" cellspacing="4" border="0">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlEntity" runat="server" OnSelectedIndexChanged="ddlEntity_SelectedIndexChanged"
                                Width="200px">
                                <asp:ListItem Text="Select Entity" Value="Select Entity" Selected="true" />
                                <asp:ListItem Text="Activity" Value="Activity" />
                                <asp:ListItem Text="ActivityAlgorithm" Value="ActivityAlgorithm" />
                                <asp:ListItem Text="ActivityAlgorithmItem  " Value="Activity" />
                                <asp:ListItem Text="ActivityState" Value="ActivityState" />
                                <asp:ListItem Text="Client" Value="Client" />
                                <asp:ListItem Text="Competency" Value="Competency" />
                                <asp:ListItem Text="CompetencyXSkill" Value="CompetencyXSkill" />
                                <asp:ListItem Text="Feature" Value="Feature" />
                                <asp:ListItem Text="Layer" Value="Layer" />
                                <asp:ListItem Text="Milestone" Value="Milestone" />
                                <asp:ListItem Text="Need" Value="Need" />
                                <asp:ListItem Text="NeedXFeature" Value="NeedXFeature" />
                                <asp:ListItem Text="Project" Value="Project" />
                                <asp:ListItem Text="ProjectTimeLine" Value="ProjectTimeLine" />
                                <asp:ListItem Text="ProjectXNeed" Value="ProjectXNeed" />
                                <asp:ListItem Text="Question" Value="Question" />
                                <asp:ListItem Text="Reward" Value="Reward" />
                                <asp:ListItem Text="Risk" Value="Risk" />
                                <asp:ListItem Text="Schedule" Value="Schedule" />
                                <asp:ListItem Text="ScheduleItem" Value="ScheduleItem" />
                                <asp:ListItem Text="ScheduleQuestion" Value="ScheduleQuestion" />
                                <asp:ListItem Text="Skill" Value="Skill" />
                                <asp:ListItem Text="SkillLevel" Value="SkillLevel" />
                                <asp:ListItem Text="SkillXPersonXSkillLevel" Value="SkillXPersonXSkillLevel" />
                                <asp:ListItem Text="Task" Value="Task" />
                                <asp:ListItem Text="TaskEntity" Value="TaskEntity" />
                                <asp:ListItem Text="TaskEntityType" Value="TaskEntityType" />
                                <asp:ListItem Text="TaskPackage" Value="TaskPackage" />
                                <asp:ListItem Text="TaskPriorityType" Value="TaskPriorityType" />
                                <asp:ListItem Text="TaskPriorityXApplicationUser" Value="TaskPriorityXApplicationUser" />
                                <asp:ListItem Text="TaskRun" Value="TaskRun" />
                                <asp:ListItem Text="TaskSchedule" Value="TaskSchedule" />
                                <asp:ListItem Text="TaskScheduleType" Value="TaskScheduleType" />
                                <asp:ListItem Text="TaskType" Value="TaskType" />
                                <asp:ListItem Text="TaskXCompetency" Value="TaskXCompetency" />
                                <asp:ListItem Text="TaskXApplicationUser" Value="TaskXApplicationUser" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="TestAndAuditGrid" AllowPaging="true" PageSize="20" Width="1000px"
                                runat="server" OnPageIndexChanging="GridView_PageIndexChanging">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
