IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'GetMaxId')
BEGIN
	PRINT 'Dropping Procedure GetMaxId'
	DROP  Procedure GetMaxId
END
GO

PRINT 'Creating Procedure GetMaxId'
GO


CREATE PROCEDURE GetMaxId
AS
BEGIN
SELECT MAX (ProjectId) AS MaxId, 'Project' as 'tableName' FROM dbo.Project
UNION ALL
SELECT MAX (QuestionId) AS MaxId, 'Question' as 'tableName' FROM dbo.Question
UNION ALL
SELECT MAX (ScheduleId) AS MaxId, 'Schedule' as 'tableName' FROM dbo.Schedule
UNION ALL
SELECT MAX (ScheduleItemId) AS MaxId, 'ScheduleItem' as 'tableName' FROM dbo.ScheduleItem
UNION ALL
SELECT MAX (ScheduleQuestionId) AS MaxId, 'ScheduleQuestion' as 'tableName' FROM dbo.ScheduleQuestion
UNION ALL
SELECT MAX (TaskFormulationId) AS MaxId, 'TaskFormulation' as 'tableName' FROM dbo.TaskFormulation
UNION ALL
SELECT MAX (LayerId) AS MaxId, 'Layer' as 'tableName' FROM dbo.Layer
UNION ALL
SELECT MAX (FeatureId) AS MaxId, 'Feature' as 'tableName' FROM dbo.Feature
UNION ALL
SELECT MAX (NeedId) AS MaxId, 'Need' as 'tableName' FROM dbo.Need
UNION ALL
SELECT MAX (TaskEntityTypeId) AS MaxId, 'TaskEntityType' as 'tableName' FROM dbo.TaskEntityType
UNION ALL
SELECT MAX (TaskScheduleTypeId) AS MaxId, 'TaskScheduleType' as 'tableName' FROM dbo.TaskScheduleType
UNION ALL
SELECT MAX (TaskEntityId) AS MaxId, 'TaskEntity' as 'tableName' FROM dbo.TaskEntity
UNION ALL
SELECT MAX (TaskScheduleId) AS MaxId, 'TaskSchedule' as 'tableName' FROM dbo.TaskSchedule
UNION ALL
SELECT MAX (TaskRunId) AS MaxId, 'TaskRun' as 'tableName' FROM dbo.TaskRun
UNION ALL
SELECT MAX (MilestoneId) AS MaxId, 'Milestone' as 'tableName' FROM dbo.Milestone
UNION ALL
SELECT MAX (ClientId) AS MaxId, 'Client' as 'tableName' FROM dbo.Client
UNION ALL
SELECT MAX (NeedXFeatureId) AS MaxId, 'NeedXFeature' as 'tableName' FROM dbo.NeedXFeature
UNION ALL
SELECT MAX (ActivityId) AS MaxId, 'Activity' as 'tableName' FROM dbo.Activity
UNION ALL
SELECT MAX (TaskTypeId) AS MaxId, 'TaskType' as 'tableName' FROM dbo.TaskType
UNION ALL
SELECT MAX (TaskPriorityTypeId) AS MaxId, 'TaskPriorityType' as 'tableName' FROM dbo.TaskPriorityType
UNION ALL
SELECT MAX (TaskPriorityXPersonId) AS MaxId, 'TaskPriorityXPerson' as 'tableName' FROM dbo.TaskPriorityXPerson
UNION ALL
SELECT MAX (RiskId) AS MaxId, 'Risk' as 'tableName' FROM dbo.Risk
UNION ALL
SELECT MAX (RewardId) AS MaxId, 'Reward' as 'tableName' FROM dbo.Reward
UNION ALL
SELECT MAX (ProjectId) AS MaxId, 'ProjectTimeLine' as 'tableName' FROM dbo.ProjectTimeLine
UNION ALL
SELECT MAX (TaskRiskRewardRankingXPersonId) AS MaxId, 'TaskRiskRewardRankingXPerson' as 'tableName' FROM dbo.TaskRiskRewardRankingXPerson
UNION ALL
SELECT MAX (ProjectXNeedId) AS MaxId, 'ProjectXNeed' as 'tableName' FROM dbo.ProjectXNeed
UNION ALL
SELECT MAX (TaskPackageId) AS MaxId, 'TaskPackage' as 'tableName' FROM dbo.TaskPackage
UNION ALL
SELECT MAX (ActivityAlgorithmId) AS MaxId, 'ActivityAlgorithm' as 'tableName' FROM dbo.ActivityAlgorithm
UNION ALL
SELECT MAX (SkillId) AS MaxId, 'Skill' as 'tableName' FROM dbo.Skill
UNION ALL
SELECT MAX (SkillLevelId) AS MaxId, 'SkillLevel' as 'tableName' FROM dbo.SkillLevel
UNION ALL
SELECT MAX (CompetencyId) AS MaxId, 'Competency' as 'tableName' FROM dbo.Competency
UNION ALL
SELECT MAX (SkillXPersonXSkillLevelId) AS MaxId, 'SkillXPersonXSkillLevel' as 'tableName' FROM dbo.SkillXPersonXSkillLevel
UNION ALL
SELECT MAX (CompetencyXSkillId) AS MaxId, 'CompetencyXSkill' as 'tableName' FROM dbo.CompetencyXSkill
UNION ALL
SELECT MAX (TaskXCompetencyId) AS MaxId, 'TaskXCompetency' as 'tableName' FROM dbo.TaskXCompetency
UNION ALL
SELECT MAX (ActivityAlgorithmItemId) AS MaxId, 'ActivityAlgorithmItem' as 'tableName' FROM dbo.ActivityAlgorithmItem
UNION ALL
SELECT MAX (ActivityStateId) AS MaxId, 'ActivityState' as 'tableName' FROM dbo.ActivityState
END