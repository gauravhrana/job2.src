IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeInsert')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeInsert'
	DROP  Procedure  ActivitySubTypeInsert
END
GO

PRINT 'Creating Procedure ActivitySubTypeInsert'
GO
/******************************************************************************
**		File: 
**		Name: pActivitySubTypeSubTypeInsert
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
CREATE Procedure dbo.ActivitySubTypeInsert
(
		@ActivitySubTypeId		INT				=NULL   OUTPUT
	,	@ApplicationId			INT
	,   @ActivityTypeId		    INT
	,	@Name				    VARCHAR(50)
	,	@Description		    VARCHAR(500)	= NULL
	,	@SortOrder			    INT				= 1
	,   @AuditId			    INT			
    ,   @AuditDate		        DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'ActivitySubType'
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ActivitySubTypeId OUTPUT
			
	INSERT INTO dbo.ActivitySubType
	(
			ActivitySubTypeId
		,	ApplicationId
		,   ActivityTypeId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@ActivitySubTypeId
		,	@ApplicationId
		,	@ActivityTypeId
		,	@Name
		,	@Description
		,	@SortOrder
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivitySubTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
