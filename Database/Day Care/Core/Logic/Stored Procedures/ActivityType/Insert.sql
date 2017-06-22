IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeInsert')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeInsert'
	DROP  Procedure  ActivityTypeInsert
END
GO

PRINT 'Creating Procedure ActivityTypeInsert'
GO

/******************************************************************************
**		File: 
**		Name: ActivityTypeInsert
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
CREATE Procedure dbo.ActivityTypeInsert
(
		@ActivityTypeId		INT			    = NULL   OUTPUT 
	,	@ApplicationId		INT
	,	@Name				VARCHAR(50)
	,	@Description	    VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'ActivityType'
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ActivityTypeId OUTPUT
	

	INSERT INTO dbo.ActivityType
	(
			ActivityTypeId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@ActivityTypeId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityTypeId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO