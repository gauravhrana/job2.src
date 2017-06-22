IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'VerifyApplicationIdValue')
BEGIN
	PRINT 'Dropping Procedure VerifyApplicationIdValue'
	DROP PROCEDURE VerifyApplicationIdValue
END
GO

PRINT 'Creating Procedure VerifyApplicationIdValue'
GO

/******************************************************************************
**		File: 
**		Name: VerifyApplicationIdValue
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/


Create Procedure dbo.VerifyApplicationIdValue
(
		@ApplicationOperationId					VARCHAR(50) = NULL
	,	@ApplicationRoleId						VARCHAR(50)	= NULL
	,	@ApplicationOperationXApplicationRoleId	INT		= NULL
	,	@ApplicationUserXApplicationRoleId		INT		= NULL
	,	@ApplicationUserId						INT		= NULL
	,	@ApplicationId							INT		= NULL
	,	@ApplicationId1							INT		= NULL
	,	@ApplicationId2							INT		= NULL
	
)
AS

CREATE TABLE #IDTrack
(
			ApplicationOperationXApplicationRoleId INT
)
-- Count number oif rows in Client
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.ApplicationOperationXApplicationRole
		
		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1
		
		DECLARE @tmpApplicationOperationXApplicationRoleId AS INT
		
		Select * into #tmp from dbo.ApplicationOperationXApplicationRole
		
		WHILE @iCNT <= @Count
		BEGIN
		
		SELECT @tmpApplicationOperationXApplicationRoleId = MAX(ApplicationOperationXApplicationRoleId)
		FROM #tmp a
		
	SELECT	
	   @ApplicationOperationXApplicationRoleId = ApplicationOperationXApplicationRoleId                    
	,  @ApplicationOperationId = ApplicationOperationId
	,  @ApplicationRoleId = ApplicationRoleId
	,  @ApplicationId = ApplicationId		
				
	FROM  ApplicationOperationXApplicationRole
	WHERE ApplicationOperationXApplicationRoleId = @tmpApplicationOperationXApplicationRoleId
	
	SELECT @ApplicationId1=ApplicationId
	FROM ApplicationOperation
	WHERE ApplicationOperationId = @ApplicationOperationId
	
	SELECT @ApplicationId2=ApplicationId
	FROM ApplicationRole
	WHERE ApplicationRoleId = @ApplicationRoleId
	
	IF @ApplicationId1 = @ApplicationId2
	BEGIN
	IF @ApplicationId = @ApplicationId1
	PRINT 'ApplicationIDs are equal'
	ELSE
	INSERT INTO #IDTrack (ApplicationOperationXApplicationRoleId) VALUES( @ApplicationOperationXApplicationRoleId)
	PRINT  @ApplicationOperationXApplicationRoleId
	END
	ELSE
	INSERT INTO #IDTrack (ApplicationOperationXApplicationRoleId) VALUES( @ApplicationOperationXApplicationRoleId)
	PRINT  @ApplicationOperationXApplicationRoleId
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp WHERE ApplicationOperationXApplicationRoleId = @ApplicationOperationXApplicationRoleId
	
	END
	
	DROP TABLE #tmp
	
	Select * from #IDTrack
	
	
	CREATE TABLE #IDTrack2
(
			ApplicationUserXApplicationRoleId INT
)

		SELECT @Count = COUNT(*) FROM dbo.ApplicationUserXApplicationRole
		
		--Initialize loop iterator
		SET @iCNT = 1
		
		DECLARE @tmpApplicationUserXApplicationRoleId AS INT
		
		Select * into #tmp2 from dbo.ApplicationUserXApplicationRole
		
		WHILE @iCNT <= @Count
		BEGIN
		
		SELECT @tmpApplicationUserXApplicationRoleId = MAX(ApplicationUserXApplicationRoleId)
		FROM #tmp2 a
		
	SELECT	
	   @ApplicationUserXApplicationRoleId = ApplicationUserXApplicationRoleId                    
	,  @ApplicationUserId = ApplicationUserId
	,  @ApplicationRoleId = ApplicationRoleId
	,  @ApplicationId = ApplicationId		
				
	FROM  ApplicationUserXApplicationRole
	WHERE ApplicationUserXApplicationRoleId = @tmpApplicationUserXApplicationRoleId
	
	SELECT @ApplicationId1=ApplicationId
	FROM ApplicationUser
	WHERE ApplicationUserId = @ApplicationUserId
	
	SELECT @ApplicationId2=ApplicationId
	FROM ApplicationRole
	WHERE ApplicationRoleId = @ApplicationRoleId
	
	IF @ApplicationId1 = @ApplicationId2
	BEGIN
	IF @ApplicationId = @ApplicationId1
	PRINT 'ApplicationIDs are equal'
	ELSE
	INSERT INTO #IDTrack2 (ApplicationUserXApplicationRoleId) VALUES( @ApplicationUserXApplicationRoleId)
	PRINT  @ApplicationUserXApplicationRoleId
	END
	ELSE
	INSERT INTO #IDTrack2 (ApplicationUserXApplicationRoleId) VALUES( @ApplicationUserXApplicationRoleId)
	PRINT  @ApplicationUserXApplicationRoleId
	
	SET @iCNT = @iCNT + 1
	
	DELETE FROM #tmp2 WHERE ApplicationUserXApplicationRoleId = @ApplicationUserXApplicationRoleId
	
	END
	
	Select * from #IDTrack2
	
	
	
	
		

