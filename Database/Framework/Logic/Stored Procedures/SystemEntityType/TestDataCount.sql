-- =============================================
-- Script Template
-- ===========================================IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetTestDataCount')
BEGIN
	PRINT 'Dropping Procedure GetTestDataCount'
	DROP  Procedure GetTestDataCount
END
GO

PRINT 'Creating Procedure GetTestDataCount'
GO

/*********************************************************************************************
**		File: 
**		Name:GetTestDataCount
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.GetTestDataCount

AS
BEGIN
DECLARE @EntityId AS INT = 0
DECLARE @Count AS INT = 0
CREATE TABLE #TestAndAuditDataTrack
(
Entity VARCHAR(100),
EntityId	INT,
Description	VARCHAR(500)
)
		SELECT @Count = COUNT(*) FROM dbo.Client
		
		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1
		
		DECLARE @tmpEntityId AS INT
		
		Select * into #tmp from dbo.Client
		
		WHILE @iCNT <= @Count
		BEGIN
		
		SELECT @tmpEntityId = MAX(ClientId)
		FROM #tmp a
		
	SELECT	
	   @EntityId = ClientId     		
				
	FROM  Client
	WHERE ClientId = @tmpEntityId
	
	DECLARE @SystemEntityTypeId AS INT
	DECLARE @NoOfRecords AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Client')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ClientId
	
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ClientID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Client', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ClientID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Client', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp WHERE ClientId = @EntityId
	
	END
	
	DROP TABLE #tmp
	
	--Feature
	
	SELECT @Count = COUNT(*) FROM dbo.Feature
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp2 from dbo.Feature
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(FeatureId)
	FROM #tmp2 a
		
	SELECT	@EntityId = FeatureId     					
	FROM  Feature
	WHERE FeatureId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Feature')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS FeatureId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'FeatureID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Feature', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'FeatureID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Feature', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp2 WHERE FeatureId = @EntityId
	
	END
	
	DROP TABLE #tmp2
	
	
	SELECT @Count = COUNT(*) FROM dbo.Activity
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp3 from dbo.Activity
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ActivityId)
	FROM #tmp3 a
		
	SELECT	@EntityId = ActivityId     					
	FROM  Activity
	WHERE ActivityId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Activity')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ActivityId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ActivityID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Activity', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ActivityID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Activity', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp3 WHERE ActivityId = @EntityId
	
	END
	
	DROP TABLE #tmp3
	
	SELECT @Count = COUNT(*) FROM dbo.ActivityAlgorithm
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp4 from dbo.ActivityAlgorithm
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ActivityAlgorithmId)
	FROM #tmp4 a
		
	SELECT	@EntityId = ActivityAlgorithmId     					
	FROM  ActivityAlgorithm
	WHERE ActivityAlgorithmId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ActivityAlgorithm')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ActivityAlgorithmId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ActivityAlgorithmID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ActivityAlgorithm', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ActivityAlgorithmID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ActivityAlgorithm', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp4 WHERE ActivityAlgorithmId = @EntityId
	
	END
	
	DROP TABLE #tmp4
	
	SELECT @Count = COUNT(*) FROM dbo.ActivityAlgorithmItem
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp5 from dbo.ActivityAlgorithmItem
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ActivityAlgorithmItemId)
	FROM #tmp5 a
		
	SELECT	@EntityId = ActivityAlgorithmItemId     					
	FROM  ActivityAlgorithmItem
	WHERE ActivityAlgorithmItemId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ActivityAlgorithmItem')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ActivityAlgorithmItemId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ActivityAlgorithmItemID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ActivityAlgorithmItem', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ActivityAlgorithmItemID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ActivityAlgorithmItem', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp5 WHERE ActivityAlgorithmItemId = @EntityId
	
	END
	
	DROP TABLE #tmp5
	
	SELECT @Count = COUNT(*) FROM dbo.ActivityState
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp6 from dbo.ActivityState
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ActivityStateId)
	FROM #tmp6 a
		
	SELECT	@EntityId = ActivityStateId     					
	FROM  ActivityState
	WHERE ActivityStateId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ActivityState')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ActivityStateId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ActivityStateID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ActivityState', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ActivityStateID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ActivityState', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp6 WHERE ActivityStateId = @EntityId
	
	END
	
	DROP TABLE #tmp6
	
	SELECT @Count = COUNT(*) FROM dbo.Competency
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp7 from dbo.Competency
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(CompetencyId)
	FROM #tmp7 a
		
	SELECT	@EntityId = CompetencyId     					
	FROM  Competency
	WHERE CompetencyId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Competency')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS CompetencyId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'CompetencyID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Competency', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'CompetencyID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Competency', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp7 WHERE CompetencyId = @EntityId
	
	END
	
	DROP TABLE #tmp7
	
	SELECT @Count = COUNT(*) FROM dbo.CompetencyXSkill
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp8 from dbo.CompetencyXSkill
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(CompetencyXSkillId)
	FROM #tmp8 a
		
	SELECT	@EntityId = CompetencyXSkillId     					
	FROM  CompetencyXSkill
	WHERE CompetencyXSkillId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('CompetencyXSkill')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS CompetencyXSkillId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'CompetencyXSkillID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'CompetencyXSkill', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'CompetencyXSkillID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'CompetencyXSkill', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp8 WHERE CompetencyXSkillId = @EntityId
	
	END
	
	DROP TABLE #tmp8
	
	SELECT @Count = COUNT(*) FROM dbo.Layer
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp9 from dbo.Layer
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(LayerId)
	FROM #tmp9 a
		
	SELECT	@EntityId = LayerId     					
	FROM  Layer
	WHERE LayerId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Layer')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS LayerId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'LayerID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Layer', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'LayerID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Layer', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp9 WHERE LayerId = @EntityId
	
	END
	
	DROP TABLE #tmp9
	
	SELECT @Count = COUNT(*) FROM dbo.Milestone
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp10 from dbo.Milestone
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(MilestoneId)
	FROM #tmp10 a
		
	SELECT	@EntityId = MilestoneId     					
	FROM  Milestone
	WHERE MilestoneId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Milestone')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS MilestoneId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'MilestoneID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Milestone', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'MilestoneID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Milestone', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp10 WHERE MilestoneId = @EntityId
	
	END
	
	DROP TABLE #tmp10
	
	SELECT @Count = COUNT(*) FROM dbo.Need
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp11 from dbo.Need
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(NeedId)
	FROM #tmp11 a
		
	SELECT	@EntityId = NeedId     					
	FROM  Need
	WHERE NeedId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Need')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS NeedId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'NeedID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Need', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'NeedID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Need', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp11 WHERE NeedId = @EntityId
	
	END
	
	DROP TABLE #tmp11
	
	SELECT @Count = COUNT(*) FROM dbo.NeedXFeature
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp12 from dbo.NeedXFeature
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(NeedXFeatureId)
	FROM #tmp12 a
		
	SELECT	@EntityId = NeedXFeatureId     					
	FROM  NeedXFeature
	WHERE NeedXFeatureId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('NeedXFeature')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS NeedXFeatureId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'NeedXFeatureID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'NeedXFeature', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'NeedXFeatureID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'NeedXFeature', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp12 WHERE NeedXFeatureId = @EntityId
	
	END
	
	DROP TABLE #tmp12
	
	SELECT @Count = COUNT(*) FROM dbo.Project
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp13 from dbo.Project
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ProjectId)
	FROM #tmp13 a
		
	SELECT	@EntityId = ProjectId     					
	FROM  Project
	WHERE ProjectId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Project')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ProjectId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ProjectID negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Project', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ProjectID has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Project', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp13 WHERE ProjectId = @EntityId
	
	END
	
	DROP TABLE #tmp13
	
	
	SELECT @Count = COUNT(*) FROM dbo.Project
		
	--Initialize loop iterator
	SET @iCNT = 1
		
	Select * into #tmp14 from dbo.Project
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ProjectId)
	FROM #tmp14 a
		
	SELECT	@EntityId = ProjectId     					
	FROM  Project
	WHERE ProjectId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ProjectTimeLine')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ProjectId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ProjectId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ProjectTimeLine', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ProjectId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ProjectTimeLine', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp14 WHERE ProjectId = @EntityId
	
	END
	
	DROP TABLE #tmp14
	
	SELECT @Count = COUNT(*) FROM dbo.ProjectXNeed
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp15 from dbo.ProjectXNeed
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ProjectXNeedId)
	FROM #tmp15 a
		
	SELECT	@EntityId = ProjectXNeedId     					
	FROM  ProjectXNeed
	WHERE ProjectXNeedId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ProjectTimeLine')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ProjectXNeedId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ProjectXNeedId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ProjectTimeLine', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ProjectXNeedId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ProjectTimeLine', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp15 WHERE ProjectXNeedId = @EntityId
	
	END
	
	DROP TABLE #tmp15
	
	SELECT @Count = COUNT(*) FROM dbo.Question
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp16 from dbo.Question
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(QuestionId)
	FROM #tmp16 a
		
	SELECT	@EntityId = QuestionId     					
	FROM  Question
	WHERE QuestionId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Question')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS QuestionId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'QuestionId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Question', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'QuestionId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Question', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp16 WHERE QuestionId = @EntityId
	
	END
	
	DROP TABLE #tmp16
	
	SELECT @Count = COUNT(*) FROM dbo.Reward
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp17 from dbo.Reward
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(RewardId)
	FROM #tmp17 a
		
	SELECT	@EntityId = RewardId     					
	FROM  Reward
	WHERE RewardId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Reward')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS RewardId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'RewardId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Reward', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'RewardId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Reward', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp17 WHERE RewardId = @EntityId
	
	END
	
	DROP TABLE #tmp17
	
	SELECT @Count = COUNT(*) FROM dbo.Risk
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp18 from dbo.Risk
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(RiskId)
	FROM #tmp18 a
		
	SELECT	@EntityId = RiskId     					
	FROM  Risk
	WHERE RiskId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Risk')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS RiskId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'RiskId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Risk', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'RiskId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Risk', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp18 WHERE RiskId = @EntityId
	
	END
	
	DROP TABLE #tmp18
	
	SELECT @Count = COUNT(*) FROM dbo.Schedule
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp19 from dbo.Schedule
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ScheduleId)
	FROM #tmp19 a
		
	SELECT	@EntityId = ScheduleId     					
	FROM  Schedule
	WHERE ScheduleId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Schedule')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ScheduleId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ScheduleId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Schedule', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ScheduleId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Schedule', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp19 WHERE ScheduleId = @EntityId
	
	END
	
	DROP TABLE #tmp19
	
	SELECT @Count = COUNT(*) FROM dbo.ScheduleItem
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp20 from dbo.ScheduleItem
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ScheduleItemId)
	FROM #tmp20 a
		
	SELECT	@EntityId = ScheduleItemId     					
	FROM  ScheduleItem
	WHERE ScheduleItemId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ScheduleItem')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ScheduleItemId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ScheduleItemId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ScheduleItem', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ScheduleItemId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ScheduleItem', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp20 WHERE ScheduleItemId = @EntityId
	
	END
	
	DROP TABLE #tmp20
	
	SELECT @Count = COUNT(*) FROM dbo.ScheduleQuestion
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp21 from dbo.ScheduleQuestion
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(ScheduleQuestionId)
	FROM #tmp21 a
		
	SELECT	@EntityId = ScheduleQuestionId     					
	FROM  ScheduleQuestion
	WHERE ScheduleQuestionId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('ScheduleQuestion')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS ScheduleQuestionId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'ScheduleQuestionId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ScheduleQuestion', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'ScheduleQuestionId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'ScheduleQuestion', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp21 WHERE ScheduleQuestionId = @EntityId
	
	END
	
	DROP TABLE #tmp21
	
	SELECT @Count = COUNT(*) FROM dbo.Skill
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp22 from dbo.Skill
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(SkillId)
	FROM #tmp22 a
		
	SELECT	@EntityId = SkillId     					
	FROM  Skill
	WHERE SkillId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Skill')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS SkillId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'SkillId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Skill', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'SkillId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Skill', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp22 WHERE SkillId = @EntityId
	
	END
	
	DROP TABLE #tmp22
	
	SELECT @Count = COUNT(*) FROM dbo.SkillLevel
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp23 from dbo.SkillLevel
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(SkillLevelId)
	FROM #tmp23 a
		
	SELECT	@EntityId = SkillLevelId     					
	FROM  SkillLevel
	WHERE SkillLevelId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('SkillLevel')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS SkillLevelId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'SkillLevelId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'SkillLevel', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'SkillLevelId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'SkillLevel', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp23 WHERE SkillLevelId = @EntityId
	
	END
	
	DROP TABLE #tmp23
	
	SELECT @Count = COUNT(*) FROM dbo.SkillXPersonXSkillLevel
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp24 from dbo.SkillXPersonXSkillLevel
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(SkillXPersonXSkillLevelId)
	FROM #tmp24 a
		
	SELECT	@EntityId = SkillXPersonXSkillLevelId     					
	FROM  SkillXPersonXSkillLevel
	WHERE SkillXPersonXSkillLevelId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('SkillXPersonXSkillLevel')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS SkillXPersonXSkillLevelId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'SkillXPersonXSkillLevelId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'SkillXPersonXSkillLevel', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'SkillXPersonXSkillLevelId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'SkillXPersonXSkillLevel', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp24 WHERE SkillXPersonXSkillLevelId = @EntityId
	
	END
	
	DROP TABLE #tmp24
	
	SELECT @Count = COUNT(*) FROM dbo.Task
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp25 from dbo.Task
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskId)
	FROM #tmp25 a
		
	SELECT	@EntityId = TaskId     					
	FROM  Task
	WHERE TaskId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('Task')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Task', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'Task', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp25 WHERE TaskId = @EntityId
	
	END
	
	DROP TABLE #tmp25
	
	SELECT @Count = COUNT(*) FROM dbo.TaskEntity
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp26 from dbo.TaskEntity
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskEntityId)
	FROM #tmp26 a
		
	SELECT	@EntityId = TaskEntityId     					
	FROM  TaskEntity
	WHERE TaskEntityId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskEntity')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskEntityId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskEntityId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskEntity', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskEntityId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskEntity', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp26 WHERE TaskEntityId = @EntityId
	
	END
	
	DROP TABLE #tmp26
	
	SELECT @Count = COUNT(*) FROM dbo.TaskEntityType
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp27 from dbo.TaskEntityType
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskEntityTypeId)
	FROM #tmp27 a
		
	SELECT	@EntityId = TaskEntityTypeId     					
	FROM  TaskEntityType
	WHERE TaskEntityTypeId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskEntityType')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskEntityTypeId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskEntityTypeId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskEntityType', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskEntityTypeId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskEntityType', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp27 WHERE TaskEntityTypeId = @EntityId
	
	END
	
	DROP TABLE #tmp27
	
	SELECT @Count = COUNT(*) FROM dbo.TaskPackage
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp28 from dbo.TaskPackage
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskPackageId)
	FROM #tmp28 a
		
	SELECT	@EntityId = TaskPackageId     					
	FROM  TaskPackage
	WHERE TaskPackageId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskPackage')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskPackageId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskPackageId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskPackage', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskPackageId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskPackage', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp28 WHERE TaskPackageId = @EntityId
	
	END
	
	DROP TABLE #tmp28
	
	SELECT @Count = COUNT(*) FROM dbo.TaskPriorityType
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp29 from dbo.TaskPriorityType
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskPriorityTypeId)
	FROM #tmp29 a
		
	SELECT	@EntityId = TaskPriorityTypeId     					
	FROM  TaskPriorityType
	WHERE TaskPriorityTypeId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskPriorityType')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskPriorityTypeId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskPriorityTypeId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskPriorityType', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskPriorityTypeId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskPriorityType', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp29 WHERE TaskPriorityTypeId = @EntityId
	
	END
	
	DROP TABLE #tmp29
	
	SELECT @Count = COUNT(*) FROM dbo.TaskPriorityXPerson
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp30 from dbo.TaskPriorityXPerson
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskPriorityXPersonId)
	FROM #tmp30 a
		
	SELECT	@EntityId = TaskPriorityXPersonId     					
	FROM  TaskPriorityXPerson
	WHERE TaskPriorityXPersonId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskPriorityXPerson')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskPriorityXPersonId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskPriorityXPersonId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskPriorityXPerson', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskPriorityXPersonId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskPriorityXPerson', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp30 WHERE TaskPriorityXPersonId = @EntityId
	
	END
	
	DROP TABLE #tmp30
	
	SELECT @Count = COUNT(*) FROM dbo.TaskRiskRewardRankingXPerson
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp31 from dbo.TaskRiskRewardRankingXPerson
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskRiskRewardRankingXPersonId)
	FROM #tmp31 a
		
	SELECT	@EntityId = TaskRiskRewardRankingXPersonId     					
	FROM  TaskRiskRewardRankingXPerson
	WHERE TaskRiskRewardRankingXPersonId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskRiskRewardRankingXPerson')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskRiskRewardRankingXPersonId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskRiskRewardRankingXPersonId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskRiskRewardRankingXPerson', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskRiskRewardRankingXPersonId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskRiskRewardRankingXPerson', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp31 WHERE TaskRiskRewardRankingXPersonId = @EntityId
	
	END
	
	DROP TABLE #tmp31
	
	SELECT @Count = COUNT(*) FROM dbo.TaskRun
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp32 from dbo.TaskRun
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskRunId)
	FROM #tmp32 a
		
	SELECT	@EntityId = TaskRunId     					
	FROM  TaskRun
	WHERE TaskRunId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskRun')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskRunId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskRunId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskRun', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskRunId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskRun', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp32 WHERE TaskRunId = @EntityId
	
	END
	
	DROP TABLE #tmp32
	
	SELECT @Count = COUNT(*) FROM dbo.TaskSchedule
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp33 from dbo.TaskSchedule
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskScheduleId)
	FROM #tmp33 a
		
	SELECT	@EntityId = TaskScheduleId     					
	FROM  TaskSchedule
	WHERE TaskScheduleId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskSchedule')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskScheduleId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskScheduleId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskSchedule', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskScheduleId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskSchedule', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp33 WHERE TaskScheduleId = @EntityId
	
	END
	
	DROP TABLE #tmp33
	
	SELECT @Count = COUNT(*) FROM dbo.TaskScheduleType
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp34 from dbo.TaskScheduleType
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskScheduleTypeId)
	FROM #tmp34 a
		
	SELECT	@EntityId = TaskScheduleTypeId     					
	FROM  TaskScheduleType
	WHERE TaskScheduleTypeId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskScheduleType')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskScheduleTypeId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskScheduleTypeId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskScheduleType', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskScheduleTypeId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskScheduleType', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp34 WHERE TaskScheduleTypeId = @EntityId
	
	END
	
	DROP TABLE #tmp34
	
	SELECT @Count = COUNT(*) FROM dbo.TaskType
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp35 from dbo.TaskType
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskTypeId)
	FROM #tmp35 a
		
	SELECT	@EntityId = TaskTypeId     					
	FROM  TaskType
	WHERE TaskTypeId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskType')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskTypeId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskTypeId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskType', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskTypeId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskType', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp35 WHERE TaskTypeId = @EntityId
	
	END
	
	DROP TABLE #tmp35
	
	SELECT @Count = COUNT(*) FROM dbo.TaskXCompetency
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp36 from dbo.TaskXCompetency
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskXCompetencyId)
	FROM #tmp36 a
		
	SELECT	@EntityId = TaskXCompetencyId     					
	FROM  TaskXCompetency
	WHERE TaskXCompetencyId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskXCompetency')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskXCompetencyId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskXCompetencyId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskXCompetency', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskXCompetencyId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskXCompetency', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp36 WHERE TaskXCompetencyId = @EntityId
	
	END
	
	DROP TABLE #tmp36
	
	SELECT @Count = COUNT(*) FROM dbo.TaskXPerson
		
	--Initialize loop iterator
	SET @iCNT = 1
	
	Select * into #tmp37 from dbo.TaskXPerson
		
	WHILE @iCNT <= @Count
	BEGIN
		
	SELECT @tmpEntityId = MAX(TaskXPersonId)
	FROM #tmp37 a
		
	SELECT	@EntityId = TaskXPersonId     					
	FROM  TaskXPerson
	WHERE TaskXPersonId = @tmpEntityId
	
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId('TaskXPerson')
    EXEC CommonServices.dbo.AuditHistoryFindNoOfRecords @EntityKey=@tmpEntityId, @SystemEntityId=@SystemEntityTypeId , @NoOfRecords=@NoOfRecords OUT
	--SELECT @NoOfRecords AS NoOfRecords, @EntityId AS TaskXPersonId
	
	IF @EntityId < 0
	BEGIN
	PRINT 'TaskXPersonId negative'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskXPerson', @EntityId, 'Test data')
	END
	
	IF @NoOfRecords > 0
	BEGIN
	PRINT 'TaskXPersonId has Auditdata'
	INSERT INTO #TestAndAuditDataTrack (Entity, EntityId, Description) VALUES( 'TaskXPerson', @EntityId, 'Has associated Audit History data')
	PRINT  @EntityId
	END
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp37 WHERE TaskXPersonId = @EntityId
	
	END
	
	DROP TABLE #tmp37
	
	IF OBJECT_ID ('tempdb..#TestAndAuditDataTrack') IS NOT NULL
	BEGIN
	Select * from #TestAndAuditDataTrack
	END
	
	DROP TABLE #TestAndAuditDataTrack
END	
GO

 