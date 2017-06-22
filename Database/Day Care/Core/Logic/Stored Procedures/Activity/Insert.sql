IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityInsert')
BEGIN
	PRINT 'Dropping Procedure ActivityInsert'
	DROP  Procedure  ActivityInsert
END
GO

PRINT 'Creating Procedure ActivityInsert'
GO

/******************************************************************************
**		File: 
**		Name: pActivityInsert
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
CREATE Procedure dbo.ActivityInsert
(  
		@StudentID			INT
	,	@ApplicationId		INT
	,	@ActivityID			INT
	,	@ActivityTypeID		INT
	,	@ActivitySubTypeId  INT	
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Activity'
)
AS
BEGIN	
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ActivityId OUTPUT

	INSERT INTO dbo.Activity
	(
			 ActivityId
		,	 ApplicationId
		,	 StudentId
		,	 ActivityTypeId
		,	 ActivitySubTypeId
	)
	VALUES
	(
			@ActivityId
		,	@ApplicationId
		,	@StudentId
		,	@ActivityTypeId
		,	@ActivitySubTypeId
	)
	
	--Create Audit Record
	  EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
